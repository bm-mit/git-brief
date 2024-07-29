namespace InteractiveConsole;

public class SelectionPrompt<T> : ISelectionPrompt<T> where T : notnull
{
    public Converter<T, string> Convert { get; init; } = item => item.ToString() ?? "";
    public ConsoleColor ForegroundColor { get; init; } = Console.ForegroundColor;
    public ConsoleColor HighlightColor { get; init; } = ConsoleColor.Blue;
    public ConsoleColor TitleColor { get; init; } = ConsoleColor.Red;
    public string? Title { get; init; }
    public SelectionItemCollection<T> Items { get; init; }
    public (int, int)? BaseCursorPosition { get; private set; }
    public (int, int)? BaseSelectionCursorPosition { get; private set; }
    public char HighlightChar { get; private set; } = '>';
    public char UnhighlightChar { get; private set; } = ' ';

    private int _selectionIndex;

    public int SelectionIndex
    {
        get => _selectionIndex;
        private set
        {
            UnhighlightItem(_selectionIndex);
            _selectionIndex = value;
            HighlightItem(_selectionIndex);
        }
    }

    public SelectionPrompt(
        string? title = null, ConsoleColor? foregroundColor = null, ConsoleColor? highlightColor = null,
        ConsoleColor? titleColor = null, T[]? items = null, Converter<T, string>? convert = null)
    {
        Title = title;
        ForegroundColor = foregroundColor ?? ForegroundColor;
        HighlightColor = highlightColor ?? HighlightColor;
        TitleColor = titleColor ?? TitleColor;
        Items = new SelectionItemCollection<T>(items) ?? Items;
        Convert = convert ?? Convert;
    }

    private void MoveUp()
    {
        SelectionIndex = ((SelectionIndex - 1) % Items.Length + Items.Length) % Items.Length;
    }

    private void MoveDown()
    {
        SelectionIndex = ((SelectionIndex + 1) % Items.Length + Items.Length) % Items.Length;
    }

    private void Select()
    {
    }

    private void Cancel()
    {
    }

    private void HandleKeyPress(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.J:
            case ConsoleKey.DownArrow:
                MoveDown();
                break;
            case ConsoleKey.K:
            case ConsoleKey.UpArrow:
                MoveUp();
                break;
            case ConsoleKey.Enter:
                Select();
                break;
            case ConsoleKey.Escape:
                Cancel();
                break;
        }
    }

    private static void WriteItem(string representString, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(representString);
        Console.ResetColor();
    }

    private void Render()
    {
        BaseSelectionCursorPosition ??= Console.GetCursorPosition();
        Console.SetCursorPosition(BaseSelectionCursorPosition.Value.Item1, BaseSelectionCursorPosition.Value.Item2);

        for (int i = 0; i < Items.Length; ++i)
        {
            string item = Items[i].RepresentString;
            Items[i].CursorPosition = Console.GetCursorPosition();

            if (i == SelectionIndex)
                WriteItem($"{HighlightChar} {item}", HighlightColor);
            else
                WriteItem($"{UnhighlightChar} {item}", ForegroundColor);
        }
    }

    private void RewriteItem(int index, string representString, ConsoleColor color)
    {
        SelectionItem<T> item = Items[index];
        (int, int) lastCursorPos = Console.GetCursorPosition();
        (int, int) itemCursorPos = ((int, int))Items[index].CursorPosition!;

        Console.SetCursorPosition(itemCursorPos.Item1, itemCursorPos.Item2);
        WriteItem($"{representString}", color);
        Console.SetCursorPosition(lastCursorPos.Item1, lastCursorPos.Item2);
    }

    private void UnhighlightItem(int index)
    {
        RewriteItem(index, $"{UnhighlightChar} {Items[index].RepresentString}", ForegroundColor);
    }

    private void HighlightItem(int index)
    {
        RewriteItem(index, $"{HighlightChar} {Items[index].RepresentString}", HighlightColor);
    }

    private T? MainLoop()
    {
        Console.WriteLine();
        do
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            HandleKeyPress(keyPressed);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        return default;
    }

    public T? Show()
    {
        BaseCursorPosition = Console.GetCursorPosition();
        if (Title != null)
        {
            Console.ForegroundColor = TitleColor;
            Console.WriteLine(Title, TitleColor);
            Console.ResetColor();
        }

        Render();

        return MainLoop();
    }
}