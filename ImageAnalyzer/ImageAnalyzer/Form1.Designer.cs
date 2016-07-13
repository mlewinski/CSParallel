namespace ImageAnalyzer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonPrepare = new System.Windows.Forms.Button();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.chartR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonStopAnalysis = new System.Windows.Forms.Button();
            this.chartG = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartB = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartB)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(12, 41);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(580, 639);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 0;
            this.pictureBoxPreview.TabStop = false;
            // 
            // buttonPrepare
            // 
            this.buttonPrepare.Location = new System.Drawing.Point(12, 12);
            this.buttonPrepare.Name = "buttonPrepare";
            this.buttonPrepare.Size = new System.Drawing.Size(97, 23);
            this.buttonPrepare.TabIndex = 1;
            this.buttonPrepare.Text = "Prepare image";
            this.buttonPrepare.UseVisualStyleBackColor = true;
            this.buttonPrepare.Click += new System.EventHandler(this.buttonPrepare_Click);
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(116, 13);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(97, 23);
            this.buttonAnalyze.TabIndex = 2;
            this.buttonAnalyze.Text = "Analyze";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // chartR
            // 
            chartArea1.Name = "ChartArea1";
            this.chartR.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartR.Legends.Add(legend1);
            this.chartR.Location = new System.Drawing.Point(598, 41);
            this.chartR.Name = "chartR";
            this.chartR.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red};
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartR.Series.Add(series1);
            this.chartR.Size = new System.Drawing.Size(519, 209);
            this.chartR.TabIndex = 3;
            this.chartR.Text = "chartR";
            // 
            // buttonStopAnalysis
            // 
            this.buttonStopAnalysis.Location = new System.Drawing.Point(220, 12);
            this.buttonStopAnalysis.Name = "buttonStopAnalysis";
            this.buttonStopAnalysis.Size = new System.Drawing.Size(97, 23);
            this.buttonStopAnalysis.TabIndex = 4;
            this.buttonStopAnalysis.Text = "Stop analisys";
            this.buttonStopAnalysis.UseVisualStyleBackColor = true;
            this.buttonStopAnalysis.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartG
            // 
            chartArea2.Name = "ChartArea1";
            this.chartG.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartG.Legends.Add(legend2);
            this.chartG.Location = new System.Drawing.Point(598, 256);
            this.chartG.Name = "chartG";
            this.chartG.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Green};
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartG.Series.Add(series2);
            this.chartG.Size = new System.Drawing.Size(519, 209);
            this.chartG.TabIndex = 5;
            this.chartG.Text = "chartG";
            // 
            // chartB
            // 
            chartArea3.Name = "ChartArea1";
            this.chartB.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartB.Legends.Add(legend3);
            this.chartB.Location = new System.Drawing.Point(598, 471);
            this.chartB.Name = "chartB";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartB.Series.Add(series3);
            this.chartB.Size = new System.Drawing.Size(519, 209);
            this.chartB.TabIndex = 6;
            this.chartB.Text = "chartB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 692);
            this.Controls.Add(this.chartB);
            this.Controls.Add(this.chartG);
            this.Controls.Add(this.buttonStopAnalysis);
            this.Controls.Add(this.chartR);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.buttonPrepare);
            this.Controls.Add(this.pictureBoxPreview);
            this.Name = "Form1";
            this.Text = "Image Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonPrepare;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartR;
        private System.Windows.Forms.Button buttonStopAnalysis;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartG;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartB;
    }
}

