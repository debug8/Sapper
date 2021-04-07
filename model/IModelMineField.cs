using System;
namespace model
{
    public interface IModelMineField
    {
        event EventHandler Loose;
        event EventHandler Win;
        event EventHandler<ElementEventArgs> ElementChanged;

        void OpenElement(int row, int column);
        void Reset();
    }
}
