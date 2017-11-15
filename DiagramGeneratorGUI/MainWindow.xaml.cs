using DiagramGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagramGeneratorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DiagramDrawing dDrawing = new DiagramDrawing();
        double canvasWidth;
        double canvasHeight;
        bool initialized = false;
        int marginLeft = 50;
        int marginBottom = 50;

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Read from the textboxes and set up the Diagram Drawing class to be used for drawing and adding coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOKDiagram_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInt(txtDivX.Text,false) && ValidateInt(txtDivY.Text, false) && ValidateInt(txtIntervalX.Text, false) && ValidateInt(txtIntervalY.Text, false))
            {
                dDrawing.XDivisions = double.Parse(txtDivX.Text);
                dDrawing.YDivisions = double.Parse(txtDivY.Text);
                dDrawing.XInterval = double.Parse(txtIntervalX.Text);
                dDrawing.YInterval = double.Parse(txtIntervalY.Text);

                ClearDiagram();
                DrawDiagram();
                initialized = true;
                DisableSettings();
            }
            else
            {
                MessageBox.Show("Divisions and interval values cannot be zero or under!");
            }
            
        }

        private void ClearDiagram()
        {
            canvas.Children.Clear();
        }
        /// <summary>
        /// Create the lines and numbers that make up the X and Y axis to the diagram and add to the canvas
        /// </summary>
        private void DrawDiagram()
        {
            //diagram title
            var diagramLabel = new TextBlock();
            diagramLabel.Text = txtDiagramTitle.Text;
            diagramLabel.Foreground = Brushes.Black;
            Canvas.SetLeft(diagramLabel, canvas.Width / 2);
            Canvas.SetTop(diagramLabel, 0);
            canvas.Children.Add(diagramLabel);


            //y axis
            var lineYAxis = new Line();
            lineYAxis.Stroke = Brushes.LightBlue;
            lineYAxis.X1 = marginLeft;
            lineYAxis.X2 = marginLeft;
            lineYAxis.Y1 = marginBottom;
            lineYAxis.Y2 = canvas.Height - marginBottom;
            canvasHeight = lineYAxis.Y2;
            lineYAxis.HorizontalAlignment = HorizontalAlignment.Left;
            lineYAxis.VerticalAlignment = VerticalAlignment.Center;
            lineYAxis.StrokeThickness = 2;
            canvas.Children.Add(lineYAxis);

            //x axis
            var lineXAxis = new Line();
            lineXAxis.Stroke = Brushes.LightBlue;
            lineXAxis.X1 = marginLeft;
            lineXAxis.X2 = canvas.Width - marginLeft;
            lineXAxis.Y1 = canvas.Height - marginBottom;
            lineXAxis.Y2 = canvas.Height - marginBottom;
            canvasWidth = lineXAxis.X2;
            lineXAxis.HorizontalAlignment = HorizontalAlignment.Left;
            lineXAxis.VerticalAlignment = VerticalAlignment.Center;
            lineXAxis.StrokeThickness = 2;
            canvas.Children.Add(lineXAxis);

            //x divisions
            double divisionXLength = (lineXAxis.X2 - lineXAxis.X1) / dDrawing.XDivisions;
            for(int i = 0; i <= dDrawing.XDivisions; i++)
            {
                var lineDiv = new Line();
                lineDiv.Stroke = Brushes.LightBlue;
                lineDiv.StrokeThickness = 2;
                lineDiv.X1 = marginLeft + divisionXLength * i;
                lineDiv.X2 = marginLeft + divisionXLength * i;
                lineDiv.Y1 = lineXAxis.Y2 - 10;
                lineDiv.Y2 = lineXAxis.Y2 + 10;
                canvas.Children.Add(lineDiv);

                var lineText = new TextBlock();
                lineText.Text = (dDrawing.XInterval * i).ToString();
                lineText.Foreground = new SolidColorBrush(Brushes.LightBlue.Color);
                lineText.HorizontalAlignment = HorizontalAlignment.Center;
                canvas.Children.Add(lineText);
                Canvas.SetLeft(lineText, lineDiv.X2 - 5);
                Canvas.SetTop(lineText, lineDiv.Y1 + 20);
            }

            //y divisions
            double divisionYLength = (lineYAxis.Y2 - lineYAxis.Y1) / dDrawing.YDivisions;
            for (int i = 0; i <= dDrawing.YDivisions; i++)
            {
                var lineDiv = new Line();
                lineDiv.StrokeThickness = 2;
                lineDiv.Stroke = Brushes.LightBlue;
                lineDiv.X1 = lineXAxis.X1 - 10;
                lineDiv.X2 = lineXAxis.X1 + 10;
                lineDiv.Y1 = lineYAxis.Y2 - divisionYLength * i;
                lineDiv.Y2 = lineYAxis.Y2 - divisionYLength * i;
                canvas.Children.Add(lineDiv);

                var lineText = new TextBlock();
                lineText.Text = (dDrawing.YInterval * i).ToString();
                lineText.Foreground = new SolidColorBrush(Brushes.LightBlue.Color);
                lineText.TextAlignment = TextAlignment.Right;
                Canvas.SetLeft(lineText, lineDiv.X1 - 25);
                Canvas.SetTop(lineText, lineDiv.Y2 - 10);
                canvas.Children.Add(lineText);
            }
        }
        /// <summary>
        /// Disable diagram creation
        /// </summary>
        private void DisableSettings()
        {
            txtDivX.IsEnabled = false;
            txtDivY.IsEnabled = false;
            txtIntervalX.IsEnabled = false;
            txtIntervalY.IsEnabled = false;
            btnOKDiagram.IsEnabled = false;
            btnClearDiagram.IsEnabled = true;
        }
        /// <summary>
        /// Enable diagram creation
        /// </summary>
        private void EnableSettings()
        {
            txtDivX.IsEnabled = true;
            txtDivY.IsEnabled = true;
            txtIntervalX.IsEnabled = true;
            txtIntervalY.IsEnabled = true;
            btnOKDiagram.IsEnabled = true;
            btnClearDiagram.IsEnabled = false;
        }
        /// <summary>
        /// Check if the string is a number, and if it is non-negative
        /// </summary>
        /// <param name="value"></param>
        /// <param name="allowZero">Check if zero can be passed as a value</param>
        /// <returns></returns>
        private bool ValidateInt(string value, bool allowZero)
        {
            float val;
            if(float.TryParse(value, out val))
            {
                if(val <= 0)
                {
                    if (allowZero)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Draw coordinates to the diagram
        /// </summary>
        private void DrawCoordinates()
        {
            if (!initialized)
            {
                MessageBox.Show("Create a diagram first!");
                return;
            }
            ClearDiagram();
            DrawDiagram();
            

            if (dDrawing.Coordinates.Count != 0)
            {
                var coordinates = dDrawing.Coordinates.OrderBy(a => a.X).ToList();

                for (int i = 0; i < dDrawing.Coordinates.Count; i++)
                {
                    double differenceX = coordinates[i].X / (dDrawing.XDivisions * dDrawing.XInterval);
                    double canvasX = differenceX * (canvasWidth - marginLeft) + marginLeft;

                    double differenceY = coordinates[i].Y / (dDrawing.YDivisions * dDrawing.YInterval);
                    double canvasY = differenceY * (canvasHeight - marginBottom) + marginBottom;

                    var dot = new Ellipse();
                    dot.Height = 4;
                    dot.Width = 4;

                    Canvas.SetLeft(dot, canvasX - 2);
                    Canvas.SetBottom(dot, canvasY - 2);

                    dot.StrokeThickness = 2;
                    dot.Stroke = Brushes.Black;
                    canvas.Children.Add(dot);

                    if(i != dDrawing.Coordinates.Count-1)
                    {
                        double x2 = (coordinates[i + 1].X / (dDrawing.XDivisions * dDrawing.XInterval)) * (canvasWidth - marginLeft) + marginLeft;
                        double y2 = (coordinates[i + 1].Y / (dDrawing.YDivisions * dDrawing.YInterval)) * (canvasHeight - marginBottom) + marginBottom;

                        var line = new Line();
                        line.X1 = canvasX;
                        line.X2 = x2;
                        line.Y1 = canvas.Height - canvasY;
                        line.Y2 = canvas.Height - y2;
                        line.Name = "line";
                        line.StrokeThickness = 2;
                        line.Stroke = Brushes.Black;
                        canvas.Children.Add(line);

                    }
                }
            }
            else
            {
                MessageBox.Show("cannot draw");
            }
            
        }
        /// <summary>
        /// Add a coordinate to the diagram's list of coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            if (!initialized)
            {
                MessageBox.Show("Please create a diagram first!");
                return;
            }
            else if(ValidateInt(txtCoordX.Text, true) && ValidateInt(txtCoordY.Text, true))
            {
                dDrawing.AddCoordinate(double.Parse(txtCoordX.Text), double.Parse(txtCoordY.Text));
                DrawCoordinates();
                UpdateCoordinateListBox();
            }
        }
        /// <summary>
        /// Add a XY coordinate where the mouse cursor is on the diagram
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!initialized)
            {
                MessageBox.Show("Please create a diagram first!");
                return;
            }
            Point mouseP = Mouse.GetPosition(canvas);
            if (mouseP.Y > marginBottom && mouseP.Y < canvas.Height - marginBottom &&
                mouseP.X > marginLeft && mouseP.X < canvas.Width - marginLeft)
            {
                mouseP.Y = canvas.Height - mouseP.Y;
                double coordX = ((mouseP.X - marginLeft) / (canvas.Width - marginLeft - marginLeft)) * dDrawing.XDivisions * dDrawing.XInterval;
                coordX = Math.Round(coordX * 100) / 100;
                double coordY = ((mouseP.Y - marginBottom) / (canvas.Height - marginLeft - marginLeft)) * dDrawing.YDivisions * dDrawing.YInterval;
                coordY = Math.Round(coordY * 100) / 100;
                string message = "Would you like to add a point at (" + coordX + ", " + coordY + ") ?";
                MessageBoxResult result = MessageBox.Show(message, "Add point", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    dDrawing.AddCoordinate(coordX, coordY);
                    DrawCoordinates();
                    UpdateCoordinateListBox();
                }
                //TODO: clear coordinates on clearing diagram, showing coords in listbox, refine shit
            }
        }
        /// <summary>
        /// Reset canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDiagram_Click(object sender, RoutedEventArgs e)
        {
            initialized = false;
            ClearDiagram();
            dDrawing.ClearCoordinates();
            UpdateCoordinateListBox();
            EnableSettings();
        }
        /// <summary>
        /// Draw a text where the mouse cursor is on the diagram and display the diagram's
        /// x and y value at mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!initialized)
            {
                return;
            }
            ClearDiagram();
            DrawDiagram();
            //it throws an error message trying to draw zero coordinates, so skip
            if(dDrawing.Coordinates.Count == 0)
            {
                return;
            }
            DrawCoordinates();
            Point mouseP = Mouse.GetPosition(canvas);
            if (mouseP.Y > marginBottom && mouseP.Y < canvas.Height - marginBottom &&
                mouseP.X > marginLeft && mouseP.X < canvas.Width - marginLeft)
            {
                mouseP.Y = canvas.Height - mouseP.Y;
                double coordX = ((mouseP.X - marginLeft) / (canvas.Width - marginLeft - marginLeft)) * dDrawing.XDivisions * dDrawing.XInterval;
                double coordY = ((mouseP.Y - marginBottom) / (canvas.Height - marginLeft - marginLeft)) * dDrawing.YDivisions * dDrawing.YInterval;

                Point find = new Point(mouseP.X, mouseP.Y);
                var xyString = new TextBlock();
                xyString.Text = "X: " + coordX + " Y: " + coordY;
                xyString.Foreground = Brushes.Black;
                Canvas.SetLeft(xyString, find.X + 10);
                Canvas.SetBottom(xyString, find.Y + 10);
                canvas.Children.Add(xyString);
                Debug.WriteLine(coordY);
            }
        }
        /// <summary>
        /// Display coordinates that exist in the diagram
        /// </summary>
        private void UpdateCoordinateListBox()
        {
            lstCoordinates.Items.Clear();
            for(int i = 0; i < dDrawing.Coordinates.Count; i++)
            {
                lstCoordinates.Items.Add(dDrawing.Coordinates[i].ToString());
            }
        }
    }
}
