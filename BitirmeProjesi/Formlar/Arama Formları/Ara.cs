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
    public partial class Ara : Form
    {
        string kullaniciAdi = "";
        int formYKonumu = 0;
        public Ara(int FormYKonumu, string KullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void Ara_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            radioButton1.Checked = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtAra.Text != null)
            {
                if (radioButton1.Checked == true)
                {
                    AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 1, txtAra.Text);
                    ara.MdiParent = this.MdiParent;
                    ara.Show();
                }
                else if (radioButton3.Checked == true)
                {
                    AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 2, txtAra.Text);
                    ara.MdiParent = this.MdiParent;
                    ara.Show();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button1.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button2.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button4.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button5.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button6.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button7.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button8.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AramaSonuc ara = new AramaSonuc(this.Location.Y, kullaniciAdi, 3, button9.Text);
            ara.MdiParent = this.MdiParent;
            ara.Show();
        }
    }
}
