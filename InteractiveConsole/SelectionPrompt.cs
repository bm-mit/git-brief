namespace InteractiveConsole;

public class SelectionPrompt<T> where T : notnull
{
    private Converter<T, string> Convert { get; init; } = item => item.ToString() ?? "";
    private ConsoleColor ForegroundColor { get; init; } = Console.ForegroundColor;
    private ConsoleColor HighlightColor { get; init; } = ConsoleColor.Blue;
    private ConsoleColor TitleColor { get; init; } = ConsoleColor.Red;
    private string? Title { get; init; }
    private T[] Items { get; init; } = [];
    private List<string> ConvertedItems { get; init; } = [];

    private int _selectionIndex;

    private int SelectionIndex
    {
        get => _selectionIndex;
        set
        {
            _selectionIndex = value;
            Rerender();
        }
    }

    private (int, int) BasePromptPosition { get; set; }
    private (int, int) SelectionAreaPosition { get; set; }

    public SelectionPrompt(
        string? title = null, ConsoleColor? foregroundColor = null, ConsoleColor? highlightColor = null,
        ConsoleColor? titleColor = null, T[]? items = null, Converter<T, string>? convert = null)
    {
        Title = title;
        ForegroundColor = foregroundColor ?? ForegroundColor;
        HighlightColor = highlightColor ?? HighlightColor;
        TitleColor = titleColor ?? TitleColor;
        Items = items ?? Items;
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

    private void ItemsToString()
    {
        foreach (var items in Items)
            ConvertedItems.Add(Convert(items));
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
        Console.SetCursorPosition(SelectionAreaPosition.Item1, SelectionAreaPosition.Item2);
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
        BasePromptPosition = Console.GetCursorPosition();
        if (Title != null)
        {
            Console.ForegroundColor = TitleColor;
            Console.WriteLine(Title, TitleColor);
            Console.ResetColor();
        }

        SelectionAreaPosition = Console.GetCursorPosition();
        ItemsToString();
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