using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WielomianInterpolujacy
{
    class Program
    {
        private static int wymiar;
        private static int[] węzły;
        private static int[] dane;
        private static int[,] macierz;

        static void Main(string[] args)
        {
            WczytajDane();
            StwórzMacierz();

            DrukujMacierz();
            
            Console.ReadLine();
        }

        /*          wykładnik
         *          potęgi  --->
         *  węzeł   1   1   1
         *     |    1   2   4
         *     |    1   3   9
         *     \/
         * 
         * 
         */

        static void WczytajDane()
        {
            Console.WriteLine("Podaj ilość węzłów:");
            wymiar = Int32.Parse(Console.ReadLine());
            // TODO: wymiar ma być większy od ?
            węzły = new int[wymiar];
            dane = new int[wymiar];
            macierz = new int[wymiar, wymiar];

            Console.WriteLine();
            for (int i = 0; i < wymiar; i++)
            {
                Console.Write("Podaj wartość węzła x" + i + ": ");
                węzły[i] = Int32.Parse(Console.ReadLine());
                // TODO: węzły 'x' mają być różne
            }

            Console.WriteLine();
            for (int i = 0; i < wymiar; i++)
            {
                Console.Write("Podaj daną y" + i + ": ");
                dane[i] = Int32.Parse(Console.ReadLine());
            }

            Console.WriteLine();
        }

        static void StwórzMacierz()
        {
            for (int węzeł = 0; węzeł < wymiar; węzeł++)
            {
                for (int wykładnik = 0; wykładnik < wymiar; wykładnik++)
                {
                    macierz[wykładnik, węzeł] = Potęga(węzły[węzeł], wykładnik);
                }
            }
        }

        static int Potęga(int a, int b)
        {
            if (b == 0) 
                return 1;
            else
                return a = a * Potęga(a, --b);
        }

        static void DrukujMacierz()
        {
            Console.WriteLine("\n Macierz: ");
            for (int wiersz = 0; wiersz < wymiar; wiersz++)
            {
                for (int kolumna = 0; kolumna < wymiar; kolumna++)
                {
                    Console.Write(macierz[wiersz, kolumna] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
