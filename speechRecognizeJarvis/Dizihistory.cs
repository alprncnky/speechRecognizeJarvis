using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Automation;

namespace speechRecognizeJarvis
{
    // TODO: readText e cektigin icin guncelleyecegin zaman eslesen linkin oldugu kısımları string ten sil sonra yenisi ekleyip geri dosyay yazdir !!!

    class Dizihistory
    {
        string path = "..\\..\\..\\logs\\dizi.txt";
        string dizi_name = "";      // findName() de bulundu
        string dizi_link = "";      // netten gelen link
        public string o_link = "";         // dosyadan gelen link
        public int input_dizi_sezon, saved_dizi_sezon;               // gelen ve kayitli dizinin kacinci sezon oldugu
        public int input_dizi_bolum, saved_dizi_bolum;               // gelen ve kayitli  dizinin kacinci bolum oldugu
        int silinecekLine_start, silinecekLine_son;             // en son dosya guncellenirken bu satır araligini yazdirma !
        bool outputmu = false;                                // Dizinfo() fonksiyonuna sonradan matchedDizi() fonksiyonundan erisim olursa diye bunu ekledim sonradan
        string tempdosya = "";                               // dosyaya yazdırılcak olan string guncellenmeis olan
        public string send = "";

        public Dizihistory()
        {
            // eger logs dosyasi icinde dizi.txt yoksa olustur
            if(!File.Exists(path))
                File.WriteAllText(path,"");
        }


        public string diziGetir(string dizi_str)
        {
            Console.WriteLine("--Dizi getir()");
            o_link = "";
            string readText = System.IO.File.ReadAllText(path);
            dizi_link = dizi_str;
            findName();
            if (dizi_name.Length > 5)
            {
                if (readText.Contains(dizi_name))
                {
                    int temp = readText.IndexOf(dizi_name);
                    while (readText[temp] != '*')
                        temp--;
                    temp = temp + 1;
                    silinecekLine_start = temp;         // yıldızdan sonraki harf
                    while (readText[temp] != '*')
                        temp++;
                    silinecekLine_son = temp;       // yıldız dahil
                    for (int i = silinecekLine_start; i < silinecekLine_son; i++)
                    {
                        o_link += readText[i];
                    }
                }
            }
            return o_link;
        }


        public string GetActiveTabUrl()
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");

            if (procsChrome.Length <= 0)
                return null;

            foreach (Process proc in procsChrome)
            {
                // the chrome process must have a window 
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;

                // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                if (SearchBar != null)
                    return (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
            }
            return null;
        }

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
                // fill the dizi_link and dizi_name !
                dizi_link = dr[1].ToString();
                findName();
                if (dizi_name.Length < 50)
                {
                    Dizinfo();
                    savedDizi();
                    matchedDizi();
                }           
               // Console.WriteLine(dr[1].ToString());
            }
        }

        // netten gelen dizinin adini bul
        public void findName()
        {
            // sezon kelimesini bul sonra 2 itre olunca toplamay basla / olunca dur
            dizi_name = "";
            string temp = "";
            if (dizi_link.Contains("sezon") && dizi_link.Contains("bolum") && dizi_link.Contains("dizi"))
            {
                int sLoc = dizi_link.IndexOf("sezon");
                int tireCount = 0;
                int slash = 0;
                while (slash < 2)
                {
                    if (dizi_link[sLoc] == '/')
                        slash++;
                    if (tireCount > 1 && slash < 2)
                        temp += dizi_link[sLoc];
                    if (dizi_link[sLoc] == '-' || dizi_link[sLoc] == '/')
                    {
                        tireCount++;
                    }
                    sLoc--;
                }
                char[] arr = temp.ToCharArray();
                Array.Reverse(arr);
                temp = new string(arr);
                dizi_name = temp;
            }
        }


        // dizi leri karsilastirip yapilacak islemi secme fonksiyonu
        public void matchedDizi()
        {            
            tempdosya = "";
            string readText = System.IO.File.ReadAllText(path);
            if(readText.Contains(dizi_name))
            {
                // dosyadaki diziden buyuk mu kontrol et buyukse dosyayı yenile
                savedDizi();
                Dizinfo();
                if(input_dizi_sezon>saved_dizi_sezon)
                {
                    // guncelle
                    int i = 0;
                    bool devam = true;
                    while(devam && i<readText.Length)
                    {
                        if (i<silinecekLine_start || i >silinecekLine_son)
                        {
                            tempdosya += readText[i];
                        }
                        i++;
                    }
                    ekleDizi();
                }
                else if(input_dizi_sezon==saved_dizi_sezon)     // sezonlar esit bolum buyukse
                {
                    if (input_dizi_bolum>saved_dizi_bolum)
                    {
                        // guncelle
                        int i = 0;
                        bool devam = true;
                        while (devam && i < readText.Length)
                        {
                            if (i < silinecekLine_start || i > silinecekLine_son)
                            {
                                tempdosya += readText[i];
                            }
                            i++;
                        }
                        ekleDizi();
                    }
                }
                // else birşey yapma
            }
            else if(dizi_name.Length<40)        // sacma reklam linkleri takiliyor o linkleri almamak icin <40 dedim
            {
                // yeni bir dizi olmali bunu ekle dosyaya
                tempdosya = readText;
                ekleDizi();
            }
        }
        
        // dosyanın sonuna dizi link ekleyip yazdirma
        public void ekleDizi()
        {
            if (tempdosya.Length < 10)
                tempdosya = "*";
            tempdosya += dizi_link;
            tempdosya += "*";
            File.WriteAllText(path, tempdosya);
        }


        // kayıtlı dizinin sezon ve bolumunu bulmak icin Dizinfo() ya yolla ve dosyadaki silinecek satirlari belirle
        public void savedDizi()
        {
            o_link = "";
            string readText = System.IO.File.ReadAllText(path);
            if (readText.Contains(dizi_name))
            {
                int temp = readText.IndexOf(dizi_name);
                while (readText[temp] != '*')
                    temp--;
                temp = temp + 1;
                silinecekLine_start = temp;         // yıldızdan sonraki harf
                while (readText[temp] != '*')
                    temp++;
                silinecekLine_son = temp;       // yıldız dahil
                for (int i = silinecekLine_start; i < silinecekLine_son; i++)
                {
                    o_link += readText[i];
                }
                outputmu = true;
                Dizinfo();
            }
            else
            {
                saved_dizi_sezon = 0;
                saved_dizi_bolum = 0;
            }
        }


        // gelen linke göre sezon ve bolum sayisini bul
        public void Dizinfo()
        {
            try
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
                    if (d_link[temp] == '-' || d_link[temp] == '/')
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
                if (outputmu)
                    saved_dizi_sezon = Convert.ToInt32(dizi_sezon);
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
                if (outputmu)
                    saved_dizi_bolum = Convert.ToInt32(dizi_bolum);
                else
                    input_dizi_bolum = Convert.ToInt32(dizi_bolum);
                outputmu = false;
            }
            catch(Exception e)
            {
                Console.WriteLine("/////   HATA dizinfo()  /////");
            }
        }

    }
}
