using System;
using System.Collections.Generic;

namespace speechRecognizeJarvis
{
    class Komutlar
    {
        public int komutSayisi;

        public List<string> komutlar1 = new List<string>();
        public List<string> komutlar2 = new List<string>();
        public List<string> komutlar3 = new List<string>();
        public List<string> komutlar4 = new List<string>();
        public List<string> komutlar5 = new List<string>();
        public List<string> komutlar6 = new List<string>();
        public List<string> komutlar7 = new List<string>();
        public List<string> komutlar8 = new List<string>();
        public List<string> komutlar9 = new List<string>();
        public List<string> komutlar10 = new List<string>();
        public List<string> komutlar11 = new List<string>();
        public List<string> komutlar12 = new List<string>();

        public Komutlar()
        {
            KomutDosyasi k = new KomutDosyasi();
            komutlar1 = k.D_komutlar1;
            komutlar2 = k.D_komutlar2;
            komutlar3 = k.D_komutlar3;
            komutlar4 = k.D_komutlar4;
            komutlar5 = k.D_komutlar5;
            komutlar6 = k.D_komutlar6;
            komutlar7 = k.D_komutlar7;
            komutlar8 = k.D_komutlar8;
            komutlar9 = k.D_komutlar9;
            komutlar10 = k.D_komutlar10;
            komutlar11 = k.D_komutlar11;
            komutlar12 = k.D_komutlar12;
            komutSayisi = komutlar1.Count;
        }

        public int komutBul(string gelenKelime)         // gelen kelime komut stringlerinden birine eslestirip hangi komut olduguna gore sayi donduruyor.
        {
            int number = -1;
            for(int i=0;i<komutSayisi;i++)
            {
                if (String.Equals(komutlar1[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar1[i]);
                    number = i;
                    break;
                }
                if (String.Equals(komutlar2[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar2[i]);
                    number = i;
                    break;
                }
                if (String.Equals(komutlar3[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar3[i]);
                    number = i;
                    break;
                }
                if (String.Equals(komutlar4[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar4[i]);
                    number = i;
                    break;
                }
                if (String.Equals(komutlar5[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar5[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar6[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar6[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar7[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar7[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar8[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar8[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar9[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar9[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar10[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar10[i]);
                    number = i;
                    break;
                }
                if (gelenKelime.Contains(komutlar11[i]))   // string in icinde varsa
                {
                    Console.WriteLine("if eslesmesi :" + komutlar11[i]);
                    number = i;
                    break;
                }
                if (String.Equals(komutlar12[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    Console.WriteLine("if eslesmesi :" + komutlar12[i]);
                    number = i;
                    break;
                }
            }
            return number;
        }

    }
}
