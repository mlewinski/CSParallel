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

namespace UISynchronization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            TaskScheduler interfaceScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            lbDivisors.Items.Clear();
            int n=0;
            try
            {
                n = Int32.Parse(txtNumber.Text);
                if (n < 1) throw new OverflowException();
            }
            catch
            {
                MessageBox.Show("Input correct number!", "Wrong number", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            btnFind.IsEnabled = false;
            pbProgress.Value = 0;

            Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= n + 1; i++)
                {
                    //Thread.Sleep(10);
                    if (n%i == 0)
                    {
                        Task.Factory.StartNew((i2) =>
                        {
                            lbDivisors.Items.Add(i2);
                        }, i, CancellationToken.None, TaskCreationOptions.None, interfaceScheduler);
                        Task.Factory.StartNew((i2) =>
                        {
                            pbProgress.Value = ((int)i2 *100)/(n);
                        }, i, CancellationToken.None, TaskCreationOptions.None, interfaceScheduler);
                    }
                }
            }).ContinueWith(_ => { btnFind.IsEnabled = true; }, interfaceScheduler);
        }
    }
}
