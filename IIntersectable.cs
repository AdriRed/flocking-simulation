namespace flocking_sim
{
    public interface IIntersectable<T> : IPositionable
    {
        bool Intersects(T other);
    }
}