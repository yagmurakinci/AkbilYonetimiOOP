﻿using AkbilYonetimBusinessLayer;
using AkbilYonetimiDataLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkbilYonetimiFormUI
{
    public partial class FrmAyarlar : Form
    {
        IVeriTabaniIslemleri veriTabaniIslemleri = new SQLVeriTabaniİslemleri();
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            dtpDogumTarihi.Format = DateTimePickerFormat.Short;
        }

        private void KullaniciBilgileriniDoldur()
        {
            try
            {
                //Isim, Soyisim, DogumTarihi
                var sonuc = veriTabaniIslemleri.VeriOku("Kullanicilar",new string[] { "Isim" , "Soyisim" , "DogumTarihi" }, $"Id= {GenelIslemler.GirisYapmisKullaniciID} ");

                txtIsim.Text = sonuc["Isim"].ToString();
                txtSoyisim.Text = sonuc["Soyisim"].ToString();
                dtpDogumTarihi.Text = sonuc["DogumTarihi"].ToString();



            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu" + hata.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIsim.Text==null || txtIsim.Text==string.Empty || txtSoyisim.Text == null || txtSoyisim.Text == string.Empty)
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurunuz!");
                    return;
                }

                Hashtable kolonlar = new Hashtable();
                kolonlar.Add("Isim", $"'{txtIsim.Text}'");
                kolonlar.Add("Soyisim", $"'{txtSoyisim.Text}'");
                kolonlar.Add("DogumTarihi", $"'{dtpDogumTarihi.Value.ToString ("yyyyMMdd")}'");

                int sonuc = veriTabaniIslemleri.KomutIsle(veriTabaniIslemleri.VeriGuncellemeCumlesiOlustur("Kullanicilar", kolonlar, $"Id= {GenelIslemler.GirisYapmisKullaniciID}"));
                if (sonuc>0)
                    MessageBox.Show("Bilgiler güncellendi");
                else
                    MessageBox.Show("Bilgi güncelleme işlemi BAŞARISIZ!");


            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu" + hata.Message); 
            }
        }

        private void anamenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIslemler frmIslemler = new FrmIslemler();
            //açık olan tüm formları gizleyecek
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Application.OpenForms[i].Hide();
            }

            frmIslemler.Show();
        }
    }
}
