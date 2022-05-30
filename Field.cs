using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class Field
    {
        public static void Create()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var a = new Cell(i, j, new Point(30 + i * 120, 30 + j * 120));
                    Form1.CellsInfo[i, j] = a;
                    Form1.Labels[i, j] = new Label()
                    {
                        Location = new Point(30 + i * 120, 30 + j * 120),
                        Size = new Size(100, 100),
                        BackColor = Color.Bisque,
                        Font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold),
                        Text = Form1.CellsInfo[i, j].Value.ToString(),
                            TextAlign = ContentAlignment.MiddleCenter
                    };
                }
            }
        }
    }
}