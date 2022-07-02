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
    public partial class AramaSonuc : Form
    {
        string kullaniciAdi;
        string arama;
        int aramaTuru, formYKonumu = 0;

        ListBox lbKitapAdi = new ListBox();
        ListBox lbYazarAdi = new ListBox();
        ListBox lbResim = new ListBox();
        int aralikX = 30;
        int aralikY = 25;
        int[] konumX;
        int konumXYedek = 0;
        int konumYYedek = 0;
        int[] konumY;
        GroupBox[] gb;
        Label[] lblYazar;
        PictureBox[] pbResim;

        public AramaSonuc(int FormYKonumu, string KullaniciAdi,int AramaTuru,string Arama)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.aramaTuru = AramaTuru;
            this.arama = Arama;
            this.formYKonumu = FormYKonumu;
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AramaSonuc_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            
            AramaIslemleri ai = new AramaIslemleri();

            if (aramaTuru == 1)
            {
                ai.KitapAra(arama, ref lbKitapAdi, ref lbYazarAdi, ref lbResim);
            }
            else if (aramaTuru == 2)
            {
                ai.YazarAra(arama, ref lbKitapAdi, ref lbYazarAdi, ref lbResim);
            }
            else if (aramaTuru == 3)
            {
                ai.TureGoreAra(arama, ref lbKitapAdi, ref lbYazarAdi, ref lbResim);
            }
            int elemanSayisi = lbKitapAdi.Items.Count;
            konumX = new int[elemanSayisi];
            konumY = new int[elemanSayisi];
            gb = new GroupBox[elemanSayisi];
            lblYazar = new Label[elemanSayisi];
            pbResim = new PictureBox[elemanSayisi];
            #region Kutular
            if (lbKitapAdi.Items.Count > 0)
            {
                for (int i = 0; i < lbKitapAdi.Items.Count; i++)
                {
                    if (lbKitapAdi.Items[i] != null)
                    {
                        string kitap = lbKitapAdi.Items[i].ToString();
                        string yazar = lbYazarAdi.Items[i].ToString();
                        string resimLink = lbResim.Items[i].ToString();

                        gb[i] = new GroupBox();
                        gb[i].ForeColor = Color.White;
                        gb[i].Text = yazar;
                        gb[i].Size = new Size(groupBox1.Size.Width, groupBox1.Size.Height);
                        this.Controls.Add(gb[i]);

                        pbResim[i] = new PictureBox();
                        pbResim[i].Size = new Size(groupBox1.Size.Width, groupBox1.Size.Height-20);
                        pbResim[i].ImageLocation = resimLink;
                        pbResim[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbResim[i].Cursor = Cursors.Hand;
                        this.Controls.Add(pbResim[i]);
                        pbResim[i].BringToFront();

                        lblYazar[i] = new Label();
                        lblYazar[i].Text = kitap;
                        lblYazar[i].ForeColor = Color.White;
                        this.Controls.Add(lblYazar[i]);
                        lblYazar[i].BringToFront();

                        void pb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }
                        
                        pbResim[i].Click += new EventHandler(pb_Click);
                    }
                }
            }
            #endregion
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBaslik.Location = new Point((this.Size.Width / 2) - (lblBaslik.Size.Width / 2), lblBaslik.Location.Y);
            lblSayfaAciklama.Location = new Point((this.Size.Width / 2) - (lblSayfaAciklama.Size.Width / 2), lblSayfaAciklama.Location.Y);

            #region Otomatik Boyutlandırma
            konumXYedek = groupBox1.Location.X;
            konumYYedek = groupBox1.Location.Y;

            for (int i = 0; i < gb.Length; i++)
            {
                if (gb[i] != null)
                {
                    if (konumXYedek + groupBox1.Size.Width >= this.Size.Width)
                    {
                        konumYYedek += groupBox1.Size.Height + aralikY;
                        konumXYedek = groupBox1.Location.X;
                    }
                    konumX[i] = konumXYedek;
                    konumY[i] = konumYYedek;

                    gb[i].Location = new Point(konumX[i], konumY[i]);
                    pbResim[i].Location = new Point(gb[i].Location.X, gb[i].Location.Y+20);
                    lblYazar[i].Size = new Size(lblYazar[i].Text.Length * 10, lblYazar[i].Size.Height);
                    lblYazar[i].Location = new Point(gb[i].Location.X + (gb[i].Size.Width / 2) - (lblYazar[i].Size.Width / 2), gb[i].Location.Y + gb[i].Size.Height - (lblYazar[i].Size.Height / 2));
                    konumXYedek += groupBox1.Size.Width + aralikX;
                }
            }

            NavBar navBar = new NavBar(kullaniciAdi);
            if (konumYYedek + groupBox1.Size.Height + aralikY > this.MdiParent.Size.Height - 20)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, konumYYedek + groupBox1.Size.Height + aralikY);

            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            #endregion
        }
    }
}
