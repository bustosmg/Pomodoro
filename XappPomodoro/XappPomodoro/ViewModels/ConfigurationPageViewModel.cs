using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XappPomodoro.ViewModels;

namespace XappPomodoro.ViewMdels
{
    public class ConfigurationPageViewModel : NotificationObject
    {
        private ObservableCollection<int> pomodoroDuration;
        public ObservableCollection<int> PomodoroDuration
        {
            get { return pomodoroDuration; }
            set { pomodoroDuration = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> breakDuration;
        public ObservableCollection<int> BreakDuration
        {
            get { return breakDuration; }
            set { breakDuration = value;
                OnPropertyChanged();
            }
        }

        private int selectPomodoroDuration;
        public int SelectPomodoroDuration
        {
            get { return selectPomodoroDuration; }
            set { selectPomodoroDuration = value;
                OnPropertyChanged();
            }
        }

        private int selectBreakDuration;
        public int SelectBreakDuration
        {
            get { return selectBreakDuration; }
            set { selectBreakDuration = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }

        public ConfigurationPageViewModel()
        {
            LoadPomodoroDurations();
            LoadBreakDurations();
            LoadConfiguration();
            SaveCommand = new Command(SaveCommandExecuteAsync);
        }

        private void LoadConfiguration()
        {
            if (Application.Current.Properties.ContainsKey(Literals.PomodoroDurations))
            {
                SelectPomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDurations] ;
            }

            if (Application.Current.Properties.ContainsKey(Literals.BreakDurations))
            {
                SelectBreakDuration= (int)Application.Current.Properties[Literals.BreakDurations] ;
            }
        }

            private void LoadPomodoroDurations()
        {
            PomodoroDuration = new ObservableCollection<int>();
            PomodoroDuration.Add(1);
            PomodoroDuration.Add(5);
            PomodoroDuration.Add(10);
            PomodoroDuration.Add(15);
            PomodoroDuration.Add(20);
        }

        private void LoadBreakDurations()
        {
            BreakDuration = new ObservableCollection<int>();
            BreakDuration.Add(1);
            BreakDuration.Add(5);
            BreakDuration.Add(10);
            BreakDuration.Add(15);
            BreakDuration.Add(20);
        }

        private async void SaveCommandExecuteAsync()
        {
            Application.Current.Properties[Literals.PomodoroDurations] = SelectPomodoroDuration;
            Application.Current.Properties[Literals.BreakDurations] = SelectBreakDuration;

            await Application.Current.SavePropertiesAsync();
        }
    }
}
