using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuZaleznosci
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
    }
    class Pomocnicze
    {
    }
}
