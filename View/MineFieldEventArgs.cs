using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class MineFieldEventArgs : EventArgs
    {
        byte row;
        byte column;

        internal MineFieldEventArgs(byte row, byte column) 
        {
            this.row = row;
            this.column = column;
        }

        public byte Row { get { return row; } }
        public byte Column { get { return column; } }
    }
}
