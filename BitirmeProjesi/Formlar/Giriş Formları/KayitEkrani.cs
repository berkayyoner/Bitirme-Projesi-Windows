using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitirmeProjesi.Formlar
{
    public partial class KayitEkrani : Form
    {
        public KayitEkrani()
        {
            InitializeComponent();
        }

        private void KayitEkrani_Load(object sender, EventArgs e)
        {
            #region Merkeze Konumlandırma
            this.Anchor = AnchorStyles.None;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();

            if (txtKullaniciAdi.Text != "" && txtSifre.Text != "" && txtEposta.Text != "" && txtAdi.Text != "" && txtSoyadi.Text != "" && txtTelNo.Text != "" && txtTelNo.TextLength == 10)
            {
                switch (gi.Kayit(txtKullaniciAdi.Text, txtSifre.Text, txtEposta.Text, txtAdi.Text, txtSoyadi.Text, dateTimePicker1.Value, Convert.ToInt64(txtTelNo.Text)))
                {
                    case -2:
                        MessageBox.Show("Bu kullanıcı adı kullanılmakta. Lütfen farklı bir kullanıcı adı deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtKullaniciAdi.BackColor = Color.Red;
                        break;
                    case 1:
                        MessageBox.Show("Kayıt Başarılı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GirisEkrani girisEkrani = new GirisEkrani();
                        girisEkrani.MdiParent = this.MdiParent;
                        this.Hide();
                        girisEkrani.Show();
                        break;
                }
            }
            else
            {
                //Forecolor'lar Red ve TextChanged durumunda tekrar White olmalı
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtTelNo.Text, "[^0-9]"))
            {
                txtTelNo.Text = txtTelNo.Text.Remove(txtTelNo.Text.Length - 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IlkEkran ilkEkran = new IlkEkran();
            ilkEkran.MdiParent = this.MdiParent;
            this.Close();
            ilkEkran.Show();
        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            txtKullaniciAdi.BackColor = Color.Black;
        }
    }
}
