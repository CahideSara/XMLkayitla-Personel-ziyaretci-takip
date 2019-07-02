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
{//personel ve ziyaretci sınıflarına kalıtım verir
    class Kişiler
    {
        private string Adi;
        private string soyadı;
        private string TC;
        private string yası;
        public string getTC()
        {
            return TC;
        }
        public void setTC(string TC)
        {
            this.TC = TC;
        }
        public string getAdi()
        {
            return Adi;
        }
        public void setAdi(string Adi)
        {
            this.Adi = Adi;
        }
        public string getSoyadı()
        {
            return soyadı;
        }
        public void setSoyadı(string soyadı)
        {
            this.soyadı = soyadı;
        }
        public string getYası()
        {
            return yası;
        }
        public void setYası(string yası)
        {
            this.yası = yası;
        }      
    }
}
