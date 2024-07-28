namespace InteractiveConsole;

public interface ISelectionItemCollection<out T> : IEnumerable<T> where T : notnull
{
    void Render();
}