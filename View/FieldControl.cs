using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace View
{
    public partial class FieldControl : UserControl, View.IViewMineField
    {
        public event EventHandler<MineFieldEventArgs> MineButtonPressed;
        public event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        private readonly byte fieldSize;
        private const int ButtonSize = 25;
        private FieldButton [,] fieldArray;

        public FieldControl() : this(10)
        {

        }

        public FieldControl(byte fieldSize)
        {
            this.fieldSize = fieldSize;
            fieldArray = new FieldButton[fieldSize, fieldSize];
            InitializeComponent();

            FillMineField(fieldSize);
            Size = new System.Drawing.Size(ButtonSize * fieldSize, ButtonSize * fieldSize);
        }

        public void SetFieldElement(byte row, byte column, string text, Color color)
        {
            var element = Controls.OfType<FieldButton>().Where(b => b.Row == row && b.Column == column).First();

            element.Text = text;
            element.BackColor = color ;
            //Refresh();
            //Thread.Sleep(20);// для отладки алгоритма прохождения клеток
        }

        private void FillMineField(byte fieldSize)
        {
            for (byte i = 0; i < fieldSize; i++)
            {
                for (byte j = 0; j < fieldSize; j++)
                {
                    var b = CreateFieldButton(j, i);
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

        private FieldButton CreateFieldButton(byte row, byte column)
        {
            var button = new FieldButton(row, column);
            SuspendLayout();

            button.Location = new System.Drawing.Point(0, 0);
            button.Margin = new System.Windows.Forms.Padding(0);

            button.Size = new System.Drawing.Size(ButtonSize, ButtonSize);
            button.UseVisualStyleBackColor = true;

            return button;
        }

        public void Reset()
        {
            for (byte i = 0; i < fieldSize; i++)
            {
                for (byte j = 0; j < fieldSize; j++)
                {
                    Controls.OfType<FieldButton>().ToList().ForEach(control => 
                    {
                        control.BackColor = Color.FromKnownColor(KnownColor.Control);
                        control.Text = string.Empty;
                    });
                }
            }
        }
    }
}
