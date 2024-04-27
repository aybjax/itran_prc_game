namespace Task3_Paper_Rock_Scissor.Types;

public delegate Task AsyncEventHandler<TArgs>(object? sender, TArgs e) where TArgs : EventArgs;