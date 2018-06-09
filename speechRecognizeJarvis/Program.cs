using System;
using System.Speech.Recognition;


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

        static void Main(string[] args)
        {
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
