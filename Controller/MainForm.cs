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
        }


        public event EventHandler<MineFieldEventArgs> MineButtonPressed;

        public event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        public event Action ResetForm;

        public void SetFieldElement(byte row, byte column, string text, Color col)
        {
            fieldControl.SetFieldElement(row, column, text, col);
        }

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
    }
}
