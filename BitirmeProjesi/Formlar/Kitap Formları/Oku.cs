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
    public partial class Oku : Form
    {
        string kullaniciAdi = "", kitapAdi = "", yazar = "";
        int chapter = 0, formYKonumu = 0;
        ListBox lbChapterAdi = new ListBox();
        ListBox lbBolum = new ListBox();

        public Oku(int FormYKonumu, string KullaniciAdi, string KitapAdi, string Yazar, int Chapter)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.kitapAdi = KitapAdi;
            this.yazar = Yazar;
            this.chapter = Chapter;
            this.formYKonumu = FormYKonumu;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);
            if (lblBolum.Location.Y + lblBolum.Size.Height + 20 > this.Size.Height)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, lblBolum.Location.Y + lblBolum.Size.Height + 20);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            #endregion
        }

        private void lblSayi_TextChanged(object sender, EventArgs e)
        {
            lblBaslik.Text = lbChapterAdi.Items[chapter].ToString();
            lblBolum.Text = lbBolum.Items[chapter].ToString();
        }

        private void btnOnceki_Click(object sender, EventArgs e)
        {
            chapter -= 1;
            lblSayi.Text = chapter.ToString();
            KitapIslemleri ki = new KitapIslemleri();
            if (chapter < 1)
            {
                btnOnceki.Enabled = false;
            }
            else {
                btnOnceki.Enabled = true;
            }
            if (chapter >= (ki.ChapterSayisi(yazar, kitapAdi) - 2))
            {
                btnSonraki.Enabled = false;
            }
            else
            {
                btnSonraki.Enabled = true;
            }

        }

        private void btnSonraki_Click(object sender, EventArgs e)
        {
            chapter += 1;
            lblSayi.Text = chapter.ToString();
            KitapIslemleri ki = new KitapIslemleri();
            if (chapter < 1)
            {
                btnOnceki.Enabled = false;
            }
            else
            {
                btnOnceki.Enabled = true;
            }
            if (chapter >= (ki.ChapterSayisi(yazar, kitapAdi) - 2))
            {
                btnSonraki.Enabled = false;
            }
            else
            {
                btnSonraki.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lbChapterAdi.Items.Count; i++)
            {
                if (comboBox1.Text == lbChapterAdi.Items[i].ToString())
                {
                    lblBaslik.Text = lbChapterAdi.Items[i].ToString();
                    lblBolum.Text = lbBolum.Items[i].ToString();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            gi.SesliOku(lblBaslik);
            gi.SesliOku(lblBolum);
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Oku_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
            int chapterSayisi = chapter;
            timer1.Enabled = true;
            KitapIslemleri ki = new KitapIslemleri();
            ki.OkuSayfasi(yazar, kitapAdi, lbChapterAdi, lbBolum);
            lblBaslik.Text = lbChapterAdi.Items[chapterSayisi].ToString();
            lblBolum.Text = lbBolum.Items[chapterSayisi].ToString();
            comboBox1.Text = lbChapterAdi.Items[chapterSayisi].ToString();
            for (int i = 0; i < lbChapterAdi.Items.Count; i++)
            {
                comboBox1.Items.Add(lbChapterAdi.Items[i].ToString());  
            }

            if (chapter < 1)
            {
                btnOnceki.Enabled = false;
            }
            if (chapter >= (ki.ChapterSayisi(yazar, kitapAdi) - 2))
            {
                btnSonraki.Enabled = false;
            }
        }
    }
}
