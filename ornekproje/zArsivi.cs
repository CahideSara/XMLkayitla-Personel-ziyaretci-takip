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
    class zArsivi
    {
    //"zArşivi" ziyaretçi giriş-cıkış işlem kayıtlarını arşivlemek için xml dosya işlemlerini yapar
        private string TC;
        private string ZİYARETÇİ;
        private string zaman;
        public string getTC()
        {
            return TC;
        }
        public void setTC(string TC)
        {
            this.TC = TC;
        }
        public string getZİYARETÇİ()
        {
            return ZİYARETÇİ;
        }
        public void setZİYARETÇİ(string ZİYARETÇİ)
        {
            this.ZİYARETÇİ = ZİYARETÇİ;
        }
        public string getZaman()
        {
            return zaman;
        }
        public void setZaman(string zaman)
        {
            this.zaman = zaman;
        }       
       
        public static string dosyaZ= "zArsivi.xml";//ziyaretçi giriş-cıkışlarının arşivlenmesini saglayan xml dosyası
        private DataSet ziyaretciler = new DataSet("Ziyaretci");
        private DataTable dataTableZiyaretci = new DataTable("ZiyaretciBilgileri");

        public zArsivi()
        {
            DataSetOlustur();
        }
        public zArsivi(string TC, string ZİYARETÇİ, string zaman)
        {
            this.setTC( TC);
            this.setZİYARETÇİ(ZİYARETÇİ);
            this.setZaman( zaman);
            DataSetOlustur();
        }      
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaZ)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                ziyaretciler.ReadXml(dosyaZ);
                dataTableZiyaretci = ziyaretciler.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTableZiyaretci.Columns.Add("TC");
                dataTableZiyaretci.Columns.Add("ZİYARETÇİ");
                dataTableZiyaretci.Columns.Add("zaman");

                ziyaretciler.Tables.Add(dataTableZiyaretci);

                ziyaretciler.WriteXml(dosyaZ, XmlWriteMode.WriteSchema);
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
            row["TC"] = getTC();
            row["ZİYARETÇİ"] =getZİYARETÇİ();
            row["zaman"] = getZaman();
            dataTableZiyaretci.Rows.Add(row);
            ziyaretciler.WriteXml(dosyaZ, XmlWriteMode.WriteSchema);
        } 
        public void PersonelKaydet(DataTable dt)
        {
            //Gridview tamamını tablo olarak kaydetmek için Datasetteki Datatable önce silinir:
            ziyaretciler.Tables.Clear();
            //Yeni Datatable kopyalanır:
            dataTableZiyaretci = dt.Copy();
            //Datatable tekrar datasete eklenir:
            ziyaretciler.Tables.Add(dataTableZiyaretci);
            ziyaretciler.WriteXml(dosyaZ, XmlWriteMode.WriteSchema);
        }
    }
}
