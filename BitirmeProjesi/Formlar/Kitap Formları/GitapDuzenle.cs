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
    public partial class GitapDuzenle : Form
    {
        string kullaniciAdi = "", kitapAdi = "", yazarAdi = "";
        int formYKonumu = 0;
        public GitapDuzenle(int FormYKonumu, string KullaniciAdi, string KitapAdi, string YazarAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.kitapAdi = KitapAdi;
            this.yazarAdi = YazarAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void GitapDuzenle_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            DuzenlemeIslemleri di = new DuzenlemeIslemleri();
            di.DuzenleBilgileri(kitapAdi, yazarAdi, pictureBox1, txtKitapAdi, cbKitapTuru, txtKitapKonusu, txtEtiket, txtFiyat, txtFotograf);
            KitapIslemleri ki = new KitapIslemleri();
            ListBox lb = new ListBox();
            ki.ChapterAdlari(yazarAdi, kitapAdi, lb);
            for (int i = 0; i < lb.Items.Count; i++)
            {
                cbBolumler.Items.Add(lb.Items[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DuzenlemeIslemleri di = new DuzenlemeIslemleri();
            if (txtKitapAdi.Text != "" && txtKitapKonusu.Text != "" && cbKitapTuru.Text != "" && txtFotograf.Text != "")
            {
                switch (di.DuzenlemeleriKaydet(kitapAdi, yazarAdi, pictureBox1, txtKitapAdi, cbKitapTuru, txtKitapKonusu, txtEtiket, txtFiyat, txtFotograf))
                {
                    case 1:
                        MessageBox.Show("Kitap başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Gitaplarım git = new Gitaplarım(this.Location.Y, kullaniciAdi);
                        git.MdiParent = this.MdiParent;
                        this.Close();
                        git.Show();
                        break;
                    case -1:
                        MessageBox.Show("Kitap güncellenemedi.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }

        }

        private void cbBolumler_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChapterDuzenle cd = new ChapterDuzenle(this.Location.Y, yazarAdi, kitapAdi, cbBolumler.Text);
            cd.MdiParent = this.MdiParent;
            cd.Show();
        }

        private void txtFiyat_TextChanged(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.SadeceSayi(txtFiyat);
        }

        private void txtFotograf_TextChanged(object sender, EventArgs e)
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
