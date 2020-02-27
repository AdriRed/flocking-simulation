using System;
using flocking_sim.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace flocking_sim.Shapes
{
    public class Point : IPositionable
    {
        public Point(Vector2f position)
        {
            Position = position;
        }

        public Point(Vector2f position, object data) : this(position)
        {
            Data = data;
        }

        public object Data {get; set;}
        public Vector2f Position { get; set; }
        public bool HasData {
            get {
                return !(Data is null);
            }
        }
    }
}