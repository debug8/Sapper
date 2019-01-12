using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class ElementEventArgs
    {
        byte row;
        byte column;
        bool hasMine;
        byte mineAround;

        internal ElementEventArgs(byte row, byte column, bool hasMine, byte mineAround) 
        {
            this.row = row;
            this.column = column;
            this.hasMine = hasMine;
            this.mineAround = mineAround;
        }

        public byte Row { get { return row; } }
        public byte Column { get { return column; } }
        public bool HasMine { get { return hasMine; } }
        public byte MineAround { get { return mineAround; } }
    }
}
