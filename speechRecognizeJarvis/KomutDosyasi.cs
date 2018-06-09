using System;
using System.Collections.Generic;
using System.IO;

namespace speechRecognizeJarvis
{
    class KomutDosyasi
    {
        string path = "..\\..\\..\\data\\komutlar.txt";

        public List<string> D_komutlar1 = new List<string>();
        public List<string> D_komutlar2 = new List<string>();
        public List<string> D_komutlar3 = new List<string>();
        public List<string> D_komutlar4 = new List<string>();
        public List<string> D_komutlar5 = new List<string>();
        public List<string> D_komutlar6 = new List<string>();
        public List<string> D_komutlar7 = new List<string>();
        public List<string> D_komutlar8 = new List<string>();
        public List<string> D_komutlar9 = new List<string>();
        public List<string> D_komutlar10 = new List<string>();
        public List<string> D_komutlar11 = new List<string>();
        public List<string> D_komutlar12 = new List<string>();


        public KomutDosyasi()
        {
            // eger komutlar.txt yoksa olustur
            if (!File.Exists(path))
                File.WriteAllText(path, "");
            read();
        }


        public void read()
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                key_words(line);
            }
        }

        public void key_words(string line)
        {
            string key = "";
            int sira = 1;
            if (sira < 13)      // dosyadaki komut sayisi 12 yi gecerse algilama
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '+')
                        key += line[i];
                    else
                    {
                        // fonk yolla
                        doldur(key, sira);
                        key = "";
                        sira++;
                    }
                    if (i == line.Length - 1)        // satir sonundaki kelimeyi almak icin
                    {
                        // fonk yolla
                        doldur(key, sira);
                        key = "";
                    }
                }
            }
            if(sira<12)
            {
                // dosyadaki komut sayisi 12 den az ise geri kalan kismi komut kelimesi algılanmicak bir sesle degistir ve doldur.
                for(int i=sira+1;i<13;i++)
                {
                    doldur("vcvcwvc", i);
                }
            }
        }

        public void doldur(string key,int sira)
        {
            switch (sira)
            {
                case 1:
                    D_komutlar1.Add(key);
                    break;

                case 2:
                    D_komutlar2.Add(key);
                    break;

                case 3:
                    D_komutlar3.Add(key);
                    break;

                case 4:
                    D_komutlar4.Add(key);
                    break;

                case 5:
                    D_komutlar5.Add(key);
                    break;

                case 6:
                    D_komutlar6.Add(key);
                    break;

                case 7:
                    D_komutlar7.Add(key);
                    break;

                case 8:
                    D_komutlar8.Add(key);
                    break;

                case 9:
                    D_komutlar9.Add(key);
                    break;

                case 10:
                    D_komutlar10.Add(key);
                    break;

                case 11:
                    D_komutlar11.Add(key);
                    break;

                case 12:
                    D_komutlar12.Add(key);
                    break;
            }
        }

    }
}
