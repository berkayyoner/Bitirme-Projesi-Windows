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
    public partial class IlkEkran : Form
    {
        public IlkEkran()
        {
            InitializeComponent();
        }

        private void IlkEkran_Load(object sender, EventArgs e)
        {
            #region Merkeze Konumlandırma
            this.Anchor = AnchorStyles.None;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisEkrani girisEkrani = new GirisEkrani();
            girisEkrani.MdiParent = this.MdiParent;
            this.Close();
            girisEkrani.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Formlar.KayitEkrani kayitEkrani = new Formlar.KayitEkrani();
            kayitEkrani.MdiParent = this.MdiParent;
            this.Close();
            kayitEkrani.Show();
        }
    }
}
