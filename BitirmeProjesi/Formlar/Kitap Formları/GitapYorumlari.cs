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
    public partial class GitapYorumlari : Form
    {
        int formYKonumu = 0;
        string kullaniciAdi, yazarKullaniciAdi, kitapAdi;
        ListBox lbKullanicilar = new ListBox();
        ListBox lbYorumlar = new ListBox();
        Label[] lblKullanici;
        Label[] lblYorum;
        Button[] btnSil;
        int konumXYedek, konumYYedek;

        private void GitapYorumlari_Load(object sender, EventArgs e)
        {
            #region NavBar'a Yanaştırma
            NavBar navBar = new NavBar(kullaniciAdi);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.Location = new Point(navBar.Size.Width, formYKonumu);
            this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            #endregion
            label1.Text = kitapAdi;
            if (label1.Text.Length > 1)
            {
                label1.Location = new Point(label1.Location.X - (label1.Size.Width / 4), label1.Location.Y);
            }
            KitapIslemleri ki = new KitapIslemleri();
            ki.Yorumlar(yazarKullaniciAdi, kitapAdi, ref lbKullanicilar, ref lbYorumlar);
            if (lbKullanicilar.Items.Count > 0)
            {
                label4.Visible = false;
                lblKullanici = new Label[lbKullanicilar.Items.Count];
                lblYorum = new Label[lbKullanicilar.Items.Count];
                btnSil = new Button[lbKullanicilar.Items.Count];
                konumXYedek = gbYorumlar.Location.X + label4.Location.X;
                konumYYedek = gbYorumlar.Location.Y + label4.Location.Y;
                int uzunluk = 0;
                for (int i = 0; i < lbKullanicilar.Items.Count; i++)
                {
                    if(lbKullanicilar.Items[i] != null)
                    {
                        string hedef = lbKullanicilar.Items[i].ToString();
                        string yorum = lbYorumlar.Items[i].ToString();

                        lblKullanici[i] = new Label();
                        lblKullanici[i].Text = hedef;
                        lblKullanici[i].ForeColor = Color.White;
                        lblKullanici[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
                        lblKullanici[i].MaximumSize = new Size(lbYorumlar.Items[i].ToString().Length * 8, 999);
                        lblKullanici[i].Location = new Point(konumXYedek, konumYYedek);
                        lblKullanici[i].Cursor = Cursors.Hand;
                        this.Controls.Add(lblKullanici[i]);
                        lblKullanici[i].BringToFront();

                        void lblKullanici_Click(object asender, EventArgs a)
                        {
                            Profil pr = new Profil(this.Location.Y, kullaniciAdi, hedef);
                            pr.MdiParent = this.MdiParent;
                            pr.Show();
                        }
                        lblKullanici[i].Click += new EventHandler(lblKullanici_Click);

                        lblYorum[i] = new Label();
                        lblYorum[i].Text = yorum;
                        lblYorum[i].MaximumSize = new Size(gbYorumlar.Size.Width - lblYorum[i].Size.Width - 20, 999);
                        lblYorum[i].Size = new Size(lbYorumlar.Items[i].ToString().Length * 8, lblYorum[i].Size.Height);
                        lblYorum[i].ForeColor = Color.White;
                        lblYorum[i].Location = new Point(konumXYedek + 10, konumYYedek + 20);
                        this.Controls.Add(lblYorum[i]);
                        lblYorum[i].BringToFront();

                        btnSil[i] = new Button();
                        btnSil[i].FlatStyle = FlatStyle.Flat;
                        btnSil[i].ForeColor = Color.White;
                        btnSil[i].BackColor = Color.Black;
                        btnSil[i].Text = "Yorumu sil";
                        btnSil[i].Location = new Point(gbYorumlar.Size.Width - 60, lblKullanici[i].Location.Y - (btnSil[i].Size.Height / 4));
                        if (hedef == kullaniciAdi)
                        {
                            this.Controls.Add(btnSil[i]);
                            btnSil[i].BringToFront();
                        }

                        void btnSil_Click(object asender, EventArgs a)
                        {
                            switch (ki.YorumuSil(hedef, yazarKullaniciAdi, kitapAdi, yorum))
                            {
                                case 1:
                                    GitapYorumlari gy = new GitapYorumlari(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                                    gy.MdiParent = this.MdiParent;
                                    this.Close();
                                    gy.Show();
                                    break;

                                case -1:
                                    MessageBox.Show("Yorumu silme esnasında bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        btnSil[i].Click += new EventHandler(btnSil_Click);

                        uzunluk += lblKullanici[i].Size.Height + lblYorum[i].Size.Height + 10;
                        konumYYedek = lblYorum[i].Location.Y + lblYorum[i].Size.Height + 10;
                    }
                }
                gbYorumlar.Size = new Size(this.Size.Width - (gbYorumlar.Location.X * 2) + 16, uzunluk + 16);

                timer1.Enabled = true;
            }
        }

        private void lblKullanici_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Otomatik Boyutlandırma
            NavBar navBar = new NavBar(kullaniciAdi);

            if (gbYorumlar.Location.Y + gbYorumlar.Size.Height + 20 > this.MdiParent.Size.Height - 45)
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, gbYorumlar.Location.Y + gbYorumlar.Size.Height + 10);
            }
            else
            {
                this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
            }
            int uzunluk = 0;
            for (int i = 0; i < lbKullanicilar.Items.Count; i++)
            {
                if (lbKullanicilar.Items[i] != null)
                {
                    lblYorum[i].MaximumSize = new Size(gbYorumlar.Size.Width - (lblYorum[i].Location.X * 2), 999);

                    btnSil[i].Location = new Point(gbYorumlar.Size.Width - 60, lblKullanici[i].Location.Y - (btnSil[i].Size.Height / 4));
                    uzunluk += lblKullanici[i].Size.Height + lblYorum[i].Size.Height + 10;
                }
            }
            gbYorumlar.Size = new Size(this.Size.Width - (gbYorumlar.Location.X * 2) + 16, uzunluk + 16);
            #endregion
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public GitapYorumlari(int FormYKonumu, string KullaniciAdi, string YazarKullaniciAdi, string KitapAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = KullaniciAdi;
            this.kitapAdi = KitapAdi;
            this.yazarKullaniciAdi = YazarKullaniciAdi;
            this.formYKonumu = FormYKonumu;
        }

        private void btnYorumPaylas_Click(object sender, EventArgs e)
        {
            KitapIslemleri ki = new KitapIslemleri();
            if (txtYorum.Text != "")
            {
                switch (ki.YorumuPaylas(kullaniciAdi, yazarKullaniciAdi, kitapAdi, txtYorum.Text))
                {
                    case 1:
                        GitapYorumlari gy = new GitapYorumlari(this.Location.Y, kullaniciAdi, yazarKullaniciAdi, kitapAdi);
                        gy.MdiParent = this.MdiParent;
                        this.Close();
                        gy.Show();
                        break;
                    case 2:
                        MessageBox.Show("Yorumu paylaşma esnasında bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Yorumu paylaşmak için herhangi bir şey yazmalısınız.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
