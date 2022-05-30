using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public static int BestResult = Int32.Parse(File.ReadAllText(Environment.CurrentDirectory + @"\BestResult.txt"));
        public static double TotalScore = 0;
        public static bool Changed = false;
        private static readonly string[] Keys = new string[] {"Left", "Right", "Up", "Down"};
        public static Cell[,] CellsInfo = new Cell[4, 4];
        public static Label[,] Labels = new Label[4, 4];
        
        Label End = new Label()
        {
            Name = "End",
            Text = "GAME OVER",
            Font = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold),
            Location = new Point(600, 400),
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.DarkRed,
            AutoSize = true
        };


        Button Restart = new Button()
        {
            Name = "Restart",
            Text = "New Game",
            Font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold),
            Location = new Point(650, 450),
            AutoSize = true
        };

        Label Score = new Label()
        {
            Name = "TotalScore",
            Text = "Total Score: " + TotalScore.ToString(),
            Font = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold),
            Location = new Point(550, 100),
            AutoSize = true
        };

        Label Record = new Label()
        {
            Name = "Record",
            Text = "Your record: " + BestResult.ToString(),
            Font = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold),
            ForeColor = Color.Goldenrod,
            Location = new Point(550, 200),
            AutoSize = true
        };

        public Form1()
        {
            Controls.Add(Record);
            Controls.Add(Score);
            Controls.Add(End);
            Controls.Add(Restart);
            Restart.Click += (sender, args) => Application.Restart();
            Restart.Hide();
            End.Hide();
            KeyDown += new KeyEventHandler(KeyboardEvent);
            Field.Create();
            foreach (var e in Labels)
            {
                Controls.Add(e);
            }
            Cell.GenerateNew();
            Cell.GenerateNew();
            InitializeComponent();
            Update();
        }

        public static void Update()
        {
            foreach (var t in Form1.CellsInfo)
            {
                if (t.Degree > -1)
                    t.Value = Math.Pow(2, t.Degree);
                if (Form1.CellsInfo[t.X, t.Y].Degree > 0)
                {
                    Labels[t.X, t.Y].Text = Form1.CellsInfo[t.X, t.Y].Value.ToString();
                    Labels[t.X, t.Y].ForeColor = Color.Black;
                }

                t.Merged = false;
                switch (t.Degree)
                {
                    case -2:
                        Labels[t.X, t.Y].BackColor = Color.Peru;
                        Labels[t.X, t.Y].Text = ":2";
                        Labels[t.X, t.Y].ForeColor = Color.Navy;
                        Labels[t.X, t.Y].Font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold);
                        break;
                    case -1:
                        Labels[t.X, t.Y].BackColor = Color.Peru;
                        Labels[t.X, t.Y].Text = "x2";
                        Labels[t.X, t.Y].ForeColor = Color.Brown;
                        Labels[t.X, t.Y].Font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold);
                        break;
                    case 0:
                        Labels[t.X, t.Y].BackColor = Color.Bisque;
                        Labels[t.X, t.Y].Text = "";
                        break;
                    case 1:
                        Labels[t.X, t.Y].BackColor = Color.DarkRed;
                        break;
                    case 2:
                        Labels[t.X, t.Y].BackColor = Color.Chocolate;
                        break;
                    case 3:
                        Labels[t.X, t.Y].BackColor = Color.DarkOrange;
                        break;
                    case 4:
                        Labels[t.X, t.Y].BackColor = Color.Gold;
                        break;
                    case 5:
                        Labels[t.X, t.Y].BackColor = Color.OliveDrab;
                        break;
                    case 6:
                        Labels[t.X, t.Y].BackColor = Color.DarkGreen;
                        break;
                    case 7:
                        Labels[t.X, t.Y].BackColor = Color.DodgerBlue;
                        break;
                    case 8:
                        Labels[t.X, t.Y].BackColor = Color.Navy;
                        break;
                    case 9:
                        Labels[t.X, t.Y].BackColor = Color.Fuchsia;
                        break;
                    case 10:
                        Labels[t.X, t.Y].BackColor = Color.BlueViolet;
                        break;
                    case 11:
                        Labels[t.X, t.Y].BackColor = Color.Indigo;
                        break;
                }
            }
            EndOfGame();
        }

        public static void EndOfGame()
        {
            if (Win())
            {
                var end = ActiveForm.Controls["End"] as Label;
                ActiveForm.Controls["Restart"].Show();
                end.Text = "Win!";
                end.Location = new Point(end.Location.X + 70, end.Location.Y);
                end.Show();
                if (BestResult < TotalScore)
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\BestResult.txt", TotalScore.ToString());
                    end.Text = "NEW RECORD!\n" + end.Text;
                    end.Location = new Point(end.Location.X, end.Location.Y - 70);
                }
            }
            else if (GameIsOver())
            {
                var end = ActiveForm.Controls["End"] as Label;
                ActiveForm.Controls["Restart"].Show();
                ActiveForm.Controls["End"].Show();
                if (BestResult < TotalScore)
                {
                    File.WriteAllText(Environment.CurrentDirectory + @"\BestResult.txt", TotalScore.ToString());
                    end.Text = "NEW RECORD!\n" + end.Text;
                    end.Location = new Point(end.Location.X, end.Location.Y - 70);
                }
            }
        }

        public static bool Win()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CellsInfo[i, j].Degree == 11)
                        return true;
                }
            }
            return false;
        }

        public static bool GameIsOver()
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0 && CellsInfo[i - 1, j].Degree == CellsInfo[i, j].Degree ||
                        i + 1 < 4 && CellsInfo[i + 1, j].Degree == CellsInfo[i, j].Degree ||
                        j - 1 >= 0 && CellsInfo[i, j - 1].Degree == CellsInfo[i, j].Degree ||
                        j + 1 < 4 && CellsInfo[i, j + 1].Degree == CellsInfo[i, j].Degree)
                        count += 1;
                }
            }
            return (count == 0 && !EmptyCells());
        }

        public static bool EmptyCells()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (CellsInfo[i, j].Degree <= 0)
                        return true;
            return false;
        }

        private void Move(string direction)
        {
            switch (direction)
            {
                case "Right":
                    Directions.MoveRight();
                    break;
                case "Left":
                    Directions.MoveLeft();
                    break;
                case "Up":
                    Directions.MoveUp();
                    break;
                case "Down":
                    Directions.MoveDown();
                    break;
            }
            ActiveForm.Controls["TotalScore"].Text = "Total Score: " + TotalScore.ToString();
            Update();
            InitializeComponent();
        }

        private void KeyboardEvent(object sender, KeyEventArgs e)
        {
            var command = e.KeyCode.ToString();
            if (Keys.Contains(command))
            {
                Move(e.KeyCode.ToString());
                if (Changed)
                {
                    Cell.GenerateNew();
                    Changed = false;
                }
            }
        }
    }
}
