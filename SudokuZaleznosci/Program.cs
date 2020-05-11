using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        public class Matrix
        {
            private int[,] storage = new int[9, 9];

            public int this[int row, int column]
            {
                get { return storage[row, column]; }
                set { storage[row, column] = value; }
            }
            public override string ToString()
            {
                string text = "";
                int i = 0;
                int j = 0;
                bool done = true;
                foreach (var item in storage)
                {
                    text += item+ " ";
                    i++;
                    if (i==9)
                    {
                        text += "\n";
                        i = 0;
                        j++;
                        done = false;
                    }
                    else
                    {
                        if (i%3==0)
                        {
                            text += "|";
                        }
                    }
                    if (j%3==0 && done == false && j!=9)
                    {
                        text += "___________________\n";
                        done = true;
                    }
                }
                return text;
            }
            public string OdczytajLinie(int a)
            {
                string text = "";
                for (int i = 0; i < 9; i++)
                {
                    text += storage[a, i] + " ";
                    if (i % 3 == 2 && i!=8)
                    {
                        text += "|";
                    }
                }
                return text;
            }
        }

        public static void generuj(int a, SudokuBoard GameBoard, List<Matrix> listMatrix)
        {
            for (int b = 0; b < a; b++)
            {
                Matrix CellControls = new Matrix();

                GameBoard.Clear();
                GameBoard.Solver.SolveThePuzzle(UseRandomGenerator: true);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        CellControls[i, j] = GameBoard.GetCell(9 * i + j).Value;
                    }
                }
                Console.WriteLine("plansza " +(b+1) + "\n"+  CellControls.ToString());
                listMatrix.Add(CellControls);
            }
        }

        public static int zliczJedynkiNaPoczatku(List<Matrix> SudokuList)
        {

            int w = 0;
            for (int i = 0; i < SudokuList.Count; i++)
            {
                if (SudokuList[i][0, 0] == 1)
                    w++;

            }
            return w;
        }

        public static List<int[,]> wykryjTrojke(List<Matrix> SudokuList)
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

            for (int a = 0; a < SudokuList.Count; a++)
            {
                for (int i = 0; i < MozliweTrojki.Count; i++)
                {
                    if (SudokuList[a][MozliweTrojki[i][0,0], MozliweTrojki[i][0,1]]== SudokuList[a][MozliweTrojki[i][1, 0], MozliweTrojki[i][1, 1]] &&
                        SudokuList[a][MozliweTrojki[i][0, 0], MozliweTrojki[i][0, 1]] == SudokuList[a][MozliweTrojki[i][2, 0], MozliweTrojki[i][2, 1]])
                    {
                        macierzeZTrojka.Add(new int[1, 2] { { a+1, i+1 } });                       
                    }
                }
            }
            return macierzeZTrojka;
        }
        static void Main(string[] args)
        {
            // GameBoard Instance
            SudokuBoard GameBoard = new SudokuBoard();
            List<Matrix> SudokuList = new List<Matrix>();
            List<int[,]> lista = new List<int[,]>();

            generuj(20, GameBoard, SudokuList);
            lista = wykryjTrojke(SudokuList);
            foreach (int[,] item in lista)
            {
                Console.WriteLine(item[0, 0] + " " + item[0, 1]);
            }

            string FILE_NAME = "Wynik.txt";

            StreamWriter sw = new StreamWriter(FILE_NAME);
            for (int i = 0; i < SudokuList.Count; i++)
            {
                sw.WriteLine("plansza " + (i + 1));
                for (int j = 0; j < 9; j++)
                {
                    sw.WriteLine(SudokuList[i].OdczytajLinie(j));
                    if (j % 3 == 2 && j != 9 && j != 0)
                    {
                        sw.WriteLine("___________________");

                    }
                }
            }
            sw.Close();
            Console.ReadKey();
        }
    }
}