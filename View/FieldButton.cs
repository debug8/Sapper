using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    internal class FieldButton : Button
    {
        private byte row, column;
        private const int ButtonSize = 16;

        internal FieldButton(byte row, byte column)
        {
            this.row = row;
            this.column = column;

            SetStyle(ControlStyles.Selectable, false);

            Size = new System.Drawing.Size(ButtonSize, ButtonSize);

            TabStop = false;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.BorderColor = Color.Red;
            BackgroundImage = Bitmap.FromFile("Resources\\buttonUnpressed.gif");
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            BackgroundImage = Bitmap.FromFile("Resources\\buttonPressed.gif");
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            BackgroundImage = Bitmap.FromFile("Resources\\buttonUnpressed.gif");
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
    }
}
