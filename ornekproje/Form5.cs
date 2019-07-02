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
    public partial class Form5 : Form
    {
      //yeni ziyaretçi ekleme,silme ve güncelleme işlemini yapar Form5
        public static string dosyaZiyaretci = "ziyaretci.xml";//ziyaretci kayıtlarını tutan xml dosyası
        public static string dosyaIlk = "personel.xml";// personel kayıtlarını tutan xml dosyası
        public Form5()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0; 
            Listele();
        }
        private void Listele()
        {//ziyaretci kayıtlarını dataGrid e listeler.
            Ziyaretci z = new Ziyaretci();

            DataTable dt = z.PersonelListesiGetir();
            dataGridView1.DataSource = dt;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            Listele();
        }
        private void button1_Click(object sender, EventArgs e)
        {// "button1" özellikleri girilen ziyaretçiyinin kayıt işlemini yapar.
            Ziyaretci z = new Ziyaretci();
            Personel p = new Personel();            
             XDocument x = XDocument.Load(@dosyaZiyaretci);
             XDocument y = XDocument.Load(@dosyaIlk);
            int a = 1;
            //textBox ve comboBox ların bos olup olmadıklarının kontrollerini yapmak için tanımlandı a degeri.
            if (textBox1.Text.Length == 0)
                a = 0;
            if (textBox2.Text.Length == 0)
                a = 0;
            if (textBox3.Text.Length == 0)
                a = 0;
            if (textBox4.Text.Length == 0)
                a = 0;
            if (comboBox1.Text.Length == 0)
                a = 0;          
            if ((a == 0))
                MessageBox.Show("LÜTFEN BOS ALAN BIRAKMAYIN ");
            else
            {
                var ziyaretci = (from t0 in x.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                                 where t0.Element("TC").Value == textBox3.Text
                                 select t0).FirstOrDefault();
                if (ziyaretci != null)
                {
                    MessageBox.Show("Girdiğiniz 'TC' de kayıtlı ziyeretci vardır");
                    textBox3.Clear();
                }
                else
                {
                    var personel = (from t0 in y.Element("Personel").Elements("PersonelBilgileri")
                                    where t0.Element("Adi").Value == (textBox4.Text).ToUpper()
                                    select t0).FirstOrDefault();


                    if (personel != null)//ziyaret edecegi personel personel listesinde var mı diye kontrolu yapar.
                    { 
                       //kayıt işlemi yapılır
             z.setAdi(textBox1.Text);
            z.setSoyadı (textBox2.Text);
            z.setTC(textBox3.Text);
            z.setKimİcin ( textBox4.Text);
            z.setYası(comboBox1.Text);

                        z.PersonelKaydet();
                        MessageBox.Show("Ziyaretci Eklendi");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();                      
                    }
                    else
                    {
                        MessageBox.Show("'KİMİN ZİYARETCİSİ' İLE ESLESEN BİR PERSONELİMİZ YOKTUR");
                        textBox4.Clear();
                    }
                }
            }
            Listele();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //dataGrid deki degişiklikleri ziyaretçi xml ne aktarır.
            Ziyaretci z = new Ziyaretci();
            DataTable dt = (DataTable)dataGridView1.DataSource;
            z.PersonelKaydet(dt);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try//try catch bloklarıyla texBox3 e girilen degerin sayısal bir deger olmasını saglar.
            {
                if ((Convert.ToInt32(textBox3.Text) > 0))
                {
                    return;
                }
            }
            catch
            {
                textBox3.Clear();
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
