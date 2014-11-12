using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WielomianInterpolujacy
{
    class Program
    {
        private static int _wymiar;
        private static int _wyznacznikGłówny;
        private static int[] _węzły;
        private static int[] _dane;
        private static int[] _wyznaczniki;
        private static int[] _współczynniki;
        private static int[,] _macierz;

        /* -------------------------
        *  macierz[WIERSZ, KOLUMNA] 
        *  ------------------------- */

        static void Main(string[] args)
        {
            WczytajDane();
            if (!(_wymiar > 0))
            {
                Console.WriteLine("Podany wymiar jest niewłaściwy.");
                Thread.Sleep(10000);
                Environment.Exit(1);
            }


            StwórzMacierz();
            Console.WriteLine("Macierz główna: ");
            DrukujMacierz(_macierz);
            _wyznacznikGłówny = Wyznacznik(_macierz.GetLength(0), _macierz);
            Console.WriteLine("\nWyznacznik główny: " + _wyznacznikGłówny);
            Console.WriteLine("\nWyznaczniki cząstkowe: ");
            ObliczWyznaczniki();
            Console.WriteLine("\nWspółczynniki wielomianu: ");
            ObliczWspółczynniki();
            DrukujWielomian();
            DrukujCalke();

            Console.ReadLine();
        }


        static void WczytajDane()
        {
            Console.WriteLine("Podaj ilość węzłów:");
            _wymiar = Int32.Parse(Console.ReadLine());
            _węzły = new int[_wymiar];
            _dane = new int[_wymiar];
            _wyznaczniki = new int[_wymiar];
            _współczynniki = new int[_wymiar];
            _macierz = new int[_wymiar, _wymiar];

            Console.WriteLine();
            for (int i = 0; i < _wymiar; i++)
            {
                Console.Write("Podaj wartość węzła x" + i + ": ");
                int wartość = Int32.Parse(Console.ReadLine());
                if (_węzły.Contains(wartość))
                {
                    Console.WriteLine("Podana wartość węzła powtarza się.");
                    i--;
                }
                else
                {
                    _węzły[i] = wartość;
                }

            }

            Console.WriteLine();
            for (int i = 0; i < _wymiar; i++)
            {
                Console.Write("Podaj daną y" + i + ": ");
                _dane[i] = Int32.Parse(Console.ReadLine());
            }

            Console.WriteLine();
        }

        static void StwórzMacierz()
        {
            for (int wiersz = 0; wiersz < _wymiar; wiersz++)
            {
                for (int kolumna = 0; kolumna < _wymiar; kolumna++)
                {
                    _macierz[wiersz, kolumna] = Potęga(_węzły[wiersz], kolumna);
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

        static void ObliczWyznaczniki()
        {
            for (int i = 0; i < _wymiar; i++)
            {
                _wyznaczniki[i] = Wyznacznik(_wymiar, MacierzTymczasowa(i, _macierz));
                Console.WriteLine(" W" + i + ": " + _wyznaczniki[i] + "\n");
            }
        }

        static int[,] MacierzTymczasowa(int kolumna, int[,] macierz)
        {
            int[,] macierzTymczasowa = (int[,]) macierz.Clone();

            for (int i = 0; i < _wymiar; i++)
            {
                macierzTymczasowa[i, kolumna] = _dane[i];
            }

            DrukujMacierz(macierzTymczasowa);

            return macierzTymczasowa;
        }

        static int Współczynnik(int rząd)
        {
            return _wyznaczniki[rząd]/_wyznacznikGłówny;
        }

        static void ObliczWspółczynniki()
        {
            for (int i = 0; i < _wymiar; i++)
            {
                _współczynniki[i] = Współczynnik(i);
                Console.WriteLine(" a" + i + ": " + _współczynniki[i]);
            }
        }

        static void DrukujWielomian()
        {
            Console.WriteLine("\nWielomian interpolujący: ");
            for (int i = 0; i < _wymiar; i++)
            {
                // na potrzeby formatowania
                bool flaga = false;

                if (_współczynniki[i] != 0)
                {
                    if (i == 0)
                        Console.Write(_współczynniki[i]);
                    else if (i != 1)
                        Console.Write(_współczynniki[i] + "x^" + i);
                    else
                        Console.Write(_współczynniki[i] + "x");


                    flaga = true;
                }

                if (i + 1 < _współczynniki.Length && flaga)  
                    if (_współczynniki[i + 1] != 0)
                        Console.Write(" + ");
            }
        }

        static void DrukujCalke()
        {
            Console.WriteLine("\nCalka: ");
            for (int i = 0; i < _wymiar; i++)
            {
                bool flaga = false;

                if (_współczynniki[i] != 0)
                {
                    if (i == 0)
                        Console.Write(_współczynniki[i] + "x");
                    else
                        Console.Write("("+_współczynniki[i] + "x^" + (i+1) + ")/"+(i+1));

                    flaga = true;
                }

                if (i + 1 < _współczynniki.Length && flaga)
                {
                    if (_współczynniki[i + 1] != 0)
                        Console.Write(" + ");
                }
            }
            Console.Write(" + C\n\n");

        }

        static void DrukujMacierz(int[,] macierz)
        {
            for (int wiersz = 0; wiersz < _wymiar; wiersz++)
            {
                for (int kolumna = 0; kolumna < _wymiar; kolumna++)
                {
                    Console.Write(macierz[wiersz, kolumna] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
