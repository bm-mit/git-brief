using Colorful;
using Spectre.Console;

namespace InteractiveConsole;

public class SelectionPrompt<T>
{
    private Func<T, string>? converter;
    private Color backgroundColor;
    private Color foregroundColor;
    private T[] items;

    public SelectionPrompt<T> Converter(Func<T, string> converter)
    {
        this.converter = converter;
        return this;
    }

    public SelectionPrompt<T> BackgroundColor(Color color)
    {
        this.backgroundColor = color;
        return this;
    }

    public SelectionPrompt<T> ForegroundColor(Color color)
    {
        this.foregroundColor = color;
        return this;
    }

    public SelectionPrompt<T> Color(Color foreground, Color background)
    {
        this.foregroundColor = foreground;
        this.backgroundColor = background;
        return this;
    }

    public SelectionPrompt<T> Items(T[] items)
    {
        this.items = items;
        return this;
    }
}