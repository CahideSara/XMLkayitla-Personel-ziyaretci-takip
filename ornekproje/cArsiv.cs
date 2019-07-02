using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace ornekproje
{
    public class cArsiv : Giris_Cıkıs
    {
        //cArsiv ile personel cıkıslarının kayıtları tutulup arşivlenir
        private static string dosyaC = "cArsivi.xml";//bütün cıkış kayıtlarını tutan xml
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
        public cArsiv()
        {
            DataSetOlustur();
        }
        public cArsiv(string PERSONEL, string TC, string ZAMAN)
        {
            this.PERSONEL = PERSONEL;
            this.TC = TC;
            this.ZAMAN = ZAMAN;
            DataSetOlustur();
        }
        private void DataSetOlustur()
        {
            if (System.IO.File.Exists(dosyaC)) //Daha önce dosya oluşturulmuşsa dosyadan bilgileri oku
            {
                personeller.ReadXml(dosyaC);
                dataTablePersonel = personeller.Tables[0];
            }
            else //Dosya yoksa ilk defa kayıt için DataTable oluştur:
            {
                dataTablePersonel.Columns.Add("TC");
                dataTablePersonel.Columns.Add("PERSONEL");
                dataTablePersonel.Columns.Add("ZAMAN");
                personeller.Tables.Add(dataTablePersonel);
                personeller.WriteXml(dosyaC, XmlWriteMode.WriteSchema);
            }
        }
        public DataTable PersonelListesiGetir()//xml' e kayıtlı personelleri getirir
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
            personeller.WriteXml(dosyaC, XmlWriteMode.WriteSchema);
        }    }}
