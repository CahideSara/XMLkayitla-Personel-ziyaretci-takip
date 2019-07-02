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
    public partial class Form1 : Form
    {
        public static string dosyaIlk = "personel.xml";//personel bilgilerini tutar
        public static string dosyaGiris = "giris.xml";//giriş işlemleri kontrolu icin
        public static string dosyaZiyaretci = "ziyaretci.xml";//ziyaretci bilgilerini tutar
        public static string dosyaCıkıs = "cıkıs.xml";//cıkıs işlemleri kontrolu için
        public static string dosyaZDefteri = "zDefteri.xml";//ziyaretcı gırıs-cıkıs kontrolu için
        public static string dosyaG = "gArsivi.xml";//personel giriş bilgilerini tutar
        public static string dosyaC = "cArsivi.xml";//personel cıkıs bilgilerini tutar
        public static string dosyaZ = "zArsivi.xml";//ziyaretci giriş-cıkıs bilgilerini tutar
        public Form1()
        {
            InitializeComponent();
            Listele();
            Listele2();
            Listele3();
        } 
      
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //try-catch ile textBox1 deki verinin sayı dısında yazıldıgı anda silmesini saglamak
            try
            {
                if ((Convert.ToInt32(textBox1.Text) > 0))
                {return;}

              }
            catch
            {
                textBox1.Clear();
                }
        }

        #region işlem
        private void Listele()
        {
            // "Listele" ile personel giriş bilgilerini "dataGridView1" e aktarmak
            gArsiv ga = new gArsiv();

            DataTable dt = ga.PersonelListesiGetir();
            dataGridView1.DataSource = dt;
        }

        private void Listele3()
        {    // "Listele3" ile personel cıkıs bilgilerini "dataGridView2" e aktarmak
            cArsiv ca = new cArsiv();
            DataTable dt = ca.PersonelListesiGetir();
            dataGridView2.DataSource = dt;
        }

        private void Listele2()
        {
            // "Listele2" ile ziyaretci giriş-cıkıs bilgilerini "dataGridView3" e aktarmak
            zArsivi za = new zArsivi();
            DataTable dt = za.PersonelListesiGetir();
            dataGridView3.DataSource = dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {//kayıtlı personelleri silme ,güncelleme veya yeni kayıt eklemek icin "Form2" acmak..
            Form2 göster = new Form2();
            göster.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {}
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {}
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {}
        private void label4_Click(object sender, EventArgs e)
        { }
        private void button6_Click(object sender, EventArgs e)
        {//yeni ziyaretci eklemek,güncellemek ve silmek için "Form5" acılır..
            Form5 göster = new Form5();
            göster.Show();
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
           Giris_islemi gi = new Giris_islemi();
           Cıkıs ck = new Cıkıs();
            XDocument x = XDocument.Load(@dosyaIlk);
            XDocument y = XDocument.Load(@dosyaGiris);
            XDocument z= XDocument.Load(@dosyaCıkıs);
            // "textBox1" e girdigimiz TC de bir personel varmı (personel),giriş yapmıs mı(gpersonel)
            //yada cıkıs yapmıs mı(cıkıs) dıye kontrollerı sırayla yapılır
              var personel = (from t0 in x.Element("Personel").Elements("PersonelBilgileri")
                                where t0.Element("TC").Value == textBox1.Text
                                select t0).FirstOrDefault();
            var gpersonel = (from t0 in y.Element("Personel").Elements("PersonelBilgileri")
                                where t0.Element("TC").Value == textBox1.Text
                                select t0).FirstOrDefault();
            var cıkıs = (from t0 in z.Element("Personel").Elements("PersonelBilgileri")
                             where t0.Element("TC").Value == textBox1.Text
                             select t0).FirstOrDefault();


            if (personel != null)//girilen "TC" de personle varsa
            {
                if (gpersonel != null)
                {
                    // daha önce giriş yapmıssa uyarı verılır
                    MessageBox.Show("GİRİŞ YAPMIŞ DURUMDASINIZ LÜTFEN ÇIKIŞ YAPINIZ..");

                }
                else
                { 
                    // daha önce giriş yapmamıssa bu personel kaydedilir 
                    gArsiv ga = new gArsiv();
                    //ve bu personel anlık giriş zamanıyla giriş arsivine kaydedilir
                    ga.setTC(textBox1.Text); 
                    ga.setPERSONEL( personel.Element("Adi").Value + "  " + personel.Element("soyadı").Value + 
                        "  giriş yapmıstır");
                    ga.setZAMAN( DateTime.Now.ToString());
                    ga.PersonelKaydet();
                    // giriş kontrollrını yapmak icin ise "TC" si kaydedilir giriş.xml dosyasına
                    gi.setTC(textBox1.Text);
                    gi.PersonelKaydet();
                          
                    if (cıkıs != null) { 
                        //giriş-cıkıs işlemleri kontrolleri içinde cıkıs.xml den tc silinir
                    cıkıs.Remove();
                    z.Save(dosyaCıkıs);}
                    textBox1.Clear();
                    Listele();
                    Listele3();
                }

              
            }
            else
                // girilen "TC" de personel yoksa uyarı verır.....
                MessageBox.Show("GİRMİŞ OLDUGUNUZ TC'DE PERSONEL BULUNMAMAKTADIR");



        }
        
        private void button4_Click(object sender, EventArgs e)
        {
           int a = 1;//textBox2 ve textBox3 alanlarının bos olmaması icin a ile kontrol yapılır
            if (textBox2.Text.Length == 0)
                a = 0;
            if (textBox3.Text.Length == 0)
                a = 0;
            if ((a==0)){//iksinden biri bos oldugunda uyarı verır
            MessageBox.Show("LÜTFEN 'TC' VE 'KİM ZİYARET EDİLİCEK' ALANLARINI BOS BIRAKMAYINIZ");
            
            }else {// eger ikiside bos degılse işlemlere devam edilir
                XDocument x = XDocument.Load(@dosyaZiyaretci);
                  ziyaretciDefteri zd = new ziyaretciDefteri();
                  XDocument y = XDocument.Load(@dosyaIlk);
                  XDocument z= XDocument.Load(@dosyaZDefteri);
           //ziyaret edilecek personelin kontrolu,ziyaretci kayıtlı mı,daha önce gırıs yapmıs mı 
           //dıye sırayla kontrol yapılır
             var personel = (from t0 in y.Element("Personel").Elements("PersonelBilgileri")
                             where t0.Element("Adi").Value == (textBox3.Text).ToUpper()
                             select t0).FirstOrDefault();

             var eleman = (from t0 in x.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                             where t0.Element("TC").Value == (textBox2.Text)
                             select t0).FirstOrDefault();
             var defter = (from t0 in z.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                             where t0.Element("TC").Value == (textBox2.Text)
                             select t0).FirstOrDefault();

             if (personel != null)//öyle bir personel varsa
             {
                 if (eleman != null) {//ziyaretci kayıtlı ise
                     if (defter != null)//cıkıs yapmadan tekrar giriş yapıyorsa uyarı verilir
                     {
                         MessageBox.Show("GİRİŞ YAPMIŞ DURUMDASINIZ LÜTFEN CIKIŞ YAPINIZ.");
                     }
                     else{
                         zArsivi za = new zArsivi();
                         //ziyaretci kaydedilerek arsivlenir
               za.setZİYARETÇİ(textBox3.Text.ToUpper()+" nın  "+eleman.Element("Adi").Value +
                  " "+eleman.Element("soyadı").Value+" adındaki ziyaretcisi giriş yapmıstır");
               za.setTC( textBox2.Text);              
               za.setZaman( DateTime.Now.ToString());
               za.PersonelKaydet();
               //giriş-cikiş kontrolleri icinde "zDefteri.xml" ne tc kaydedilir...
                zd.setTC(textBox2.Text);
                zd.PersonelKaydet();
                Listele2();

            MessageBox.Show("GİRİŞ YAPILDI");
            textBox2.Clear();
            textBox3.Clear();
                     } 
                 }
                 else
                 {//eger kayıtlı ziyaretci yoksa uyarı verilir
                     MessageBox.Show("BU 'TC' DE KAYITLI ZİYARETÇİMİZ YOKTUR");
                     textBox2.Clear();
                 }
             }
             else
             {//ziyeret edilecek personel kayıtlı degılse uyarı verilir
                 MessageBox.Show("'KİM ZİYARET EDİLECEK' İLE ESLESEN BİR PERSONELİMİZ YOKTUR");
                 textBox3.Clear();
             } }}   

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try //textbox2 ye girilen degerleri sadece integer deger olanları kabul eder try catch bloklarıyla
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

        private void button2_Click(object sender, EventArgs e)
        {//button2 personel cıkıs işlemini yapar 

            Cıkıs cks= new Cıkıs();
            
            XDocument x = XDocument.Load(@dosyaIlk);
            XDocument y = XDocument.Load(@dosyaCıkıs);
            XDocument z = XDocument.Load(@dosyaGiris);
            var personel = (from t0 in x.Element("Personel").Elements("PersonelBilgileri")
                            where t0.Element("TC").Value == textBox1.Text
                            select t0).FirstOrDefault();
            var gpersonel = (from t0 in z.Element("Personel").Elements("PersonelBilgileri")
                             where t0.Element("TC").Value == textBox1.Text
                             select t0).FirstOrDefault();
            var cpersonel = (from t0 in y.Element("Personel").Elements("PersonelBilgileri")
                             where t0.Element("TC").Value == textBox1.Text
                             select t0).FirstOrDefault();

            if (personel != null)//personelin kaydının kontrolunu yapar.
            {
                if (cpersonel != null)// cıkıs kontrolu yapılıyor.
                {
                   MessageBox.Show(" ÇIKIŞ YAPMIŞ DURUMDASINIZ LÜTFEN GİRİŞ YAPINIZ..");
               
                }
                else
                {
                     
                  if (gpersonel != null){//cıkıs yapabilmek için giriş yapmıs mı dıye kontrol eder
                      //cıkıs arsivine ve cıkıs listesine kayıt yapar.
                      cArsiv ca = new cArsiv();
                        ca.setTC( textBox1.Text);
                       ca.setPERSONEL( personel.Element("Adi").Value + "  " + personel.Element("soyadı").Value + 
                           "  çıkış yapmıstır");
                       ca.setZAMAN( DateTime.Now.ToString());
                       ca.PersonelKaydet();
                       cks.setTC(textBox1.Text);
                        cks.PersonelKaydet();
                        gpersonel.Remove();
                        z.Save(dosyaGiris);
                       
                        textBox1.Clear();
                        Listele3();
                        Listele();
                    }
                 

                    else 
                          MessageBox.Show("GİRİŞ YAPMADAN CIKIŞ YAPAMAZSINIZ");

            }                }

            else
                MessageBox.Show("GİRMİŞ OLDUGUNUZ TC'DE PERSONEL BULUNMAMAKTADIR");




        }

        private void button5_Click(object sender, EventArgs e)//ziyaretci cıkıs kontrolü yapar...
        {
            if (textBox2.Text.Length == 0){
            
            MessageBox.Show("LÜTFEN 'TC' ALANINI BOS BIRAKMAYINIZ");
            
            }else {
                 
                XDocument x = XDocument.Load(@dosyaZiyaretci);
                  ziyaretciDefteri zd = new ziyaretciDefteri();
                  XDocument z= XDocument.Load(@dosyaZDefteri);

             var eleman = (from t0 in x.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                             where t0.Element("TC").Value == (textBox2.Text)
                             select t0).FirstOrDefault();
             var defter = (from t0 in z.Element("Ziyaretci").Elements("ZiyaretciBilgileri")
                             where t0.Element("TC").Value == (textBox2.Text)
                             select t0).FirstOrDefault();

                     if (defter != null)//ziyaretcinin giriş kontrolu yapılır eger giriş yapılmıssa cıkıs yapılır.
                     {// "zArsivi" dosyasına cıkıs kaydı yapılır ve "ziyaretciDefteri" nden kayıt silinir.

                         zd.ziyaretciSilme(textBox2.Text);
                         zArsivi za = new zArsivi();
                         ziyaretciDefteri zde = new ziyaretciDefteri();
                         
                za.setZİYARETÇİ( eleman.Element("Adi").Value+"  " +eleman.Element("soyadı").Value+
                    "  çıkış yapmıstır");
                za.setTC(textBox2.Text);
              
               za.setZaman( DateTime.Now.ToString());
               za.PersonelKaydet();

               
                Listele2();

            MessageBox.Show("CIKIŞ YAPILDI");
            textBox2.Clear();
            textBox3.Clear();
                        
                     }
                     else{
                   MessageBox.Show("GİRİŞ YAPMADAN CIKIŞ YAPAMAZSINIZ..");
                     } 
                  }
        }
        }
    }
