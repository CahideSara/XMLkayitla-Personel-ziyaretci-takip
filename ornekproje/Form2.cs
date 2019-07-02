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
    public partial class Form2 : Form
    {
      //Form2  ekranı personel kayıt,güncelleme ve silme işlemlerini yapar.
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void Listele() //personel listesini datagrid1 e atar
        {
            Personel personel = new Personel();

            DataTable dt = personel.PersonelListesiGetir();
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {//yeni personel eklenmesi icin yeni form ekranı acar.
            Form3 göster = new Form3();
            göster.Show();
            Listele();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button2 tuşu  girdigimiz "TC" deki personeli silme işlemini yapar
            Personel p = new Personel();
            p.PersonelSilme(textBox2.Text);
            
            textBox2.Clear();
            Listele();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonGüncelle_Click(object sender, EventArgs e)
        {
           

        }

        private void buttonXmlGÜNCELLE_Click(object sender, EventArgs e)
        { //dataGrid üzerindeki degişiklikleri kaydeder.
            Personel p = new Personel();

            DataTable dt = (DataTable)dataGridView1.DataSource;
            p.PersonelKaydet(dt);
            Listele();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {// dataGrid in güncellemasini yapar.
            Listele();
        }
    }
}
