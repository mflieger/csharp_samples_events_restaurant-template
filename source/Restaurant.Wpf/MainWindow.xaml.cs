using Restaurant.Core;
using System;
using System.Text;

namespace Restaurant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        DateTime startTime = DateTime.Parse("12:00:00");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            FastClock.Instance.Time = startTime;
            FastClock.Instance.OneMinuteIsOver += Instance_OnOneMinuteIsOver;
            FastClock.Instance.IsRunning = true;
            Waiter waiter = new Waiter(startTime);
            waiter.TaskFinished += OnTaskIsOver; 
        }

        private void OnTaskIsOver(object sender, string message)
        {
            StringBuilder text = new StringBuilder(TextBlockLog.Text);
            text.Append(FastClock.Instance.Time.ToShortTimeString() + "\t");
            text.Append(message + "\n");
            TextBlockLog.Text = text.ToString();
        }
        private void Instance_OnOneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"Restaurantsimulator, Uhrzeit: {e.ToShortTimeString()}";
        }

    }
}
