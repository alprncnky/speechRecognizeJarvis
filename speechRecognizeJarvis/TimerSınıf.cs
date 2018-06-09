using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecognizeJarvis
{
    class TimerSınıf
    {
        public int sure = 0;
        public int choice = 0;

        public void zaman(int dakika,int ch)
        {
            sure = (1000) * 60 * dakika;
            choice = ch;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = sure;
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            // zaman yarı calicak ama ilk kod gelidigindede fonksiyon calissin diye burda cagirdim.
            if (choice == 1)
                one();
        }

        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //YourCode
            switch(choice)
            {
                case 1:
                    // run one() function
                    one();
                    break;

                    /*
                case 2:
                    // yeni ekle
                    break;
                    */

                default:
                    break;
            }

        }

        // --- FONKSIYONLARI BURAYA EKLE

        public void one()
        {
            // arka planda belirli aralıklarla chrome gecmisini incelemek lazım
            // chrome calisiyorsa ac
            Process[] chromeInstances = Process.GetProcessesByName("chrome");
            if (chromeInstances.Length == 0)
            {
                Dizihistory nesne = new Dizihistory();
                nesne.chrome_history();
            }
        }
    }
}
