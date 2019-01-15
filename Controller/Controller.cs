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
            var status = view.GetElementStatus(e.Row, e.Column);
            if (status == FieldElementStatus.Active)
            {
                view.SetElementStatus(e.Row, e.Column, FieldElementStatus.BombFlagged);
                return;
            }
            if (status == FieldElementStatus.BombFlagged) 
            {
                view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Active);
            }
        }

        void view_MineButtonPressed(object sender, MineFieldEventArgs e)
        {
            var status = view.GetElementStatus(e.Row, e.Column);
            if (status == FieldElementStatus.BombFlagged)
            {
                return;
            }

            model.OpenElement(e.Row, e.Column);
        }

        void model_ElementChanged(object sender, ElementEventArgs e)
        {
            switch (e.MineAround)
            {
                case 0:
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open0);
                return;
                case 1: 
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open1);
                    return;
                case 2: 
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open2);
                    return;
                case 3: 
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open3);
                    return;
                case 4: 
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open4);
                    return;
                case 5: 
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open5);
                    return;
                case 6:
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open6);
                    return;
                case 7:
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open7);
                    return;
                case 8:
                    view.SetElementStatus(e.Row, e.Column, FieldElementStatus.Open8);
                    return;
            }
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
