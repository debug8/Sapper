namespace model
{
    public class ElementEventArgs
    {
        readonly int _row;
        readonly int _column;
        readonly bool _hasMine;
        readonly int _mineAround;

        internal ElementEventArgs(int row, int column, bool hasMine, int mineAround) 
        {
            _row = row;
            _column = column;
            _hasMine = hasMine;
            _mineAround = mineAround;
        }

        public int Row { get { return _row; } }
        public int Column { get { return _column; } }
        public bool HasMine { get { return _hasMine; } }
        public int MineAround { get { return _mineAround; } }
    }
}
