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
    public partial class ChapterYaz : Form
    {
        string kullaniciAdi = "", kitapAdi = "", yazarAdi = "";
        int ChapterSayisi = 0, formYKonumu = 0;
        public ChapterYaz(int FormYKonumu, string KullaniciAdi, string KitapAdi, string YazarAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.kitapAdi = KitapAdi;
            this.yazarAdi = YazarAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void txtChapter_TextChanged(object sender, EventArgs e)
        {
            lblHarf.Text = txtChapter.Text.Length.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            DuzenlemeIslemleri di = new DuzenlemeIslemleri();

            if (txtChapterAdi != null && txtChapter != null && checkBox1.Checked == false)
            {
                switch (ki.ChapterKaydet(yazarAdi, kitapAdi, ChapterSayisi, txtChapterAdi, txtChapter))
                {
                    case 1:
                        MessageBox.Show("Chapter başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Gitaplarım git = new Gitaplarım(this.Location.Y, kullaniciAdi);
                        git.MdiParent = this.MdiParent;
                        this.Close();
                        git.Show();
                        break;
                    case -1:
                        MessageBox.Show("Chapter kaydedilemedi.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else if (txtChapterAdi != null && txtChapter != null && checkBox1.Checked == true)
            {
                switch (ki.ChapterKaydet(yazarAdi, kitapAdi, ChapterSayisi, txtChapterAdi, txtChapter))
                {
                    case 1:
                        switch (di.KitabiTamamla(kitapAdi, yazarAdi))
                        {
                            case 1:
                                MessageBox.Show("Gitap başarıyla tamamlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Gitaplarım git = new Gitaplarım(this.Location.Y, kullaniciAdi);
                                git.MdiParent = this.MdiParent;
                                this.Close();
                                git.Show();
                                break;
                            case -1:
                                break;
                        }
                        break;

                    case -1:
                        MessageBox.Show("Gitap tamamlanamadı.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Alanlar boş bırakılamaz.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChapterYaz_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            timer1.Enabled = true;
            KitapIslemleri ki = new KitapIslemleri();
            ChapterSayisi = ki.ChapterSayisi(yazarAdi, kitapAdi);
            lblChapter.Text = ChapterSayisi.ToString();
            lblKitap.Text = kitapAdi;
            pictureBox1.ImageLocation = ki.KitapKapakFotografi(yazarAdi, kitapAdi);
        }
    }
}
