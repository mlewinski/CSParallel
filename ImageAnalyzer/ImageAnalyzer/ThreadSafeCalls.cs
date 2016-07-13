using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageAnalyzer
{
    static class ThreadSafeCalls
    {
        private delegate void AddPointsToChartDelegate(Chart chart, int seriesIndex, double[] yValues);

        public static void AddPointsToChart(Chart chart, int seriesIndex, double[] yValues)
        {
            if (chart.InvokeRequired)
            {
                chart.Invoke(new AddPointsToChartDelegate(AddPointsToChart), new object[] {chart, seriesIndex, yValues});
            }
            else
            {
                if (seriesIndex < chart.Series.Count)
                {
                    chart.Series[seriesIndex].Points.Clear();
                    for (int i = 0; i < yValues.Length; i++)
                    {
                        chart.Series[seriesIndex].Points.AddXY(i, yValues[i]);
                    }
                }
            }
        }

        private delegate void SetImageDelegate(PictureBox pictureBox, Bitmap image);

        public static void SetImage(PictureBox pictureBox, Bitmap image)
        {
            if (pictureBox.InvokeRequired)
                pictureBox.Invoke(new SetImageDelegate(SetImage), new object[] {pictureBox, image});
            else
            {
                if (pictureBox != null && image != null) pictureBox.Image = image;
            }
        }
    }
}
