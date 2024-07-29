namespace InteractiveConsole;

public interface ISelectionPrompt<T>
{
    public Converter<T, string> Convert { get; init; }
    public ConsoleColor ForegroundColor { get; init; }
    public ConsoleColor HighlightColor { get; init; }
    public ConsoleColor TitleColor { get; init; }
    public string? Title { get; init; }
    public SelectionItemCollection<T> Items { get; init; }
    public int SelectionIndex { get; }
    public (int, int)? BaseCursorPosition { get; }
    public (int, int)? BaseSelectionCursorPosition { get; }
    public string HighlightPrefix { get; }
    public string UnhighlightPrefix { get; }

    public T? Show();
}