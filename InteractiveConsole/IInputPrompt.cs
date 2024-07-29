namespace InteractiveConsole;

public interface IInputPrompt
{
    public string Title { get; init; }
    public ConsoleColor TitleColor { get; init; }
    public ConsoleColor InputColor { get; init; }

    public string Show();
}