using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Question2
{
   public class ViewModel3:INotifyPropertyChanged
    {
        private bool canexecute = true;
        private string size;
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }
        private string url;
        public string URL
        {
            get
            {
                return this.url;
            }
            set
            {
                url = value;
                OnPropertyChanged("URL");
            }
        }
        
        public DelegateCommand MyDelegate { get; set; }

        public ViewModel3()
        {    
            
            MyDelegate = new DelegateCommand(ExecuteCommand,CanExecute);

            Size = "Please wait...";

            Task.Run(() =>
            {
                while (true)
                {
                    MyDelegate.RaiseCanExecuteChanged();
                    Thread.Sleep(500);
                }

            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public bool CanExecute()
        {
            return canexecute;
        }

        private async void ExecuteCommand()
        {
            canexecute = false;
            string text;
            await Task.Run(() =>
            {              
                WebRequest webRequest = WebRequest.Create(URL);
                WebResponse response = webRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                Size = text.Length.ToString();
            });
            canexecute = true;
        }
    }
}
