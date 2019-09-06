using Question2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Question3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel3 view = new ViewModel3(); 

        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = view;
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            string text;

            MyButton.IsEnabled = false;

            string Url = MyTextBox.Text;

            Task.Run(() =>
            {
                WebRequest webRequest = WebRequest.Create(Url);
                WebResponse response = webRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }

                string size = text.Length.ToString();

                Action uiwork = () =>
                {
                    MyTextBlock.Text = size;
                    MyButton.IsEnabled = true;
                };
                    SafeInvoke(uiwork);
                });
           
            }
   
        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }
    }
}
