using DiagramGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;

namespace DiagramGeneratorGUI
{
    public class DiagramDrawing
    {
        public double XDivisions { get; set; }
        public double YDivisions { get; set; }
        public double XInterval { get; set; }
        public double YInterval { get; set; }
        /// <summary>
        /// The size of the diagram to display coordinates on the X axis (0 &lt; x &lt;= diagram size)
        /// </summary>
        public double diagramSizeX { get; set; }
        /// <summary>
        /// The size of the diagram to display coordinates on the Y axis (0 &lt; y &lt;= diagram size)
        /// </summary>
        public double diagramSizeY { get; set; }

        public List<XYCoordinate> coordinates = new List<XYCoordinate>();
        /// <summary>
        /// Return the list of coordinates (read only, no modification)
        /// </summary>
        public ReadOnlyCollection<XYCoordinate> Coordinates
        {
            get
            {
                return coordinates.AsReadOnly();
            }
        }
        /// <summary>
        /// Add a coordinate to the list
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddCoordinate(double x, double y)
        {
            if(coordinates.Find(a => a.X == x) == null)
            {
                coordinates.Add(new XYCoordinate(x, y));
            }
            else
            {
                var toChange = coordinates.FirstOrDefault(a => a.X == x);
                toChange.Y = y;
            }
        }
        /// <summary>
        /// Empties the list of coordinates
        /// </summary>
        public void ClearCoordinates()
        {
            coordinates = new List<XYCoordinate>();
        }
    }
}
