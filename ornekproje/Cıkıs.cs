using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ornekproje
{
    public class Cıkıs : Giris_Cıkıs
    {
        // "Cıkıs" ile pesonelin cıkıs işlemleri kontrolu  yapılıyo 
        private static string dosyaCıkıs = "cıkıs.xml";// cıkıs yapacak olan personelin "TC" bilgisini tutan xml
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
        public Cıkıs()
        {
            DataSetOlustur();
        }
        public Cıkıs(string TC)
        {
            this.TC = TC;
            DataSetOlustur();
        }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaCıkıs)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                personeller.ReadXml(dosyaCıkıs);
                dataTablePersonel = personeller.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTablePersonel.Columns.Add("TC");
                personeller.Tables.Add(dataTablePersonel);
                personeller.WriteXml(dosyaCıkıs, XmlWriteMode.WriteSchema);
            }
        }
        public DataTable PersonelListesiGetir()
        {
            return dataTablePersonel;
        }
        public void PersonelKaydet()
        { //Kayıt ekleme:
            DataRow row = dataTablePersonel.NewRow();
            row["TC"] = getTC();
            dataTablePersonel.Rows.Add(row);
            personeller.WriteXml(dosyaCıkıs, XmlWriteMode.WriteSchema);
        }
    }

}
