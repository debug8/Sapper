﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using View.FieldElement;

namespace View
{
    public partial class FieldControl : UserControl, View.IViewMineField
    {
        public event EventHandler<MineFieldEventArgs> MineButtonPressed;
        public event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        private const int ButtonSize = 16;
        private readonly int fieldSize;      
        private FieldButton [,] fieldArray;       

        public FieldControl() : this(10)
        {

        }

        public FieldControl(int fieldSize)
        {
            this.fieldSize = fieldSize;
            fieldArray = new FieldButton[fieldSize, fieldSize];
            InitializeComponent();

            FillMineField(fieldSize);
            Size = new System.Drawing.Size(ButtonSize * fieldSize, ButtonSize * fieldSize);
        }

        private void FillMineField(int fieldSize)
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    var b = new FieldButton(i, j);
                    b.Location = new System.Drawing.Point(ButtonSize * i, ButtonSize * j);
                    b.Click += b_Click;
                    b.MouseDown += b_MouseDown;
                    this.Controls.Add(b);
                }
            }
        }

        void b_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var button = sender as FieldButton;
                if (MineButtonRightPressed != null)
                    MineButtonRightPressed(this, new MineFieldEventArgs(button.Row, button.Column));
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            var button = sender as FieldButton;
            if (MineButtonPressed != null)
                MineButtonPressed(this, new MineFieldEventArgs(button.Row, button.Column));

        }       

        public void Reset()
        {
            Controls.OfType<FieldButton>().ToList().ForEach(element => element.SetStatus(FieldElementStatus.Active));
        }

        public void SetElementStatus(int row, int column, FieldElementStatus status)
        {
            var element = Controls.OfType<FieldButton>().Where(b => b.Row == row && b.Column == column).First();

            element.SetStatus(status);
        }

        public FieldElementStatus GetElementStatus(int row, int column)
        {
            var element = Controls.OfType<FieldButton>().Where(b => b.Row == row && b.Column == column).First();

            return element.Status;
        }
    }
}
