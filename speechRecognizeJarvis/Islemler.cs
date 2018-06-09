using System;
using WindowsInput;
using Emgu.CV;
using System.Runtime.InteropServices;
using System.Media;
using System.Threading;
using System.Diagnostics;

namespace speechRecognizeJarvis
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern Boolean AllocConsole();
    }

    class Islemler
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        string path = "..\\..\\..\\data\\";     // !!! farkli pc de degistir  !!!  ses dosyaları
        string wp_path = @"C:\Users\alperen\AppData\Local\WhatsApp\WhatsApp.exe";        // !!! farkli pc de degistir  !!!  whatsapp.exe calistirmak icin exe konumu

        Thread tid1;        // winform islemi icin thread olustur
        Random random = new Random();   // for random number

        html h = new html();    // bilim kurgu filmleri sayfasi
        html h2 = new html();   // aksiyon filmleri sayfasi
        html h3 = new html();   // savas filmleri sayfasi

        public static string filmPhoto = "";
        public static string filmName = "";
        public int filmNo = 0;
        public static Form1 f;


        // -- uyku
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        // -- end


        public void fonksiyonBul(int number)        // hangi fonksiyon calisicaksa burdan cagir
        {
            switch (number)
            {
                case 0:
                    // jarvis
                    // 2 kere cagirdim cunku kamera isigi 3 kere yanıp sonsun diye
                    jarvis();
                    break;

                case 1:
                    // for dongusu koy
                    fordongusu();
                    break;

                case 2:
                    // uyku modu
                    uyku();
                    break;

                case 3:
                    // dizi izleyelim
                    // dizileri aç
                    // diziler
                    diziler();
                    break;

                case 4:
                    // youtube
                    youtube();
                    break;

                case 5:
                    // whatsapp
                    whatsapp();
                    break;

                case 6:
                    // filim bul
                    filmgetir();
                    break;

                case 7:
                    // degistir
                    // gec
                    degistir();
                    break;

                case 8:
                    // dizi History
                    dizihistory();
                    break;

                case 9:
                    // case 8:  TEKRAR ! komut sayisi yeterli gelmedi
                    // dizi History
                    dizihistory();
                    break;

                default:
                    Console.WriteLine("hatalı");
                    break;
            }
        }


        //- - - - FONKSIYONLAR - - - -

        public void jarvis()
        {
            // bu fonk. kamerayı calistirarak kamera isigini yanip sonmesini saglar ve ses dosyası oynatır.
            //( Amac jarvis kelimesi soylendiginde kullanıcıya bunu algıladığını isaret vermektir. )
            Console.WriteLine("- - - -jarvis- - -- ");

            InputSimulator sim = new InputSimulator();
            VideoCapture capture = new VideoCapture();
            bool tell = capture.IsOpened;
            capture.Dispose();

            using (var soundPlayer = new SoundPlayer(path+"jarvis.wav"))
            {
                soundPlayer.Play();
            }

            sim = new InputSimulator();
            capture = new VideoCapture();
            tell = capture.IsOpened;
            capture.Dispose();
        }

        public void fordongusu()
        {
            // kod yazarken for dongusu koy denilirse bu fonk. yapıyor
            Console.WriteLine("--- for dongusu koy ---");
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.TextEntry("for(int i=0;i<1;i++)");
            sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            sim.Keyboard.TextEntry("{");
            sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            sim.Keyboard.TextEntry("}");
        }

        public void uyku()
        {
            Console.WriteLine("--uyku()");
            // bilgisyarı uyku moduna geçiriyor
            SetSuspendState(false, true, true);
        }

        public void diziler()
        {
            // dizi siteleri aciliyor
            Console.WriteLine("Yeahhh");
            System.Diagnostics.Process.Start("http://www.dizibox5.com");
            System.Diagnostics.Process.Start("http://www.dizist1.com");
            System.Diagnostics.Process.Start("http://www.dizipub.com");
            System.Diagnostics.Process.Start("http://www.dizimag5.co");
        }

        public void youtube()
        {
            Console.WriteLine("--youtube()");
            // youtube aciliyor
            System.Diagnostics.Process.Start("http://www.youtube.com");
        }

        public void whatsapp()
        {
            Console.WriteLine("--whatsapp()");
            // whatsapp.exe calistiriliyor
            System.Diagnostics.Process.Start(wp_path);
        }


        public void filmgetir()
        {
            Console.WriteLine("--filmgetir()");
            // filmler listeleniyor
            //ses
            // pencerede ac film resmini ve ismini
            using (var soundPlayer = new SoundPlayer(path+"oneriler.wav"))
            {
                soundPlayer.Play();
            }

            h.str_yukle(1);     // filmleri stringlere yukleyip nesneden cagirmaya hazir hale getiriyoruz
            h2.str_yukle(2);
            h3.str_yukle(3);

            filmName = h.filmIsimleri[filmNo];
            filmPhoto = h.filmresimleri[filmNo];

            Console.WriteLine("name :" + filmName);
            Console.WriteLine(filmPhoto);

            tid1 = new Thread(new ThreadStart(Islemler.Thread1));
            tid1.Start();
        }

            public static void Thread1()        // form acma islemi kodu bloke etmemesi icin thread
            {
                 Thread.Sleep(100);
                System.Windows.Forms.Application.Run(new Form1(filmName,filmPhoto));
                f = new Form1(filmName,filmPhoto);
                f.Show();
            }


        public void degistir()
        {
            Console.WriteLine("--degistir()");
            // yeni film oneri
            int filmturu = rastgele_sayi(3) + 1;        //   hangi iflm?  1=bilimurgu 2=aksiyon 3=savas
            filmNo++;          // elimzdeki film sayısını asmamak icin deger tut

            tid1.Abort();               // onceki acik pencereyi kapat
            SetCursorPos(960, 540);
            if (filmNo < 8)
            {
                if (filmturu == 1)
                {
                    filmName = h.filmIsimleri[filmNo];
                    filmPhoto = h.filmresimleri[filmNo];
                }
                if (filmturu == 2)
                {
                    filmName = h2.filmIsimleri[filmNo];
                    filmPhoto = h2.filmresimleri[filmNo];
                }
                if (filmturu == 3)
                {
                    filmName = h3.filmIsimleri[filmNo];
                    filmPhoto = h3.filmresimleri[filmNo];
                }
                tid1 = new Thread(new ThreadStart(Islemler.Thread1));
                tid1.Start();
            }
            else
            {
                // ses oneriler tamamlandı
                using (var soundPlayer = new SoundPlayer(path+"oneritamam.wav"))
                {
                    soundPlayer.Play();
                }
            }
        }


        // dizi sitesi acikken kaldigin bolumu istediginde bu fonk cagir
        public void dizihistory()
        {
            Console.WriteLine("--dizihistory()");

            Process[] chromeInstances = Process.GetProcessesByName("chrome");
            if (chromeInstances.Length > 0)             // chrome calisiyorsa gir yoksa hata alir program
            {
                Dizihistory dizinesne = new Dizihistory();
                // hemen bakiyorum ses
                using (var soundPlayer = new SoundPlayer(path + "hemenbakıyorum.wav"))
                {
                    soundPlayer.Play();
                }
                string dizi = dizinesne.GetActiveTabUrl();
                if (dizinesne.diziGetir(dizi).Length > 5)
                {
                    // iyi seyirler ses
                    using (var soundPlayer = new SoundPlayer(path + "iyiseyirler.wav"))
                    {
                        soundPlayer.Play();
                    }
                    Console.WriteLine("***** dizi aciliyor...");
                    System.Diagnostics.Process.Start(dizinesne.o_link);
                }
                else
                {
                    // dizi kayitlarda yok ses
                    using (var soundPlayer = new SoundPlayer(path + "dizikayıtlardayok.wav"))
                    {
                        soundPlayer.Play();
                    }
                    Console.WriteLine("***** dizi kayitlarda yok ...");
                }
            }
            else
                Console.WriteLine("chrome calismiyor");
            
        }


        public int rastgele_sayi(int max)
        {
            int randomNumber = random.Next(0,max);
            return randomNumber;
        }
    }
}