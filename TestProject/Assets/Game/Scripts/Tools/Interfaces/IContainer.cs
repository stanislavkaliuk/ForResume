namespace Tools
{
    public interface IContainer<out T>
    {
        T GetItem (int index);
        int Count { get; }
    }
}