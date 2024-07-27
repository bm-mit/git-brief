namespace InteractiveConsole;

public class SelectionPrompt<T>
{
    private Func<T, string> converter = item => item.ToString();
    private ConsoleColor foregroundColor = Console.ForegroundColor;
    private ConsoleColor highlightColor = ConsoleColor.Blue;
    private T[] items = [];
    private List<string> convertedItems = [];
    private int index = 0;

    private void MoveUp()
    {
        index = ((index - 1) % items.Length + items.Length) % items.Length;
    }

    private void MoveDown()
    {
        index = ((index + 1) % items.Length + items.Length) % items.Length;
    }

    private void Select()
    {
    }

    private void Cancel()
    {
    }

    public SelectionPrompt<T> Converter(Func<T, string> converter)
    {
        this.converter = converter;
        return this;
    }

    public SelectionPrompt<T> ForegroundColor(ConsoleColor color)
    {
        this.foregroundColor = color;
        return this;
    }

    public SelectionPrompt<T> HighlightColor(ConsoleColor color)
    {
        this.highlightColor = color;
        return this;
    }

    public SelectionPrompt<T> Color(ConsoleColor highlight, ConsoleColor foreground)
    {
        this.highlightColor = highlight;
        this.foregroundColor = foreground;
        return this;
    }

    public SelectionPrompt<T> Items(T[] items)
    {
        this.items = items;
        return this;
    }

    private void ItemsToString()
    {
        for (int i = 0; i < items.Length; ++i)
            convertedItems.Add(converter(items[i]));
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

        for (int i = 0; i < items.Length; ++i)
        {
            string item = convertedItems[i];
            
            if (i == index)
            {
                Console.ForegroundColor = highlightColor;
                Console.WriteLine($"{selectIcon} {item}");
            }
            else
            {
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine($"{unselectIcon} {item}");
            }

            Console.ResetColor();
        }
    }

    public T? Show()
    {
        ItemsToString();
        
        Render();

        do
        {
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        return default;
    }
}