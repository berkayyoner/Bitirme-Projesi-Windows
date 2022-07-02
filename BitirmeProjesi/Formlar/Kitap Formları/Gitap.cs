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
    public partial class Gitap : Form
    {
        string kullaniciAdi = "", kitapAdi = "", yazarKullaniciAdi = "";
        ListBox lbChapterAdlari = new ListBox();
        ListBox lbKullanicilar = new ListBox();
        ListBox lbYorumlar = new ListBox();
        int formYKonumu = 0;

        public Gitap(int FormYKonumu, string KullaniciAdi, string YazarKullaniciAdi, string KitapAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.kitapAdi = KitapAdi;
            this.yazarKullaniciAdi = YazarKullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void Gitap_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            KitapIslemleri ki = new KitapIslemleri();
            ki.KitabiGoruntule(kitapAdi, yazarKullaniciAdi, lblKitap, lblYazar, lblKitapTuru, lblKitapKonusu, lblEtiketler, lblOkunma, pictureBox1);
            lblbolumsayisi.Text = (ki.ChapterSayisi(yazarKullaniciAdi, kitapAdi) - 1).ToString();
            lblDurum.Text = ki.KitapDurum(yazarKullaniciAdi, kitapAdi);
            ki.ChapterAdlari(yazarKullaniciAdi, kitapAdi, lbChapterAdlari);
            for (int i = 0; i < lbChapterAdlari.Items.Count; i++)
            {
                comboBox1.Items.Add(lbChapterAdlari.Items[i].ToString());
            }

            ki.FiyatKontrol(kullaniciAdi, yazarKullaniciAdi, kitapAdi, lblFiyat, btnOku);
            if (lblFiyat.Text.Length > 1)
            {
                lblFiyat.Location = new Point(lblFiyat.Location.X - (lblFiyat.Size.Width / 4), lblFiyat.Location.Y);
            }

            #region Beğeni-Beğenmeme Özelliği
            if (ki.BegeniKontrol(kullaniciAdi,yazarKullaniciAdi,kitapAdi))
            {
                btnBegen.Text = "Beğenildi";
                btnBegen.BackColor = Color.White;
                btnBegen.ForeColor = Color.Black;
            }
            if (ki.BegenmemeKontrol(kullaniciAdi, yazarKullaniciAdi, kitapAdi))
            {
                btnBegenme.Text = "Beğenilmedi";
                btnBegenme.BackColor = Color.White;
                btnBegenme.ForeColor = Color.Black;
            }
            lblBeğeni.Text = ki.KacBegeni(yazarKullaniciAdi, kitapAdi).ToString();
            lblBegenmeme.Text = ki.KacBegenmeme(yazarKullaniciAdi,kitapAdi).ToString();
            #endregion

            ki.Yorumlar(yazarKullaniciAdi, kitapAdi, ref lbKullanicilar, ref lbYorumlar);
            lblYorumSayisi.Text = lbKullanicilar.Items.Count.ToString();
        }

        private void btnOku_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (ki.ChapterSayisi(yazarKullaniciAdi, kitapAdi) > 1)
            {
                if (btnOku.Text == "Oku")
                {
                    ki.EtiketleriKaydet(kullaniciAdi, yazarKullaniciAdi, lblKitapTuru.Text, lblEtiketler.Text);
                    Oku o = new Oku(this.Location.Y, kullaniciAdi, kitapAdi, yazarKullaniciAdi, 0);
                    o.MdiParent = this.MdiParent;
                    o.Show();
                }
                else if (btnOku.Text == "Satın Al")
                {
                    if (MessageBox.Show("Gitabı satın almak istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        switch (ki.SatinAl(this.kullaniciAdi, yazarKullaniciAdi, kitapAdi))
                        {
                            case 1:
                                MessageBox.Show("Gitap başarı ile satın alındı.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                                gt.MdiParent = this.MdiParent;
                                this.Close();
                                gt.Show();
                                break;

                            case -1:
                                MessageBox.Show("Yetersiz bakiye.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case -2:
                                MessageBox.Show("Gitap satın alınırken bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bu kitaba henüz chapter eklenmemiştir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lbChapterAdlari.Items.Count; i++)
            {
                if (comboBox1.Text == lbChapterAdlari.Items[i].ToString())
                {
                    if (btnOku.Text == "Oku")
                    {
                        Oku o = new Oku(this.Location.Y, kullaniciAdi, kitapAdi, yazarKullaniciAdi, i);
                        o.MdiParent = this.MdiParent;
                        o.Show();
                    }
                    else if (btnOku.Text == "Satın Al")
                    {
                        MessageBox.Show("Kitabı satın almadan bölümler görüntülenemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
        }

        private void btnBegen_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (btnBegen.Text == "Beğen")
            {
                ki.Begen(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                if (btnBegenme.Text == "Beğenilmedi")
                {
                    ki.BegenmeGeriAl(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                }
                Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                gt.MdiParent = this.MdiParent;
                this.Close();
                gt.Show();
            }
            else if (btnBegen.Text == "Beğenildi")
            {
                ki.BegenGeriAl(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                gt.MdiParent = this.MdiParent;
                this.Close();
                gt.Show();
            }
        }

        private void btnBegenme_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (btnBegenme.Text == "Beğenme")
            {
                ki.Begenme(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                if (btnBegen.Text == "Beğenildi")
                {
                    ki.BegenGeriAl(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                }
                Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                gt.MdiParent = this.MdiParent;
                this.Close();
                gt.Show();
            }
            else if (btnBegenme.Text == "Beğenilmedi")
            {
                ki.BegenmeGeriAl(kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                gt.MdiParent = this.MdiParent;
                this.Close();
                gt.Show();
            }
        }

        private void lblYazar_Click(object sender, EventArgs e)
        {
            Profil pr = new Profil(this.Location.Y, kullaniciAdi, yazarKullaniciAdi);
            pr.MdiParent = this.MdiParent;
            pr.Show();
        }

        private void btnYorumPaylas_Click(object sender, EventArgs e)
        {
            GitapYorumlari gy = new GitapYorumlari(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
            gy.MdiParent = this.MdiParent;
            gy.Show();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);
            lblKitapKonusu.MaximumSize = new Size(gbKonu.Size.Width - lblKitapKonusu.Location.X - 10, 0);

            if (lblKitapKonusu.Size.Height > 13)
            {
                gbKonu.Size = new Size(this.Size.Width - 119, (lblKitapKonusu.Location.Y * 2) + lblKitapKonusu.Size.Height);
                gbYorumlar.Location = new Point(gbYorumlar.Location.X, gbKonu.Location.Y + gbKonu.Size.Height + 5);
            }
            else
            {
                gbKonu.Size = new Size(this.Size.Width - 119, gbKonu.Size.Height);
            }
            if (gbYorumlar.Location.Y + gbYorumlar.Size.Height + 20 > this.MdiParent.Size.Height - 45)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, gbYorumlar.Location.Y + gbYorumlar.Size.Height + 10);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            #endregion
        }
    }
}