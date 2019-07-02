using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace ornekproje
{
   public interface Giris_Cıkıs
    {
       //personel ve ziyaretci giriş-cıkış işlemlerine arayüz oluşturan sınıf
          void PersonelKaydet();
          DataTable PersonelListesiGetir();
       

    }
}
