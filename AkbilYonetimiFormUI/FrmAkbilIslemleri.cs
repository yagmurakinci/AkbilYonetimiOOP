﻿using AkbilYonetimBusinessLayer;
using AkbilYonetimiDataLayer;
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
        IVeriTabaniIslemleri veriTabaniIslemleri = new SQLVeriTabaniİslemleri();
        public FrmAkbilIslemleri()
        {
            InitializeComponent();
        }

        private void btnAkbilKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAkbilSeriNo.Text == null || txtAkbilSeriNo.Text == string.Empty)
                {
                    MessageBox.Show("HATA: Akbil Seri numarası boş geçilemez!");
                    return;
                }

                if (txtAkbilSeriNo.Text.Length != 16)
                {
                    MessageBox.Show("HATA: Akbil Seri 16 haneli olmalıdır!");
                    return;
                }

                foreach (char item in txtAkbilSeriNo.Text)
                {
                    if (!char.IsDigit(item))
                    {
                        throw new Exception("Akbil numarası sadece rakamlardan oluşmalıdır!");
                    }
                }
                Akbil yeniAkbil = new Akbil()
                {
                    AkbilSahibiID = GenelIslemler.GirisYapmisKullaniciID,
                    AkbilNo = txtAkbilSeriNo.Text,
                    Bakiye = 0,
                    KayitTarihi = DateTime.Now,
                    AkbilTipi = 0 // düzenlicez
                };
                yeniAkbil.SonKullanimTarihi = yeniAkbil.KayitTarihi.AddYears(5);

                Dictionary<string, object> kolonlar = new Dictionary<string, object>();
                kolonlar.Add("AkbilNo", $"'{yeniAkbil.AkbilNo}'");
                kolonlar.Add("Bakiye", 0);
                kolonlar.Add("KayitTarihi", $"'{yeniAkbil.KayitTarihi.ToString("yyyy-MM-dd HH:mm:ss")}'");
                kolonlar.Add("AkbilTipi", yeniAkbil.AkbilTipi);
                kolonlar.Add("SonKullanimTarihi", $"'{yeniAkbil.SonKullanimTarihi.ToString("yyyy-MM-dd HH:mm:ss")}'");
                kolonlar.Add("AkbilSahibiID", yeniAkbil.AkbilSahibiID);

                int eklenenAkbilSayisi = veriTabaniIslemleri.KomutIsle(
                    veriTabaniIslemleri.VeriEklemeCumlesiOlustur("Akbiller", kolonlar));
                if (eklenenAkbilSayisi>0)
                {
                    MessageBox.Show("Yeni Akbil Eklendi");
                    DataGridViewiDoldur();
                    txtAkbilSeriNo.Clear();
                    cmbBoxAkbilTipleri.SelectedIndex = -1;
                    cmbBoxAkbilTipleri.Text = "Akbil Tipi Seçiniz..";

                }
                else
                {
                    MessageBox.Show("Ekleme İşlemi Başarısız");
                }





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
            txtAkbilSeriNo.MaxLength = 16;
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
                dataGridViewAkbiller.DataSource = veriTabaniIslemleri.VeriGetir("Akbiller",kosullar:$"AkbilSahibiId={GenelIslemler.GirisYapmisKullaniciID}");
                //id alanı gizlensin

                dataGridViewAkbiller.Columns["AkbilSahibiID"].Visible = false;
                dataGridViewAkbiller.Columns["KayitTarihi"].Width = 200;
                dataGridViewAkbiller.Columns["SonKullanimTarihi"].Width = 200;
                dataGridViewAkbiller.Columns["AkbilNo"].Width = 200;

                dataGridViewAkbiller.Columns["AkbilNo"].HeaderText = "Akbil No";
                dataGridViewAkbiller.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";
                dataGridViewAkbiller.Columns["SonKullanimTarihi"].HeaderText =
                    "Son Kullanım Tarihi";
                dataGridViewAkbiller.Columns["AkbilTipi"].HeaderText =
                   "Akbil Tipi";
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
