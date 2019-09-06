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

namespace Question3WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ViewModel vm;
        private bool button1Click = false;
        private bool button2Click = false;
        private bool button3Click = false;
        private bool button4Click = false;

        public int TimerNum;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //vm = new ViewModel();
            //this.DataContext = vm;
            TimerNum = 30;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
        }
        public void timerTick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Action uiwork = () =>
                {
                    if (TimerNum > 0)
                    {
                        TimerNum--;
                        Counter.Text = ($"{TimerNum}");
                    }
                    if (TimerNum <= 0 || button1Click == true || button3Click == true || button4Click == true)
                    {
                        timer.Stop();
                        option1.IsEnabled = false;
                        option2.IsEnabled = false;
                        option3.IsEnabled = false;
                        option4.IsEnabled = false;
                        BrushConverter bc = new BrushConverter();
                        Brush brush = (Brush)bc.ConvertFrom("#ff0051");
                        this.Background = brush;
                    }

                    if (button2Click == true)
                    {
                        timer.Stop();
                        option1.IsEnabled = false;
                        option2.IsEnabled = false;
                        option3.IsEnabled = false;
                        option4.IsEnabled = false;

                        BrushConverter bc = new BrushConverter();
                        Brush brush = (Brush)bc.ConvertFrom("#0dff00");
                        this.Background = brush;
                    }
                };

                Dispatcher.BeginInvoke(uiwork);
                Thread.Sleep(20);
            }
            );

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            button1Click = true;  
        }

        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            button2Click = true;
        }

        private void Option3_Click(object sender, RoutedEventArgs e)
        {
            button3Click = true;
        }

        private void Option4_Click(object sender, RoutedEventArgs e)
        {
            button4Click = true;
        }
    }
}

