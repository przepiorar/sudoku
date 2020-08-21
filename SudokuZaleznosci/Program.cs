using System;
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
            bool inny = true;
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
                foreach (var item in listMatrix)
                {
                    if (CellControls == item)
                    {
                        inny = false;
                        break;
                    }
                }
                if (inny)
                {
                    listMatrix.Add(CellControls);
                }
                else
                {
                    b--;
                }
                inny = true;
                if (b%1000==0)
                {
                    Console.WriteLine("iteracja nr: " + b);
                }
            }
        }

        public static List<List<int>> wykryjTrojkiIDwojki(List<Matrix> SudokuList, int ilosc)
        //zwraca krotkę złożoną z 2 list 2-wymiarowych (numer sudoku w którym występuje i nr trójki/dwójki)
        //1 lista jest dla trójek, a 2 lista jest dla dwójek
        {
            List<List<int>> listaZaleznosci = new List<List<int>>(ilosc);// lista list składa się z listy dwójek i trójek dla każdego sudoku

            for (int i = 1; i < ilosc + 1; i++)
            {
                listaZaleznosci.Add(new List<int> { i }); //dodanie nr sudoku do listy zalezności
            }
            
            int maxSuma = 0;
            int tmp = 0;
            int numer = 0;
            bool brak = true;
            int ileBrak = 0;
            for (int a = 0; a < SudokuList.Count; a++)
            {
                for (int i = 0; i < Stale.ListaTrojek.Count; i++)
                {
                    if (SudokuList[a][Stale.ListaTrojek[i][0,0], Stale.ListaTrojek[i][0,1]]== SudokuList[a][Stale.ListaTrojek[i][1, 0], Stale.ListaTrojek[i][1, 1]] &&
                        SudokuList[a][Stale.ListaTrojek[i][0, 0], Stale.ListaTrojek[i][0, 1]] == SudokuList[a][Stale.ListaTrojek[i][2, 0], Stale.ListaTrojek[i][2, 1]])
                    {
                        listaZaleznosci[a].Add(i); // dodanie nr trójki do odpowiedniej podlisty(z numerem sudoku-1)
                        tmp--;
                    }
                }
                for (int i = 0; i < Stale.ListaDwojek.Count; i++)
                {
                    if (SudokuList[a][Stale.ListaDwojek[i][0, 0], Stale.ListaDwojek[i][0, 1]] == SudokuList[a][Stale.ListaDwojek[i][1, 0], Stale.ListaDwojek[i][1, 1]])
                    {
                        listaZaleznosci[a].Add(i + 16); //dodanie nr dwójki do odpowiedniej podlisty(z numerem sudoku-1)
                        tmp++;
                        brak = false;
                    }
                }
                if (tmp > maxSuma)
                {
                    maxSuma = tmp;
                    numer = a;
                }
                tmp = 0;
                if (brak)
                {
                    ileBrak++;
                }
                brak = true;
            }
            Console.WriteLine("Maksymalna ilość trójek i dwójek to: " + maxSuma + " , w sudoku numer: " + numer); //za wczesnie bo trojki liczy potrojnie moze-1 przy trojce xD
            Console.WriteLine("Sudoku bez żadnej dwójki i trójki: " + ileBrak);
            for (int i = 0; i < listaZaleznosci.Count; i++)
            {
                if (listaZaleznosci[i].Count < 3)//jeśli jest tylko jedna lub żadna trójka lub dwójka
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

        public static List<int[,]> znajdzZaleznosci (Matrix macierz)
        {
            List<int[,]> potencjalne = new List<int[,]>();
            int prog = 25;
            for (int i = 0; i < 72; i++)
            {
                for (int j = 0; j < 72; j++)
                {
                    if (prog < macierz[i, j] && i != j)
                    {
                        potencjalne.Add(new int[1, 2] { { i, j } });
                    }
                }
            }
            int licznik = potencjalne.Count;
            for (int i = 0; i < licznik; i++)
            {
                int a = potencjalne[i][0, 0]; 
                int b = potencjalne[i][0, 1];
                int tmp = 0;
                if (a < 16 && b < 16 || a>=16 && b>=16)
                { }
                else
                {
                    if (a > b) //znaczy to że a jest dwójką a b trójką
                    { //zamieniamy a z b tak żeby a było trójką b dwójką
                        tmp = a;
                        a = b;
                        b = tmp;
                    }
                    b = b - 16; //w macierzy dwójki są numerowane w przedziale 15-71, a w programie 0-55
                    if (Stale.ListaTrojek[a][0,0] ==Stale.ListaDwojek[b][0,0] && Stale.ListaTrojek[a][0, 1] == Stale.ListaDwojek[b][0, 1] &&
                        Stale.ListaTrojek[a][1,0] == Stale.ListaDwojek[b][1,0] && Stale.ListaTrojek[a][1, 1] == Stale.ListaDwojek[b][1, 1] || 
                        Stale.ListaTrojek[a][1,0] == Stale.ListaDwojek[b][0,0] && Stale.ListaTrojek[a][1, 1] == Stale.ListaDwojek[b][0, 1] &&
                        Stale.ListaTrojek[a][2,0] == Stale.ListaDwojek[b][1,0] && Stale.ListaTrojek[a][2, 1] == Stale.ListaDwojek[b][1, 1])
                    {
                        potencjalne.RemoveAt(i);
                        licznik--;
                        i--;
                    }

                }
            }
            return potencjalne;
        }


        public static Matrix KostkaZaleznosci(List<List<int>> wynik,Matrix macierz)
        {
            Matrix macierzWynikowa = new Matrix(72, 72);
            foreach (var item in wynik)
            {
                for (int i = 1; i < item.Count; i++) //pierwszy element to numer sudoku
                {
                    for (int j = i + 1; j < item.Count; j++)
                    {
                        macierzWynikowa[item[i], item[j]]++; //dodanie zaleznosci miedzy a i b
                        macierzWynikowa[item[j], item[i]]++;
                    }
                    macierzWynikowa[item[i], item[i]]++; //dodanie informacji ze w sudoku wystapila dana trojka lub dwojka
                }
            }
            return macierzWynikowa;
        }

        static void Main(string[] args)
        {
            SudokuBoard GameBoard = new SudokuBoard();
            List<Matrix> SudokuList = new List<Matrix>();
            List<List<int>> wynik = new List<List<int>>();
            int iloscGeneracji = 10000;
            generuj(iloscGeneracji, GameBoard, SudokuList);
            wynik = wykryjTrojkiIDwojki(SudokuList, iloscGeneracji);
            Metody.zapisz(SudokuList);
            
            //foreach (var item in wynik)
            //{
            //    string text = "nr sudoku: " + item[0] + " wykryto: ";
            //    for (int i = 1; i < item.Count; i++)
            //    {
            //        text += item[i]+1 + " ";
            //    }
            //    Console.WriteLine(text);
            //}
            Matrix macierzKoncowa = MacierzZaleznosci(wynik);
            string nazwa = "Wynik2.txt";
            Metody.zapisz2(macierzKoncowa, nazwa);
            Matrix macierzKoncowaProcentowa = obliczProcent(macierzKoncowa);
            nazwa = "Wynik3.txt";
            Metody.zapisz2(macierzKoncowaProcentowa, nazwa);

            List<int[,]> wykryteZaleznosci = znajdzZaleznosci(macierzKoncowaProcentowa);
            for (int i = 0; i < wykryteZaleznosci.Count; i++)
            {
                Console.WriteLine((wykryteZaleznosci[i][0,0]+1) + " " + (wykryteZaleznosci[i][0, 1]+1) + " proc: " + macierzKoncowaProcentowa[wykryteZaleznosci[i][0, 0], wykryteZaleznosci[i][0, 1]] +  "\n");
            }
            Console.WriteLine(wykryteZaleznosci.Count);
            Console.ReadKey();
        }
    }
}