namespace flocking_sim
{
    public interface IContainable : IPositionable
    {
        bool Contains(IPositionable other);
    }
}