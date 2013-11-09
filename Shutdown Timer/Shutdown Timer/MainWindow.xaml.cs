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
using System.Diagnostics;
using System.Timers;

namespace Shutdown_Timer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Boolean timerstatus = false;
        Timer ShutdownTimer = new Timer();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (timerstatus == false)
            {

                Double selectedValue=0;

                if (radio30.IsChecked == true)
                {
                    selectedValue = 30;
                }
                else if (radio60.IsChecked == true)
                {
                    selectedValue = 60;
                }
                else if (radio90.IsChecked == true)
                {
                    selectedValue = 90;
                }
                else
                {
                    selectedValue = 0;
                    MessageBox.Show("Fehler");
                }
                
                Double IntervallInSeconds = TimeSpan.FromMinutes(selectedValue).TotalMilliseconds;
                ShutdownTimer.Elapsed += new ElapsedEventHandler(RunEvent);
                ShutdownTimer.Interval = IntervallInSeconds;
                ShutdownTimer.Enabled = true;

                // Progress();

                DateTime endDate = DateTime.Now;
                endDate = endDate.AddMinutes(selectedValue);

                TimeLabel.Content = endDate.ToString();
                TimerButton.Content = "Timer stoppen";
                timerstatus = true;
            }
            else
            {
                TimeLabel.Content = "";
                ShutdownTimer.Enabled = false;
                TimerButton.Content = "Timer starten";
                timerstatus = false ;

            }
         }

        public void RunEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("dvbviewer"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           Process.Start("shutdown", "/s /t 0");
            ShutdownTimer.Enabled = false;
        }

        //Create a Delegate that matches the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);
        
        private void Progress()
        {
            //Configure the ProgressBar
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = short.MaxValue;
            ProgressBar1.Value = 0;

            //Stores the value of the ProgressBar
            double value = 0;

            //Create a new instance of our ProgressBar Delegate that points
            //  to the ProgressBar's SetValue method.
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(ProgressBar1.SetValue);

            //Tight Loop:  Loop until the ProgressBar.Value reaches the max
            do
            {
                value += 1;

                /*Update the Value of the ProgressBar:
                  1)  Pass the "updatePbDelegate" delegate that points to the ProgressBar1.SetValue method
                  2)  Set the DispatcherPriority to "Background"
                  3)  Pass an Object() Array containing the property to update (ProgressBar.ValueProperty) and the new value */
                Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });

            }
            while (ProgressBar1.Value != ProgressBar1.Maximum);

        }
    }
}
