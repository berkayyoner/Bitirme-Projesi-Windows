using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using SpeechLib;

namespace BitirmeProjesi
{
    class GenelIslemler
    {
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Gutuphane;Trusted_Connection=true;Timeout=2;");
        public bool SQLControl() //Sunucunun açık olduğunu kontrol eden fonksiyon
        {
            bool sunucuDurum = false;
            try
            {
                baglanti.Open();
                sunucuDurum = true;
                baglanti.Close();
            }
            catch
            {
                baglanti.Close();
            }
            return sunucuDurum;
        }

        public int Giris(string kullaniciAdi, string sifre) //Giriş yapmak için kullanılan algoritma
        {
            SqlCommand cmd1 = new SqlCommand("select KullaniciAdi from Kullanicilar where KullaniciAdi = @user", baglanti);
            cmd1.Parameters.AddWithValue("@user", kullaniciAdi);
            SqlCommand cmd2 = new SqlCommand("select Sifre from Kullanicilar where KullaniciAdi = @user", baglanti);
            cmd2.Parameters.AddWithValue("@user", kullaniciAdi);
            string kullanici = "", sifresi = "";

            try
            {
                baglanti.Open();
                SqlDataReader reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    kullanici = reader1.GetString(0);
                }

                reader1.Close();
                cmd1.Dispose();
                baglanti.Close();
            }
            catch
            {
                cmd1.Dispose();
                baglanti.Close();
                return 1;
            }

            if (kullanici != "" && kullanici == kullaniciAdi)
            {
                try
                {
                    baglanti.Open();
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        sifresi = reader2.GetString(0);
                    }

                    reader2.Close();
                    cmd2.Dispose();
                    baglanti.Close();
                }
                catch
                {
                    cmd2.Dispose();
                    baglanti.Close();
                    return 2;
                }
            }

            if (kullanici == kullaniciAdi && sifresi == sifre)
            {
                return 3;
            }
            else if (kullanici == kullaniciAdi && sifresi != sifre)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }

        public int Kayit(string kullaniciAdi, string sifre, string eposta, string adi, string soyadi, DateTime dogumTarihi, Int64 telno )
        {
            string kontrolStr = "";

            SqlCommand cmd = new SqlCommand("insert into Kullanicilar (KullaniciAdi, Sifre, [E-Posta], Adi, Soyadi, [Dogum Tarihi], No, [Kayit Tarihi], Yetki, Para)" +
                "values (@ka, @si, @ep, @a, @so, @dt, @no, @kt, @yt, 0)", baglanti);
            cmd.Parameters.AddWithValue("@ka", kullaniciAdi);
            cmd.Parameters.AddWithValue("@si", sifre);
            cmd.Parameters.AddWithValue("@ep", eposta);
            cmd.Parameters.AddWithValue("@a", adi);
            cmd.Parameters.AddWithValue("@so", soyadi);
            cmd.Parameters.AddWithValue("@dt", dogumTarihi);
            cmd.Parameters.AddWithValue("@no", telno);
            cmd.Parameters.AddWithValue("@kt", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@yt", "Kullanici");

            SqlCommand kullaniciKontrol = new SqlCommand("select KullaniciAdi from Kullanicilar where KullaniciAdi = @ka", baglanti);
            kullaniciKontrol.Parameters.AddWithValue("@ka", kullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = kullaniciKontrol.ExecuteReader();

                while(reader.Read())
                {
                    kontrolStr = reader.GetString(0);
                }

                reader.Close();
                kullaniciKontrol.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                baglanti.Close();
                MessageBox.Show(ex.Message);
                return 0;
            }

            if (kontrolStr == "")
            {
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
            else
            {
                return -2;
            }
        }

        public string AdiNe(string kullaniciAdi)
        {
            string adi = "";

            SqlCommand cmd = new SqlCommand("select * from Kullanicilar where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", kullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader.GetString(4) + " " + reader.GetString(5);
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return adi;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return "";
            }
        }

        public void ProfilBilgileri(string kullaniciAdi, Label lblAdi, Label lblSoyadi, Label lblEposta, Label lblKayitTarihi, PictureBox pbFotograf)
        {
            SqlCommand cmd = new SqlCommand("select * from Kullanicilar where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", kullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblAdi.Text = reader.GetString(4);
                    lblSoyadi.Text = reader.GetString(5);
                    lblEposta.Text = reader.GetString(3);
                    lblKayitTarihi.Text = reader.GetDateTime(8).ToString();
                    pbFotograf.ImageLocation = reader.GetString(11);
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public void SesliOku(Label OkunacakTextBox)
        {
            SpVoice oku = new SpVoice();
            oku.Speak(OkunacakTextBox.Text, SpeechVoiceSpeakFlags.SVSFDefault);
        }

        public void CuzdanBilgileri(string KullaniciAdi, Label lblPara)
        {
            SqlCommand cmd = new SqlCommand("select Para from Kullanicilar where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblPara.Text = reader.GetSqlMoney(0).ToString() + " TL";
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public void ParaYukle(string KullaniciAdi, double YuklenecekMiktar)
        {
            SqlCommand cmd = new SqlCommand("update Kullanicilar set Para = Para + @ym where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ym", YuklenecekMiktar);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public void ParayiSifirla(string KullaniciAdi)
        {
            SqlCommand cmd = new SqlCommand("update Kullanicilar set Para = 0 where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public void SadeceSayi(TextBox textBox)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, @"[^0-9^\,^]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        public long ToplamOkunma(string KullaniciAdi)
        {
            SqlCommand cmd = new SqlCommand("select sum(OkunmaSayisi) from Kitaplar where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            long ToplamOkunma = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ToplamOkunma = reader.GetInt64(0);
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return ToplamOkunma;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return 0;
            }
        }

        public int TakipciSayisi(string KullaniciAdi)
        {
            SqlCommand cmd = new SqlCommand("select count(YazarAdi) from Takip where YazarAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            int Takipciler = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Takipciler = reader.GetInt32(0);
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return Takipciler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return Takipciler;
            }
        }

        public int TakipEdilenSayisi(string KullaniciAdi)
        {
            SqlCommand cmd = new SqlCommand("select count(YazarAdi) from Takip where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            int TakipEdilen = 0;

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TakipEdilen = reader.GetInt32(0);
                }
                cmd.Dispose();
                reader.Close();
                baglanti.Close();
                return TakipEdilen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return TakipEdilen;
            }
        }

        public bool TakipKontrol(string KullaniciAdi, string YazarAdi)
        {
            SqlCommand cmd = new SqlCommand("select * from Takip where KullaniciAdi = @kul and YazarAdi = @ya", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
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

        public int TakipEt(string KullaniciAdi, string YazarAdi)
        {
            SqlCommand cmd = new SqlCommand("insert into Takip (KullaniciAdi, YazarAdi) values (@kul, @ya)", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
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

        public int TakibiGeriAl(string KullaniciAdi, string YazarAdi)
        {
            SqlCommand cmd = new SqlCommand("delete from Takip where KullaniciAdi = @kul and YazarAdi = @ya", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);
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
    }
}