namespace InteractiveConsole;

public class SelectionPrompt<T> : ISelectionPrompt<T> where T : notnull
{
    private int _selectionIndex;

    public SelectionPrompt(
        string? title = null, ConsoleColor? foregroundColor = null, ConsoleColor? highlightColor = null,
        ConsoleColor? titleColor = null, T[]? items = null, Converter<T, string>? converter = null)
    {
        Title = title;
        ForegroundColor = foregroundColor ?? ForegroundColor;
        HighlightColor = highlightColor ?? HighlightColor;
        TitleColor = titleColor ?? TitleColor;
        Items = new SelectionItemCollection<T>(items ?? [], converter);
        Convert = converter ?? Convert;
    }

    public Converter<T, string> Convert { get; init; } = item => item.ToString() ?? "";
    public ConsoleColor ForegroundColor { get; init; } = Console.ForegroundColor;
    public ConsoleColor HighlightColor { get; init; } = ConsoleColor.Blue;
    public ConsoleColor TitleColor { get; init; } = ConsoleColor.Red;
    public string? Title { get; init; }
    public SelectionItemCollection<T> Items { get; init; }
    public (int, int)? BaseCursorPosition { get; private set; }
    public (int, int)? BaseSelectionCursorPosition { get; private set; }
    public char HighlightChar { get; } = '>';
    public char UnhighlightChar { get; } = ' ';

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

    public T? Show()
    {
        Render();
        return MainLoop();
    }

    private void MoveUp()
    {
        SelectionIndex = ((SelectionIndex - 1) % Items.Length + Items.Length) % Items.Length;
    }

    private void MoveDown()
    {
        SelectionIndex = ((SelectionIndex + 1) % Items.Length + Items.Length) % Items.Length;
    }

    private T Select()
    {
        return Items[SelectionIndex].Item;
    }

    private static void WriteItem(string representString, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(representString);
        Console.ResetColor();
    }

    private void RenderTitle()
    {
        BaseCursorPosition = Console.GetCursorPosition();
        if (Title == null) return;

        WriteItem(Title, TitleColor);
    }

    private void RenderItem()
    {
        BaseSelectionCursorPosition ??= Console.GetCursorPosition();

        for (var i = 0; i < Items.Length; ++i)
        {
            var item = Items[i].RepresentString;
            Items[i].CursorPosition = Console.GetCursorPosition();

            if (i == SelectionIndex)
                WriteItem($"{HighlightChar} {item}", HighlightColor);
            else
                WriteItem($"{UnhighlightChar} {item}", ForegroundColor);
        }
    }

    private void Render()
    {
        RenderTitle();
        RenderItem();
    }

    private void RewriteItem(int index, string representString, ConsoleColor color)
    {
        var item = Items[index];
        (int, int) lastCursorPos = Console.GetCursorPosition();
        var itemCursorPos = ((int, int))item.CursorPosition!;

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

    private static SelectionPromptAction HandleKeyPress(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.J or ConsoleKey.DownArrow => SelectionPromptAction.MoveDown,
            ConsoleKey.K or ConsoleKey.UpArrow => SelectionPromptAction.MoveUp,
            ConsoleKey.Enter => SelectionPromptAction.Select,
            ConsoleKey.Escape => SelectionPromptAction.Cancel,
            _ => SelectionPromptAction.Invalid
        };
    }

    private T? MainLoop()
    {
        do
        {
            var keyInfo = Console.ReadKey(true);
            var action = HandleKeyPress(keyInfo.Key);

            switch (action)
            {
                case SelectionPromptAction.MoveDown:
                    MoveDown();
                    break;
                case SelectionPromptAction.MoveUp:
                    MoveUp();
                    break;
                case SelectionPromptAction.Cancel:
                    return default;
                case SelectionPromptAction.Select:
                    return Select();
            }
        } while (true);
    }
}