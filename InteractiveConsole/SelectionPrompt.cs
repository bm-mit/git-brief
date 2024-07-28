namespace InteractiveConsole;

public class SelectionPrompt<T> where T: notnull
{
    private Func<T, string> Convert { get; init; } = item => item.ToString() ?? "";
    private ConsoleColor ForegroundColor { get; init; } = Console.ForegroundColor;
    private ConsoleColor HighlightColor { get; init; } = ConsoleColor.Blue;
    private ConsoleColor TitleColor { get; init; } = ConsoleColor.Red;
    private string Title { get; init; }
    private T[] Items { get; init; } = [];
    private List<string> ConvertedItems { get; init; } = [];
    private int SelectionIndex { get; set; } = 0;
    private (int, int) PromptPosition { get; set; }

    public SelectionPrompt(
        string? title = null, ConsoleColor? foregroundColor = null, ConsoleColor? highlightColor = null,
        ConsoleColor? titleColor = null, T[]? items = null, Func<T, string>? convert = null)
    {
        Title = title ?? Title;
        ForegroundColor = foregroundColor ?? ForegroundColor;
        HighlightColor = highlightColor ?? HighlightColor;
        TitleColor = titleColor ?? TitleColor;
        Items = items ?? Items;
        Convert = convert ?? convert;
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

    private void ItemsToString()
    {
        for (int i = 0; i < Items.Length; ++i)
            ConvertedItems.Add(Convert(Items[i]));
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

    private void Render()
    {
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
        PromptPosition = Console.GetCursorPosition();

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