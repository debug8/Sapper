using System;
using System.Drawing;
namespace View
{
    public interface IViewMineField
    {
        event EventHandler<MineFieldEventArgs> MineButtonPressed;
        event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        void SetElementStatus(int row, int column, FieldElementStatus status);
        FieldElementStatus GetElementStatus(int row, int column);
        void Reset();
    }
}
