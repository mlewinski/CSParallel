using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageAnalyzer
{
    public partial class Form1 : Form
    {
        private const int _width = 800;
        private const int _height = 600;
        private volatile bool _analysisActive = false;

        public Form1()
        {
            InitializeComponent();
            buttonAnalyze.Enabled = false;
            buttonStopAnalysis.Enabled = false;
            ConfigureChart();

            Form1.CheckForIllegalCrossThreadCalls = false;
        }

        private void ConfigureChart()
        {
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = _width;
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart.Legends.Clear();
        }

        private void buttonPrepare_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(_width, _height);
            int w = _width;
            int h = _height;
            Random r = new Random();
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    double cos = r.NextDouble()*Math.Cos((i + j)*Math.PI/r.NextDouble());
                    byte val = Convert.ToByte(255.0 * r.NextDouble()*Math.Abs(Math.Sin(Math.Tan(i)*cos)));
                    img.SetPixel(i,j,Color.FromArgb((int)(r.NextDouble() * val), (int)(r.NextDouble()*val), (int)(r.NextDouble() * val)));
                }
            }
            pictureBoxPreview.Image = img;
            buttonAnalyze.Enabled = true;
        }

        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(AnalyzeImage);
            _analysisActive = true;
            thread.Start();
            buttonStopAnalysis.Enabled = true;
        }

        private void AnalyzeImage()
        {
            Bitmap orgBitmap = new Bitmap(pictureBoxPreview.Image);
            const int msDelay = 50;
            double[] lineData = new double[orgBitmap.Width];
            for (int i = 0; i < pictureBoxPreview.Image.Height && _analysisActive; i++)
            {
                Bitmap tmpBitmap = new Bitmap(orgBitmap);
                for (int j = 0; j < pictureBoxPreview.Image.Width; j++)
                {
                    lineData[j] = tmpBitmap.GetPixel(j, i).R;
                }
                ThreadSafeCalls.AddPointsToChart(chart, 0, lineData);

                Graphics g = Graphics.FromImage(tmpBitmap);
                g.DrawLine(Pens.Red,0,i,pictureBoxPreview.Image.Width,i);
               ThreadSafeCalls.SetImage(pictureBoxPreview, tmpBitmap);

                Thread.Sleep(msDelay);
            }
        }

        private void AddPointsToChart(Chart chart, int seriesIndex, double[] yValues)
        {
            if (seriesIndex < chart.Series.Count && seriesIndex >= 0 && chart != null)
            {
                chart.Series[seriesIndex].Points.Clear();
                for (int i = 0; i < yValues.Length; i++)
                {
                    chart.Series[seriesIndex].Points.AddXY(i, yValues[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _analysisActive = false;
            buttonStopAnalysis.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _analysisActive = false;
        }
    }
}
