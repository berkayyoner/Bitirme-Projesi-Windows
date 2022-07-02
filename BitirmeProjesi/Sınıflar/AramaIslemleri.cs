using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BitirmeProjesi
{
    internal class AramaIslemleri
    {
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Gutuphane;Trusted_Connection=true;Timeout=2;");

        public void KitapAra(string KitapAdi, ref ListBox LbKitapAdi, ref ListBox LbYazarAdi, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KitapAdi like @ka order by OkunmaSayisi desc", baglanti);
            cmd.Parameters.AddWithValue("@ka", "%" + KitapAdi + "%");


            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LbKitapAdi.Items.Add(reader.GetString(2));
                    LbYazarAdi.Items.Add(reader.GetString(1));
                    LbResim.Items.Add(reader.GetString(5));
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

        public void YazarAra(string YazarAdi, ref ListBox LbKitapAdi, ref ListBox LbYazarAdi, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KullaniciAdi like @ka order by OkunmaSayisi desc", baglanti);
            cmd.Parameters.AddWithValue("@ka", "%" + YazarAdi + "%");


            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LbKitapAdi.Items.Add(reader.GetString(2));
                    LbYazarAdi.Items.Add(reader.GetString(1));
                    LbResim.Items.Add(reader.GetString(5));
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

        public void TureGoreAra(string KitapTürü, ref ListBox LbKitapAdi, ref ListBox LbYazarAdi, ref ListBox LbResim)
        {
            SqlCommand cmd = new SqlCommand("select * from Kitaplar where KitapTuru = @kt order by OkunmaSayisi desc", baglanti);
            cmd.Parameters.AddWithValue("@kt", KitapTürü);

            try
            {
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LbKitapAdi.Items.Add(reader.GetString(2));
                    LbYazarAdi.Items.Add(reader.GetString(1));
                    LbResim.Items.Add(reader.GetString(5));
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
