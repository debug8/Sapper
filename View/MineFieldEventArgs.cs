using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class MineFieldEventArgs : EventArgs
    {
        int row;
        int column;

        internal MineFieldEventArgs(int row, int column) 
        {
            this.row = row;
            this.column = column;
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
    }
}
