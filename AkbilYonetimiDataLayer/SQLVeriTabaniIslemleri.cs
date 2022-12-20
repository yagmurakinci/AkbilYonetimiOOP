using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkbilYonetimiDataLayer
{
    public class SQLVeriTabaniİslemleri : IVeriTabaniIslemleri
    {
        public string ConnectionStringCumlesi { get; set; } 
        private SqlConnection baglantiNesnesi;
        private SqlCommand komutNesnesi;
        public SQLVeriTabaniİslemleri()
        {
            baglantiNesnesi = new SqlConnection();
            komutNesnesi = new SqlCommand();
            ConnectionStringCumlesi = @"Server = DESKTOP-OFVK2FD\MSSQLSERVER01; Database = AKBİLYONETİMİDB; Trusted_Connection = True; ";
            baglantiNesnesi.ConnectionString = ConnectionStringCumlesi;
            komutNesnesi.Connection = baglantiNesnesi;
        }
        private void BaglantiyiAc()
        {
            if (baglantiNesnesi.State != ConnectionState.Open)
            {
                baglantiNesnesi.Open();
            }
        }

        public int KomutIsle(string eklemeyadaGuncellemeCumlesi)
        {
            try
            {
                using (baglantiNesnesi)
                {
                    baglantiNesnesi.ConnectionString = ConnectionStringCumlesi;
                    komutNesnesi.CommandText = eklemeyadaGuncellemeCumlesi;
                    BaglantiyiAc();
                    int etkilenenSatirSayisi = komutNesnesi.ExecuteNonQuery();
                    return etkilenenSatirSayisi;
                    //return komutNesnesi.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public string VeriEklemeCumlesiOlustur(string tabloAdi, Dictionary<string, object> kolonlar)
        {
            string sorgu = "";
            string alanlar = string.Empty;
            string degerler = String.Empty;
            foreach (KeyValuePair<string, object> item in kolonlar)
            {
                alanlar += $"{item.Key},";
                degerler += $"{item.Value},";
            }
            alanlar = alanlar.TrimEnd(',');
            degerler = degerler.TrimEnd(',');
            sorgu = $"insert into {tabloAdi} ({alanlar}) values ({degerler})";
            return sorgu;

        }

        public DataTable VeriGetir(string tabloAdi, string kolonlar = "*", string kosullar = null)
        {

            try
            {
                using (baglantiNesnesi) // using bloğu kullandığı kaynağı blok bitince kapatır ya da yok eder.
                {
                    //ÖRN: select * from Akbiller where KullaniciId=3
                    string sorgu = $"select {kolonlar} from {tabloAdi} ";
                    if (kosullar != null || kosullar != string.Empty)
                    {
                        sorgu += $" where {kosullar}";
                    }
                    komutNesnesi.CommandText = sorgu;
                    SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
                    BaglantiyiAc();
                    DataTable sonuc = new DataTable();
                    adaptor.Fill(sonuc);
                    return sonuc;
                }
            }
            catch
            {
                throw new Exception("VeriGetirirken beklenmedik sorun oldu.");
            }
        }

        public string VeriGuncellemeCumlesiOLustur(string tabloAdi, Hashtable kolonlar, string kosullar = null)
        {
            string sorgu = string.Empty;
            string setler = string.Empty;

            foreach (var item in kolonlar.Keys)
            {
                setler += $"{item} ={kolonlar[item].ToString()},";
            }
            setler = setler.TrimEnd(',');
            sorgu = $"update {tabloAdi}set {setler}";
            if (kosullar != null || kosullar != string.Empty)
            {
                sorgu += $"where{kosullar}";
            }
            return sorgu;

        }


        public string VeriGuncellemeCumlesiOlustur(string tabloAdi)
        {
            throw new NotImplementedException();
        }

        public int VeriSil(string tabloAdi, string kosullar = null)
        {
            try
            {
                using (baglantiNesnesi)
                {
                    string sorgu = $"delete from {tabloAdi} ";
                    if (kosullar != null || kosullar != string.Empty)
                    {
                        sorgu += $" where {kosullar}";
                    }
                    komutNesnesi.CommandText = sorgu;
                    BaglantiyiAc();
                    int silinenVeriSayisi = komutNesnesi.ExecuteNonQuery();
                    return silinenVeriSayisi;
                }
            }
            catch
            {

                throw;
            }
        }

        public string VeriGuncellemeCumlesiOlustur(string tabloAdi, Hashtable kolonlar, string kosullar = null)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> VeriOku(string tabloAdi, string[] kolonlar, string kosullar = null)
        {
            try
            {
                Dictionary<string, object> sonuc = new Dictionary<string, object>();
                string alanlar = string.Empty;
                foreach (var item in kolonlar)
                {
                    alanlar += $"{item},";
                }
                alanlar = alanlar.TrimEnd(',');
                string sorgu = $"select {alanlar} from {tabloAdi}";
                if (kosullar !=null || kosullar!=string.Empty)
                {
                    sorgu += $"where {kosullar}";
                }

                komutNesnesi.CommandText = sorgu;
                using (baglantiNesnesi)
                {
                    BaglantiyiAc();
                    SqlDataReader okuyucu = komutNesnesi.ExecuteReader();
                    if (okuyucu.Read())
                    {
                        while (okuyucu.Read())
                        {
                            for (int i = 0; i < kolonlar.Length; i++)
                            {
                                sonuc.Add(kolonlar[i], okuyucu[kolonlar[i]]);
                            }//for bitti
                        }//while bitti?? fazlalık mı?
                    }//if bitti
                }//using bitti
                return sonuc;
            }
            catch 
            {

                throw;
            }
        }
    }
}
