using flocking_sim.Interfaces;
using SFML.System;

namespace flocking_sim.Shapes
{
    public class Rectangle : IContainable, IIntersectable<Rectangle>, IIntersectable<Circumference>
    {
        public Vector2f Position { get; set; }
        public Vector2f Edges { get; set; }

        public Rectangle(Vector2f position, Vector2f edges)
        {
            Position = position;
            Edges = edges;
        }

        public Rectangle(float x, float y, float w, float h) : this(
            new Vector2f(x, y),
            new Vector2f(w, h))
        {
        }

        public Rectangle()
        {

        }

        public bool Intersects(Rectangle other)
        {
            return !(other.Position.X - other.Edges.X > this.Position.X + this.Position.X ||
                other.Position.X + other.Edges.X < this.Position.X - this.Position.X ||
                other.Position.Y - other.Edges.Y > this.Position.Y + this.Position.Y ||
                other.Position.Y + other.Edges.Y < this.Position.Y - this.Position.Y
            );
        }

        public bool Contains(IPositionable p)
        {
            return (p.Position.X > this.Position.X - this.Edges.X) && (p.Position.X < this.Position.X + this.Edges.X) &&
            (p.Position.Y > this.Position.Y - this.Edges.Y) && (p.Position.Y < this.Position.Y + this.Edges.Y);
        }

        public bool Intersects(Circumference other)
        {
            return other.Intersects(this);
        }
    }
}