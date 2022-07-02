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
    public partial class Profil : Form
    {
        string kullaniciAdi = "", hedefKullaniciAdi = "";
        int KitapSayisi = 0, formYKonumu = 0;
        public Profil(int FormYKonumu, string KullaniciAdi, string HedefKullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.hedefKullaniciAdi = HedefKullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void Profil_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            GenelIslemler gi = new GenelIslemler();
            gi.ProfilBilgileri(hedefKullaniciAdi, lblAdi, lblSoyadi, lblEposta, lblKayitTarihi, pictureBox1);
            lblKullaniciAdi.Text = hedefKullaniciAdi;
            lblToplamOkunma.Text = gi.ToplamOkunma(hedefKullaniciAdi).ToString();
            lblTakipçi.Text = gi.TakipciSayisi(hedefKullaniciAdi).ToString();
            lblTakipEdilen.Text = gi.TakipEdilenSayisi(hedefKullaniciAdi).ToString();

            if (gi.TakipKontrol(kullaniciAdi, hedefKullaniciAdi))
            {
                btnTakip.Text = "Takip Ediliyor";
                btnTakip.ForeColor = Color.Black;
                btnTakip.BackColor = Color.White;
            }

            if (lblToplamOkunma.Text.Length > 1)
            {
                lblToplamOkunma.Location = new Point(lblToplamOkunma.Location.X - (lblToplamOkunma.Size.Width / 4), lblToplamOkunma.Location.Y);
            }

            if (kullaniciAdi != hedefKullaniciAdi)
            {
                btnTakip.Visible = true;
            }
            else
            {
                txtFotograf.Visible = true;
                btnKaydet.Visible = true;
            }

            #region Otomatik Boyutlandırma
            KitapIslemleri ki = new KitapIslemleri();
            KitapSayisi = ki.KisininKitaplari(hedefKullaniciAdi, this, groupBox1, lblYok, lblPaylasim);
            if (groupBox1.Location.Y + groupBox1.Size.Height + 40 > this.MdiParent.Size.Height)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, groupBox1.Location.Y + groupBox1.Size.Height + (KitapSayisi * 38) + 40);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            #endregion
        }

        private void btnTakip_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            if (btnTakip.Text == "Takip Et")
            {
                gi.TakipEt(kullaniciAdi, hedefKullaniciAdi);
                Profil pr = new Profil(this.Location.Y, kullaniciAdi, hedefKullaniciAdi);
                pr.MdiParent = this.MdiParent;
                this.Close();
                pr.Show();
            }
            else if (btnTakip.Text == "Takip Ediliyor")
            {
                gi.TakibiGeriAl(kullaniciAdi, hedefKullaniciAdi);
                Profil pr = new Profil(this.Location.Y, kullaniciAdi, hedefKullaniciAdi);
                pr.MdiParent = this.MdiParent;
                this.Close();
                pr.Show();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            DuzenlemeIslemleri di = new DuzenlemeIslemleri();
            switch(di.ProfilFotografiKaydet(hedefKullaniciAdi, txtFotograf.Text))
            {
                case 1:
                    MessageBox.Show("Profil başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -1:
                    MessageBox.Show("Profil güncellenemedi.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            KitapIslemleri ki = new KitapIslemleri();
            NavBar navBar = new NavBar(kullaniciAdi);
            if (groupBox1.Location.Y + groupBox1.Size.Height + 40 > this.MdiParent.Size.Height)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, groupBox1.Location.Y + groupBox1.Size.Height + (KitapSayisi * 38) + 40);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
