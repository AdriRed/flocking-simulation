using System;
using SFML.System;

namespace flocking_sim
{
    public class Circumference : IContainable, IIntersectable<Rectangle>
    {
        public Circumference(Vector2f position, float radius)
        {
            Position = position;
            Radius = radius;
        }
        public Circumference(float x, float y, float radius) : this(new Vector2f(x, y), radius) { }

        public Vector2f Position { get; set; }
        public float Radius { get; set; }

        public bool Contains(IPositionable other)
        {
            var dPos = this.Position - other.Position;
            var distance = MathF.Sqrt(dPos.X * dPos.X + dPos.Y * dPos.Y);
            return distance <= this.Radius;
        }

        public bool Intersects(Rectangle other)
        {
            float xDist = MathF.Abs(other.Position.X - this.Position.X);
            float yDist = MathF.Abs(other.Position.Y - this.Position.Y);

            // radius of the circle
            float r = this.Radius;

            float w = other.Edges.X;
            float h = other.Edges.Y;
            float edges = MathF.Pow((xDist - w), 2) + MathF.Pow((yDist - h), 2);

            // no intersection
            if (xDist > (r + w) || yDist > (r + h))
                return false;

            // intersection within the circle
            if (xDist <= w || yDist <= h)
                return true;

            // intersection on the edge of the circle
            return edges <= this.Radius * this.Radius;
        }
    }
}