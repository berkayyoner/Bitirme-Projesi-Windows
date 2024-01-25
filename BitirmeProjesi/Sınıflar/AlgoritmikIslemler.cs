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

		public void Begenebilecekleriniz(string KullaniciAdi, ref ListBox YazarAdlari, ref ListBox KitapAdlari, ref ListBox LbResim)
		{
			SqlCommand cmd = new SqlCommand("exec Begenebilecekleriniz @kul", baglanti);
			cmd.Parameters.AddWithValue("@kul", KullaniciAdi);

			try
			{
				baglanti.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					YazarAdlari.Items.Add(reader.GetString(1));
					KitapAdlari.Items.Add(reader.GetString(2));
					LbResim?.Items.Add(reader.GetString(5));
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

		public void AktifSureGuncelle(string KullaniciAdi)
		{
			SqlCommand cmd1 = new SqlCommand("select * from GunlukAktifSureler where KullaniciAdi = @kul", baglanti);
			cmd1.Parameters.AddWithValue("@kul", KullaniciAdi);

			SqlCommand cmd2 = new SqlCommand("select * from HaftalikAktifSureler where KullaniciAdi = @kul", baglanti);
			cmd1.Parameters.AddWithValue("@kul", KullaniciAdi);

			int gunlukSure = -1;
			int haftalikSure = -1;
			DateTime guncelTarih = DateTime.UtcNow;
			DateTime gunlukKayitliTarih = DateTime.UtcNow;
			DateTime haftalikKayitliTarih = DateTime.UtcNow;

			try
			{
				baglanti.Open();
				SqlDataReader reader1 = cmd1.ExecuteReader();

				while (reader1.Read())
				{
					gunlukSure = reader1.GetInt32(2);
					gunlukKayitliTarih = reader1.GetDateTime(3);
				}
				cmd1.Dispose();
				reader1.Close();

				SqlDataReader reader2 = cmd2.ExecuteReader();

				while (reader2.Read())
				{
					haftalikSure = reader2.GetInt32(2);
					haftalikKayitliTarih = reader2.GetDateTime(3);
				}

				cmd2.Dispose();
				reader2.Close();
				baglanti.Close();
			}
			catch (Exception ex)
			{
				cmd1.Dispose();
				cmd2.Dispose();
				baglanti.Close();
				MessageBox.Show(ex.Message);
			}

			if (gunlukSure > -1)
			{
				if (gunlukKayitliTarih.Subtract(guncelTarih).Days >= 1)
				{
					SqlCommand cmd5 = new SqlCommand("delete from GunlukAktifSureler where KullaniciAdi = @kul", baglanti);
					cmd5.Parameters.AddWithValue("@kul", KullaniciAdi);
					try
					{
						baglanti.Open();
						cmd5.ExecuteNonQuery();

						cmd5.Dispose();
						baglanti.Close();
					}
					catch (Exception ex)
					{
						cmd5.Dispose();
						baglanti.Close();
						MessageBox.Show(ex.Message);
					}
				}
				else
				{
					SqlCommand cmd3 = new SqlCommand("update GunlukAktifSureler set AktifSure = @aks where KullaniciAdi = @kul", baglanti);
					cmd3.Parameters.AddWithValue("@kul", KullaniciAdi);
					cmd3.Parameters.AddWithValue("@aks", gunlukSure + 1);
					try
					{
						baglanti.Open();
						cmd3.ExecuteNonQuery();

						cmd3.Dispose();
						baglanti.Close();
					}
					catch (Exception ex)
					{
						cmd3.Dispose();
						baglanti.Close();
						MessageBox.Show(ex.Message);
					}
				}
			}
			else
			{
				SqlCommand cmd4 = new SqlCommand("insert into GunlukAktifSureler (KullaniciAdi, AktifSure, [Ilk Kayit]) values (@ka, aks, ik)", baglanti);
				cmd4.Parameters.AddWithValue("@kul", KullaniciAdi);
				cmd4.Parameters.AddWithValue("@aks", 0);
				cmd4.Parameters.AddWithValue("@ik", guncelTarih);

				try
				{
					baglanti.Open();
					cmd4.ExecuteNonQuery();

					cmd4.Dispose();
					baglanti.Close();
				}
				catch (Exception ex)
				{
					cmd4.Dispose();
					baglanti.Close();
					MessageBox.Show(ex.Message);
				}
			}

			if (haftalikSure > -1)
			{
				if (haftalikKayitliTarih.Subtract(guncelTarih).Days > 7)
				{
					SqlCommand cmd6 = new SqlCommand("delete from HaftalikAktifSureler where KullaniciAdi = @kul", baglanti);
					cmd6.Parameters.AddWithValue("@kul", KullaniciAdi);
					try
					{
						baglanti.Open();
						cmd6.ExecuteNonQuery();

						cmd6.Dispose();
						baglanti.Close();
					}
					catch (Exception ex)
					{
						cmd6.Dispose();
						baglanti.Close();
						MessageBox.Show(ex.Message);
					}
				}
				else
				{
					SqlCommand cmd7 = new SqlCommand("update HaftalikAktifSureler set AktifSure = @aks where KullaniciAdi = @kul", baglanti);
					cmd7.Parameters.AddWithValue("@kul", KullaniciAdi);
					cmd7.Parameters.AddWithValue("@aks", gunlukSure + 1);
					try
					{
						baglanti.Open();
						cmd7.ExecuteNonQuery();

						cmd7.Dispose();
						baglanti.Close();
					}
					catch (Exception ex)
					{
						cmd7.Dispose();
						baglanti.Close();
						MessageBox.Show(ex.Message);
					}
				}
			}
			else
			{
				SqlCommand cmd8 = new SqlCommand("insert into HaftalikAktifSureler (KullaniciAdi, AktifSure, [Ilk Kayit]) values (@ka, aks, ik)", baglanti);
				cmd8.Parameters.AddWithValue("@kul", KullaniciAdi);
				cmd8.Parameters.AddWithValue("@aks", 0);
				cmd8.Parameters.AddWithValue("@ik", guncelTarih);

				try
				{
					baglanti.Open();
					cmd8.ExecuteNonQuery();

					cmd8.Dispose();
					baglanti.Close();
				}
				catch (Exception ex)
				{
					cmd8.Dispose();
					baglanti.Close();
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
