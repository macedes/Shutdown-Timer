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
                
                Double selectedComboBoxValue = Convert.ToDouble(ComboBoxMinutenPicker.SelectedValue);

                Double IntervallInSeconds = TimeSpan.FromMinutes(selectedComboBoxValue).TotalMilliseconds;
                ShutdownTimer.Elapsed += new ElapsedEventHandler(RunEvent);
                ShutdownTimer.Interval = IntervallInSeconds;
                ShutdownTimer.Enabled = true;

                DateTime endDate = DateTime.Now;
                endDate = endDate.AddMinutes(selectedComboBoxValue);

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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
