using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ornekproje
{
    public class gArsiv : Giris_Cıkıs
    {
        //"gArsiv" ile personel girişleri kayıtları tutulup arsivlenir
        private static string dosyaG ="gArsivi.xml";//personel girişlerinin arşivlenmesini saglayan xml dosyası
        private string PERSONEL;
        private string TC;
        private string ZAMAN;
         public string getPERSONEL()
         {
             return PERSONEL;
         }
         public void setPERSONEL(string PERSONEL)
         {
             this.PERSONEL = PERSONEL;
         }
         public string getTC()
         {
             return TC;
         }
         public void setTC(string TC)
         {
             this.TC = TC;
         }
         public string getZAMAN()
         {
             return ZAMAN;
         }
         public void setZAMAN(string ZAMAN)
         {
             this.ZAMAN = ZAMAN;
         }

        private DataSet personeller = new DataSet("Personel");
        private DataTable dataTablePersonel = new DataTable("PersonelBilgileri");
        public gArsiv()
        {
            DataSetOlustur();
        }
        public gArsiv(string PERSONEL, string TC, string ZAMAN)
        {
            this.PERSONEL = PERSONEL;
            this.TC = TC;
            this.ZAMAN = ZAMAN;
            DataSetOlustur();
        }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaG)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                personeller.ReadXml(dosyaG);
                dataTablePersonel = personeller.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTablePersonel.Columns.Add("TC");
                dataTablePersonel.Columns.Add("PERSONEL");
                dataTablePersonel.Columns.Add("ZAMAN");
                personeller.Tables.Add(dataTablePersonel);
                personeller.WriteXml(dosyaG, XmlWriteMode.WriteSchema);
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
            row["TC"] = TC;
            row["PERSONEL"] = PERSONEL;
            row["ZAMAN"] = ZAMAN;
            dataTablePersonel.Rows.Add(row);
            personeller.WriteXml(dosyaG, XmlWriteMode.WriteSchema);

        }
    }

}
