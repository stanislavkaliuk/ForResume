using Tools;

namespace Managers
{
    public interface IMultipleResourceManager<T> : IResourceManager<T>
    {
        void SwitchTarget(IContainer<T> container);
        bool IsStandardActive();
    }
}