#undef DEBUG

using System;
using System.Numerics;
using System.Collections.Generic;
using SFML.Graphics;
using SFMLReady;
using SFML.Window;
using SFML.System;

namespace flocking_sim
{
    class Simulation : GameLoop
    {
        Quadtree qt;
        List<ISimulable> elements = new List<ISimulable>();

        // Circumference queryArea = new Circumference(rng.Next(300, 500), rng.Next(300, 500), rng.Next(100, 250));
        Shape queryShape;

        public Simulation(uint width, uint height, string title, Color clearColor) : base(width, height, title, clearColor, 60)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            // qt.Draw(this.Window);
            // this.Window.Draw(queryShape);
            foreach (var element in elements)
            {
                element.Draw(this.Window);
            }
            DebugUtility.DrawPerformanceData(this);

        }
        
        static Random rng = new Random();

        public override void Initialize()
        {
            float x = this.Window.Size.X / 2, y = this.Window.Size.Y / 2;
            qt = new Quadtree(x, y, x, y, 5);
            
            for (int i = 0; i < 3000; i++)
            {
                var part = new Particle(PARTICLE_RADIUS, new Vector2f(rng.Next(0, (int)Math.Floor(x*2d)), rng.Next(0, (int)Math.Floor(y*2d))));
                part.FillColor = Color.Red;
                elements.Add(part);
                qt.Insert(new Point(part.Position, part));
            }
            
            Window.MouseButtonPressed += OnMousePressed;
            // queryshape = new Rectangle(rng.Next(300, 500), rng.Next(300, 500), rng.Next(100, 200), rng.Next(100, 200));
            
            // if (queryArea is Circumference c) {
            //     queryShape = new CircleShape(c.Radius);
            //     queryShape.Position = c.Position - new Vector2f(c.Radius, c.Radius);
            // }/* else if (queryArea is Rectangle r){
            //     queryShape = new RectangleShape();
            //     queryShape.Position = queryShape.Position - queryShape.Edges;
            //     queryShape.Size = queryShape.Edges * 2;
            // }*/

            
            // queryShape.OutlineThickness = 2;
            // queryShape.OutlineColor = Color.Red;
            // queryShape.FillColor = Color.Transparent;
        }
        int PARTICLE_RADIUS = 10;
        public void OnMousePressed(object sender, MouseButtonEventArgs args) 
        {
            if (args.Button != Mouse.Button.Left) return;
            
            var part = new Particle(PARTICLE_RADIUS, new Vector2f(args.X, args.Y));
            part.FillColor = Color.Red;
            elements.Add(part);
            qt.Insert(new Point(part.Position, part));
        }

        public override void LoadContent()
        {
            DebugUtility.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            elements.ForEach(x => { 
                x.UpdateEntity();
                ((Particle)x).FillColor = Color.Blue;
            });
            float x = this.Window.Size.X / 2, y = this.Window.Size.Y / 2;
            qt = new Quadtree(x, y, x, y, 5);
            elements.ForEach((x) => qt.Insert(new Point(x.Position, x)));

            foreach (var item in elements)
            {
                var circum = new Circumference(item.Position, ((Particle)item).Radius);
                var query = qt.Query(circum);
                foreach (var other in query)
                {
                    if (item != other.Data) 
                    {
                        ((Particle)other.Data).FillColor = Color.Red;
                    }
                }
                // circum.Data.Intersected = false;
            }

            // var query = qt.Query(queryArea);

            // foreach (var element in query)
            // {
            //     Point point = element as Point;
            //     Particle part = point.Data as Particle;
            //     part.FillColor = Color.Red;
            // }
        }
    }
}