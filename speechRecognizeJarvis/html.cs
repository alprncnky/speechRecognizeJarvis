using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecognizeJarvis
{
    class html
    {
        public string site = "";
        public string sitekirp = "";
        public string[] filmIsimleri = { "", "", "", "", "","","","","",};
        public string[] filmresimleri = { "", "", "", "", "", "", "", "", "", };

        public void str_yukle(int sayfa)            // sitenin html kodlarını "site" string ine aktarır
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw;

            if (sayfa == 1)
            {
                // bilimkurgu sayfasi
                raw = wc.DownloadData("https://www.fullhdfilmizlesene.org/filmrobot/?tarz=&tur=5&yil=&imdb=7x&hd=");
            }
            else if (sayfa == 2)
            {
                // aksiyon sayfasi
                raw = wc.DownloadData("https://www.fullhdfilmizlesene.org/filmrobot/?tarz=&tur=3&yil=&imdb=7x&hd=");
            }
            else
            {
                raw = wc.DownloadData("https://www.fullhdfilmizlesene.org/");
            }
            site = System.Text.Encoding.UTF8.GetString(raw);
            isimler_al();
            resimleri_al();
            //Console.WriteLine(site);
        }

        public void resimleri_al()
        {
            bool devam = true;
            bool kelime = false;
            int sayi = site.IndexOf("index-orta");
            string word = "";
            string film = "";
            int count = 0;
            Console.WriteLine(sayi);

            while (devam)
            {
                word += site[sayi];
                if (kelime)
                {
                    if (site[sayi] == '"')
                    {
                        kelime = false;
                        filmresimleri[count] = film;
                        film = "";
                        word = "";
                        count++;
                    }
                    else
                    {
                        film += site[sayi];
                    }
                }
                if (site[sayi] == ' ' && !kelime)
                {
                    word = "";
                }
                if (word == "src=" && !kelime)
                {
                    sayi++;
                    kelime = true;
                }
                if (count == 5)         // toplam 5 film alınca duruyor.
                    devam = false;
                sayi++;
            }
        }

        public void isimler_al()
        {
            bool devam = true;
            bool kelime = false;
            int sayi = site.IndexOf("index-orta");
            string word = "";
            string film = "";
            int count = 0;
            Console.WriteLine(sayi);

            while(devam)
            {
                word += site[sayi];
                if(kelime)
                {
                    if (site[sayi] == '"')
                    {
                        kelime = false;
                        filmIsimleri[count] = film;
                        film = "";
                        word = "";
                        count++;
                    }
                    else
                    {
                        film += site[sayi];
                    }
                }
                if(site[sayi]==' ' && !kelime)
                {
                    word = "";
                }
                if(word=="alt=" && !kelime)
                {
                    sayi++;
                    kelime = true;
                }
                if (count == 5)         // toplam 5 film alınca duruyor.
                    devam = false;
                sayi++;
            }
           // Console.WriteLine("film ismi :" +filmIsimleri[4]);
        }


    }
}
