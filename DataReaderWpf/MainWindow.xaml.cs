using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace DataReaderWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private volatile bool _threadActive = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThreadFunc(object param)
        {
            const int dataCount = 1000;
            const int msDelayTime = 5;

            Random random = new Random();
            DispatcherSynchronizationContext context = param as DispatcherSynchronizationContext;

            for (int i = 1; i <= dataCount && _threadActive; i++)
            {
                context.Send(UpdateUI, new SynchronizationParameter(100 * i / dataCount, random.Next(255)));
                Thread.Sleep(msDelayTime);
            }
        }

        private void UpdateUI(object param)
        {
            SynchronizationParameter p = param as SynchronizationParameter;
            ListBoxData.Items.Add(p.Value);
            ListBoxData.SelectedIndex = ListBoxData.Items.Count - 1;
            Progress.Value = p.Progress;
            if (p.Progress == 100) ConfigureButtons(false);
        }

        private void ConfigureButtons(bool threadActive)
        {
            ButtonStartReading.IsEnabled = !threadActive;
            ButtonStopReading.IsEnabled = threadActive;
        }

        private void ButtonStartReading_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(ThreadFunc);
            _threadActive = true;
            thread.Start(SynchronizationContext.Current);
            ConfigureButtons(true);
        }

        private void ButtonStopReading_Click(object sender, RoutedEventArgs e)
        {
            _threadActive = false;
            ConfigureButtons(false);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ListBoxData.Items.Clear();
            Progress.Value = 0;
        }
    }
}
