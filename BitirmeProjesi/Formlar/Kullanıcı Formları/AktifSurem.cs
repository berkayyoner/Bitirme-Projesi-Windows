using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitirmeProjesi.Formlar.Kullanıcı_Formları
{
	public partial class AktifSurem : Form
	{
		string kullaniciAdi = "";
		int formYKonumu = 0, gunlukSure = -1, haftalikSure = -1, ortalamaSure = -1;
		public AktifSurem(int FormYKonumu, string KullaniciAdi)
		{
			InitializeComponent();
			this.kullaniciAdi = KullaniciAdi;
			this.formYKonumu = FormYKonumu;
		}

		private void AktifSurem_Load(object sender, EventArgs e)
		{
			#region NavBar'a Yanaştırma
			NavBar navBar = new NavBar(kullaniciAdi);
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.Location = new Point(navBar.Size.Width, formYKonumu);
			this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 20, this.MdiParent.Size.Height - 45);
			#endregion

			timer1.Enabled = true;
			GenelIslemler gi = new GenelIslemler();
			lblHosgeldin.Text = "Hoşgeldin, " + gi.AdiNe(kullaniciAdi);
			lblHosgeldin.Location = new Point((this.Size.Width/2)-(lblHosgeldin.Size.Width/2), lblHosgeldin.Location.Y);

			gunlukSure = gi.GunlukAktifSureAl(kullaniciAdi);
			haftalikSure = gi.HaftalikAktifSureAl(kullaniciAdi);
			ortalamaSure = gi.OrtalamaAktifSureAl();

			lblGun.Text = (gunlukSure / 60).ToString() + " Saat " + (gunlukSure % 60).ToString() + " Dakika";
			lblHafta.Text = (haftalikSure / 3600).ToString() + " Gün " + ((haftalikSure % 3600)/60).ToString() + " Saat " + (haftalikSure % 60).ToString() + " Dakika";
			string ortalamaSureYazisi = (ortalamaSure / 3600).ToString() + " Gün " + ((ortalamaSure % 3600) / 60).ToString() + " Saat " + (ortalamaSure % 60).ToString() + " Dakika";

			if (gunlukSure < 120)
			{
				lblGunlukDegerlendirme.Text = "Gözlerin bir kartal kadar keskin, zihninin çarkları bir saat gibi dönüyor. Kitap okuma, bu zekayı keskinleştirme ve eğlenme zamanı!";
			}
			else if (gunlukSure >= 120)
			{
				lblGunlukDegerlendirme.Text = "Bu günlük uygulamada bilimsel olarak tavsiye edilen günlük kitap okuma vaktini yani 2 saati doldurdun. Ne kadar devam edeceğinin farkında olarak devam etmen gözlerinin yorulmaması açısından iyi olacaktır.";
			}
			else if (gunlukSure >= 240)
			{
				lblGunlukDegerlendirme.Text = "Bu gün uygulamada 4 saati aşkın bir vakit geçirdin ve gözlerin bu süreçte yorulmuş olabilir. Lütfen göz kaslarının sağlığı için yorulduğunu düşünüyorsan dinlen. Seni düşünüyorum :)";
			}
			else if (gunlukSure >= 360)
			{
				lblGunlukDegerlendirme.Text = "Bu gün uygulamada 6 saati aşkın bir vakit geçirdin ve gözlerinin yanı sıra beynin de yorulmuş olmalı. Lütfen sağlığın için gözlerini kapatmayı ve genel olarak dinlenmeyi denemelisin.";
			}


			if (haftalikSure < 210)
			{
				lblHaftalikDegerlendirme.Text = "Bu haftalık ortalama alt limitine yani 3,5 saate henüz ulaşmadın. Okumaya devam kitap kurdu!";
			}
			else if (haftalikSure >= 210)
			{
				lblHaftalikDegerlendirme.Text = "Bu haftanın alt limitini doldurdun. Kitap okumayı ve yazmayı seviyorsun anlaşılan. Seninle gurur duyuyorum.";
			}
			else if (haftalikSure >= 840)
			{
				lblHaftalikDegerlendirme.Text = "Bu hafta toplam 14 saati aşkın kitap okudun. Sınırları aşıyorsun anlaşılan. Sen özelsin ;)";
			}
			else if (haftalikSure >= 1680)
			{
				lblHaftalikDegerlendirme.Text = "Bu hafta toplam 28 saati aştın ve yorgunluğa aldırış etmiyor olabilirsin ama lütfen sosyalliğine ve sağlığına dikkat et. Biliyorsun seni seviyoruz.";
			}
			else if (haftalikSure >= 3200)
			{
				lblHaftalikDegerlendirme.Text = "Sağlığını önemsemiyor olabilirsin ama en azından çık dışarıya ve biraz çimenlere dokun. Ciddiyim :(";
			}

			if (haftalikSure < (ortalamaSure/2))
			{
				lblGenelDegerlendirme.Text = "Diğer kullanıcılar şu anda haftalık " + ortalamaSureYazisi + " ortalamaya sahip ve bu ortalamaya yakın bile değilsin. Okumaya devam!";
			}
			else if (haftalikSure >= (ortalamaSure / 2))
			{
				lblGenelDegerlendirme.Text = "Diğer kullanıcılar şu anda haftalık " + ortalamaSureYazisi + " ortalamaya sahip ve bu değere yakınsın. İyi gidiyorsun!";
			}
			else if (haftalikSure >= (ortalamaSure * 3 / 4))
			{
				lblGenelDegerlendirme.Text = "Diğer kullanıcılar şu anda haftalık " + ortalamaSureYazisi + " ortalamaya sahip ve bu değere çok yakınsın. Harika gidiyorsun!";
			}
			else if (haftalikSure > ortalamaSure)
			{
				lblGenelDegerlendirme.Text = "Diğer kullanıcılar şu anda haftalık " + ortalamaSureYazisi + " ortalamaya sahip ve bu değerin üzerindesin. Sen tam bir kitap kurdusun!";
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
			this.Size = new Size(this.MdiParent.Size.Width - navBar.Size.Width - 40, this.MdiParent.Size.Height - 45);
			#endregion
		}
	}
}
