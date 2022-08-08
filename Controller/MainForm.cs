using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;
using model;

namespace Controller
{
    public partial class MainForm : Form, IViewMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            fieldControl.MineButtonPressed += OnMineButtonPressed;
            fieldControl.MineButtonRightPressed += OnMineButtonRightPressed;
            this.Size = new Size(fieldControl.Size.Width + 35, fieldControl.Size.Height);
        }


        public event EventHandler<MineFieldEventArgs> MineButtonPressed;

        public event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        public event Action ResetForm;

        private void OnMineButtonPressed(object o, MineFieldEventArgs e) 
        {
            if (MineButtonPressed != null)
                MineButtonPressed(o, e);
        }

        private void OnMineButtonRightPressed(object o, MineFieldEventArgs e)
        {
            if (MineButtonRightPressed != null)
                MineButtonRightPressed(o, e);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
            if (ResetForm != null)
            {
                ResetForm();
            }
        }

        public void Reset()
        {
            fieldControl.Reset();
        }
                        
        public void SetElementStatus(int row, int column, FieldElementStatus status)
        {
            fieldControl.SetElementStatus(row, column, status);
        }

        public FieldElementStatus GetElementStatus(int row, int column)
        {
            return fieldControl.GetElementStatus(row, column);
        }
    }
}
