using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace XappPomodoro.ViewModels
{
    public class PomodoroPageViewModel: NotificationObject
    {
        private TimeSpan ellapsed;
        private Timer timer;
        private bool isRunning;
        private bool isInBreak;

        private int PomodoroDuration;
        private int BreakDuration;
        private object jsonconvert;

        public TimeSpan Ellapsed
        {
            get { return ellapsed; }
            set { ellapsed = value;
                   OnPropertyChanged(); }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }

        public bool IsInBreak
        {
            get { return isInBreak; }
            set
            {
                isInBreak = value;
                OnPropertyChanged();
            }
        }
        public ICommand StartOrPauseCommand { get; set; }

        public PomodoroPageViewModel()
        {
            InitializeTimer();
            LoadConfigurationValues();
            StartOrPauseCommand = new Command(StartOrPauseCommandExecute);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));

            if (IsRunning && Ellapsed.TotalSeconds == PomodoroDuration * 60)
            {
                IsRunning = false;
                IsInBreak = true;
                Ellapsed = TimeSpan.Zero;
                await SavePomodoroAsync(); 
            }

            if (IsInBreak && Ellapsed.TotalSeconds == BreakDuration * 60)
            {
                IsRunning = true;
                IsInBreak = false;
                Ellapsed = TimeSpan.Zero;
            }

        }

        private async Task SavePomodoroAsync()
        {
            List<DateTime> history;

            if (Application.Current.Properties.ContainsKey(Literals.History))
            {
                // uso List para serializar - sino usar DB relacional
                //history = Application.Current.Properties[Literals.History] as List<DateTime>;

                var json = Application.Current.Properties[Literals.History].ToString();
                history = JsonConvert.DeserializeObject<List<DateTime>>(json);
            }
            else
            {
                 history = new List<DateTime>();
            }

            //history.Add(DateTime.Now); 
            var serializeJsonConvert = JsonConvert.SerializeObject(history);
            Application.Current.Properties[Literals.History] = serializeJsonConvert;

            await Application.Current.SavePropertiesAsync();
        }
                

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;
        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
        }

        private void StartOrPauseCommandExecute()
        {
            if (IsRunning)
                StopTimer();
            else
                StartTimer();
        }

        private void LoadConfigurationValues()
        {
            //if (Application.Current.Properties.ContainsKey(Literals.PomodoroDurations))
            //{
            PomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDurations];
            //}

            //if (Application.Current.Properties.ContainsKey(Literals.BreakDurations))
            //{
            BreakDuration = (int)Application.Current.Properties[Literals.BreakDurations];
            //}
        }

    }
}
