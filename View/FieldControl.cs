using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using View.FieldElement;

namespace View
{
    public partial class FieldControl : UserControl, IViewMineField
    {
        public event EventHandler<MineFieldEventArgs> MineButtonPressed;
        public event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        private const int ButtonSize = 16;
        private readonly int _fieldHight;
        private readonly int _fieldWidth;
        private FieldButton [,] _fieldArray;       

        public FieldControl() : this(20, 50)
        {

        }

        public FieldControl(int fieldHight, int fieldWidth)
        {
            _fieldHight = fieldHight;
            _fieldWidth = fieldWidth;

            _fieldArray = new FieldButton[fieldHight, fieldWidth];
            InitializeComponent();

            FillMineField(fieldHight, fieldWidth);
            Size = new System.Drawing.Size(ButtonSize * fieldWidth, ButtonSize * fieldHight);
        }

        private void FillMineField(int fieldHight, int fieldWidth)
        {
            for (int i = 0; i < fieldHight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    var b = new FieldButton(i, j);
                    b.Location = new System.Drawing.Point(ButtonSize * j, ButtonSize * i);
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
            Thread.Sleep(10);
            element.Refresh();
        }

        public FieldElementStatus GetElementStatus(int row, int column)
        {
            var element = Controls.OfType<FieldButton>().Where(b => b.Row == row && b.Column == column).First();

            return element.Status;
        }
    }
}
