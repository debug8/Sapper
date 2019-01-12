using model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;

namespace Controller
{
    public class Controller
    {
        private IViewMainForm view;
        private IModelMineField model;

        public Controller(IViewMainForm view, IModelMineField model) 
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize() 
        {
            view.MineButtonPressed += view_MineButtonPressed;
            view.MineButtonRightPressed += view_MineButtonRightPressed;
            view.ResetForm += view_ResetForm;

            model.Loose += model_Loose;
            model.Win += model_Win;
            model.ElementChanged += model_ElementChanged;
        }

        void view_ResetForm()
        {
            model.Reset();
        }

        void view_MineButtonRightPressed(object sender, MineFieldEventArgs e)
        {
            view.SetFieldElement(e.Row, e.Column, "✔", Color.Orange);
        }

        void view_MineButtonPressed(object sender, MineFieldEventArgs e)
        {
            model.OpenElement(e.Row, e.Column);
        }

        void model_ElementChanged(object sender, ElementEventArgs e)
        {
            var color = e.HasMine ? Color.Red : Color.SpringGreen;
            var text = e.HasMine ? "X" : e.MineAround == 0 ? "" : e.MineAround.ToString();

            view.SetFieldElement(e.Row, e.Column, text, color);
        }

        void model_Loose(object sender, EventArgs e)
        {
            MessageBox.Show("You loose! \r\n New Game?", "Message", MessageBoxButtons.OK);
            model.Reset();
            view.Reset();
        }

        void model_Win(object sender, EventArgs e)
        {
            MessageBox.Show("You win! \r\n New Game?", "Message");
            model.Reset();
            view.Reset();
        }
    }
}
