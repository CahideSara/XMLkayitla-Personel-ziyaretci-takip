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

    public partial class Form3 : Form
    {
        public static string dosyaIlk = "personel.xml";//personel listesini tutan xml
        public Form3()
        {           
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Personel p = new Personel();
            int a = 1;//texBox ve comboBox ların bos olup olamdıgının kontrolunu yapmak icin a degeri kullanılır
            XDocument x = XDocument.Load(@dosyaIlk);
                       if (textBox1.Text.Length == 0)
                         a = 0;
                       if (textBox3.Text.Length == 0)
                         a = 0;
                       if (textBox2.Text.Length == 0)
                         a = 0;
                       if (comboBox1.Text.Length == 0)
                         a = 0;
                       if (textBox5.Text.Length == 0)
                         a = 0;

                    p.setSoyadı(textBox3.Text.ToUpper());
                    p.setTC (textBox2.Text);
                    p.setCalıstıgıKat (comboBox1.Text.ToUpper());
                    p.setMeslegi (textBox5.Text.ToUpper());
                    p.setAdi ((textBox1.Text).ToUpper()); 
                    p.setYası (comboBox2.Text);
              
            if (checkBox2.Checked == true)
                p.setCinsiyet(checkBox2.Text);
            if (checkBox1.Checked == true)
                 p.setCinsiyet(checkBox1.Text);

            if(((checkBox1.Checked == true && checkBox2.Checked == true) ||
                (checkBox1.Checked == false && checkBox2.Checked == false)))//checkBox1 de cinsiyetin sadece 1 deger olamsı için kontrolu yapar
                a=0;
                      
           if ((a==0))
            MessageBox.Show("LÜTFEN BOS ALAN BIRAKMAYIN VE CİNSİYETİNİZİ DOGRU İSARETLEDİGİNİZDEN EMİN OLUN");
            else {
               var personel = (from t0 in x.Element("Personel").Elements("PersonelBilgileri")
                                where t0.Element("TC").Value == textBox2.Text
                                select t0).FirstOrDefault();
           if (personel != null)
               {
                   MessageBox.Show("Girdiğiniz 'TC' de kayıtlı personel vardır");
                   textBox2.Clear();
               }
               else
               {//eger bütün sartlar saglanmıs ise yeni personelin kaydı yapılır
                   p.PersonelKaydet();
                   MessageBox.Show("Personel Eklendi");
                   textBox1.Clear();
                   textBox2.Clear();
                   textBox3.Clear();
                   textBox5.Clear();
               }               
          }
        }    
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try//try catch ile texBox2 ye girilen degerin sadece integer deger olmasını saglar
            {
                if ((Convert.ToInt32(textBox2.Text) > 0))
                {
                    return;
                }                          
                }
           catch
            {  
                textBox2.Clear();            
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
}
