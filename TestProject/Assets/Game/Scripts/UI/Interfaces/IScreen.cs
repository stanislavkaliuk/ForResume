namespace UI
{
    public interface IScreen
    {
        int GetID();
        void Activate();
        void Deactivate();
    }
}