using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ornekproje
{
    class Ziyaretci:Kişiler
    {
        //"Ziyaretci" sınıfı ziyaretci kayıtlarının tutan xml dosya işlemlerini yapar
        private string kimİcin;
        public string getKimİcin()
        {
            return kimİcin;
        }
        public void setKimİcin(string kimİcin)
        {
            this.kimİcin = kimİcin;
        }

        public static string dosyaZiyaretci = "ziyaretci.xml";//ziyaretci bilgilerini tutar  
        private DataSet ziyaretciler = new DataSet("Ziyaretci");
        private DataTable dataTableZiyaretci = new DataTable("ZiyaretciBilgileri");

         public Ziyaretci()
        {
            DataSetOlustur();
        }
         public Ziyaretci(string Adi, string soyadı, string TC, string kimİcin,string yası)
        {
            this.setAdi( Adi);
            this.setSoyadı(soyadı);
            this.setTC( TC);
            this.setKimİcin ( kimİcin);
            this.setYası( yası);
            DataSetOlustur();
        }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaZiyaretci)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                ziyaretciler.ReadXml(dosyaZiyaretci);
                dataTableZiyaretci = ziyaretciler.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTableZiyaretci.Columns.Add("Adi");
                dataTableZiyaretci.Columns.Add("soyadı");
                dataTableZiyaretci.Columns.Add("TC");
                dataTableZiyaretci.Columns.Add("kimİcin");
                dataTableZiyaretci.Columns.Add("yası");
                ziyaretciler.Tables.Add(dataTableZiyaretci);
                ziyaretciler.WriteXml(dosyaZiyaretci, XmlWriteMode.WriteSchema);
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
            row["Adi"] = getAdi().ToUpper();
            row["soyadı"] = getSoyadı().ToUpper();
            row["TC"] =getTC();
            row["kimİcin"] = getKimİcin().ToUpper();
            row["yası"] = getYası();
            dataTableZiyaretci.Rows.Add(row);
            ziyaretciler.WriteXml(dosyaZiyaretci, XmlWriteMode.WriteSchema);
        }
        public void PersonelKaydet(DataTable dt)
        {
            //Gridview tamamını tablo olarak kaydetmek için Datasetteki Datatable önce silinir:
            ziyaretciler.Tables.Clear();
            //Yeni Datatable kopyalanır:
            dataTableZiyaretci = dt.Copy();
            //Datatable tekrar datasete eklenir:
            ziyaretciler.Tables.Add(dataTableZiyaretci);
            ziyaretciler.WriteXml(dosyaZiyaretci, XmlWriteMode.WriteSchema);
        }
    }
}
