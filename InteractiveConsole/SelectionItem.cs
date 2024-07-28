namespace InteractiveConsole;

public class SelectionItem<T> : ISelectionItem<T> where T: notnull
{
    public (int, int)? CursorPosition { get; private set; }
    public T Item { get; init; }
    public string RepresentString { get; init; }

    public SelectionItem(T item, string? representString = null)
    {
        Item = item;
        RepresentString = representString ?? Item.ToString() ?? "null";
    }

    public void Render(ConsoleColor color)
    {
        CursorPosition ??= Console.GetCursorPosition();
        
        Console.SetCursorPosition(CursorPosition.Value.Item1, CursorPosition.Value.Item2);
        Console.ForegroundColor = color;
        Console.WriteLine(RepresentString);
        Console.ResetColor();
    }
}