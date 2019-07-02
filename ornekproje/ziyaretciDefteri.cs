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
    class ziyaretciDefteri:Giris_Cıkıs
    {
        //"ziyaretciDefteri" ile ziyaretcinin giriş-cıkıs işlemleri kontrolu  yapılıyo 
        public string TC;
        public string getTC()
        {
            return TC;
        }
        public void setTC(string TC)
        {
            this.TC = TC;
        }
        public static string dosyaZDefteri = "zDefteri.xml";//giriş-cıkıs yapacak ziyaretcinin kontrolunu yapmak için olusturulan xml dosyası.
        private DataSet ziyaretciler = new DataSet("Ziyaretci");
        private DataTable dataTableZiyaretci = new DataTable("ZiyaretciBilgileri");

         public ziyaretciDefteri()
        {
            DataSetOlustur();
        }
         public ziyaretciDefteri(string TC)
        { 
            this.setTC(TC);
            DataSetOlustur();
        }
         public void ziyaretciSilme(string TC)
         {//girilen "TC" deki ziyaretciyi siler "ziyaretciDefteri" nden
             XDocument x = XDocument.Load(@dosyaZDefteri);
             if (x != null)
             {
                 var personel = (from t0 in x.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                                 where t0.Element("TC").Value == TC
                                 select t0).FirstOrDefault();
                 if (personel != null)
                 {
                     personel.Remove();
                     x.Save(dosyaZDefteri);                 
                 }                
             }
         }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaZDefteri)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                ziyaretciler.ReadXml(dosyaZDefteri);
                dataTableZiyaretci = ziyaretciler.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTableZiyaretci.Columns.Add("TC");
                dataTableZiyaretci.Columns.Add("ZİYARETÇİ");
                dataTableZiyaretci.Columns.Add("zaman");
                ziyaretciler.Tables.Add(dataTableZiyaretci);
                ziyaretciler.WriteXml(dosyaZDefteri, XmlWriteMode.WriteSchema);
            }
        }
        public DataTable PersonelListesiGetir()
        {
            return dataTableZiyaretci;
        }
        public void PersonelKaydet()
        {
           //Kayıt ekleme:
           DataRow row = dataTableZiyaretci.NewRow();
           row["TC"] =getTC();   
           dataTableZiyaretci.Rows.Add(row);
           ziyaretciler.WriteXml(dosyaZDefteri, XmlWriteMode.WriteSchema);
        }
        public void PersonelKaydet(DataTable dt)
        {
            //Gridview tamamını tablo olarak kaydetmek için Datasetteki Datatable önce silinir:
            ziyaretciler.Tables.Clear();
            //Yeni Datatable kopyalanır:
            dataTableZiyaretci = dt.Copy();
            //Datatable tekrar datasete eklenir:
            ziyaretciler.Tables.Add(dataTableZiyaretci);
            ziyaretciler.WriteXml(dosyaZDefteri, XmlWriteMode.WriteSchema);
        }
    }
}
