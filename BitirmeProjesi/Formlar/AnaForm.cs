using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SpeechLib;

namespace BitirmeProjesi
{
    public partial class AnaForm : Form
    {
        public int sayfa = 0;
        public AnaForm()
        {
            InitializeComponent();

            #region Ana Form Renk
            foreach (Control control in this.Controls)
            {
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    client.BackColor = Color.FromArgb(30, 30, 30);
                    break;
                }
            }
            #endregion
        }

        private void BaslangicForm_Load(object sender, EventArgs e)
        {
            SpVoice oku = new SpVoice();
            //oku.Speak("Bismillahirrahmanirrahim", SpeechVoiceSpeakFlags.SVSFDefault);
        }

        private void BaslangicForm_Shown(object sender, EventArgs e)
        {
            GenelIslemler gi = new GenelIslemler();
            lblDurum.Text = "Durum: Sunucular gontrol ediliyor...";
            if (gi.SQLControl() == true)
            {
                lblDurum.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;

                lblDurum.Text = "Durum: Girişe yönlendiriliyorsunuz...";
                IlkEkran ilkEkran = new IlkEkran();
                ilkEkran.MdiParent = this;
                ilkEkran.Show();
            }
            else
            {
                lblDurum.Text = "Durum: Sunuculara bağlantı kurulamadı!";
            }
        }

        private void lblDurum_Click(object sender, EventArgs e)
        {

        }
    }
}
