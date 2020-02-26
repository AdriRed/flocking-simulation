using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace flocking_sim
{
    class Quadtree
    {
        public Rectangle Boundary { get; private set; }
        public List<Point> Elements { get; private set; } = new List<Point>();
        public int Capacity { get; private set; }

#if DEBUG
        private RectangleShape shape;

        internal void Draw(RenderWindow window)
        {
            if (!divided) window.Draw(shape);
            else
            {
                Northeast.Draw(window);
                Northwest.Draw(window);
                Southeast.Draw(window);
                Southwest.Draw(window);
            }
        }
#endif


        private Quadtree Northwest { get; set; }
        private Quadtree Northeast { get; set; }
        private Quadtree Southwest { get; set; }



        private Quadtree Southeast { get; set; }
        private bool divided = false;


        public Quadtree(Rectangle boundary, int capacity)
        {
            Boundary = boundary;
            Capacity = capacity;
#if DEBUG
            shape = new RectangleShape();
            shape.Position = boundary.Position - boundary.Edges;
            shape.Size = boundary.Edges * 2;
            shape.FillColor = Color.Transparent;
            shape.OutlineThickness = 1;
            shape.OutlineColor = Color.Black;
#endif
        }

        public Quadtree(float x, float y, float w, float h, int cap) : this(new Rectangle(x, y, w, h), cap)
        {

        }

        public void Subdivide()
        {
            float halfW = this.Boundary.Edges.X / 2;
            float halfH = this.Boundary.Edges.Y / 2;
            float x = this.Boundary.Position.X;
            float y = this.Boundary.Position.Y;
            this.Northwest = new Quadtree(x + halfW, y - halfH, halfW, halfH, this.Capacity);
            this.Northeast = new Quadtree(x - halfW, y - halfH, halfW, halfH, this.Capacity);
            this.Southwest = new Quadtree(x + halfW, y + halfH, halfW, halfH, this.Capacity);
            this.Southeast = new Quadtree(x - halfW, y + halfH, halfW, halfH, this.Capacity);
        }

        public bool Insert(Point p)
        {
            if (!this.Boundary.Contains(p)) return false;


            if (this.Elements != null && this.Elements.Count < this.Capacity)
            {
                this.Elements.Add(p);
            }
            else
            {
                if (!divided)
                {
                    this.Subdivide();
#if DEBUG
                    shape = null;
#endif
                    divided = true;
                    foreach (var item in this.Elements)
                    {
                        this.Insert(item);
                    }
                    Elements = null;
                }
                if (this.Northeast.Insert(p)) return true;
                if (this.Northwest.Insert(p)) return true;
                if (this.Southeast.Insert(p)) return true;
                if (this.Southwest.Insert(p)) return true;
            }
            return true;

        }

        public List<IPositionable> Query(Rectangle range)
        {
            List<IPositionable> results = new List<IPositionable>();

            if (this.Boundary.Intersects(range))
                if (!divided)
                {
                    foreach (var element in Elements)
                    {
                        if (range.Contains(element)) results.Add(element);
                    }
                }
                else
                {
                    results.AddRange(Northeast.Query(range));
                    results.AddRange(Northwest.Query(range));
                    results.AddRange(Southeast.Query(range));
                    results.AddRange(Southwest.Query(range));
                }

            return results;
        }

        public List<Point> Query(Circumference range)
        {
            List<Point> results = new List<Point>();

            if (this.Boundary.Intersects(range))
                if (!divided)
                {
                    foreach (var element in Elements)
                    {
                        if (range.Contains(element)) results.Add(element);
                    }
                }
                else
                {
                    results.AddRange(Northeast.Query(range));
                    results.AddRange(Northwest.Query(range));
                    results.AddRange(Southeast.Query(range));
                    results.AddRange(Southwest.Query(range));
                }

            return results;
        }

        public override string ToString()
        {
            if (!this.divided)
                return $"Capacity = {this.Capacity}, Count = {this.Elements?.Count ?? 0}";
            else
                return "Divided";
        }
    }
}