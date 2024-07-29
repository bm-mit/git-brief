namespace InteractiveConsole;

public class SelectionPrompt<T> : ISelectionPrompt<T> where T : notnull
{
    public Converter<T, string> Convert { get; init; } = item => item.ToString() ?? "";
    public ConsoleColor ForegroundColor { get; init; } = Console.ForegroundColor;
    public ConsoleColor HighlightColor { get; init; } = ConsoleColor.Blue;
    public ConsoleColor TitleColor { get; init; } = ConsoleColor.Red;
    public string? Title { get; init; }
    public SelectionItemCollection<T> Items { get; init; }
    public List<string> ConvertedItems { get; init; } = [];
    public (int, int)? BaseCursorPosition { get; private set; }
    public (int, int)? BaseSelectionCursorPosition { get; private set; }

    private int _selectionIndex;
    public int SelectionIndex
    {
        get => _selectionIndex;
        private set
        {
            _selectionIndex = value;
            Rerender();
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
        Rerender();
    }

    private void MoveDown()
    {
        SelectionIndex = ((SelectionIndex + 1) % Items.Length + Items.Length) % Items.Length;
        Rerender();
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

    private void Rerender()
    {
        Render();
    }

    private void Render()
    {
        BaseSelectionCursorPosition ??= Console.GetCursorPosition();
        Console.SetCursorPosition(BaseSelectionCursorPosition.Value.Item1, BaseSelectionCursorPosition.Value.Item2);

        const char selectIcon = '>';
        const char unselectIcon = ' ';

        for (int i = 0; i < Items.Length; ++i)
        {
            string item = ConvertedItems[i];

            if (i == SelectionIndex)
            {
                Console.ForegroundColor = HighlightColor;
                Console.WriteLine($"{selectIcon} {item}");
            }
            else
            {
                Console.ForegroundColor = ForegroundColor;
                Console.WriteLine($"{unselectIcon} {item}");
            }

            Console.ResetColor();
        }
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

        Console.WriteLine();
        do
        {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            HandleKeyPress(keyPressed);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        return default;
    }
}