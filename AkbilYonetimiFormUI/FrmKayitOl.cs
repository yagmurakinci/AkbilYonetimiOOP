using AkbilYonetimBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkbilYonetimiFormUI
{
    public partial class FrmKayitOl : Form
    {
        public FrmKayitOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Beklenmedik hata oluştu! \n Hata: {ex.Message}","HATA BİLDİRİMİ",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void FrmKayitOl_Load(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '*';
            dtpDogumTarihi.MaxDate = new DateTime(2015, 12, 31);
            dtpDogumTarihi.Value = new DateTime(2015, 12, 31);
            
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Email = txtEmail.Text;
            frmGiris.Show();
        }

        private void FrmKayitOl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Email = txtEmail.Text;
            frmGiris.Show();
        }
    }
}
