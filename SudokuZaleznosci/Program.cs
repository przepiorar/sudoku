﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku;
using System.IO;

namespace SudokuZaleznosci
{
    class Program
    {
        public static void generuj(int a, SudokuBoard GameBoard, List<Matrix> listMatrix)
        {
            for (int b = 0; b < a; b++)
            {
                Matrix CellControls = new Matrix(9,9);
                GameBoard.Clear();
                GameBoard.Solver.SolveThePuzzle(UseRandomGenerator: true);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        CellControls[i, j] = GameBoard.GetCell(9 * i + j).Value;
                    }
                }
              //  Console.WriteLine("plansza " +(b+1) + "\n"+  CellControls.ToString());
                listMatrix.Add(CellControls);
            }
        }

        public static Tuple< List<int[,]>, List<int[,]>> wykryjTrojkiIDwojki(List<Matrix> SudokuList)
        //zwraca krotkę złożoną z 2 list 2-wymiarowych (numer sudoku w którym występuje i nr trójki/dwójki)
        //1 lista jest dla trójek, a 2 lista jest dla dwójek
        {
            List<int[,]> macierzeZTrojka = new List<int[,]>();

            List<int[,]> MozliweTrojki = new List<int[,]>();
            MozliweTrojki.Add(new int[3, 2] { { 1, 2 }, { 2, 3 }, { 3, 4 } });
            MozliweTrojki.Add(new int[3, 2] { { 2, 1 }, { 3, 2 }, { 4, 3 } });
            MozliweTrojki.Add(new int[3, 2] { { 3, 1 }, { 2, 2 }, { 1, 3 } });
            MozliweTrojki.Add(new int[3, 2] { { 4, 2 }, { 3, 3 }, { 2, 4 } });

            MozliweTrojki.Add(new int[3, 2] { { 4, 2 }, { 5, 3 }, { 6, 4 } });
            MozliweTrojki.Add(new int[3, 2] { { 5, 1 }, { 6, 2 }, { 7, 3 } });
            MozliweTrojki.Add(new int[3, 2] { { 6, 1 }, { 5, 2 }, { 4, 3 } });
            MozliweTrojki.Add(new int[3, 2] { { 7, 2 }, { 6, 3 }, { 5, 4 } });

            MozliweTrojki.Add(new int[3, 2] { { 1, 5 }, { 2, 6 }, { 3, 7 } });
            MozliweTrojki.Add(new int[3, 2] { { 2, 4 }, { 3, 5 }, { 4, 6 } });
            MozliweTrojki.Add(new int[3, 2] { { 3, 4 }, { 2, 5 }, { 1, 6 } });
            MozliweTrojki.Add(new int[3, 2] { { 4, 5 }, { 3, 6 }, { 2, 7 } });

            MozliweTrojki.Add(new int[3, 2] { { 4, 5 }, { 5, 6 }, { 6, 7 } });
            MozliweTrojki.Add(new int[3, 2] { { 5, 4 }, { 6, 5 }, { 7, 6 } });
            MozliweTrojki.Add(new int[3, 2] { { 6, 4 }, { 5, 5 }, { 4, 6 } });
            MozliweTrojki.Add(new int[3, 2] { { 7, 5 }, { 6, 6 }, { 5, 7 } });


            List<int[,]> macierzeZDwojka = new List<int[,]>();

            List<int[,]> MozliweDwojki = new List<int[,]>();
            MozliweDwojki.Add(new int[2, 2] { { 0, 2 }, { 1, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 1, 2 }, { 2, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 2 }, { 3, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 2 }, { 4, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 4, 2 }, { 5, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 2 }, { 6, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 2 }, { 7, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 7, 2 }, { 8, 3 }, });

            MozliweDwojki.Add(new int[2, 2] { { 0, 5 }, { 1, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 1, 5 }, { 2, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 5 }, { 3, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 5 }, { 4, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 4, 5 }, { 5, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 5 }, { 6, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 5 }, { 7, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 7, 5 }, { 8, 6 }, });

            MozliweDwojki.Add(new int[2, 2] { { 2, 0 }, { 3, 1 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 1 }, { 3, 2 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 3 }, { 3, 4 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 4 }, { 3, 5 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 6 }, { 3, 7 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 7 }, { 3, 8 }, });

            MozliweDwojki.Add(new int[2, 2] { { 5, 0 }, { 6, 1 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 1 }, { 6, 2 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 3 }, { 6, 4 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 4 }, { 6, 5 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 6 }, { 6, 7 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 7 }, { 6, 8 }, });


            MozliweDwojki.Add(new int[2, 2] { { 1, 2 }, { 0, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 2 }, { 1, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 2 }, { 2, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 4, 2 }, { 3, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 2 }, { 4, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 2 }, { 5, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 7, 2 }, { 6, 3 }, });
            MozliweDwojki.Add(new int[2, 2] { { 8, 2 }, { 7, 3 }, });

            MozliweDwojki.Add(new int[2, 2] { { 1, 5 }, { 0, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 2, 5 }, { 1, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 5 }, { 2, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 4, 5 }, { 3, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 5, 5 }, { 4, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 5 }, { 5, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 7, 5 }, { 6, 6 }, });
            MozliweDwojki.Add(new int[2, 2] { { 8, 5 }, { 7, 6 }, });

            MozliweDwojki.Add(new int[2, 2] { { 3, 0 }, { 2, 1 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 1 }, { 2, 2 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 3 }, { 2, 4 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 4 }, { 2, 5 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 6 }, { 2, 7 }, });
            MozliweDwojki.Add(new int[2, 2] { { 3, 7 }, { 2, 8 }, });

            MozliweDwojki.Add(new int[2, 2] { { 6, 0 }, { 5, 1 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 1 }, { 5, 2 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 3 }, { 5, 4 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 4 }, { 5, 5 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 6 }, { 5, 7 }, });
            MozliweDwojki.Add(new int[2, 2] { { 6, 7 }, { 5, 8 }, });

            for (int a = 0; a < SudokuList.Count; a++)
            {
                for (int i = 0; i < MozliweTrojki.Count; i++)
                {
                    if (SudokuList[a][MozliweTrojki[i][0,0], MozliweTrojki[i][0,1]]== SudokuList[a][MozliweTrojki[i][1, 0], MozliweTrojki[i][1, 1]] &&
                        SudokuList[a][MozliweTrojki[i][0, 0], MozliweTrojki[i][0, 1]] == SudokuList[a][MozliweTrojki[i][2, 0], MozliweTrojki[i][2, 1]])
                    {
                        macierzeZTrojka.Add(new int[1, 2] { { a+1, i } });                       
                    }
                }
                for (int i = 0; i < MozliweDwojki.Count; i++)
                {
                    if (SudokuList[a][MozliweDwojki[i][0, 0], MozliweDwojki[i][0, 1]] == SudokuList[a][MozliweDwojki[i][1, 0], MozliweDwojki[i][1, 1]])
                    {
                        macierzeZDwojka.Add(new int[1, 2] { { a + 1, i  } });
                    }
                }
            }
            return new Tuple<List<int[,]>, List<int[,]>>(macierzeZTrojka,macierzeZDwojka) ;
        }

        public static List<List<int>> wykryjZaleznosci(Tuple<List<int[,]>,List<int[,]>> krotka, int ilosc)
        {
            List<List<int>> listaZaleznosci = new List<List<int>>(ilosc);

            for (int i = 1; i < ilosc+1; i++)
            {
                listaZaleznosci.Add(new List<int> {i}); //dodanie nr sudoku do listy zalezności
            }
            foreach (var item in krotka.Item1)
            {
                listaZaleznosci[item[0, 0]-1].Add(item[0, 1]); // dodanie nr trójki do odpowiedniej podlisty(z numerem sudoku-1)
            }
            foreach (var item in krotka.Item2)
            {
                listaZaleznosci[item[0, 0]-1].Add(item[0, 1] + 16); //dodanie nr dwójki do odpowiedniej podlisty(z numerem sudoku-1)
            }
            for (int i = 0; i < listaZaleznosci.Count; i++)
            {
                if (listaZaleznosci[i].Count <3)
                {
                    listaZaleznosci.RemoveAt(i);
                    i--;
                }
            }
            return listaZaleznosci;
        }

        public static Matrix MacierzZaleznosci(List<List<int>> wynik)
        {
            Matrix macierzWynikowa = new Matrix(72,72);
            foreach (var item in wynik)
            {
                for (int i = 1; i < item.Count; i++) //pierwszy element to numer sudoku
                {
                    for (int j = i+1; j < item.Count; j++)
                    {
                        macierzWynikowa[item[i], item[j]]++; //dodanie zaleznosci miedzy a i b
                        macierzWynikowa[item[j], item[i]]++;
                    }
                    macierzWynikowa[item[i], item[i]]++; //dodanie informacji ze w sudoku wystapila dana trojka lub dwojka
                }
            }
            return macierzWynikowa;
        }

        public static Matrix obliczProcent(Matrix macierz)
        {
            Matrix macierzWynikowa = new Matrix(72, 72);
            for (int i = 0; i < 72; i++)
            {
                for (int j  = 0; j < 72; j++)
                {
                    if (macierz[i, i] != 0)
                    {
                        macierzWynikowa[i, j] = Math.Round(100 * macierz[i, j] / macierz[i, i], 2);
                    }
                    else
                        macierzWynikowa[i, j] = 0;
                }
            }
            return macierzWynikowa;
        }

        public static double znajdzNajwiekszyProcent (Matrix macierz)
        {
            double max = 0;
            for (int i = 0; i < 72; i++)
            {
                for (int j = 0; j < 72; j++)
                {
                    if (max < macierz[i, j] && macierz[i, j] != 100)
                        max = macierz[i, j];
                }
            }
            return max;
        }
        

        static void Main(string[] args)
        {
            SudokuBoard GameBoard = new SudokuBoard();
            List<Matrix> SudokuList = new List<Matrix>();
            Tuple<List<int[,]>, List<int[,]>> krotka;       
            int iloscGeneracji = 20;
            generuj(iloscGeneracji, GameBoard, SudokuList);
            krotka = wykryjTrojkiIDwojki(SudokuList);
            Metody.zapisz(SudokuList);

            List<List<int>> wynik = new List<List<int>>();
            wynik = wykryjZaleznosci(krotka, iloscGeneracji);
            foreach (var item in wynik)
            {
                string text = "nr sudoku: " + item[0] + " wykryto: ";
                for (int i = 1; i < item.Count; i++)
                {
                    text += item[i]+1 + " ";
                }
                Console.WriteLine(text);
            }
            Matrix macierzKoncowa = MacierzZaleznosci(wynik);
            string nazwa = "Wynik2.txt";
            Metody.zapisz2(macierzKoncowa, nazwa);
            Matrix macierzKoncowa2 = obliczProcent(macierzKoncowa);
            nazwa = "Wynik3.txt";
            Metody.zapisz2(macierzKoncowa2, nazwa);

            double max = znajdzNajwiekszyProcent(macierzKoncowa2);
            Console.WriteLine(max);
            Console.ReadKey();
        }
    }
}