﻿using System;
namespace model
{
    public interface IModelMineField
    {
        event EventHandler Loose;
        event EventHandler Win;
        event EventHandler<ElementEventArgs> ElementChanged;

        void OpenElement(byte row, byte column);
        void Reset();
    }
}
