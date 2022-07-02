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

        ListBox lbTakipEdilenKitapAdi = new ListBox();
        ListBox lbTakipEdilenYazarAdi = new ListBox();
        ListBox lbTakipEdilenResim = new ListBox();
        int[] TakipEdilenKonumX = new int[10];
        int[] TakipEdilenKonumY = new int[10];
        GroupBox[] gbTakipEdilen = new GroupBox[10];
        Label[] lblTakipEdilenYazar = new Label[10];
        PictureBox[] pbTakipEdilenResim = new PictureBox[10];

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
            ai.TakipEdilenlerdenKitaplar(kullaniciAdi, ref lbTakipEdilenYazarAdi, ref lbTakipEdilenKitapAdi, ref lbTakipEdilenResim);

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

            #region Takip Edilenlerden Kutuları
            if (lbTakipEdilenKitapAdi.Items.Count > 0)
            {
                for (int i = 0; i < lbTakipEdilenKitapAdi.Items.Count; i++)
                {
                    if (lbTakipEdilenKitapAdi.Items[i] != null)
                    {
                        string kitap = lbTakipEdilenKitapAdi.Items[i].ToString();
                        string yazar = lbTakipEdilenYazarAdi.Items[i].ToString();
                        string resim = lbTakipEdilenResim.Items[i].ToString();

                        gbTakipEdilen[i] = new GroupBox();
                        gbTakipEdilen[i].ForeColor = Color.White;
                        gbTakipEdilen[i].Text = kitap;
                        gbTakipEdilen[i].Size = new Size(groupBox4.Size.Width, groupBox4.Size.Height);
                        this.Controls.Add(gbTakipEdilen[i]);
                        gbTakipEdilen[i].BringToFront();

                        pbTakipEdilenResim[i] = new PictureBox();
                        pbTakipEdilenResim[i].Size = gbTakipEdilen[i].Size;
                        pbTakipEdilenResim[i].ImageLocation = resim;
                        pbTakipEdilenResim[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        pbTakipEdilenResim[i].Cursor = Cursors.Hand;
                        this.Controls.Add(pbTakipEdilenResim[i]);
                        pbTakipEdilenResim[i].BringToFront();

                        lblTakipEdilenYazar[i] = new Label();
                        lblTakipEdilenYazar[i].Text = kitap;
                        lblTakipEdilenYazar[i].ForeColor = Color.White;
                        this.Controls.Add(lblTakipEdilenYazar[i]);
                        lblTakipEdilenYazar[i].BringToFront();

                        void gb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        gbTakipEdilen[i].Click += new EventHandler(gb_Click);

                        void pb_Click(object sendr, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, yazar, kitap);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }

                        pbTakipEdilenResim[i].Click += new EventHandler(pb_Click);
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
                    SonYazilanKonumX[i] = konumXYedek;
                    SonYazilanKonumY[i] = konumYYedek;

                    gbSonYazilan[i].Location = new Point(SonYazilanKonumX[i] + groupBox1.Location.X, SonYazilanKonumY[i] + groupBox1.Location.Y);
                    lblSonYazilanYazar[i].Size = new Size(lblSonYazilanYazar[i].Text.Length * 10, lblSonYazilanYazar[i].Size.Height);
                    lblSonYazilanYazar[i].Location = new Point(gbSonYazilan[i].Location.X + (gbSonYazilan[i].Size.Width / 2) - (lblSonYazilanYazar[i].Size.Width / 2), gbSonYazilan[i].Location.Y + gbSonYazilan[i].Size.Height - (lblSonYazilanYazar[i].Size.Height / 2));
                    pbSonYazilanResim[i].Location = gbSonYazilan[i].Location;

                    konumXYedek += groupBox2.Size.Width + aralikX;
                }
            }

            konumXYedek = groupBox4.Location.X;
            konumYYedek = groupBox4.Location.Y;

            for (int i = 0; i < gbTakipEdilen.Length; i++)
            {
                if (gbTakipEdilen[i] != null)
                {
                    TakipEdilenKonumX[i] = konumXYedek;
                    TakipEdilenKonumY[i] = konumYYedek;

                    gbTakipEdilen[i].Location = new Point(TakipEdilenKonumX[i] + groupBox3.Location.X, TakipEdilenKonumY[i] + groupBox3.Location.Y);
                    lblTakipEdilenYazar[i].Size = new Size(lblTakipEdilenYazar[i].Text.Length * 10, lblTakipEdilenYazar[i].Size.Height);
                    lblTakipEdilenYazar[i].Location = new Point(gbTakipEdilen[i].Location.X + (gbTakipEdilen[i].Size.Width / 2) - (lblTakipEdilenYazar[i].Size.Width / 2), gbTakipEdilen[i].Location.Y + gbTakipEdilen[i].Size.Height - (lblTakipEdilenYazar[i].Size.Height / 2));

                    pbTakipEdilenResim[i].Location = gbTakipEdilen[i].Location;

                    konumXYedek += groupBox4.Size.Width + aralikX;
                }
            }

            konumXYedek = groupBox4.Location.X;
            konumYYedek = groupBox4.Location.Y;

            for (int i = 0; i < gbEnCokOkunan.Length; i++)
            {
                if (gbEnCokOkunan[i] != null)
                {
                    EnCokOkunanKonumX[i] = konumXYedek;
                    EnCokOkunanKonumY[i] = konumYYedek;

                    gbEnCokOkunan[i].Location = new Point(EnCokOkunanKonumX[i] + groupBox5.Location.X, EnCokOkunanKonumY[i] + groupBox5.Location.Y);
                    lblEnCokOkunanYazar[i].Size = new Size(lblEnCokOkunanYazar[i].Text.Length * 10, lblEnCokOkunanYazar[i].Size.Height);
                    lblEnCokOkunanYazar[i].Location = new Point(gbEnCokOkunan[i].Location.X + (gbEnCokOkunan[i].Size.Width / 2) - (lblEnCokOkunanYazar[i].Size.Width / 2), gbEnCokOkunan[i].Location.Y + gbEnCokOkunan[i].Size.Height - (lblEnCokOkunanYazar[i].Size.Height / 2));

                    pbEnCokOkunannResim[i].Location = gbEnCokOkunan[i].Location;

                    konumXYedek += groupBox6.Size.Width + aralikX;
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

        private void button2_Click(object sender, EventArgs e)
        {

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
