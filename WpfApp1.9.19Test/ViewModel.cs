using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1._9._19Test
{
    public class ViewModel : INotifyPropertyChanged
    {
        private int heightbtn;
        public int HeightBTN
        {
            get
            {
                return this.heightbtn;
            }
            set
            {
                heightbtn = value;
                OnPropertyChanged("HeightBTN");
            }
        }

        private int widthbtn;
        public int WidthBTN
        {
            get
            {
                return this.widthbtn;
            }
            set
            {
                widthbtn = value;
                OnPropertyChanged("WidthBTN");
            }
        }

        private string textbtn;
        public string TextBTN
        {
            get
            {
                return this.textbtn;
            }
            set
            {
                textbtn = value;
                OnPropertyChanged("TextBTN");
            }
        }

        public DelegateCommand MyDelegate { get; set; }

        public ViewModel()
        {
            MyDelegate = new DelegateCommand(ExecuteCommand);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void ExecuteCommand()
        {
            MonitorWindow mw = new MonitorWindow(this);
            mw.Show();
        }
    }
   
}
