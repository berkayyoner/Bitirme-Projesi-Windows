using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitirmeProjesi.Formlar.Kullanıcı_Formları
{
	public partial class AktifSurem : Form
	{
		string kullaniciAdi = "";
		int formYKonumu = 0;
		public AktifSurem(int FormYKonumu, string KullaniciAdi)
		{
			InitializeComponent();
			this.kullaniciAdi = KullaniciAdi;
			this.formYKonumu = FormYKonumu;
		}

		private void AktifSurem_Load(object sender, EventArgs e)
		{
			#region NavBar'a Yanaştırma
			NavBar navBar = new NavBar(kullaniciAdi);
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.Location = new Point(navBar.Size.Width, formYKonumu);
			this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
			#endregion

			timer1.Enabled = true;
			GenelIslemler gi = new GenelIslemler();
			lblHosgeldin.Text = "Hoşgeldin, " + gi.AdiNe(kullaniciAdi);
			lblHosgeldin.Location = new Point((this.Size.Width/2)-(lblHosgeldin.Size.Width/2), lblHosgeldin.Location.Y);
		}

		private void btnGeri_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			#region Otomatik Boyutlandırma
			NavBar navBar = new NavBar(kullaniciAdi);
			this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
			#endregion
		}
	}
}
