using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    internal class FieldButton : Button
    {
        private byte row, column;

        internal FieldButton(byte row, byte column)
        {
            this.row = row;
            this.column = column;

            SetStyle(ControlStyles.Selectable, false);
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
