using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace model
{
    public class MineField : IModelMineField 
    {
        private FieldElement[,] field;
        private byte fieldSize;
        private float persentMines;

        public event EventHandler Loose;
        public event EventHandler Win;
        public event EventHandler<ElementEventArgs> ElementChanged;

        public MineField(byte fieldSize) : this(fieldSize, 0.15f) { }

        public MineField(byte fieldSize, float persentMines) 
        {
            if (fieldSize < 5 || fieldSize > 100) throw new ArgumentOutOfRangeException("fieldSize", "must be 5 < fieldSize < 100");
            this.fieldSize = fieldSize;
            field = new FieldElement[fieldSize, fieldSize];

            if (persentMines < 0.01 || persentMines > 0.95) throw new ArgumentOutOfRangeException("persentMines","must be 0.01<persentMines<0.95");
            this.persentMines = persentMines;
        }

        public void OpenElement(byte row, byte column)
        {
            if (row >= fieldSize && column >= fieldSize)   
                throw new ArgumentOutOfRangeException("row and column can't be more than fieldSize");
            if (field[row, column] == null) 
            {
                InitializeField(row, column);
            }

            if (field[row, column].hasMine)
            {
                OnLoose();
                return;
            }

            OpenFreeElement(row, column);

            bool isWin = true;
            foreach (var element in field) 
            {
                if (element.hasMine) continue;
                isWin &= element.isOpen;
            }
            if (isWin) OnWin();
        }

        public void Reset()
        {
            field = new FieldElement[fieldSize, fieldSize];
        }

        private void OpenFreeElement(int row, int column) 
        {
             if (field[row, column].isOpen) return;
            field[row, column].isOpen = true;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == fieldSize - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == fieldSize - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (field[i, j].hasMine) continue;
                    if (MineCountAround(i, j) == 0)
                    {
                        OpenFreeElement(i, j);
                    }
                    else 
                    {                       
                        if (!field[i, j].isOpen && ElementChanged != null)
                        {
                            ElementChanged(this, new ElementEventArgs((byte)i, (byte)j, false, MineCountAround(i, j)));
                        }

                      field[i, j].isOpen = true;
                    }
                }
            }

            if (ElementChanged != null)
            {
                ElementChanged(this, new ElementEventArgs((byte)row, (byte)column, false, MineCountAround(row, column)));
            }
        }

        private void OnLoose()
        {
            if (ElementChanged != null)
            {               
                EnumerateArray((i, j) => OpenMines(i, j));
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

        private void OpenMines(byte i, byte j) 
        {
            if (field[i, j].hasMine)
                ElementChanged(this, new ElementEventArgs(i, j, true, 0));
        }       

        private byte MineCountAround(int row, int column) 
        {   
            byte res = 0;

            var rowMin = row == 0 ? 0 : -1;
            var rowMax = row == fieldSize - 1 ? 0 : 1;

            var columnMin = column == 0 ? 0 : -1;
            var columnMax = column == fieldSize - 1 ? 0 : 1;

            for (int i = row + rowMin; i <= row + rowMax; i++)
            {
                for (int j = column + columnMin; j <= column + columnMax; j++)
                {
                    if (i == row && j == column) continue;
                    if(field[i,j].hasMine) res++;
                }
            }
            return res;
        }

        private void InitializeField(byte firstClickRow, byte firstClickColumn) 
        {
            EnumerateArray((i, j) => field[i, j] = new FieldElement());

            SetMines(firstClickRow, firstClickColumn);
        }

        private void SetMines(byte firstClickRow, byte firstClickColumn) 
        {
            int amt = (int)(fieldSize * fieldSize * persentMines);
            Random r = new Random();
            for (int i = 0; i < amt; i++)
            {
                int x = r.Next(fieldSize);
                int y = r.Next(fieldSize);
                if (field[x, y].hasMine || (x==firstClickRow && y==firstClickColumn))
                {
                    i--;
                    continue;
                }
                field[x, y].hasMine = true;
            }
        }

        private void EnumerateArray(Action<byte, byte> action) 
        {
            for (byte i = 0; i < fieldSize; i++)
            {
                for (byte j = 0; j < fieldSize; j++)
                {
                    action(i, j);
                }
            }
        }
    }
}
