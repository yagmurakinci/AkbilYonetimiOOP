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
    public partial class FrmTalimatIslemleri : Form
    {
        public decimal YuklenecekMiktar { get; private set; }

        public FrmTalimatIslemleri()
        {
            InitializeComponent();
        }

        

        private void ComboBoxAkbilleriGetir()
        {
            try
            {
                

                   
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Beklenmedik bir hata oluştu! "+hata.Message);
                    //TODO: loglama txt dosyasına yazdır
                }
            
            
        }

        private void FrmTalimatIslemleri_Load(object sender, EventArgs e)
        {
            
            ComboBoxAkbilleriGetir();
            cmbBoxAkbiller.SelectedIndex = -1;
            cmbBoxAkbiller.Text = "Akbil Seçiniz";
            dataGridViewTalimatlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            timerBekleyenTalimat.Interval = 1000;
            timerBekleyenTalimat.Enabled = true;
            //metodu tekrar inceleyeceğiz
            GrideTalimatlariGetir();
            BekleyenTalimatSayisiniGetir(); // hata verdiği için yorum satırı yaptık
            dataGridViewTalimatlar.ContextMenuStrip = contextMenuStripTalimatGrid;
            groupBoxBakiye.Enabled = false;
        }

        private void cmbBoxAkbiller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxAkbiller.SelectedIndex>=0)
            {
                groupBoxBakiye.Enabled = true;
                txtBakiye.Clear();
            }
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            try
            {
                
                #region TalimatiKaydetKodlari


                string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                SqlCommand komutNesnesi = new SqlCommand();
                komutNesnesi.Connection = baglantiNesnesi;
                komutNesnesi.CommandText = $"insert into Talimatlar (OlustulmaTarihi,AkbilID,YuklenecekTutar,YuklendiMi,YuklendigiTarih) values (@olusTrh, @akbilid, @tutar,0, null)";

                komutNesnesi.Parameters.AddWithValue("@olusTrh",yeniTalimat.OlusturulmaTarihi);
                komutNesnesi.Parameters.AddWithValue("@akbilid",yeniTalimat.AkbilID);
                komutNesnesi.Parameters.AddWithValue("@tutar",yeniTalimat.YuklenecekMiktar);
                
                
                baglantiNesnesi.Open();
                if (komutNesnesi.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Talimat oluşturuldu!");
                }
                baglantiNesnesi.Close();
                cmbBoxAkbiller.SelectedIndex = -1;
                cmbBoxAkbiller.Text = "Akbil .seçiniz..";
                txtBakiye.Clear();
                groupBoxBakiye.Enabled = false;

                if (checkBoxBekleyenTalimatlar.Checked)
                {
                    GrideTalimatlariGetir(true);
                }
                else
                {
                    GrideTalimatlariGetir();
                }
                BekleyenTalimatSayisiniGetir();


                #endregion
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu" + hata.Message);
            }
        }
        private void GrideTalimatlariGetir (bool sadeceBekleyenleriGoster = false)
        {
            
        }

        private void checkBoxBekleyenTalimatlar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBekleyenTalimatlar.Checked)
            {
                GrideTalimatlariGetir(true);
            }
            else
            {
                GrideTalimatlariGetir(false);
            }
        }

        private void BekleyenTalimatSayisiniGetir()
        {
            try
            {
               
            }
            catch (Exception hata)
            {
                MessageBox.Show("Beklenmedik bir sorun oluştu!" + hata.Message);
                // hata log
            }

        }

        private void timerBekleyenTalimat_Tick(object sender, EventArgs e)
        {
           // if (lblBekleyenTalimat.Text!) "0")
            //{
                if (DateTime.Now.Second % 2==0)
                {
                    lblBekleyenTalimat.Font = new Font("Microsoft Sans Serif", 40);
                }
                else
                {
                    lblBekleyenTalimat.Font = new Font("Microsoft Sans Serif", 20);

                }
           // }
        }

        private void cikisyapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sistemden çıkılacak

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

        private void anaMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIslemler frmIslemler = new FrmIslemler();
            frmIslemler.Show();
            this.Close();
        }

        private void groupBoxBakiye_Enter(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void talimatigerceklestirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu! " + hata.Message);
            }
        }

        private void talimatiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu! " + hata.Message);
            }
        }
    }
}
