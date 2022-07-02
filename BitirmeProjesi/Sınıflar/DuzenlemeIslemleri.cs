using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BitirmeProjesi
{
    internal class DuzenlemeIslemleri
    {
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Gutuphane;Trusted_Connection=true;Timeout=2;");

        public int KitabiTamamla(string KitapAdi, string YazarAdi)
        {
            SqlCommand cmd = new SqlCommand("update Kitaplar set Durum = 'Tamamlandı' where KitapAdi = @ka and KullaniciAdi = @ya", baglanti);
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
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return -1;
            }
        }

        public int ProfilFotografiKaydet(string KullaniciAdi, string Fotograf)
        {
            SqlCommand cmd = new SqlCommand("update Kullanicilar set Fotograf = @ft where KullaniciAdi = @ka", baglanti);
            cmd.Parameters.AddWithValue("@ka", KullaniciAdi);
            cmd.Parameters.AddWithValue("@ft", Fotograf);

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
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return -1;
            }
        }

        public void DuzenleBilgileri(string KitapAdi, string YazarAdi, PictureBox resimKutusu, TextBox txtKitapAdi, ComboBox txtKitapTuru, TextBox txtKitapKonusu, TextBox txtEtiketler, TextBox txtFiyat, TextBox txtFotograf)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KitapAdi = @ka and KullaniciAdi = @ya", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtKitapAdi.Text = reader.GetString(2);
                    txtKitapTuru.Text = reader.GetString(3);
                    txtKitapKonusu.Text = reader.GetString(4);
                    txtEtiketler.Text = reader.GetString(6);
                    txtFiyat.Text = reader.GetSqlMoney(9).ToString();
                    txtFotograf.Text = reader.GetString(5);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch
            {
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public int DuzenlemeleriKaydet(string KitapAdi, string YazarAdi, PictureBox resimKutusu, TextBox txtKitapAdi, ComboBox txtKitapTuru, TextBox txtKitapKonusu, TextBox txtEtiketler, TextBox txtFiyat, TextBox txtFotograf)
        {
            SqlCommand cmd = new SqlCommand("update Kitaplar set KitapAdi = @txtKitapAdi, KitapTuru = @kt, KitapKonusu = @kk, Etiketler = @et, Fiyat = @fi, KapakFotografi = @kf where KitapAdi = @ka and KullaniciAdi = @ya", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@txtKitapAdi", txtKitapAdi.Text);
            cmd.Parameters.AddWithValue("@kt", txtKitapTuru.Text);
            cmd.Parameters.AddWithValue("@kk", txtKitapKonusu.Text);
            cmd.Parameters.AddWithValue("@et", txtEtiketler.Text);
            cmd.Parameters.AddWithValue("@fi", Convert.ToDouble(txtFiyat.Text));
            cmd.Parameters.AddWithValue("@kf", txtFotograf.Text);

            SqlCommand cmd2 = new SqlCommand("update Chapterlar set KitapAdi = @yenika where KitapAdi = @ka and Yazar = @ya", baglanti);
            cmd2.Parameters.AddWithValue("@yenika", txtKitapAdi.Text);
            cmd2.Parameters.AddWithValue("@ka", KitapAdi);
            cmd2.Parameters.AddWithValue("@ya", YazarAdi);

            try
            {
                baglanti.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                cmd.Dispose();
                cmd2.Dispose();
                baglanti.Close();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                cmd2.Dispose();
                baglanti.Close();
                return -1;
            }
        }

        public void BolumDuzenleBilgileri(string KitapAdi, string YazarAdi, string ChapterAdi, TextBox txtBaslik, TextBox txtIcerik, Label lblKitapAdi, Label lblChapterSayisi)
        {
            SqlCommand cmd = new SqlCommand("select * from Chapterlar where KitapAdi = @ka and Yazar = @ya and Baslik = @ba", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@ba", ChapterAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblKitapAdi.Text = reader.GetString(2);
                    txtBaslik.Text = reader.GetString(4);
                    txtIcerik.Text = reader.GetString(5);
                    lblChapterSayisi.Text = reader.GetString(3);
                }

                cmd.Dispose();
                reader.Close();
                baglanti.Close();
            }
            catch
            {
                cmd.Dispose();
                baglanti.Close();
            }
        }

        public int BolumDuzenlemeleriKaydet(string KitapAdi, string YazarAdi, string BolumAdi, TextBox txtBaslik, TextBox txtIcerik)
        {
            SqlCommand cmd = new SqlCommand("update Chapterlar set Baslik = @ba, Chapter = @ca where KitapAdi = @ka and Yazar = @ya and Baslik = @bol", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@ba", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@ca", txtIcerik.Text);
            cmd.Parameters.AddWithValue("@bol", BolumAdi);

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
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return -1;
            }
        }

        public int BolumSil(string KitapAdi, string YazarAdi, string BolumAdi)
        {
            SqlCommand cmd = new SqlCommand("delete from Chapterlar where KitapAdi = @ka and Yazar = @ya and Baslik = @bol", baglanti);
            cmd.Parameters.AddWithValue("@ka", KitapAdi);
            cmd.Parameters.AddWithValue("@ya", YazarAdi);
            cmd.Parameters.AddWithValue("@bol", BolumAdi);

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
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                baglanti.Close();
                return -1;
            }
        }
    }
}
