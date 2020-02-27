namespace flocking_sim.Interfaces
{
    public interface IIntersectable<T> : IPositionable
    {
        bool Intersects(T other);
    }
}