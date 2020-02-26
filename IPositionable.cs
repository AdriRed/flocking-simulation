using SFML.Graphics;
using SFML.System;

namespace flocking_sim
{
    public interface IPositionable
    {
        Vector2f Position { get; set; }
    }

    public interface IDrawable {
        void Draw(RenderWindow window);
    }
}
