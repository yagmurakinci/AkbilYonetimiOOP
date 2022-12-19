using AkbilYonetimBusinessLayer;
using AkbilYonetimiEntityLayer.Entities;
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
    public partial class FrmAkbilIslemleri : Form
    {
        public FrmAkbilIslemleri()
        {
            InitializeComponent();
        }

        private void btnAkbilKaydet_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik hata oluştu. Mesaj:" + hata.Message);
            }
        }

        private void cmbBoxAkbilTipleri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxAkbilProps_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAkbilIslemleri_Load(object sender, EventArgs e)
        {
            AkbilTipiComboyuDoldur();
            DataGridViewiDoldur();
            dataGridViewAkbiller.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void AkbilTipiComboyuDoldur()
        {
            //ComboBox'ın dolması
            cmbBoxAkbilTipleri.DataSource = Enums.AkbilTipleriniGetir();
            //cmbBoxAkbilTipleri.DisplayMember = "";
            //cmbBoxAkbilTipleri.ValueMember = "";
            cmbBoxAkbilTipleri.Text = "Akbil Tipi Seçiniz";
        }
        private void DataGridViewiDoldur()
        {
            try
            {
 
                
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu!");
                    //TODO: loglama txt dosyasına yazdır
            }
        }

        private void dataGridViewAkbiller_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cikisyapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenelIslemler.GirisYapmisKullaniciAdSoyad = string.Empty;
            GenelIslemler.GirisYapmisKullaniciID = 0;
            MessageBox.Show("Güle Güle");
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Show();
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == "FrmGiris") continue;
                Application.OpenForms[i].Close();
            }
        }
    }
}
