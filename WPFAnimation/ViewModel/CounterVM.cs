namespace WPFAnimation.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Timers;

    public class CounterVM : INotifyPropertyChanged 
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public CounterVM()
        {
            Timer timer = new Timer(1);
            timer.Elapsed += this.Timer_Elapsed;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Now)));
        }
    }
}
