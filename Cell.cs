using System;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class Cell
    {
        public bool Merged = false;
        public int Degree;
        public Point Location;
        public int X;
        public int Y;
        public double Value;

        public Cell(int x, int y, Point location)
        {
            X = x;
            Y = y;
            Location = location;
            Degree = 0;
        }

        public static void GenerateNew()
        {
            var bonusProbability = new Random().Next(15);
            var probability = new Random().Next(10);
            var i = new Random().Next(4);
            var j = new Random().Next(4);
            while (Form1.CellsInfo[i, j].Degree != 0)
            {
                i = new Random().Next(4);
                j = new Random().Next(4);
            }
            
            if (bonusProbability == 3)
                Form1.CellsInfo[i, j].Degree = -2;
            else if (bonusProbability == 2)
                Form1.CellsInfo[i, j].Degree = -1;
            else if (probability == 1)
                Form1.CellsInfo[i, j].Degree = 2;
            else
                Form1.CellsInfo[i, j].Degree = 1;
            Form1.Update();
        }
    }
}