using System;
using System.Drawing;
namespace View
{
    public interface IViewMineField
    {
        event EventHandler<MineFieldEventArgs> MineButtonPressed;
        event EventHandler<MineFieldEventArgs> MineButtonRightPressed;

        void SetFieldElement(byte row, byte column, string text, Color col);
        void Reset();
    }

    internal interface IFieldElement
    {
        byte Row { get; }
        byte Column { get; }

        char Text { get; set; }
        System.Drawing.Color TextColor { get; set; }
    }
}
