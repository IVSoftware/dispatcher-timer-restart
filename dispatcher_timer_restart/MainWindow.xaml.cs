using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace dispatcher_timer_restart
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            // Create the dispatch timer ONCE
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);

            // This will restart the timer every
            // time the window is activated
            this.Activated += (sender, e) =>
            {
                startOrRestartDispatchTimer();
            };
        }

        private void startOrRestartDispatchTimer()
        {
            dispatcherTimer.Stop(); // If already running
            blockTime = TimeSpan.FromSeconds(15);
            Countdown_TexBlock.Text = blockTime.ToString();
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (blockTime > TimeSpan.Zero)
            {
                blockTime = blockTime.Subtract(TimeSpan.FromSeconds(1));
                Countdown_TexBlock.Text = blockTime.ToString();
                if (blockTime == TimeSpan.Zero)
                {
                    Countdown_TexBlock.Text = "Done";
                    dispatcherTimer.Stop();
                }
            }
        }

        TimeSpan blockTime = TimeSpan.FromSeconds(15);

        private DispatcherTimer dispatcherTimer;

        private void buttonRestart_Click(object sender, RoutedEventArgs e) =>
            startOrRestartDispatchTimer();
    }
}
