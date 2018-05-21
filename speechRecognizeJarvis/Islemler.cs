﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using Emgu.CV;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Media;
using System.Windows.Forms;
using System.Threading;
using System.Security.Permissions;

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


        string path = @"C:\Users\alperen\Documents\Jarvis\program data\jarvis.wav";     // !!! farkli pc de degistir  !!!  jarvis ses dosyasi
        string wp_path = @"C:\Users\alperen\AppData\Local\WhatsApp\WhatsApp.exe";        // !!! farkli pc de degistir  !!!  whatsapp.exe calistirmak icin exe konumu
        Thread tid1;
        html h = new html();

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
                    filmgetir();
                    break;

                default:
                    Console.WriteLine("hatalı");
                    break;
            }
        }


        //- - - - FONKSIYONLAR - - - -

        public void jarvis()
        {
            // bu fonk. kamera isigini yakip sondurup ve caps lock tusuna basar. ( Amac jarvis sesi geldiğinde kullanıcıya bunu algıladığını isaret vermektir. )
            Console.WriteLine("- - - -jarvis- - -- ");

            InputSimulator sim = new InputSimulator();
            VideoCapture capture = new VideoCapture();
            bool tell = capture.IsOpened;
            capture.Dispose();

            using (var soundPlayer = new SoundPlayer(path))
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
            // youtube aciliyor
            System.Diagnostics.Process.Start("http://www.youtube.com");
        }

        public void whatsapp()
        {
            // whatsapp.exe calistiriliyor
            System.Diagnostics.Process.Start(wp_path);
        }





        public void filmoner()
        {
            // hangi kategoride film istersiniz?
            // ses
            Console.WriteLine(" ***** fonskiyona girdi");
        }

        public void filmgetir()
        {
            // filmler listeleniyor
            //ses
            // pencerede ac film resmini ve ismini
            h.str_yukle(1);
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
            // yeni film öneri
            if (filmNo <= 5)
            {
                tid1.Abort();
                SetCursorPos(960, 540);
                filmNo++;
                filmName = h.filmIsimleri[filmNo];
                filmPhoto = h.filmresimleri[filmNo];
                tid1 = new Thread(new ThreadStart(Islemler.Thread1));
                tid1.Start();
            }
        }

    }
}