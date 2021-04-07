using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class ElementEventArgs
    {
        int row;
        int column;
        bool hasMine;
        int mineAround;

        internal ElementEventArgs(int row, int column, bool hasMine, int mineAround) 
        {
            this.row = row;
            this.column = column;
            this.hasMine = hasMine;
            this.mineAround = mineAround;
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
        public bool HasMine { get { return hasMine; } }
        public int MineAround { get { return mineAround; } }
    }
}
