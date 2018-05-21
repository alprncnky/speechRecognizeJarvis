using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecognizeJarvis
{
    class Komutlar
    {
        // yeni komut olursa bu string lere olusabilecek ses leri ekle
        public int komutSayisi;
        string[] komutlar1 = { "Jurors", ": who",   "We have" , "ZZ" ,      "you"           ,"wants so"     , "favor", "vhvvv", "vhvvv" };         // birebir eşit olan
        string[] komutlar2 = { "Jones",  ":"    ,   "will have" , "disease" ,    "to"       ,"loss of"      , "fewer", "vhvvv", "vhvvv" };         // birebir eşit olan
        string[] komutlar3 = { "just",   ": if" ,   "one" , "diseases" ,     "knew"         ,"muscle"       , "to human", "vhvvv", "vhvvv" };         // birebir eşit olan
        string[] komutlar4 = { "Joe's",  "for" ,    "11" , "dispatch" ,     "know"          ,"listen"       , "human", "vhvvv", "vhvvv" };         // birebir eşit olan
        string[] komutlar5 = { "jazz","homes" , "one have" ,    "the season" , "wexrdctf"   ,"was a"        , "feeding", "vhvvv", "vhvvv" };        // birebir eşit olan
        string[] komutlar6 = { "DOS", "former" ,    "would have" , "season", "wexrdctfv"    ,"Wilson"       , "feel", "vhvvv", "vhvvv" };
        string[] komutlar7 = { "germs", "Portland" ,"with" , "large", "wexrdctfvy"          ,"will see"     , "few", "vhvvv", "vhvvv" };
        string[] komutlar8 = { "James", ": school" ,"to have" , "zero", "wexrdctfvun"       ,"fdsfs"        , "fitting", "vhvvv", "vhvvv" };
        string[] komutlar9 = { "Jacques", "forms", "than have" , "easy", "wexrdctfvyn"      ,"fdsfs"        , "feet", "vhvvv", "vhvvv" };
        string[] komutlar10 = { "jackie", "four", "one more", "to dispatch", "wexrdctfvn"       ,"lots of"  , "fear", "vhvvv", "vhvvv" };
        string[] komutlar11 = { "generous", "formed", "when" , "ZF", "wexrdctfv"        , "wants"           , "female", "vhvvv", "vhvvv" };         // birebir eşit olan
        string[] komutlar12 = { "jytvbnun", "jytvbnun", "who have", "search", "wexrdctf"    , "also"        , "fever", "vhvvv", "vhvvv" };         // birebir eşit olan

        public Komutlar()
        {
            komutSayisi = komutlar1.Length;
        }

        public int komutBul(string gelenKelime)         // gelen kelime komut stringlerinden birine eslestirip hangi komut olduguna gore sayi donduruyor.
        {
            int number = -1;
            for(int i=0;i<komutSayisi;i++)
            {
                if (String.Equals(komutlar1[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (String.Equals(komutlar2[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - "+gelenKelime);
                    break;
                }
                if (String.Equals(komutlar3[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (String.Equals(komutlar4[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (String.Equals(komutlar5[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (gelenKelime.Contains(komutlar6[i]))   // string in icinde varsa
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (gelenKelime.Contains(komutlar7[i]))   // string in icinde varsa
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (gelenKelime.Contains(komutlar8[i]))   // string in icinde varsa
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (gelenKelime.Contains(komutlar9[i]))   // string in icinde varsa
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (gelenKelime.Contains(komutlar10[i]))   // string in icinde varsa
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (String.Equals(komutlar11[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
                if (String.Equals(komutlar12[i], gelenKelime, StringComparison.OrdinalIgnoreCase))   // - stringler ikisi eşitse
                {
                    number = i;
                    Console.WriteLine("- - - " + gelenKelime);
                    break;
                }
            }
            return number;
        }

    }
}
