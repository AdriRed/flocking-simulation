using System;
using SFML.Graphics;
using SFML.System;

namespace flocking_sim
{
    class Particle : CircleShape, ISimulable
    {
        public Particle(float Radius) : base(Radius)
        {

        }

        public Particle(float Radius, Vector2f Position) : this(Radius)
        {
            this.Position = Position;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }

        static Random rng = new Random();

        public void UpdateEntity()
        {
            RandomWalker();
        }

        private void RandomWalker()
        {
            Vector2f next = new Vector2f();
            float dX = (float)rng.NextDouble() * 10 - 5;
            float dY = (float)rng.NextDouble() * 10 - 5;
            float nextX = this.Position.X + dX, nextY = this.Position.Y + dY;
            if (nextX < 800 && nextX > 0) next.X = dX;
            if (nextY < 800 && nextY > 0) next.Y = dY;
            this.Position += next;
        }
    }
}