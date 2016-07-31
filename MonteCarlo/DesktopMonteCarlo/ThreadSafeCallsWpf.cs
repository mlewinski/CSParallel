using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesktopMonteCarlo
{
    class ThreadSafeCallsWpf
    {
        private delegate void DrawPointDelegate(Canvas canvas, double x, double y, bool pointInCircle);

        public static void DrawPoint(Canvas canvas, double x, double y, bool pointInCircle)
        {
            if (!canvas.Dispatcher.CheckAccess())
            {
                canvas.Dispatcher.Invoke(new DrawPointDelegate(DrawPoint), new object[] {canvas, x, y, pointInCircle});
            }
            else
            {
                Ellipse ellipse = new Ellipse();
                const int pointDiameter = 4;
                ellipse.Width = ellipse.Height = pointDiameter;
                ellipse.Fill = pointInCircle ? Brushes.LightGreen : Brushes.Red;
                canvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, x - pointDiameter / 2.0);
                Canvas.SetTop(ellipse, y - pointDiameter / 2.0);
            }
        }

        private delegate void AddElementToListDelegate(ListBox listBox, object item);

        public static void AddElementToList(ListBox listBox, object item)
        {
            if (!listBox.Dispatcher.CheckAccess())
                listBox.Dispatcher.Invoke(new AddElementToListDelegate(AddElementToList), new object[] {listBox, item});
            else
            {
                listBox.Items.Add(item);
                listBox.SelectedItem = item;
                listBox.ScrollIntoView(item);
            }
        }
    }
}
