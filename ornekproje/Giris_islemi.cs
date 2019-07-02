using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ornekproje
{
   public class Giris_islemi:Giris_Cıkıs
    {
       // personel girişi için xml dosya işlemlerini yapar
       private static string dosyaGiris = "giris.xml";//personel giriş kontrol işlemini yapmak için olan xml dosyası.
       private string TC;
        public string getTC()
        {
            return TC;
        }
        public void setTC(string TC)
        {
            this.TC = TC;
        }

        private DataSet personeller = new DataSet("Personel");
        private DataTable dataTablePersonel = new DataTable("PersonelBilgileri");

         public Giris_islemi()
        {
            DataSetOlustur();
        }
         public Giris_islemi( string TC)
        {
            this.TC = TC;
            DataSetOlustur();
        }
         private void DataSetOlustur()
         {
             if (System.IO.File.Exists(dosyaGiris)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
             {
                 personeller.ReadXml(dosyaGiris);
                 dataTablePersonel = personeller.Tables[0];
             }
             else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
             {  
                 dataTablePersonel.Columns.Add("TC");
                 personeller.Tables.Add(dataTablePersonel);
                 personeller.WriteXml(dosyaGiris, XmlWriteMode.WriteSchema);
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
            row["TC"] =getTC();
            dataTablePersonel.Rows.Add(row);
            personeller.WriteXml(dosyaGiris, XmlWriteMode.WriteSchema);

        }
   }
}
