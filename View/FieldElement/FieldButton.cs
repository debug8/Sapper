using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View.FieldElement
{
    internal class FieldButton : Button
    {
        private byte row, column;
        private const int ButtonSize = 16;
        private FieldElementStatus status;

        internal FieldElementStatus Status { get { return status; } }

        internal FieldButton(byte row, byte column)
        {
            this.row = row;
            this.column = column;

            Initialize();
            SetStatus(FieldElementStatus.Active);
        }
      
        internal void SetStatus(FieldElementStatus status) 
        {
            this.status = status;
            switch (status)
            {
                case FieldElementStatus.Open0: 
                    BackgroundImage = FieldButtonResources.buttonPressed;
                    return;
                case FieldElementStatus.Open1:
                    BackgroundImage = FieldButtonResources.buttonOpen1;
                    return;
                case FieldElementStatus.Open2: 
                    BackgroundImage = FieldButtonResources.buttonOpen2;
                    return;
                case FieldElementStatus.Open3: 
                    BackgroundImage = FieldButtonResources.buttonOpen3;
                    return;
                case FieldElementStatus.Open4:
                    BackgroundImage = FieldButtonResources.buttonOpen4;
                    return;
                case FieldElementStatus.Open5:
                    BackgroundImage = FieldButtonResources.buttonOpen5;
                    return;
                case FieldElementStatus.Open6:
                    BackgroundImage = FieldButtonResources.buttonOpen6;
                    return;
                case FieldElementStatus.Open7:
                    BackgroundImage = FieldButtonResources.buttonOpen7;
                    return;
                case FieldElementStatus.Open8:
                    BackgroundImage = FieldButtonResources.buttonOpen8;
                    return;
                case FieldElementStatus.BombFlagged:
                    BackgroundImage = FieldButtonResources.buttonFlagged;
                    return;
                case FieldElementStatus.BombRevealed:
                    BackgroundImage = FieldButtonResources.bombRevealed;
                    return;
                case FieldElementStatus.BombDeath:
                    BackgroundImage = FieldButtonResources.bombDeath;
                    return;
                case FieldElementStatus.BombMisFlagged:
                    BackgroundImage = FieldButtonResources.bombMisFlagged;
                    return;

                case FieldElementStatus.Active:
                    BackgroundImage = FieldButtonResources.buttonUnpressed;
                    return;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if(status == FieldElementStatus.Active)
            BackgroundImage = FieldButtonResources.buttonPressed;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (status == FieldElementStatus.Active)
            BackgroundImage = FieldButtonResources.buttonUnpressed;
            base.OnMouseUp(mevent);
        }

        internal byte Row
        {
            get { return row; }
        }

        internal byte Column
        {
            get { return column; }
        }

        private void Initialize()
        {
            SetStyle(ControlStyles.Selectable, false);
            Size = new System.Drawing.Size(ButtonSize, ButtonSize);
            TabStop = false;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }
    }
}
