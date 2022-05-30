using System;
using System.Security.Cryptography.Xml;
using Microsoft.Win32;

namespace WinFormsApp2
{
    public class Directions
    {

        public static void UpDegree(int x1, int y1, int x2, int y2, int degree)
        {
            Form1.CellsInfo[x1, y1].Degree = degree + 1;
            Form1.CellsInfo[x1, y1].Merged = true;
            Form1.CellsInfo[x2, y2].Degree = 0;
            Form1.Changed = true;
            Form1.TotalScore += Math.Pow(2, degree + 1);
        }

        public static void DownDegree(int x1, int y1, int x2, int y2, int degree)
        {
            if (Form1.CellsInfo[x1, y1].Degree > 1 || Form1.CellsInfo[x2, y2].Degree > 1)
            {
                Form1.CellsInfo[x1, y1].Degree = degree - 1;
                Form1.CellsInfo[x1, y1].Merged = true;
                Form1.CellsInfo[x2, y2].Degree = 0;
                Form1.Changed = true;
            }
        }

        public static void MoveRight()
        {
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    int a = 1;
                    while (i + a - 1 < 3 && Form1.CellsInfo[i + a, j].Degree == 0 && Form1.CellsInfo[i, j].Degree != 0)
                        a += 1;
                    Form1.CellsInfo[i + a - 1, j].Degree = Form1.CellsInfo[i, j].Degree;
                    if (i + a < 4 && !Form1.CellsInfo[i + a, j].Merged)
                    {
                        var degree = Form1.CellsInfo[i + a - 1, j].Degree;
                        if (degree == -1 || degree == -2) degree = Form1.CellsInfo[i + a, j].Degree;
                        if (Form1.CellsInfo[i + a - 1, j].Degree > 0 &&
                            (Form1.CellsInfo[i + a - 1, j].Degree == Form1.CellsInfo[i + a, j].Degree ||
                             Form1.CellsInfo[i + a, j].Degree == -1) ||
                            (Form1.CellsInfo[i + a - 1, j].Degree == -1 &&
                             Form1.CellsInfo[i + a, j].Degree > 0))
                            UpDegree(i + a, j, i + a - 1, j, degree);

                        if (Form1.CellsInfo[i + a - 1, j].Degree == -2 &&
                            Form1.CellsInfo[i + a, j].Degree > 0 ||
                            Form1.CellsInfo[i + a, j].Degree == -2 &&
                            Form1.CellsInfo[i + a - 1, j].Degree > 0)
                            DownDegree(i + a, j, i + a - 1, j, degree);
                    }

                    if (a != 1)
                    {
                        Form1.CellsInfo[i, j].Degree = 0;
                        Form1.Changed = true;
                    }
                }
            }
        }

        public static void MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int a = -1;
                    while (i + a + 1 > 0 && Form1.CellsInfo[i + a, j].Degree == 0 && Form1.CellsInfo[i, j].Degree != 0)
                        a -= 1;
                    Form1.CellsInfo[i + a + 1, j].Degree = Form1.CellsInfo[i, j].Degree;
                    if (i + a >= 0 && !Form1.CellsInfo[i + a, j].Merged)
                    {
                        var degree = Form1.CellsInfo[i + a + 1, j].Degree;
                        if (degree == -1 || degree == -2) degree = Form1.CellsInfo[i + a, j].Degree;
                        if (Form1.CellsInfo[i + a + 1, j].Degree > 0 &&
                            (Form1.CellsInfo[i + a + 1, j].Degree == Form1.CellsInfo[i + a, j].Degree || 
                             Form1.CellsInfo[i + a, j].Degree == -1) ||
                            (Form1.CellsInfo[i + a + 1, j].Degree == -1 &&
                             Form1.CellsInfo[i + a, j].Degree > 0))
                            UpDegree(i + a, j, i + a + 1, j, degree);
                        
                        if (Form1.CellsInfo[i + a + 1, j].Degree == -2 &&
                            Form1.CellsInfo[i + a, j].Degree > 0 ||
                            Form1.CellsInfo[i + a, j].Degree == -2 &&
                            Form1.CellsInfo[i + a + 1, j].Degree > 0)
                            DownDegree(i + a, j, i + a + 1, j, degree);
                    }

                    if (a != -1)
                    {
                        Form1.CellsInfo[i, j].Degree = 0;
                        Form1.Changed = true;
                    }
                }
            }
        }
        
        public static void MoveDown()
        {
            for (int j = 3; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    int a = 1;
                    while (j + a - 1 < 3 && Form1.CellsInfo[i, j + a].Degree == 0 && Form1.CellsInfo[i, j].Degree != 0)
                        a += 1;
                    Form1.CellsInfo[i, j + a - 1].Degree = Form1.CellsInfo[i, j].Degree;
                    if (j + a < 4 && !Form1.CellsInfo[i, j + a].Merged)
                    {
                        var degree = Form1.CellsInfo[i, j + a - 1].Degree;
                        if (degree == -1 || degree == -2) degree = Form1.CellsInfo[i, j + a].Degree;
                        if (Form1.CellsInfo[i, j + a - 1].Degree > 0 &&
                            (Form1.CellsInfo[i, j + a - 1].Degree == Form1.CellsInfo[i, j + a].Degree || 
                             Form1.CellsInfo[i, j + a].Degree == -1) ||
                            (Form1.CellsInfo[i, j + a - 1].Degree == -1 &&
                             Form1.CellsInfo[i, j + a].Degree > 0))
                            UpDegree(i, j + a, i, j + a - 1, degree);

                        if (Form1.CellsInfo[i, j + a - 1].Degree == -2 &&
                            Form1.CellsInfo[i, j + a].Degree > 0 ||
                            Form1.CellsInfo[i, j + a].Degree == -2 &&
                            Form1.CellsInfo[i, j + a - 1].Degree > 0)
                            DownDegree(i, j + a, i, j + a - 1, degree);
                    }
                    if (a != 1)
                    {
                        Form1.CellsInfo[i, j].Degree = 0;
                        Form1.Changed = true;
                    }
                }
            }
        }
        
        public static void MoveUp()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    int a = -1;
                    while (j + a + 1 > 0 && Form1.CellsInfo[i, j + a].Degree == 0 && Form1.CellsInfo[i, j].Degree != 0)
                        a -= 1;
                    Form1.CellsInfo[i, j + a + 1].Degree = Form1.CellsInfo[i, j].Degree;
                    if (j + a >= 0 && !Form1.CellsInfo[i, j + a].Merged)
                    {
                        var degree = Form1.CellsInfo[i, j + a + 1].Degree;
                        if (degree == -1 || degree == -2) degree = Form1.CellsInfo[i, j + a].Degree;
                        if (Form1.CellsInfo[i, j + a + 1].Degree > 0 &&
                            (Form1.CellsInfo[i, j + a + 1].Degree == Form1.CellsInfo[i, j + a].Degree ||
                             Form1.CellsInfo[i, j + a].Degree == -1) ||
                            (Form1.CellsInfo[i, j + a + 1].Degree == -1 &&
                             Form1.CellsInfo[i, j + a].Degree > 0))
                            UpDegree(i, j + a, i, j + a + 1, degree);
                        
                        if (Form1.CellsInfo[i, j + a + 1].Degree == -2 &&
                            Form1.CellsInfo[i, j + a].Degree > 0 ||
                            Form1.CellsInfo[i, j + a].Degree == -2 &&
                            Form1.CellsInfo[i, j + a + 1].Degree > 0)
                            DownDegree(i, j + a, i, j + a + 1, degree);
                    }

                    if (a != -1)
                    {
                        Form1.CellsInfo[i, j].Degree = 0;
                        Form1.Changed = true;
                    }
                }
            }
        }
    }
}