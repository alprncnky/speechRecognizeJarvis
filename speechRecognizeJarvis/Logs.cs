using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecognizeJarvis
{
    class Logs
    {
        string path = "..\\..\\..\\logs\\kayit.txt";

        public Logs()
        {
            // eger logs dosyasi icinde kayit.txt yoksa olustur
            if (!File.Exists(path))
                File.WriteAllText(path, "");
        }

        // baska class lardan cagir 
        public void logYaz(string str)
        {
            string readText = System.IO.File.ReadAllText(path);
            readText += str + " - ";
            readText += DateTime.Now.ToString("h:mm:ss tt");
            readText += "\r\n";
            File.WriteAllText(path, readText);
        }



    }
}
