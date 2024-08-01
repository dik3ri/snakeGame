using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeGame
{
    internal class Snake
    {
        private const int DEFAULT_ROW = 1;
        private const int DEFAULT_LENGTH = 5;

        public Queue<Point> Points { get; private set; }

        public Snake()
        {
            Points = new Queue<Point>();

            for (int i = 0; i < DEFAULT_LENGTH; i++)
            {
                Point point = new Point(DEFAULT_ROW, i);
                Points.Enqueue(point);
            }
        }
    }
}
