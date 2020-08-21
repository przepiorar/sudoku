using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuZaleznosci
{
    public class Stale
    {
        public static List<int[,]> ListaTrojek = new List<int[,]>() {
            new int[3, 2] { { 1, 2 }, { 2, 3 }, { 3, 4 } },
            new int[3, 2] { { 2, 1 }, { 3, 2 }, { 4, 3 } },
            new int[3, 2] { { 3, 1 }, { 2, 2 }, { 1, 3 } },
            new int[3, 2] { { 4, 2 }, { 3, 3 }, { 2, 4 } },

            new int[3, 2] { { 4, 2 }, { 5, 3 }, { 6, 4 } },
            new int[3, 2] { { 5, 1 }, { 6, 2 }, { 7, 3 } },
            new int[3, 2] { { 6, 1 }, { 5, 2 }, { 4, 3 } },
            new int[3, 2] { { 7, 2 }, { 6, 3 }, { 5, 4 } },

            new int[3, 2] { { 1, 5 }, { 2, 6 }, { 3, 7 } },
            new int[3, 2] { { 2, 4 }, { 3, 5 }, { 4, 6 } },
            new int[3, 2] { { 3, 4 }, { 2, 5 }, { 1, 6 } },
            new int[3, 2] { { 4, 5 }, { 3, 6 }, { 2, 7 } },

            new int[3, 2] { { 4, 5 }, { 5, 6 }, { 6, 7 } },
            new int[3, 2] { { 5, 4 }, { 6, 5 }, { 7, 6 } },
            new int[3, 2] { { 6, 4 }, { 5, 5 }, { 4, 6 } },
            new int[3, 2] { { 7, 5 }, { 6, 6 }, { 5, 7 } }
        }; //{wiersz-1, kolumna-1}

        public static List<int[,]> ListaDwojek = new List<int[,]>() {
            new int[2, 2] { { 0, 2 }, { 1, 3 } },
            new int[2, 2] { { 1, 2 }, { 2, 3 } },
            new int[2, 2] { { 2, 2 }, { 3, 3 } },
            new int[2, 2] { { 3, 2 }, { 4, 3 } },
            new int[2, 2] { { 4, 2 }, { 5, 3 } },
            new int[2, 2] { { 5, 2 }, { 6, 3 } },
            new int[2, 2] { { 6, 2 }, { 7, 3 } },
            new int[2, 2] { { 7, 2 }, { 8, 3 } },

            new int[2, 2] { { 0, 5 }, { 1, 6 }, },
            new int[2, 2] { { 1, 5 }, { 2, 6 }, },
            new int[2, 2] { { 2, 5 }, { 3, 6 }, },
            new int[2, 2] { { 3, 5 }, { 4, 6 }, },
            new int[2, 2] { { 4, 5 }, { 5, 6 }, },
            new int[2, 2] { { 5, 5 }, { 6, 6 }, },
            new int[2, 2] { { 6, 5 }, { 7, 6 }, },
            new int[2, 2] { { 7, 5 }, { 8, 6 }, },

            new int[2, 2] { { 2, 0 }, { 3, 1 }, },
            new int[2, 2] { { 2, 1 }, { 3, 2 }, },
            new int[2, 2] { { 2, 3 }, { 3, 4 }, },
            new int[2, 2] { { 2, 4 }, { 3, 5 }, },
            new int[2, 2] { { 2, 6 }, { 3, 7 }, },
            new int[2, 2] { { 2, 7 }, { 3, 8 }, },

            new int[2, 2] { { 5, 0 }, { 6, 1 }, },
            new int[2, 2] { { 5, 1 }, { 6, 2 }, },
            new int[2, 2] { { 5, 3 }, { 6, 4 }, },
            new int[2, 2] { { 5, 4 }, { 6, 5 }, },
            new int[2, 2] { { 5, 6 }, { 6, 7 }, },
            new int[2, 2] { { 5, 7 }, { 6, 8 }, },


            new int[2, 2] { { 1, 2 }, { 0, 3 }, },
            new int[2, 2] { { 2, 2 }, { 1, 3 }, },
            new int[2, 2] { { 3, 2 }, { 2, 3 }, },
            new int[2, 2] { { 4, 2 }, { 3, 3 }, },
            new int[2, 2] { { 5, 2 }, { 4, 3 }, },
            new int[2, 2] { { 6, 2 }, { 5, 3 }, },
            new int[2, 2] { { 7, 2 }, { 6, 3 }, },
            new int[2, 2] { { 8, 2 }, { 7, 3 }, },

            new int[2, 2] { { 1, 5 }, { 0, 6 }, },
            new int[2, 2] { { 2, 5 }, { 1, 6 }, },
            new int[2, 2] { { 3, 5 }, { 2, 6 }, },
            new int[2, 2] { { 4, 5 }, { 3, 6 }, },
            new int[2, 2] { { 5, 5 }, { 4, 6 }, },
            new int[2, 2] { { 6, 5 }, { 5, 6 }, },
            new int[2, 2] { { 7, 5 }, { 6, 6 }, },
            new int[2, 2] { { 8, 5 }, { 7, 6 }, },

            new int[2, 2] { { 3, 0 }, { 2, 1 }, },
            new int[2, 2] { { 3, 1 }, { 2, 2 }, },
            new int[2, 2] { { 3, 3 }, { 2, 4 }, },
            new int[2, 2] { { 3, 4 }, { 2, 5 }, },
            new int[2, 2] { { 3, 6 }, { 2, 7 }, },
            new int[2, 2] { { 3, 7 }, { 2, 8 }, },

            new int[2, 2] { { 6, 0 }, { 5, 1 } },
            new int[2, 2] { { 6, 1 }, { 5, 2 } },
            new int[2, 2] { { 6, 3 }, { 5, 4 } },
            new int[2, 2] { { 6, 4 }, { 5, 5 } },
            new int[2, 2] { { 6, 6 }, { 5, 7 } },
            new int[2, 2] { { 6, 7 }, { 5, 8 } }
        };
        
    }
    public class Metody
    {
        public static void zapisz(List<Matrix> SudokuList)
        {
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
        }
        public static void zapisz2(Matrix MacierzKoncowa, string name)
        {
            string FILE_NAME = name;
            string text = "";
            for (int i = 0; i < 73; i++)
            {
                text += i + "\t|";
            }
            StreamWriter sw = new StreamWriter(FILE_NAME);
            sw.WriteLine(text);
            for (int j = 0; j < 72; j++)
            {
                sw.WriteLine(MacierzKoncowa.OdczytajLinie2(j));
                sw.WriteLine("________________________________________________________");
            }
            sw.Close();
        }
    }
    public class Matrix
    {
        private double[,] storage;

        public double this[int row, int column]
        {
            get { return storage[row, column]; }
            set { storage[row, column] = value; }
        }

        public Matrix(int a , int b)
        {
            storage = new double[a, b];
        }
        public override string ToString()
        {
            string text = "";
            int i = 0;
            int j = 0;
            bool done = true;
            foreach (var item in storage)
            {
                text += item + " ";
                i++;
                if (i == 9)
                {
                    text += "\n";
                    i = 0;
                    j++;
                    done = false;
                }
                else
                {
                    if (i % 3 == 0)
                    {
                        text += "|";
                    }
                }
                if (j % 3 == 0 && done == false && j != 9)
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
                if (i % 3 == 2 && i != 8)
                {
                    text += "|";
                }
            }
            return text;
        }
        public string OdczytajLinie2(int a)
        {
            string text = a+1+"\t|";
            for (int i = 0; i <72 ; i++)
            {
                text += storage[a, i] + "\t|";
            }
            return text;
        }
    }
}
