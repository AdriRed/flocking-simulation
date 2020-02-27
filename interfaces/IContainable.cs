namespace flocking_sim.Interfaces
{
    public interface IContainable : IPositionable
    {
        bool Contains(IPositionable other);
    }
}