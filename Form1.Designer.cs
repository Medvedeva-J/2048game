﻿using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace WinFormsApp2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.MaximumSize = new Size(1200, 600);
            this.MinimumSize = this.MaximumSize;
            this.Text = "Form1";
        }

        #endregion
    }
}

