using System;
using SFMLReady;
using SFML.Graphics;

namespace flocking_sim
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation q = new Simulation(800, 800, "Quadtree", Color.Black);
            q.Run();
            // Console.WriteLine("Hello World!");
        }
    }
}
