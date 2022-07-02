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
    public partial class Cuzdan : Form
    {
        string kullaniciAdi = "";
        int formYKonumu = 0;
        public Cuzdan(int FormYKonumu, string KullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void Cuzdan_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width,formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            GenelIslemler gi = new GenelIslemler();
            gi.CuzdanBilgileri(kullaniciAdi, lblPara);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.ParaYukle(kullaniciAdi, 5);
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            this.Close();
            cd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.ParaYukle(kullaniciAdi, 10);
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            this.Close();
            cd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.ParaYukle(kullaniciAdi, 15);
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            this.Close();
            cd.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.ParaYukle(kullaniciAdi, 20);
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            this.Close();
            cd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.ParayiSifirla(kullaniciAdi);
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            this.Close();
            cd.Show();
        }
    }
}
