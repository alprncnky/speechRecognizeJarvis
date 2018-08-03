using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace speechRecognizeJarvis
{
    class Program
    {
        static SpeechRecognitionEngine engine;
        static Komutlar n = new Komutlar();
        static Islemler islem = new Islemler();
        static DateTime time;
        static DateTime time2;
        static int t;
        static int t2;
        static bool activate;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Main(string[] args)
        {
            
            const int SW_HIDE = 0;
            // const int SW_SHOW = 5;   // show icin deger
            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);
            // Show
            // ShowWindow(handle, SW_SHOW);      
                 
            // program pc acildiginda otomatik acilma
            RegistryKey rk = Registry.CurrentUser.OpenSubKey                // Burasi pc acildiginda auto baslatmak icin
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("speechRecognizeJarvis", Application.ExecutablePath);

            // her 30 dakikada bir calistir
            TimerSınıf tnesne = new TimerSınıf();
            tnesne.zaman(30, 1);

            activate = false;
            t = 0;
            t2 = 0;
            engine = new SpeechRecognitionEngine();
            engine.SetInputToDefaultAudioDevice();
            Grammar g = new DictationGrammar();
            engine.LoadGrammar(g);
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
            Console.ReadLine();       
        }

        static void engine_SpeechRecognized(object ob, SpeechRecognizedEventArgs e)
        {
            string word = e.Result.Text.ToString();
            Console.WriteLine("gelen input :"+word);
            int sayi= n.komutBul(word);         // gelen ses in hangi komut oldugunun sayisi bulundu //  "0" gelirse jarvis , "-1" gelirse kelime bulunamadı demek

            if (sayi == 0)                // jarvis seslenmisse zamanı tut
            {
                activate = true;
                time = DateTime.Now;
                t = time.Second;
            }

            time2 =  DateTime.Now;
            t2 = time2.Second;
            int zaman = t2 - t;
            if (zaman < 0)
                zaman = 60 + zaman;

            if (zaman > 20)         // jarvis kelimesi algılandıktan sonra 20 sn gecmisse artık komut calistirma
                activate = false;

            if (sayi!=-1 && activate)                        // -1 donmesi demek hicbir sese eslesmedi yani bir fonksiyon cagirmamiza gerek yok
            {
                islem.fonksiyonBul(sayi);       // bulunan sayiya gore calısacak fonksiyona git
            }
        }
    }
}
