using AkbilYonetimBusinessLayer;
using AkbilYonetimiDataLayer;
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
    public partial class FrmGiris : Form
    {
        public string Email { get; set; }
        public FrmGiris()
        {
            InitializeComponent();
        }
        IVeriTabaniIslemleri VeriTabaniIslemleri = new SQLVeriTabaniİslemleri();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisYap();
        }

        private void GirisYap()
        {
            try
            {
                foreach (var item in Controls)
                {
                    if (item is TextBox && (((TextBox)item).Text == null || ((TextBox)item).Text == string.Empty))
                    {
                        MessageBox.Show("Zorunlu alanlar boş geçilemez! ");
                        return; 
                    }
                }
                //uzun yol
                string[] istedigimKolonlar = new string[] { "Email", "Parola","Isim","Soyisim" ,"Id"};
                string kosul = $"Email='{txtEmail.Text}' and Parola = '{GenelIslemler.MD5Encryption(txtSifre.Text)}'";

                var sonuc = VeriTabaniIslemleri.VeriOku("Kullanicilar", istedigimKolonlar, kosul);


                //daha kısa
                //var sonuc = VeriTabaniIslemleri.VeriOku("Kullanıcılar", new string[] { "Email", "Parola","Id" }, $"where Email={txtEmail} and Parola= {GenelIslemler.MD5Encryption(txtSifre.Text)}";

                if (sonuc.Count==0)
                {
                    MessageBox.Show("Kullanıcı adınız ya da şifreniz yanloştır! Lütfen tekrar deneyiniz!", "HATA",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
                else
                {
                    GenelIslemler.GirisYapmisKullaniciID = Convert.ToInt32(sonuc["Id"]);
                    GenelIslemler.GirisYapmisKullaniciAdSoyad = $"{sonuc["Isim"]} {sonuc["Soyisim"]}";

                    MessageBox.Show($"Hoşgeldiniz.... {GenelIslemler.GirisYapmisKullaniciAdSoyad}");
                    if (checkBoxBeniHatirla.Checked)
                    {
                        Properties.Settings.Default.KullaniciEmail = txtEmail.Text;
                        Properties.Settings.Default.KullaniciSifre = txtSifre.Text;
                        AkbilYonetimiFormUI.Properties.Settings.Default.BeniHatirla = true;
                        Properties.Settings.Default.Save();
                    }
                    this.Hide();
                    FrmIslemler frmIslemler = new FrmIslemler();
                    frmIslemler.Show();
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik hata oluştu! HATA:" + hata.Message);
            }
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            
            FrmKayitOl frmKayitOl = new FrmKayitOl();
            frmKayitOl.Show();
            this.Close();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            if (Email !=null)
            {
                txtEmail.Text = Email;
            }
            txtEmail.TabIndex = 1;
            txtSifre.TabIndex = 2;
            btnGirisYap.TabIndex = 3;
            btnKayitOl.TabIndex = 4;


            if (AkbilYonetimiFormUI.Properties.Settings.Default.BeniHatirla)
            {
                txtEmail.Text = AkbilYonetimiFormUI.Properties.Settings.Default.KullaniciEmail;
                txtSifre.Text = AkbilYonetimiFormUI.Properties.Settings.Default.KullaniciSifre;
                checkBoxBeniHatirla.Checked = true;
            }

        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                GirisYap();
            }
        }

        private void checkBoxBeniHatirla_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBeniHatirla.Checked)
            {
                AkbilYonetimiFormUI.Properties.Settings.Default.BeniHatirla = true;
            }
            else
            {
                AkbilYonetimiFormUI.Properties.Settings.Default.BeniHatirla = false;
            }
        }

        private void FrmGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBoxBeniHatirla_CheckedChanged_1(object sender, EventArgs e)
        {
            if (!checkBoxBeniHatirla.Checked)
            {
                Properties.Settings.Default.KullaniciEmail = string.Empty;
                Properties.Settings.Default.KullaniciSifre = string.Empty;
                AkbilYonetimiFormUI.Properties.Settings.Default.BeniHatirla = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
