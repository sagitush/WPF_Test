using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Question3WPF
{
    class ViewModel:INotifyPropertyChanged
    {
        private Brush color;
        public Brush Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                OnPropertyChanged("Color");
            }
        }
        private string result;
        public string Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
                OnPropertyChanged("Result");
            }
        }
        
        public DelegateCommand MyCommandOptionWrong { get; set; }

        public DelegateCommand MyCommandOptionCorrect { get; set; }

        private Task timerRunTask;
        public bool CorrectAnswer;
        public bool WrongAnswer;
        private CancellationTokenSource tokenSource;
        public event PropertyChangedEventHandler PropertyChanged;

        
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
       
        public ViewModel()
        {
            CorrectAnswer = true;
            WrongAnswer = true;


            Result = "30";
            Color = new SolidColorBrush(Colors.WhiteSmoke);
            

            tokenSource = new CancellationTokenSource();
            RunTimerBackwords();

            MyCommandOptionWrong = new DelegateCommand(ExecuteMethod_Wrong, CanExecute_Wrong);
            MyCommandOptionCorrect = new DelegateCommand(ExecuteMethod_Correct, CanExecute_Correct);

        }

        private void RunTimerBackwords()
        {
            timerRunTask = Task.Run(() =>
            {
                if (!tokenSource.IsCancellationRequested)
                {
                    for (int i = 30; i >= 0; i--)
                    {
                        Result = i.ToString();
                        Thread.Sleep(1000);
                        if (tokenSource.IsCancellationRequested)
                            break;
                    }
                    if (!tokenSource.IsCancellationRequested)
                    {
                       CorrectAnswer = false;
                        WrongAnswer = false;
                        MyCommandOptionWrong.RaiseCanExecuteChanged();
                        MyCommandOptionCorrect.RaiseCanExecuteChanged();
                    }
                }
            }, tokenSource.Token);


        }

        private bool CanExecute_Correct()
        {
            return CorrectAnswer;
        }

        private void ExecuteMethod_Correct()
        {
            tokenSource.Cancel();
            
            CorrectAnswer = false;
            WrongAnswer = false;
            MyCommandOptionWrong.RaiseCanExecuteChanged();
            MyCommandOptionCorrect.RaiseCanExecuteChanged();
            Color = new SolidColorBrush(Colors.LightGreen);
        }

        private bool CanExecute_Wrong()
        {
            if (Result == "0")
            {
                Color = new SolidColorBrush(Colors.LightPink);
            }
            return WrongAnswer;
        }

        public void ExecuteMethod_Wrong()
        {
            tokenSource.Cancel();
           
            CorrectAnswer = false;
            WrongAnswer = false;
            MyCommandOptionWrong.RaiseCanExecuteChanged();
            MyCommandOptionCorrect.RaiseCanExecuteChanged();
            Color = new SolidColorBrush(Colors.LightPink);
        }

    }
}
