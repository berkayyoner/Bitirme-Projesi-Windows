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
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            lblHataMesaji.Text = "";
            #region Merkeze Konumlandırma
            this.Anchor = AnchorStyles.None;
            #endregion
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();

            if (kullaniciAdi.Text != "" && sifre.Text != "")
            {
                switch (gi.Giris(kullaniciAdi.Text, sifre.Text))
                {
                    case 1:
                        lblHataMesaji.Text = "Kullanıcı adı ile ilgili bir hata oluştu.";
                        break;
                    case 2:
                        lblHataMesaji.Text = "Şifre ile ilgili bir hata oluştu.";
                        break;
                    case 3:
                        lblHataMesaji.Text = "Giriş başarılı.";
                        NavBar navBar = new NavBar(kullaniciAdi.Text);
                        navBar.MdiParent = this.MdiParent;
                        this.Close();
                        navBar.Show();
                        break;
                    case 4:
                        lblHataMesaji.Text = "Hatalı sifre.";
                        break;
                    case 5:
                        lblHataMesaji.Text = "Böyle bir kullanıcı bulunamadı.";
                        break;
                }
            }
            else
            {
                lblHataMesaji.Text = "Kullanıcı adı ve şifre alanı boş bırakılamaz.";
            }
            sifre.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IlkEkran ilkEkran = new IlkEkran();
            ilkEkran.MdiParent = this.MdiParent;
            this.Close();
            ilkEkran.Show();
        }

        private void sifre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                GenelIslemler gi = new GenelIslemler();

                if (kullaniciAdi.Text != "" && sifre.Text != "")
                {
                    switch (gi.Giris(kullaniciAdi.Text, sifre.Text))
                    {
                        case 1:
                            lblHataMesaji.Text = "Kullanıcı adı ile ilgili bir hata oluştu.";
                            break;
                        case 2:
                            lblHataMesaji.Text = "Şifre ile ilgili bir hata oluştu.";
                            break;
                        case 3:
                            lblHataMesaji.Text = "Giriş başarılı.";
                            NavBar navBar = new NavBar(kullaniciAdi.Text);
                            navBar.MdiParent = this.MdiParent;
                            this.Close();
                            navBar.Show();
                            break;
                        case 4:
                            lblHataMesaji.Text = "Hatalı sifre.";
                            break;
                        case 5:
                            lblHataMesaji.Text = "Böyle bir kullanıcı bulunamadı.";
                            break;
                    }
                }
                else
                {
                    lblHataMesaji.Text = "Kullanıcı adı ve şifre alanı boş bırakılamaz.";
                }
                sifre.Text = "";
            }
            }
    }
}
