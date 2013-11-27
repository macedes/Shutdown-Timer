using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

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
            TimeLabel.Content = "Bitte starten Sie den Timer";
        }

        Boolean timerstatus = false;
        int selectedValue, currentValue, maxValue;
        DateTime startTime;

        /// <summary>
        /// Erstellt einen Delegate für den ProgressBar
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);

        /// <summary>
        /// DispatchTimer wird erzeugt
        /// </summary>
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();



        /// <summary>
        /// TimerButton_Click wird nach dem Klicken aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerButton_Click(object sender, RoutedEventArgs e)
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

                TimeLabel.Content = "00:00";
    
                //  DispatcherTimer Setup
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();

                startTime = DateTime.Now;

                TimerButton.Content = "Timer stoppen";
                timerstatus = true;
            }
            else
            {
                TimeLabel.Content = "Bitte starten Sie den Timer";
                TimerButton.Content = "Timer starten";
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;
                Progress(0, selectedValue, 0);
                timerstatus = false;
                

            }
        }

        /// <summary>
        /// Diese DispatchTimer Methode wird in jeder Sekunde aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            maxValue = Convert.ToInt16(TimeSpan.FromMinutes(selectedValue).TotalSeconds);
            TimeSpan elapsedTime = (DateTime.Now - startTime);
            currentValue =(int)elapsedTime.TotalSeconds;
            String currentTimerString = Convert.ToString(elapsedTime.ToString("mm':'ss"));
            TimeLabel.Content = currentTimerString;
            if (currentValue > maxValue)
            {
                dispatcherTimer.Stop();
                TimeLabel.Content = "";
                TimerButton.Content = "Timer starten";
                timerstatus = false;
                Progress(0, maxValue, 0);
                Shutdown();

            }
            else
            {
                Progress(0, maxValue, currentValue);
            }
        }

        /// <summary>
        /// Beendet alle Prozesse und fährt den Rechner herunter
        /// </summary>
        private void Shutdown()
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

        }

        /// <summary>
        /// ProgressBar wird aktualisiert
        /// </summary>
        /// <param name="min">Anfangswert</param>
        /// <param name="selectedValue">Maximalwert</param>
        /// <param name="current">aktueller Wert</param>
        private void Progress(int min, int selectedValue, int current)
        {
            //Konfiguriert die ProgressBar
            ProgressBar.Minimum = min;
            ProgressBar.Maximum = selectedValue;
            ProgressBar.Value = current;

            //Speichert den Wert der ProgressBar
            double value = current;


            //Erstellt eine neue Instanz auf die ProgressBar Delegate, das auf die SetValue Methode zeigt.
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(ProgressBar.SetValue);
            Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });
        }
    }
}
