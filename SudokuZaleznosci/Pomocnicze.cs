using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuZaleznosci
{
    public class Matrix
    {
        private int[,] storage;

        public int this[int row, int column]
        {
            get { return storage[row, column]; }
            set { storage[row, column] = value; }
        }

        public Matrix(int a , int b)
        {
            storage = new int[a, b];
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
        public  string ToString2()
        {
            string text = "";
            int i = 0;
            int j = 1;
            for (int a = 0; a < 72; a++)
            {
                text += (a + 1) + " | ";
            }
            text += "1 |";
            foreach (var item in storage)
            {
                text += item + " | ";
                i++;
                if (i == 72)
                {
                    text += "\n";
                    text += "______________________________________________________________________________\n";
                    i = 0;
                    j++;

                    text += j + " | ";
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
            string text = a+"\t|";
            for (int i = 1; i < 73; i++)
            {
                text += storage[a, i] + "\t|";
            }
            return text;
        }
    }
    class Pomocnicze
    {
    }
}
