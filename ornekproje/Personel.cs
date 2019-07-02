using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
namespace ornekproje
{
   class Personel:Kişiler
    {
       //"Personel" sınıfı personel kayıt işlemi için xml dosya işlemlerini yapar
       private string cinsiyet;
       private string CalıstıgıKat;
       private string meslegi;
       public string getCinsiyet()
       {
           return cinsiyet;
       }
       public void setCinsiyet(string cinsiyet)
       {
           this.cinsiyet = cinsiyet;
       }
       public string getCalıstıgıKat()
       {
           return CalıstıgıKat;
       }
       public void setCalıstıgıKat(string CalıstıgıKat)
       {
           this.CalıstıgıKat = CalıstıgıKat;
       }
       public string getMeslegi()
       {
           return meslegi;
       }
       public void setMeslegi(string meslegi)
       {
           this.meslegi = meslegi;
       }

       public static string dosyaIlk = "personel.xml"; //personel bilgilerini tutar   

         private DataSet personeller = new DataSet("Personel");
         private DataTable dataTablePersonel = new DataTable("PersonelBilgileri");

        public Personel()
        {
            DataSetOlustur();
        }
        public Personel(string Adi,string soyadı,string TC,string cinsiyet,
            string CalıstıgıKat,string meslegi,string yası)
        {
             this.setTC( TC);
            this.setAdi(Adi);
            this.setSoyadı(soyadı);
            this.setCinsiyet(cinsiyet);
            this.setCalıstıgıKat( CalıstıgıKat);
            this.setMeslegi( meslegi);
            this.setYası(yası);
            DataSetOlustur();
        }
        public void PersonelSilme( string TC)
        {
            XDocument x = XDocument.Load(@dosyaIlk);
            if (x != null)//xml dosyanın olup olmadıgının kontrolunu yapar
            {
                var personel = (from t0 in x.Element("Personel").Elements("PersonelBilgileri")
                                where t0.Element("TC").Value == TC
                                select t0).FirstOrDefault();
                if (personel != null)//personel sorgusunu yapar
                {//istenilen personelin silme işlemi yapılır
                    personel.Remove();
                    x.Save(dosyaIlk);
                    MessageBox.Show("PERSONEL SİLİNDİ");
                }
                else
                    MessageBox.Show("PERSONEL BULUNAMADI");
            }
        }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaIlk)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                personeller.ReadXml(dosyaIlk);
                dataTablePersonel = personeller.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            { 
                dataTablePersonel.Columns.Add("TC");
                dataTablePersonel.Columns.Add("Adi");
                dataTablePersonel.Columns.Add("soyadı");               
                dataTablePersonel.Columns.Add("cinsiyet");
                dataTablePersonel.Columns.Add("CalıstıgıKat");
                dataTablePersonel.Columns.Add("meslegi");
                dataTablePersonel.Columns.Add("yası");
                personeller.Tables.Add(dataTablePersonel);
                personeller.WriteXml(dosyaIlk, XmlWriteMode.WriteSchema);
            }
        }
        public DataTable PersonelListesiGetir()
        {           
            return dataTablePersonel;
        }
        public void PersonelKaydet()
        {
            //Kayıt ekleme:
            DataRow row = dataTablePersonel.NewRow();
            row["TC"] = getTC();
            row["Adi"] = getAdi().ToUpper();
            row["soyadı"] = getSoyadı().ToUpper();
            row["cinsiyet"] = getCinsiyet();
            row["CalıstıgıKat"] = getCalıstıgıKat();
            row["meslegi"] = getMeslegi().ToUpper();
            row["yası"] = getYası();      
            dataTablePersonel.Rows.Add(row);
            personeller.WriteXml(dosyaIlk, XmlWriteMode.WriteSchema);
        }
        public void PersonelKaydet(DataTable dt)
        {
            //Gridview tamamını tablo olarak kaydetmek için Datasetteki Datatable önce silinir:
            personeller.Tables.Clear();
            //Yeni Datatable kopyalanır:
            dataTablePersonel = dt.Copy();
            //Datatable tekrar datasete eklenir:
            personeller.Tables.Add(dataTablePersonel);
            personeller.WriteXml(dosyaIlk, XmlWriteMode.WriteSchema);
        }
    }
}
