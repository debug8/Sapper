using System;

namespace model
{
    public class MineField : IModelMineField 
    {
        private FieldElement[,] field;
        private int fieldSize;
        private float persentMines;

        public event EventHandler Loose;
        public event EventHandler Win;
        public event EventHandler<ElementEventArgs> ElementChanged;

        public MineField(int fieldSize) : this(fieldSize, 0.15f) { }

        public MineField(int fieldSize, float persentMines) 
        {
            if (fieldSize < 5 || fieldSize > 100) throw new ArgumentOutOfRangeException("fieldSize", "must be 5 < fieldSize < 100");
            this.fieldSize = fieldSize;
            field = new FieldElement[fieldSize, fieldSize];

            if (persentMines < 0.01 || persentMines > 0.95) throw new ArgumentOutOfRangeException("persentMines","must be 0.01<persentMines<0.95");
            this.persentMines = persentMines;
        }

        public void OpenElement(int row, int column)
        {
            if (row >= fieldSize && column >= fieldSize)   
                throw new ArgumentOutOfRangeException("row and column can't be more than fieldSize");
            if (field[row, column] == null) 
            {
                InitializeField(row, column);
            }

            if (field[row, column].HasMine)
            {
                OnLoose();
                return;
            }

            OpenFreeElement(row, column);

            bool isWin = true;
            foreach (var element in field) 
            {
                if (element.HasMine) continue;
                isWin &= element.IsOpen;
            }
            if (isWin) OnWin();
        }

        public void Reset()
        {
            field = new FieldElement[fieldSize, fieldSize];
        }

        private void OpenFreeElement(int row, int column) 
        {
             if (field[row, column].IsOpen) return;
            field[row, column].IsOpen = true;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == fieldSize - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == fieldSize - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (field[i, j].HasMine) continue;
                    if (MineCountAround(i, j) == 0)
                    {
                        OpenFreeElement(i, j);
                    }
                    else 
                    {                       
                        if (!field[i, j].IsOpen && ElementChanged != null)
                        {
                            ElementChanged(this, new ElementEventArgs((int)i, (int)j, false, MineCountAround(i, j)));
                        }

                      field[i, j].IsOpen = true;
                    }
                }
            }

            if (ElementChanged != null)
            {
                ElementChanged(this, new ElementEventArgs((int)row, (int)column, false, MineCountAround(row, column)));
            }
        }

        private void OnLoose()
        {
            if (ElementChanged != null)
            {               
                EnumerateArray(OpenMines);
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

        private void OpenMines(int i, int j) 
        {
            if (field[i, j].HasMine)
                ElementChanged(this, new ElementEventArgs(i, j, true, 0));
        }       

        private int MineCountAround(int row, int column) 
        {   
            int res = 0;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == fieldSize - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == fieldSize - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (i == row && j == column) continue;
                    if(field[i,j].HasMine) res++;
                }
            }
            return res;
        }

        private void InitializeField(int firstClickRow, int firstClickColumn) 
        {
            EnumerateArray((i, j) => field[i, j] = new FieldElement());

            SetMines(firstClickRow, firstClickColumn);
        }

        private void SetMines(int firstClickRow, int firstClickColumn) 
        {
            int amt = (int)(fieldSize * fieldSize * persentMines);
            Random r = new Random();
            for (int i = 0; i < amt; i++)
            {
                int x = r.Next(fieldSize);
                int y = r.Next(fieldSize);
                if (field[x, y].HasMine || (x==firstClickRow && y==firstClickColumn))
                {
                    i--;
                    continue;
                }
                field[x, y].HasMine = true;
            }
        }

        private void EnumerateArray(Action<int, int> action) 
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    action(i, j);
                }
            }
        }
    }
}
