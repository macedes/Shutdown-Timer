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
        int selectedValue, currentValue, maxValue;
        DateTime startTime;

        //Create a Delegate that matches the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (timerstatus == false)
            {

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
                selectedValue = 1;

                Double IntervallInSeconds = TimeSpan.FromMinutes(selectedValue).TotalMilliseconds;
                ShutdownTimer.Elapsed += new ElapsedEventHandler(RunEvent);
                ShutdownTimer.Interval = IntervallInSeconds;
                ShutdownTimer.Enabled = true;


                //  DispatcherTimer setup
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();

                startTime = DateTime.Now;
                DateTime endDate = DateTime.Now;
                endDate = endDate.AddMinutes(selectedValue);

                TimerButton.Content = "Timer stoppen";
                timerstatus = true;
            }
            else
            {
                TimeLabel.Content = "";
                ShutdownTimer.Enabled = false;
                TimerButton.Content = "Timer starten";
                timerstatus = false;

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            maxValue = Convert.ToInt16(TimeSpan.FromMinutes(selectedValue).TotalSeconds) - 1;
            TimeSpan elapsedTime = (DateTime.Now - startTime);
            currentValue = elapsedTime.Seconds;
            String currentTimerString = Convert.ToString(elapsedTime.ToString("mm':'ss"));
            TimeLabel.Content = currentTimerString;
            if (currentValue == maxValue)
            {
                dispatcherTimer.Stop();
                TimeLabel.Content = "";
                ShutdownTimer.Enabled = false;
                TimerButton.Content = "Timer starten";
                timerstatus = false;

            }
            else
            {
                Progress(0, maxValue, currentValue);
            }
        }

        public void RunEvent(object source, ElapsedEventArgs e)
        {
            /*
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
            
             */
            MessageBox.Show("PC fährt herunter");
            ShutdownTimer.Enabled = false;
        }



        private void Progress(int min, int selectedValue, int current)
        {
            //Configure the ProgressBar
            ProgressBar1.Minimum = min;
            ProgressBar1.Maximum = selectedValue;
            ProgressBar1.Value = current;

            //Stores the value of the ProgressBar
            double value = current;

            //Create a new instance of our ProgressBar Delegate that points
            //  to the ProgressBar's SetValue method.
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(ProgressBar1.SetValue);
            Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });
        }
    }
}
