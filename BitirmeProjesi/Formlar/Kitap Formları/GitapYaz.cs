using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace BitirmeProjesi
{
    public partial class GitapYaz : Form
    {
        string kullaniciAdi = "";
        int formYKonumu = 0;
        public GitapYaz(int FormYKonumu, string KullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
        }

        private void KitapYaz_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (txtKitapAdi.Text != "" && txtKitapKonusu.Text != "" && cbKitapTuru.Text != "" && txtFotograf.Text != "")
            {
                switch (ki.TaslagiKaydet(kullaniciAdi, txtKitapAdi.Text, cbKitapTuru.Text, txtKitapKonusu.Text, txtEtiket.Text, Convert.ToDouble(txtFiyat.Text), txtFotograf.Text))
                {
                    case 1:
                        MessageBox.Show("Kitap başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Gitaplarım git = new Gitaplarım(this.Location.Y, kullaniciAdi);
                        git.MdiParent = this.MdiParent;
                        this.Close();
                        git.Show();
                        break;
                    case -1:
                        MessageBox.Show("Kitap kaydedilemedi.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Alanlar boş bırakılamaz.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gitaplarım git = new Gitaplarım(this.Location.Y, kullaniciAdi);
            git.MdiParent = this.MdiParent;
            this.Close();
            git.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (txtKitapAdi.Text != "" && txtKitapKonusu.Text != "" && cbKitapTuru.Text != "" && txtFotograf.Text != "")
            {
                switch (ki.TaslagiKaydet(kullaniciAdi, txtKitapAdi.Text, cbKitapTuru.Text, txtKitapKonusu.Text, txtEtiket.Text, Convert.ToDouble(txtFiyat.Text),txtFotograf.Text))
                {
                    case 1:
                        MessageBox.Show("Kitap başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChapterYaz cy = new ChapterYaz(this.Location.Y, kullaniciAdi, txtKitapAdi.Text, kullaniciAdi);
                        cy.MdiParent = this.MdiParent;
                        this.Close();
                        cy.Show();
                        break;
                    case -1:
                        MessageBox.Show("Kitap kaydedilemedi.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Alanlar boş bırakılamaz.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFotograf.Text != "")
            {
                try
                {
                    pictureBox1.ImageLocation = txtFotograf.Text;
                }
                catch
                {
                    
                }
            }
        }

        private void txtFiyat_TextChanged(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.SadeceSayi(txtFiyat);
        }
    }
}
