using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BitirmeProjesi
{
    internal class AlgoritmikIslemler
    {
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Gutuphane;Trusted_Connection=true;Timeout=2;");

        public void SonEklenenler(ref ListBox LbKitapAdi, ref ListBox LbYazarAdi, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select TOP 10 c.ID, c.Yazar, c.KitapAdi, k.KapakFotografi from Chapterlar c inner join Kitaplar k on c.KitapAdi = k.KitapAdi order by c.ID desc", baglanti);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LbKitapAdi.Items.Add(reader.GetString(2));
                    LbYazarAdi.Items.Add(reader.GetString(1));
                    LbResim.Items.Add(reader.GetString(3));
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

        public void EnCokOkunanlar (ref ListBox LbKitapAdi, ref ListBox LbYazarAdi, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select TOP 10 OkunmaSayisi, KullaniciAdi, KitapAdi, KapakFotografi from Kitaplar order by OkunmaSayisi desc", baglanti);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LbKitapAdi.Items.Add(reader.GetString(2));
                    LbYazarAdi.Items.Add(reader.GetString(1));
                    LbResim.Items.Add(reader.GetString(3));
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

        public void TakipEdilenlerdenKitaplar(string KullaniciAdi, ref ListBox YazarAdlari, ref ListBox KitapAdlari, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select c.KullaniciAdi, c.KitapAdi, c.KapakFotografi from Kitaplar c inner join Takip t on c.KullaniciAdi = t.YazarAdi where t.KullaniciAdi = @kul order by c.ID desc", baglanti);
            cmd.Parameters.AddWithValue("@kul", KullaniciAdi);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    YazarAdlari.Items.Add(reader.GetString(0));
                    KitapAdlari.Items.Add(reader.GetString(1));
                    LbResim?.Items.Add(reader.GetString(2));
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
    }
}
