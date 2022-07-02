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
    public partial class Gitapligim : Form
    {
        int formYKonumu = 0;
        string kullaniciAdi;
        ListBox lbYazarAdi = new ListBox();
        ListBox lbKitapAdi = new ListBox();
        Label[] lblListe;
        Button[] btnGoruntule;
        int konumXYedek, konumYYedek;
        public Gitapligim(int FormYKonumu, string KullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void Gitapligim_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
            KitapIslemleri ki = new KitapIslemleri();
            ki.BegenilenKitaplar(kullaniciAdi, ref lbKitapAdi, ref lbYazarAdi);

            if (lbYazarAdi.Items.Count > 0)
            {
                label1.Visible = false;
                lblListe = new Label[lbYazarAdi.Items.Count];
                btnGoruntule = new Button[lbYazarAdi.Items.Count];
                konumXYedek = gbBegenilenler.Location.X + label1.Location.X;
                konumYYedek = gbBegenilenler.Location.Y + label1.Location.Y;
                int uzunluk = 0;
                for (int i = 0; i < lbYazarAdi.Items.Count; i++)
                {
                    if (lbYazarAdi.Items[i] != null)
                    {
                        string Yazar = lbYazarAdi.Items[i].ToString();
                        string KitapAdi = lbKitapAdi.Items[i].ToString();

                        lblListe[i] = new Label();
                        lblListe[i].Text = Yazar + " - " + KitapAdi;
                        lblListe[i].ForeColor = Color.White;
                        lblListe[i].Location = new Point(konumXYedek, konumYYedek);
                        this.Controls.Add(lblListe[i]);
                        lblListe[i].BringToFront();

                        btnGoruntule[i] = new Button();
                        btnGoruntule[i].FlatStyle = FlatStyle.Flat;
                        btnGoruntule[i].ForeColor = Color.White;
                        btnGoruntule[i].BackColor = Color.Black;
                        btnGoruntule[i].Text = "Görüntüle";
                        btnGoruntule[i].Location = new Point(gbBegenilenler.Size.Width - 60, lblListe[i].Location.Y - (btnGoruntule[i].Size.Height / 4));
                        this.Controls.Add(btnGoruntule[i]);
                        btnGoruntule[i].BringToFront();

                        void btnGoruntule_Click(object asender, EventArgs a)
                        {
                            Gitap gt = new Gitap(this.Location.Y, kullaniciAdi, KitapAdi, Yazar);
                            gt.MdiParent = this.MdiParent;
                            gt.Show();
                        }
                        btnGoruntule[i].Click += new EventHandler(btnGoruntule_Click);

                        uzunluk += lblListe[i].Location.Y + (lblListe[i].Size.Height * 2) + 10;
                        konumYYedek = lblListe[i].Location.Y + lblListe[i].Size.Height + 10;
                    }
                }
                gbBegenilenler.Size = new Size(this.Size.Width - (gbBegenilenler.Location.X * 2) + 16, uzunluk + 16);

                timer1.Enabled = true;
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

            if (gbBegenilenler.Location.Y + gbBegenilenler.Size.Height + 20 > this.MdiParent.Size.Height - 45)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, gbBegenilenler.Location.Y + gbBegenilenler.Size.Height + 10);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            int uzunluk = 0;
            for (int i = 0; i < lbYazarAdi.Items.Count; i++)
            {
                if (lbYazarAdi.Items[i] != null)
                {
                    uzunluk += lblListe[i].Size.Height + 10;
                }
            }

            gbBegenilenler.Size = new Size(this.Size.Width - (gbBegenilenler.Location.X * 2) + 16, uzunluk + 20);
            #endregion
        }
    }
}
