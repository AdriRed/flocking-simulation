using System;
using SFML.System;

namespace flocking_sim
{
    class Circumference : IContainable, IIntersectable<Circumference>
    {
        public Circumference(Vector2f position, float radius)
        {
            Position = position;
            Radius = radius;
        }
        public Circumference(float x, float y, float radius) : this(new Vector2f(x, y), radius) {}

        public Vector2f Position { get ; set; }
        public float Radius { get; set; }

        public bool Contains(IPositionable other)
        {
            var dPos = this.Position - other.Position;
            var distance = MathF.Sqrt(dPos.X*dPos.X + dPos.Y * dPos.Y);
            return distance >= this.Radius;
        }

        public bool Intersects(Circumference other)
        {
            throw new System.NotImplementedException();
        }
    }
}