namespace InteractiveConsole;

public interface ISelectionItem<T>
{
    public (int, int)? CursorPosition { get; }
    public T Item { get; init; }
    public string RepresentString { get; init; }
}