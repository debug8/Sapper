using System;

namespace model
{
    public class MineField : IModelMineField 
    {
        private FieldElement[,] _field;
        private readonly int _fieldWidth;
        private readonly int _fieldHight;
        private readonly float _persentMines;

        public event EventHandler Loose;
        public event EventHandler Win;
        public event EventHandler<ElementEventArgs> ElementChanged;

        public MineField(int fieldWidth, int fieldHight) : this(fieldWidth, fieldHight, 0.15f) { }

        public MineField(int fieldWidth, int fieldHight, float persentMines)
        {
            if (fieldWidth < 5 || fieldWidth > 100) throw new ArgumentOutOfRangeException("fieldWidth", "must be 5 < fieldSize < 100");
            _fieldWidth = fieldWidth;

            if (fieldHight < 5 || fieldHight > 100) throw new ArgumentOutOfRangeException("fieldHight", "must be 5 < fieldSize < 100");
            _fieldHight = fieldHight;

            _field = new FieldElement[_fieldHight, _fieldWidth];

            if (persentMines < 0.01 || persentMines > 0.95) throw new ArgumentOutOfRangeException("persentMines","must be 0.01<persentMines<0.95");
            _persentMines = persentMines;
        }

        public void OpenElement(int row, int column)
        {
            if (row >= _fieldHight)
                throw new ArgumentOutOfRangeException("row", "row can't be more than fieldHight");

            if (column >= _fieldWidth)
                throw new ArgumentOutOfRangeException("column", "column can't be more than fieldWidth");

            if (_field[row, column] == null) 
            {
                InitializeField(row, column);
            }

            var fieldElement = _field[row, column];
            if (fieldElement != null && fieldElement.HasMine)
            {
                OnLoose();
                return;
            }

            OpenFreeElement(row, column);

            var isWin = true;
            foreach (var element in _field) 
            {
                if (element.HasMine) continue;
                isWin &= element.IsOpen;
            }
            if (isWin) OnWin();
        }

        public void Reset()
        {
            _field = new FieldElement[_fieldHight, _fieldWidth];
        }

        private void OpenFreeElement(int row, int column) 
        {
             if (_field[row, column].IsOpen) return;
            _field[row, column].IsOpen = true;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == _fieldHight - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == _fieldWidth - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (_field[i, j].HasMine) continue;
                    if (MineCountAround(i, j) == 0)
                    {
                        OpenFreeElement(i, j);
                    }
                    else 
                    {                       
                        if (!_field[i, j].IsOpen && ElementChanged != null)
                        {
                            ElementChanged(this, new ElementEventArgs(i, j, false, MineCountAround(i, j)));
                        }

                      _field[i, j].IsOpen = true;
                    }
                }
            }

            if (ElementChanged != null)
            {
                ElementChanged(this, new ElementEventArgs(row, column, false, MineCountAround(row, column)));
            }
        }

        private void OnLoose()
        {
            if (ElementChanged != null)
            {               
                EnumerateFieldArray(OpenMine);
            }

            if (Loose != null)
            {
                Loose(this, EventArgs.Empty);
            }
        }

        private void OnWin() 
        {
            if (Win != null)
            {
                Win(this, EventArgs.Empty);
            }
        }

        private void OpenMine(int i, int j) 
        {
            if (_field[i, j].HasMine)
                ElementChanged(this, new ElementEventArgs(i, j, true, 0));
        }       

        private int MineCountAround(int row, int column) 
        {   
            var res = 0;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == _fieldHight - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == _fieldWidth - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (i == row && j == column) continue;
                    if(_field[i,j].HasMine) res++;
                }
            }
            return res;
        }

        private void InitializeField(int firstClickRow, int firstClickColumn) 
        {
            EnumerateFieldArray((i, j) => _field[i, j] = new FieldElement());

            SetMines(firstClickRow, firstClickColumn);
        }

        private void SetMines(int firstClickRow, int firstClickColumn) 
        {
            var amt = (int)(_fieldHight * _fieldWidth * _persentMines);
            var r = new Random();
            for (var i = 0; i < amt; i++)
            {
                var x = r.Next(_fieldWidth);
                var y = r.Next(_fieldHight);
                if (_field[y, x].HasMine || (x==firstClickRow && y==firstClickColumn))
                {
                    i--;
                    continue;
                }
                _field[y, x].HasMine = true;
            }
        }

        private void EnumerateFieldArray(Action<int, int> action) 
        {
            for (int i = 0; i < _fieldHight; i++)
            {
                for (int j = 0; j < _fieldWidth; j++)
                {
                    action(i, j);
                }
            }
        }
    }
}
