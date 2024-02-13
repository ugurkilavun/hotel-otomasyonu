using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotel_otomasyonu
{
    public partial class helps_form : Form
    {
        public helps_form()
        {
            InitializeComponent();
        }

        private void helps_form_Load(object sender, EventArgs e)
        {
            //richTextBox_oda_renkleri_ve_anlamlari.BackColor = Color.White;
            richTextBox_oda_renkleri_ve_anlamlari.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox_oda_renkleri_ve_anlamlari_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
