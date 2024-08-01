using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeGame
{
    internal class Point
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Point(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override bool Equals(object? obj)
        {
            Point point = obj as Point;

            return Row == point.Row && Col == point.Col;
        }



    }
}
