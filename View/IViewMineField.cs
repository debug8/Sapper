using System;
using System.Drawing;
namespace View
{
    public interface IViewMineField
    {
        event EventHandler<MineFieldEventArgs> MineButtonPressed;
        event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        void SetFieldElement(byte row, byte column, string text, Color col);
        void SetElementStatus(byte row, byte column, FieldElementStatus status);
        void Reset();
    }
}
