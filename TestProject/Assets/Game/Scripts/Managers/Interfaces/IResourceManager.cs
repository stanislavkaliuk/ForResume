namespace Managers
{
    public interface IResourceManager<out T>
    {
        
        T GetRandomItem();
        T GetItem(int index);
        int Count();
    }

}