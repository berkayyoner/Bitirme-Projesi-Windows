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
    public partial class AnaSayfa : Form
    {
        string kullaniciAdi = "";
        int durum = 0, formYKonumu = 0;

        int aralikX = 20;
        int konumXYedek = 0;
        int konumYYedek = 0;

        ListBox lbSonYazilanKitapAdi = new ListBox();
        ListBox lbSonYazilanYazarAdi = new ListBox();
        ListBox lbSonYazilanResim = new ListBox();
        int[] SonYazilanKonumX = new int[10];
        int[] SonYazilanKonumY = new int[10];
        GroupBox[] gbSonYazilan = new GroupBox[10];
        Label[] lblSonYazilanYazar = new Label[10];
        PictureBox[] pbSonYazilanResim = new PictureBox[10];

        ListBox lbEnCokOkunanKitapAdi = new ListBox();
        ListBox lbEnCokOkunanYazarAdi = new ListBox();
        ListBox lbEnCokOkunanResim = new ListBox();
        int[] EnCokOkunanKonumX = new int[10];
        int[] EnCokOkunanKonumY = new int[10];
        GroupBox[] gbEnCokOkunan = new GroupBox[10];
        Label[] lblEnCokOkunanYazar = new Label[10];
        PictureBox[] pbEnCokOkunannResim = new PictureBox[10];

        ListBox lbBegenebileceklerinizKitapAdi = new ListBox();
        ListBox lbBegenebileceklerinizYazarAdi = new ListBox();
        ListBox lbBegenebileceklerinizResim = new ListBox();
        int[] BegenebileceklerinizKonumX = new int[10];
        int[] BegenebileceklerinizKonumY = new int[10];
        GroupBox[] gbBegenebilecekleriniz = new GroupBox[10];
        Label[] lblBegenebilecekleriniz = new Label[10];
        PictureBox[] pbBegenebilecekleriniz = new PictureBox[10];

        public AnaSayfa(int FormYKonumu, string KullaniciAdi, int Durum) //İlk açılan sayfa 0, sonradan açılanlar 1 ile başlatılacak
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.durum = Durum;
            this.formYKonumu = FormYKonumu;
        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
            #endregion
            AlgoritmikIslemler ai = new AlgoritmikIslemler();
            ai.SonEklenenler(ref lbSonYazilanKitapAdi, ref lbSonYazilanYazarAdi, ref lbSonYazilanResim);
            ai.EnCokOkunanlar(ref lbEnCokOkunanKitapAdi, ref lbEnCokOkunanYazarAdi, ref lbEnCokOkunanResim);
			//ai.TakipEdilenlerdenKitaplar(kullaniciAdi, ref lbTakipEdilenYazarAdi, ref lbTakipEdilenKitapAdi, ref lbTakipEdilenResim);
			ai.Begenebilecekleriniz(kullaniciAdi, ref lbBegenebileceklerinizYazarAdi, ref lbBegenebileceklerinizKitapAdi, ref lbBegenebileceklerinizResim);


			#region Son Yazılanlar Kutuları
			if (lbSonYazilanKitapAdi.Items.Count > 0)
            {
                for (int i = 0; i < lbSonYazilanKitapAdi.Items.Count; i++)
                {
                    if (lbSonYazilanKitapAdi.Items[i] != null)
                    {
                        string kitap = lbSonYazilanKitapAdi.Items[i].ToString();
                        string yazar = lbSonYazilanYazarAdi.Items[i].ToString();
                        string resim = lbSonYazilanResim.Items[i].ToString();

                        gbSonYazilan[i] = new GroupBox();
                        gbSonYazilan[i].ForeColor = Color.White;
                        gbSonYazilan[i].Text = kitap;
                        gbSonYazilan[i].Size = new Size(groupBox2.Size.Width, groupBox2.Size.Height);
                        gbSonYazilan[i].BackColor = Color.Transparent;
                        this.Controls.Add(gbSonYazilan[i]);
                        gbSonYazilan[i].BringToFront();

                        pbSonYazilanResim[i] = new PictureBox();
                        pbSonYazilanResim[i].Size = gbSonYazilan[i].Size;
                        pbSonYazilanResim[i].ImageLocation = resim;
                        pbSonYazilanResim[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbSonYazilanResim[i].Cursor = Cursors.Hand;
                        this.Controls.Add(pbSonYazilanResim[i]);
                        pbSonYazilanResim[i].BringToFront();

                        lblSonYazilanYazar[i] = new Label();
                        lblSonYazilanYazar[i].Text = kitap;
                        lblSonYazilanYazar[i].ForeColor = Color.White;
                        this.Controls.Add(lblSonYazilanYazar[i]);
                        lblSonYazilanYazar[i].BringToFront();

                        void gb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        gbSonYazilan[i].Click += new EventHandler(gb_Click);

                        void pb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        pbSonYazilanResim[i].Click += new EventHandler(pb_Click);
                    }
                }
            }
            #endregion

            #region Beğenebilecekleriniz Kutuları
            if (lbBegenebileceklerinizKitapAdi.Items.Count > 0)
            {
                for (int i = 0; i < lbBegenebileceklerinizKitapAdi.Items.Count; i++)
                {
                    if (lbBegenebileceklerinizKitapAdi.Items[i] != null)
                    {
                        string kitap = lbBegenebileceklerinizKitapAdi.Items[i].ToString();
                        string yazar = lbBegenebileceklerinizYazarAdi.Items[i].ToString();
                        string resim = lbBegenebileceklerinizResim.Items[i].ToString();

                        gbBegenebilecekleriniz[i] = new GroupBox();
                        gbBegenebilecekleriniz[i].ForeColor = Color.White;
                        gbBegenebilecekleriniz[i].Text = kitap;
                        gbBegenebilecekleriniz[i].Size = new Size(groupBox4.Size.Width, groupBox4.Size.Height);
                        this.Controls.Add(gbBegenebilecekleriniz[i]);
                        gbBegenebilecekleriniz[i].BringToFront();

                        pbBegenebilecekleriniz[i] = new PictureBox();
                        pbBegenebilecekleriniz[i].Size = gbBegenebilecekleriniz[i].Size;
                        pbBegenebilecekleriniz[i].ImageLocation = resim;
                        pbBegenebilecekleriniz[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbBegenebilecekleriniz[i].Cursor = Cursors.Hand;
                        this.Controls.Add(pbBegenebilecekleriniz[i]);
                        pbBegenebilecekleriniz[i].BringToFront();

                        lblBegenebilecekleriniz[i] = new Label();
                        lblBegenebilecekleriniz[i].Text = kitap;
                        lblBegenebilecekleriniz[i].ForeColor = Color.White;
                        this.Controls.Add(lblBegenebilecekleriniz[i]);
                        lblBegenebilecekleriniz[i].BringToFront();

                        void gb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        gbBegenebilecekleriniz[i].Click += new EventHandler(gb_Click);

                        void pb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        pbBegenebilecekleriniz[i].Click += new EventHandler(pb_Click);
                    }
                }
            }
            #endregion

            #region En Çok Okunanlar Kutuları
            if (lbEnCokOkunanKitapAdi.Items.Count > 0)
            {
                for (int i = 0; i < lbEnCokOkunanKitapAdi.Items.Count; i++)
                {
                    if (lbEnCokOkunanKitapAdi.Items[i] != null)
                    {
                        string kitap = lbEnCokOkunanKitapAdi.Items[i].ToString();
                        string yazar = lbEnCokOkunanYazarAdi.Items[i].ToString();
                        string resim = lbEnCokOkunanResim.Items[i].ToString();

                        gbEnCokOkunan[i] = new GroupBox();
                        gbEnCokOkunan[i].ForeColor = Color.White;
                        gbEnCokOkunan[i].Text = kitap;
                        gbEnCokOkunan[i].Size = new Size(groupBox2.Size.Width, groupBox2.Size.Height);
                        this.Controls.Add(gbEnCokOkunan[i]);
                        gbEnCokOkunan[i].BringToFront();

                        pbEnCokOkunannResim[i] = new PictureBox();
                        pbEnCokOkunannResim[i].Size = gbEnCokOkunan[i].Size;
                        pbEnCokOkunannResim[i].ImageLocation = resim;
                        pbEnCokOkunannResim[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbEnCokOkunannResim[i].Cursor = Cursors.Hand;
                        this.Controls.Add(pbEnCokOkunannResim[i]);
                        pbEnCokOkunannResim[i].BringToFront();

                        lblEnCokOkunanYazar[i] = new Label();
                        lblEnCokOkunanYazar[i].Text = kitap;
                        lblEnCokOkunanYazar[i].ForeColor = Color.White;
                        this.Controls.Add(lblEnCokOkunanYazar[i]);
                        lblEnCokOkunanYazar[i].BringToFront();

                        void gb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        gbEnCokOkunan[i].Click += new EventHandler(gb_Click);

                        void pb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        pbEnCokOkunannResim[i].Click += new EventHandler(pb_Click);
                    }
                }
            }
            #endregion

            timer1.Enabled = true;
            GenelIslemler gi = new GenelIslemler();
            btnProfil.Text = gi.AdiNe(kullaniciAdi);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBaslik.Location = new Point((this.Size.Width / 2) - (lblBaslik.Size.Width / 2), lblBaslik.Location.Y);
            lblSayfaAciklama.Location = new Point((this.Size.Width / 2) - (lblSayfaAciklama.Size.Width / 2), lblSayfaAciklama.Location.Y);

            #region Otomatik Boyutlandırma
            konumXYedek = groupBox2.Location.X;
            konumYYedek = groupBox2.Location.Y;

            for (int i = 0; i < gbSonYazilan.Length; i++)
            {
                if (gbSonYazilan[i] != null)
                {
                    if (i > 0)
                    {
						SonYazilanKonumX[i] = konumXYedek;
						SonYazilanKonumY[i] = konumYYedek;

						gbSonYazilan[i].Location = new Point(SonYazilanKonumX[i] + groupBox1.Location.X, SonYazilanKonumY[i] + groupBox1.Location.Y);
						lblSonYazilanYazar[i].Size = new Size(lblSonYazilanYazar[i].Text.Length * 10, lblSonYazilanYazar[i].Size.Height);
						lblSonYazilanYazar[i].Location = new Point(gbSonYazilan[i].Location.X + (gbSonYazilan[i].Size.Width / 2) - (lblSonYazilanYazar[i].Text.Length)*3, gbSonYazilan[i].Location.Y + gbSonYazilan[i].Size.Height - (lblSonYazilanYazar[i].Size.Height / 2));
						pbSonYazilanResim[i].Location = gbSonYazilan[i].Location;

						konumXYedek += groupBox2.Size.Width + aralikX;
					}
                    else
                    {
						SonYazilanKonumX[i] = konumXYedek;
						SonYazilanKonumY[i] = konumYYedek;

						gbSonYazilan[i].Location = new Point(SonYazilanKonumX[i] + groupBox1.Location.X, SonYazilanKonumY[i] + groupBox1.Location.Y);
						lblSonYazilanYazar[i].Size = new Size(lblSonYazilanYazar[i].Text.Length * 10, lblSonYazilanYazar[i].Size.Height);
						lblSonYazilanYazar[i].Location = new Point(gbSonYazilan[i].Location.X + (gbSonYazilan[i].Size.Width / 2) - (lblSonYazilanYazar[i].Text.Length)*3, gbSonYazilan[i].Location.Y + gbSonYazilan[i].Size.Height - (lblSonYazilanYazar[i].Size.Height / 2));
						pbSonYazilanResim[i].Location = gbSonYazilan[i].Location;

						konumXYedek += groupBox2.Size.Width + aralikX;
					}
                }
            }

            konumXYedek = groupBox4.Location.X;
            konumYYedek = groupBox4.Location.Y;

            for (int i = 0; i < gbBegenebilecekleriniz.Length; i++)
            {
                if (gbBegenebilecekleriniz[i] != null)
                {
                    if (i > 0)
                    {
						BegenebileceklerinizKonumX[i] = konumXYedek;
						BegenebileceklerinizKonumY[i] = konumYYedek;

                        gbBegenebilecekleriniz[i].Location = new Point(BegenebileceklerinizKonumX[i] + groupBox3.Location.X, BegenebileceklerinizKonumY[i] + groupBox3.Location.Y);
                        lblBegenebilecekleriniz[i].Size = new Size(lblBegenebilecekleriniz[i].Text.Length * 10, lblBegenebilecekleriniz[i].Size.Height);
                        lblBegenebilecekleriniz[i].Location = new Point(gbBegenebilecekleriniz[i].Location.X + (gbBegenebilecekleriniz[i].Size.Width / 2) - (lblBegenebilecekleriniz[i].Text.Length) * 3, gbBegenebilecekleriniz[i].Location.Y + gbBegenebilecekleriniz[i].Size.Height - (lblBegenebilecekleriniz[i].Size.Height / 2));

                        pbBegenebilecekleriniz[i].Location = gbBegenebilecekleriniz[i].Location;

                        konumXYedek += groupBox4.Size.Width + aralikX;
                    }
                    else
                    {
						BegenebileceklerinizKonumX[i] = konumXYedek;
						BegenebileceklerinizKonumY[i] = konumYYedek;

						gbBegenebilecekleriniz[i].Location = new Point(BegenebileceklerinizKonumX[i] + groupBox3.Location.X, BegenebileceklerinizKonumY[i] + groupBox3.Location.Y);
						lblBegenebilecekleriniz[i].Size = new Size(lblBegenebilecekleriniz[i].Text.Length * 10, lblBegenebilecekleriniz[i].Size.Height);
						lblBegenebilecekleriniz[i].Location = new Point(gbBegenebilecekleriniz[i].Location.X + (gbBegenebilecekleriniz[i].Size.Width / 2) - (lblBegenebilecekleriniz[i].Text.Length) * 3, gbBegenebilecekleriniz[i].Location.Y + gbBegenebilecekleriniz[i].Size.Height - (lblBegenebilecekleriniz[i].Size.Height / 2));

						pbBegenebilecekleriniz[i].Location = gbBegenebilecekleriniz[i].Location;

						konumXYedek += groupBox4.Size.Width + aralikX;
					}
                }
            }

            konumXYedek = groupBox4.Location.X;
            konumYYedek = groupBox4.Location.Y;

            for (int i = 0; i < gbEnCokOkunan.Length; i++)
            {
                if (gbEnCokOkunan[i] != null)
                {
                    if (i > 0)
                    {
                        EnCokOkunanKonumX[i] = konumXYedek;
                        EnCokOkunanKonumY[i] = konumYYedek;

                        gbEnCokOkunan[i].Location = new Point(EnCokOkunanKonumX[i] + groupBox5.Location.X, EnCokOkunanKonumY[i] + groupBox5.Location.Y);
                        lblEnCokOkunanYazar[i].Size = new Size(lblEnCokOkunanYazar[i].Text.Length * 10, lblEnCokOkunanYazar[i].Size.Height);
                        lblEnCokOkunanYazar[i].Location = new Point(gbEnCokOkunan[i].Location.X + (gbEnCokOkunan[i].Size.Width / 2) - (lblEnCokOkunanYazar[i].Text.Length) * 3, gbEnCokOkunan[i].Location.Y + gbEnCokOkunan[i].Size.Height - (lblEnCokOkunanYazar[i].Size.Height / 2));

                        pbEnCokOkunannResim[i].Location = gbEnCokOkunan[i].Location;

                        konumXYedek += groupBox6.Size.Width + aralikX;
                    }
                    else
                    {
						EnCokOkunanKonumX[i] = konumXYedek;
						EnCokOkunanKonumY[i] = konumYYedek;

						gbEnCokOkunan[i].Location = new Point(EnCokOkunanKonumX[i] + groupBox5.Location.X, EnCokOkunanKonumY[i] + groupBox5.Location.Y);
						lblEnCokOkunanYazar[i].Size = new Size(lblEnCokOkunanYazar[i].Text.Length * 10, lblEnCokOkunanYazar[i].Size.Height);
						lblEnCokOkunanYazar[i].Location = new Point(gbEnCokOkunan[i].Location.X + (gbEnCokOkunan[i].Size.Width / 2) - (lblEnCokOkunanYazar[i].Text.Length) * 3, gbEnCokOkunan[i].Location.Y + gbEnCokOkunan[i].Size.Height - (lblEnCokOkunanYazar[i].Size.Height / 2));

						pbEnCokOkunannResim[i].Location = gbEnCokOkunan[i].Location;

						konumXYedek += groupBox6.Size.Width + aralikX;
					}
                }
            }

            NavBar navBar = new NavBar(kullaniciAdi);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            Profil pr = new Profil(this.Location.Y, kullaniciAdi, kullaniciAdi);
            pr.MdiParent = this.MdiParent;
            pr.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (btnGeri.Text == "Yenile")
            {
                AnaSayfa ana = new AnaSayfa(this.Location.Y, kullaniciAdi, 0);
                ana.MdiParent = this.MdiParent;
                this.Close();
                ana.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cuzdan cd = new Cuzdan(this.Location.Y, kullaniciAdi);
            cd.MdiParent = this.MdiParent;
            cd.Show();
        }
    }
}
