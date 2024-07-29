namespace InteractiveConsole;

public class InputPrompt(
    string title,
    ConsoleColor titleColor = ConsoleColor.Blue,
    ConsoleColor inputColor = ConsoleColor.Gray)
    : IInputPrompt
{
    public string Title { get; init; } = title;
    public ConsoleColor TitleColor { get; init; } = titleColor;
    public ConsoleColor InputColor { get; init; } = inputColor;

    public string Show()
    {
        Console.ForegroundColor = TitleColor;
        Console.Write($"{Title} > ");
        Console.ResetColor();

        return Console.ReadLine();
    }
}