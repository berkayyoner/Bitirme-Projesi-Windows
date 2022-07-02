using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace BitirmeProjesi
{
    internal class KitapIslemleri
    {
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Gutuphane;Trusted_Connection=true;Timeout=2;");

        public int TaslagiKaydet(string KullaniciAdi, string KitapAdi, string KitapTuru, string Icerik, string Etiketler, double Fiyat, string resimLink)
        {
            SqlCommand cmd = new SqlCommand("insert into Kitaplar (KullaniciAdi, KitapAdi, KitapTuru, KitapKonusu, Etiketler, Durum, OkunmaSayisi, Fiyat, KapakFotografi) values (@ka, @kit, @kt,@kk, @et, @dr, 0, @fi, @fp)", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            cmd.Parameters.AddWithValue("@kit", KitapAdi);
            cmd.Parameters.AddWithValue("@kt", KitapTuru);
            cmd.Parameters.AddWithValue("@kk", Icerik);
            cmd.Parameters.AddWithValue("@et", Etiketler);
            cmd.Parameters.AddWithValue("@dr", "Devam Ediyor");
            cmd.Parameters.AddWithValue("@fi", Fiyat);
            cmd.Parameters.AddWithValue("@fp", resimLink);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }

            SqlCommand cmdEkle = new SqlCommand("insert into AlinanKitaplar (KullaniciAdi, Yazar, KitapAdi) values (@kul, @yaz, @kit)", baglanti);
            cmdEkle.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmdEkle.Parameters.AddWithValue("@yaz", KullaniciAdi);
            cmdEkle.Parameters.AddWithValue("@kit", KitapAdi);

            try
            {
                baglanti.Open();

                cmdEkle.ExecuteNonQuery();

                baglanti.Close();

                return 1;
            }
            catch (Exception ex)
            {
                cmdEkle.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);

                return -1;
            }
        }

        public int KisininKitaplari(string KullaniciAdi, Form form, GroupBox Grup, Label yok, Label Paylasim)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KullaniciAdi = @ka order by ID desc", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            ListBox lb = new ListBox();

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    lb.Items.Add(reader.GetString(2));
                }

                Grup.Text = "Yazdığı Kitaplar";
                Paylasim.Text = lb.Items.Count.ToString();

                if (lb.Items.Count > 0)
                {
                    yok.Visible = false;
                    Grup.Size = new Size(Grup.Width, (lb.Items.Count * 38) + 20);
                    int lblKonum = Grup.Location.Y + yok.Location.Y;
                    for (int i = 0; i < lb.Items.Count; i++)
                    {
                        Label label = new Label();
                        label.Location = new Point(Grup.Location.X + yok.Location.X, lblKonum);
                        
                        label.ForeColor = Color.White;
                        label.Text = lb.Items[i].ToString();
                        form.Controls.Add(label);
                        label.BringToFront();

                        Button btn = new Button();
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.Text = "Görüntüle";
                        btn.BackColor = Color.Black;
                        btn.ForeColor = Color.White;
                        btn.Location = new Point(Grup.Location.X + Grup.Size.Width - label.Size.Width - yok.Location.X, lblKonum-5);
                        form.Controls.Add(btn);
                        btn.BringToFront();

                        lblKonum += 38;
                        string kitap = lb.Items[i].ToString();

                        btn.Click += new EventHandler(btn_Click);
                        void btn_Click(object sender, EventArgs e)
                        {
                            Gitap gt = new Gitap(form.Location.Y, KullaniciAdi, KullaniciAdi, kitap);
                            gt.MdiParent = form.MdiParent;
                            gt.Show();
                        }
                    }
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return lb.Items.Count;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void KitabiGoruntule(string KitapAdi, string YazarKullaniciAdi, Label lblKitapAdi, Label lblYazarAdi, Label lblKitapTuru, Label lblKitapKonusu, Label lblEtiketler, Label lblOkunmaSayisi, PictureBox pbKapakFotografi)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            string fotoLink = "";

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    lblYazarAdi.Text = reader.GetString(1);
                    lblKitapAdi.Text = reader.GetString(2);
                    lblKitapTuru.Text = reader.GetString(3);
                    lblKitapKonusu.Text = reader.GetString(4);
                    lblEtiketler.Text = reader.GetString(6);
                    lblOkunmaSayisi.Text = reader.GetInt64(8).ToString();
                    fotoLink = reader.GetString(5);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch(Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }
            if(fotoLink != "")
            {
                pbKapakFotografi.ImageLocation = fotoLink;
            }
        }
        public void Kitaplarim (string KullaniciAdi, ref ListBox lbKitapAdi, ref ListBox lbFotograf)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lbKitapAdi.Items.Add(reader.GetString(2));
                    lbFotograf.Items.Add(reader.GetString(5));
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void KitabiSil(string KullaniciAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("delete from Kitaplar where KitapAdi = @ka and KullaniciAdi = @kul", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public int ChapterSayisi(string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from Chapterlar where Yazar = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            ListBox lb = new ListBox();

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lb.Items.Add(reader.GetString(4));
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return lb.Items.Count + 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public int ChapterKaydet(string YazarAdi, string KitapAdi, int ChapterSayisi, TextBox Baslik, TextBox Chapter)
        {
            SqlCommand cmd = new SqlCommand("insert into Chapterlar (Yazar, KitapAdi, ChapterSayisi, Baslik, Chapter) values (@ya, @ka, @cs, @bas, @ch)", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@cs", ChapterSayisi);
            cmd.Parameters.AddWithValue("@bas", Baslik.Text);
            cmd.Parameters.AddWithValue("@ch", Chapter.Text);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        public int OkuSayfasi (string yazaradi , string kitapadi, ListBox lbChapterAdi, ListBox lbBolum)
        {
            SqlCommand cmd = new SqlCommand("select * from Chapterlar where Yazar = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", kitapadi);
            cmd.Parameters.AddWithValue("@ya", yazaradi);

            SqlCommand cmd2 = new SqlCommand("update Kitaplar set OkunmaSayisi = OkunmaSayisi + 1 where KitapAdi = @ka", baglanti);
            cmd2.Parameters.AddWithValue("@ka", kitapadi);

            try
            {
                baglanti.Open();
                cmd2.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lbChapterAdi.Items.Add(reader.GetString(4));
                    lbBolum.Items.Add(reader.GetString(5));
                }

                cmd.Dispose();
                cmd2.Dispose();
                reader.Close();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                cmd2.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int ChapterAdlari(string yazaradi, string kitapadi, ListBox lbChapterAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from Chapterlar where Yazar = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", kitapadi);
            cmd.Parameters.AddWithValue("@ya", yazaradi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lbChapterAdi.Items.Add(reader.GetString(4));
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int KacinciChapter(string YazarAdi, string KitapAdi, string ChapterAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from Chapterlar where Yazar = @ya and KitapAdi = @ka and Baslik = @ba", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@ba", ChapterAdi);

            int HangiChapter = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    HangiChapter = reader.GetInt32(3);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return HangiChapter;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public string KitapDurum(string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KullaniciAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            string Durum = "";

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Durum = reader.GetString(7);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return Durum;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void EtiketleriKaydet(string KullaniciAdi, string YazarAdi, string KitapTuru, string Etiketler)
        {
            SqlCommand sil = new SqlCommand("delete from Etiketler where KullaniciAdi = @ka and YazarAdi = @ya and KitapTuru = @kt", baglanti);
            sil.Parameters.AddWithValue("@ka", KullaniciAdi);
            sil.Parameters.AddWithValue("@ya", YazarAdi);
            sil.Parameters.AddWithValue("@kt", KitapTuru);

            SqlCommand cmd = new SqlCommand("insert into Etiketler (KullaniciAdi, YazarAdi, KitapTuru, Etiketler) values (@ka, @ya, @kt, @et)", baglanti);
            

            try
            {
                baglanti.Open();
                sil.ExecuteNonQuery();

                sil.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                sil.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }


            string[] Etk = Etiketler.Split(';');

            for (int i = 0; i < Etk.Length; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
                cmd.Parameters.AddWithValue("@ya", YazarAdi);
                cmd.Parameters.AddWithValue("@kt", KitapTuru);
                cmd.Parameters.AddWithValue("@et", Etk[i]);
                try
                {
                    baglanti.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    baglanti.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void FiyatKontrol(string KullaniciAdi, string YazarAdi, string KitapAdi, Label lblFiyat, Button btnOku)
        {
            SqlCommand cmd = new SqlCommand("select Fiyat from Kitaplar where KullaniciAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            double Fiyat = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fiyat = reader.GetSqlMoney(0).ToDouble();
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }

            if (Fiyat == 0)
            {
                btnOku.Text = "Oku";
                lblFiyat.Text = "Ücretsiz";
            }
            else if (Fiyat > 0)
            {
                btnOku.Text = "Satın Al";
                lblFiyat.Text = Fiyat.ToString() + " TL";
                 
                SqlCommand cmd2 = new SqlCommand("select * from AlinanKitaplar where KullaniciAdi = @kul and Yazar = @ya and KitapAdi = @ka", baglanti);
                cmd2.Parameters.AddWithValue("@kul", KullaniciAdi);
                cmd2.Parameters.AddWithValue("@ka", KitapAdi);
                cmd2.Parameters.AddWithValue("@ya", YazarAdi);

                bool varMi = false;

                try
                {
                    baglanti.Open();
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        if (reader2.GetInt32(0) != null)
                        {
                            varMi = true;
                        }
                    }

                    cmd.Dispose();
                    reader2.Close();
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    cmd.Dispose();
                    baglanti.Close();
                    MessageBox.Show(ex.Message);
                }
                if (varMi == true)
                {
                    btnOku.Text = "Oku";
                }
            }
            else
            {
                btnOku.Text = "Seyretme Dana!";
            }
        }

        public int SatinAl (string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmdFiyat = new SqlCommand("select Fiyat from Kitaplar where KullaniciAdi = @ka and KitapAdi = @kit", baglanti);
            cmdFiyat.Parameters.AddWithValue("@ka", YazarAdi);
            cmdFiyat.Parameters.AddWithValue("@kit", KitapAdi);

            double Fiyat = 0, Para = 0;
            
            try
            {
                baglanti.Open();
                SqlDataReader reader = cmdFiyat.ExecuteReader();

                while (reader.Read())
                {
                    Fiyat = reader.GetSqlMoney(0).ToDouble();
                }

                cmdFiyat.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmdFiyat.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }

            SqlCommand cmdPara = new SqlCommand("select Para from Kullanicilar where KullaniciAdi = @ka", baglanti);
            cmdPara.Parameters.AddWithValue("@ka", KullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader2 = cmdPara.ExecuteReader();

                while (reader2.Read())
                {
                    Para = reader2.GetSqlMoney(0).ToDouble();
                }

                reader2.Close();
                cmdPara.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmdPara.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }

            if (Para >= Fiyat)
            {
                SqlCommand cmdKullanici = new SqlCommand("update Kullanicilar set Para = Para - @fi where KullaniciAdi = @ka", baglanti);
                cmdKullanici.Parameters.AddWithValue("@ka", KullaniciAdi);
                cmdKullanici.Parameters.AddWithValue("@fi", Fiyat);

                try
                {
                    baglanti.Open();

                    cmdKullanici.ExecuteNonQuery();

                    cmdKullanici.Dispose();
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    cmdKullanici.Dispose();
                    baglanti.Close();
                    MessageBox.Show(ex.Message);
                }

                SqlCommand cmdYazar = new SqlCommand("update Kullanicilar set Para = Para + @fi where KullaniciAdi = @ka", baglanti);
                cmdYazar.Parameters.AddWithValue("@ka", YazarAdi);
                cmdYazar.Parameters.AddWithValue("@fi", (Fiyat / 10) * 9);

                try
                {
                    baglanti.Open();

                    cmdYazar.ExecuteNonQuery();

                    cmdYazar.Dispose();
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    cmdYazar.Dispose();
                    baglanti.Close();
                    MessageBox.Show(ex.Message);
                }

                SqlCommand cmdEkle = new SqlCommand("insert into AlinanKitaplar (KullaniciAdi, Yazar, KitapAdi) values (@kul, @yaz, @kit)", baglanti);
                cmdEkle.Parameters.AddWithValue("@kul", KullaniciAdi);
                cmdEkle.Parameters.AddWithValue("@yaz", YazarAdi);
                cmdEkle.Parameters.AddWithValue("@kit", KitapAdi);

                try
                {
                    baglanti.Open();

                    cmdEkle.ExecuteNonQuery();

                    cmdEkle.Dispose();
                    baglanti.Close();
                    return 1;
                }
                catch (Exception ex)
                {
                    cmdEkle.Dispose();
                    baglanti.Close();
                    MessageBox.Show(ex.Message);
                }
                return -2;
            }
            
            else
            {
                return -1;
            }
        }

        public bool BegeniKontrol (string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from BegenilenKitaplar where KullaniciAdi = @kul and YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            int ID = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }

            if (ID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Begen(string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("insert into BegenilenKitaplar (KullaniciAdi, YazarAdi, KitapAdi) values (@kul, @ya, @ka)", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();
                
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int BegenGeriAl(string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("delete from BegenilenKitaplar where KullaniciAdi = @kul and YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public bool BegenmemeKontrol(string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from BegenilmeyenKitaplar where KullaniciAdi = @kul and YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            int ID = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }

            if (ID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Begenme(string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("insert into BegenilmeyenKitaplar (KullaniciAdi, YazarAdi, KitapAdi) values (@kul, @ya, @ka)", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int BegenmeGeriAl(string KullaniciAdi, string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("delete from BegenilmeyenKitaplar where KullaniciAdi = @kul and YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int KacBegeni (string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select count(KullaniciAdi) from BegenilenKitaplar where YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            int Begeni = 0;

            try
            {
                baglanti.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Begeni = reader.GetInt32(0);
                }

                cmd.Dispose();
                baglanti.Close();
                return Begeni;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int KacBegenmeme(string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select count(KullaniciAdi) from BegenilmeyenKitaplar where YazarAdi = @ya and KitapAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            int Begenmeme = 0;

            try
            {
                baglanti.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Begenmeme = reader.GetInt32(0);
                }

                cmd.Dispose();
                baglanti.Close();
                return Begenmeme;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int YorumuPaylas (string KullaniciAdi, string YazarAdi, string KitapAdi, string Yorum)
        {
            SqlCommand cmd = new SqlCommand("insert into KitapYorumlari (KullaniciAdi, YazarAdi, KitapAdi, Yorum) values (@kul, @ya, @ka, @yo)", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@yo", Yorum);

            try
            {
                baglanti.Open();

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public void Yorumlar(string YazarAdi, string KitapAdi, ref ListBox Kullanicilar, ref ListBox Yorumlar)
        {
            SqlCommand cmd = new SqlCommand("select * from KitapYorumlari where YazarAdi = @ya and KitapAdi = @ka order by ID desc", baglanti);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Kullanicilar.Items.Add(reader.GetString(1));
                    Yorumlar.Items.Add(reader.GetString(4));
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public int YorumuSil(string KullaniciAdi, string YazarAdi, string KitapAdi, string Yorum)
        {
            SqlCommand cmd = new SqlCommand("delete from KitapYorumlari where KullaniciAdi = @kul and YazarAdi = @ya and KitapAdi = @ka and Yorum = @yo", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@yo", Yorum);

            try
            {
                baglanti.Open();

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public void BegenilenKitaplar(string KullaniciAdi, ref ListBox YazarAdi, ref ListBox KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from BegenilenKitaplar where KullaniciAdi = @ka order by ID desc", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    YazarAdi.Items.Add(reader.GetString(2));
                    KitapAdi.Items.Add(reader.GetString(3));
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public string KitapKapakFotografi(string YazarAdi, string KitapAdi)
        {
            SqlCommand cmd = new SqlCommand("select KapakFotografi from Kitaplar where KullaniciAdi = @kul and KitapAdi = @ka order by ID desc", baglanti);
            cmd.Parameters.AddWithValue("@kul", YazarAdi);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            string fotograf = "";

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fotograf = reader.GetString(0);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return fotograf;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return "";
            }
        }
    }
}