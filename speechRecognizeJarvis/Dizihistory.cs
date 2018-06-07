using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecognizeJarvis
{
    // TODO: readText e cektigin icin guncelleyecegin zaman eslesen linkin oldugu kısımları string ten sil sonra yenisi ekleyip geri dosyay yazdir !!!

    class Dizihistory
    {
        string path = "..\\..\\..\\logs\\dizi.txt";
        string dizi_name = "humans";
        string dizi_link = "https://www.dizibox5.com/humans-2-sezon-8-bolum-izle/";      // netten gelen link
        string o_link = "";         // dosyadan gelen link
        public int input_dizi_sezon, saved_dizi_sezon;               // gelen ve kayitli dizinin kacinci sezon oldugu
        public int input_dizi_bolum, saved_dizi_bolum;               // gelen ve kayitli  dizinin kacinci bolum oldugu
        int silinecekLine_start, silinecekLine_son;             // en son dosya guncellenirken bu satır araligini yazdirma !
        bool outputmu = false;                                // Dizinfo() fonksiyonuna sonradan matchedDizi() fonksiyonundan erisim olursa diye bunu ekledim sonradan


        // chrome kapalıylen calismasi lazim    ! HATA !
        // Bu olay zaman ayarlı task olarak yap
        public void chrome_history()
        {
            SQLiteConnection conn = new SQLiteConnection
            (@"Data Source=C:\Users\Alperen\AppData\Local\Google\Chrome\User Data\Default\History");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            //  cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
            //  Use the above query to get all the table names
            cmd.CommandText = "SELECT url AS URL, url, time(last_visit_time / 1000000 + (strftime('%s', '1601-01-01')), 'unixepoch', 'localtime') AS Time, date(last_visit_time / 1000000 + (strftime('%s', '1601-01-01')), 'unixepoch') AS Date FROM urls WHERE url LIKE '%dizi%' AND url LIKE '%bolum%' AND url LIKE '%sezon%' ORDER BY last_visit_time DESC LIMIT 50";
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // TODO: dizi_name ayarla  !!!
                // bolum ve sezon kelimleri geciyorsa gecerli fonskiyonlari cagir
                Console.WriteLine(dr[1].ToString());
            }
        }


        // dizi leri karsilastirip yapilacak islemi secme fonksiyonu
        public void matchedDizi()
        {
            string readText = System.IO.File.ReadAllText(path);
            if(readText.Contains(dizi_name))
            {
                // dosyadaki diziden buyuk mu kontrol et buyukse dosyayı yenile
                savedDizi();
                Dizinfo();
                if(input_dizi_sezon>saved_dizi_sezon)
                {
                    if(input_dizi_bolum>saved_dizi_bolum)
                    {
                        // guncelle
                    }
                }
                // else birşey yapma
            }
            else
            {
                // yeni bir dizi olmali bunu ekle dosyaya
            }
        }


        // kayıtlı dizinin sezon ve bolumunu bulmak icin Dizinfo() ya yolla ve dosyadaki silinecek satirlari belirle
        public void savedDizi()
        {
            o_link = "";
            string readText = System.IO.File.ReadAllText(path);
            int temp = readText.IndexOf(dizi_name);
            while (readText[temp] != '*')
                temp--;
            temp = temp + 1;     
            silinecekLine_start = temp;         // yıldızdan sonraki harf
            while (readText[temp] != '*')
                temp++;
            silinecekLine_son = temp;       // yıldız dahil
            for(int i=silinecekLine_start;i<silinecekLine_son;i++)
            {
                o_link+=readText[i];
            }
            outputmu = true;
            Dizinfo();
        }


        // gelen linke göre sezon ve bolum sayisini bul
        public void Dizinfo()
        {
            string d_link = "";
            if (outputmu)
                d_link = o_link;
            else
                d_link = dizi_link;
            // kacıncı sezon oldugunu bul yaptigi kisim     example/    "www.dizi.com/dizi-adi-3-sezon-15-bolum" burdaki sezon sayisi yani 3 u bulmaktir.
            int sezonLoc = d_link.IndexOf("sezon");      // sezon kelimesinin konumu
            int temp = sezonLoc;                            // while icinde dolanacagı icin temp int
            int tirecount = 0;                              // ' - ' sayısının tutan deger
            while (tirecount < 2)                              // geri dogru git
            {
                temp--;
                if (d_link[temp] == '-')
                {
                    tirecount++;
                }
            }
            temp++;
            string dizi_sezon = "";
            while (d_link[temp] != '-')
            {
                dizi_sezon += d_link[temp];
                temp++;
            }
            if(outputmu)
                saved_dizi_sezon= Convert.ToInt32(dizi_sezon);
            else
                input_dizi_sezon = Convert.ToInt32(dizi_sezon);

            // kacıncı bolum oldugunu bul yaptigi kisim     example/    "www.dizi.com/dizi-adi-3-sezon-15-bolum" burdaki bolum sayisi yani 15 i bulmaktir.
            temp = sezonLoc + 6;
            tirecount = 0;
            string dizi_bolum = "";
            while (tirecount < 1)
            {
                if (d_link[temp] == '-')
                    tirecount++;
                else
                {
                    dizi_bolum += d_link[temp];
                    temp++;
                }
            }
            if(outputmu)
                saved_dizi_bolum= Convert.ToInt32(dizi_bolum);
            else
                input_dizi_bolum = Convert.ToInt32(dizi_bolum);
            outputmu = false;
        }

    }
}
