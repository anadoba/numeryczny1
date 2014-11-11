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
            Console.WriteLine("Wyznacznik główny: " + Wyznacznik(macierz.GetLength(0), macierz));

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
                return a = a*Potęga(a, --b);
        }

        static int Wyznacznik(int stopień, int[,] macierz)
        {
            // algorytm oparty na rozwinięciu Laplace'a
            int suma = 0;
            int znak = 1;

            if (stopień == 1)
                return macierz[0, 0];
            else
            {
                int[,] temp = new int[stopień - 1, stopień - 1];
                int i, j, p, m = 0, n = 0;
                for (p = 0; p < stopień; p++)
                {
                    m = 0;
                    for (i = 0; i < stopień; i++)
                    {
                        n = 0;
                        for (j = 0; j < stopień; j++)
                        {
                            if (i != 0 && j != p)
                            {
                                temp[m, n] = macierz[i, j];
                                n++;
                            }
                        }

                        if (i != 0)
                            m++;
                    }
                    suma = suma + macierz[0, p]*znak*Wyznacznik(stopień - 1, temp);
                    znak = -znak;
                }
                return suma;
            }
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
