﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramGenerator
{
    public class XYCoordinate
    {
        public double X { get; set; }
        public double Y { get; set; }

        public XYCoordinate(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
