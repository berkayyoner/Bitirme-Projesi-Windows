using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitirmeProjesi
{
    public partial class NavBar : Form
    {
        string kullaniciAdi = "";
        AnaSayfa ana;
        public NavBar(string KullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
        }

        private void NavBar_Load(object sender, EventArgs e)
        {
            #region Sol Üst Köşeye Konumlandırma
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Size = new Size(this.Size.Width, this.MdiParent.Size.Height - 45);
            #endregion
            ana = new AnaSayfa(this.Location.Y, kullaniciAdi, 0);
            ana.MdiParent = this.MdiParent;
            ana.Show();
            timer1.Enabled = true;
        }

        private void btnKitapligim_Click(object sender, EventArgs e)
        {
            Gitapligim git = new Gitapligim(ana.Location.Y, kullaniciAdi);
            git.MdiParent = this.MdiParent;
            git.Show();
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, this.MdiParent.Size.Height - 45);
            this.Location = new Point(0, 0);
            btnCikis.Location = new Point(btnCikis.Location.X, this.Size.Height - 40);
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            AnaSayfa ana2 = new AnaSayfa(ana.Location.Y, kullaniciAdi, 1);
            ana2.MdiParent = this.MdiParent;
            ana2.Show();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Gitaplarım git = new Gitaplarım(ana.Location.Y, kullaniciAdi);
            git.MdiParent = this.MdiParent;
            git.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ara a = new Ara(ana.Location.Y, kullaniciAdi);
            a.MdiParent = this.MdiParent;
            a.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            IlkEkran ie = new IlkEkran();
            ie.MdiParent = this.MdiParent;
            ana.Close();
            this.Close();
            ie.Show();
        }
    }
}
