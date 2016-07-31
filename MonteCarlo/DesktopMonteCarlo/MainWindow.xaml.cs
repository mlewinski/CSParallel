using System;
using System.Collections.Generic;
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
using System.Threading;

namespace DesktopMonteCarlo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _circleDiameter;
        private double _circleRadius;

        private bool _threadActive = false;
        private long _samples = 0;
        private int _delay = 0;

        public MainWindow()
        {
            InitializeComponent();
            _circleDiameter = CanvasPreview.Width;
            _circleRadius = _circleDiameter / 2.0;
        }

        private void DrawCircle()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = _circleDiameter;
            ellipse.Height = _circleDiameter;
            ellipse.Stroke = Brushes.Blue;
            ellipse.StrokeThickness = 1;
            CanvasPreview.Children.Add(ellipse);
        }

        private void DrawSquare()
        {
            Rectangle rect = new Rectangle();
            rect.Width = _circleDiameter;
            rect.Height = _circleDiameter;
            rect.Stroke = Brushes.Black;
            rect.StrokeThickness = 1;
            CanvasPreview.Children.Add(rect);
        }

        private void DrawCoordinateGrid()
        {
            Line horizontalLine = new Line();
            horizontalLine.X1 = 0.0;
            horizontalLine.X2 = _circleDiameter;
            horizontalLine.Y1 = horizontalLine.Y2 = _circleRadius;

            Line verticalLine = new Line();
            verticalLine.X1 = verticalLine.X2 = _circleRadius;
            verticalLine.Y1 = 0.0;
            verticalLine.Y2 = _circleDiameter;
            verticalLine.Stroke = horizontalLine.Stroke = Brushes.Red;
            verticalLine.StrokeThickness = horizontalLine.StrokeThickness = 1;
            DoubleCollection dashes = new DoubleCollection();
            dashes.Add(10);
            verticalLine.StrokeDashArray = horizontalLine.StrokeDashArray = dashes;
            CanvasPreview.Children.Add(verticalLine);
            CanvasPreview.Children.Add(horizontalLine);
        }

        private void PreparePreview()
        {
            CanvasPreview.Children.Clear();
            DrawCircle();
            DrawSquare();
            DrawCoordinateGrid();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!_threadActive)
            {
                PreparePreview();
                _threadActive = true;
                Thread thread = new Thread(CalculatePI);
                try
                {
                    _samples = Convert.ToInt64(TextboxIterations.Text);
                    _delay = Convert.ToInt32(TextboxDelay.Text);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CalculatePI()
        {
            int delay = _delay;
            long samples = _samples;
            long hitCount = 0;
            Random r = new Random();
            for (long i = 1; i <= samples && _threadActive; i++)
            {
                double xComp = _circleDiameter * r.NextDouble();
                double yComp = _circleDiameter * r.NextDouble();
                double xC = xComp - _circleRadius;
                double yC = yComp - _circleRadius;
                double distance = Math.Sqrt(xC * xC + yC * yC);
                bool pointInCircle = false;
                if (distance < _circleRadius)
                {
                    hitCount++;
                    pointInCircle = true;
                }
                ThreadSafeCallsWpf.DrawPoint(CanvasPreview, xComp, yComp, pointInCircle);

                double result = 4.0 * hitCount / i;

                string statistics = String.Format("Sample {0} Hit Count {1} PI={2} Error={3}", i, hitCount, result,
                    Math.Abs(Math.PI - result));
                ThreadSafeCallsWpf.AddElementToList(ListboxMessages, statistics);
                Thread.Sleep(delay);
            }
            _threadActive = false;
        }

        private void ButtonAbort_Click(object sender, RoutedEventArgs e)
        {
            _threadActive = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ButtonAbort_Click(sender, null);
        }
    }
}
