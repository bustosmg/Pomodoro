using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace XappPomodoro.ViewModels
{
    public class HistoryPageViewModel:NotificationObject
    {
        private ObservableCollection<DateTime> pomodoros;

        public ObservableCollection<DateTime> Pomodoros
        {
            get { return pomodoros; }
            set { pomodoros = value;
                OnPropertyChanged();
            }
        }

        public HistoryPageViewModel()
        {
            LoadHistory();

        }

        private void LoadHistory()
        {
            if (Application.Current.Properties.ContainsKey(Literals.History))
            {
                var json = Application.Current.Properties[Literals.History].ToString();
                var history = JsonConvert.DeserializeObject<List<DateTime>>(json);

                Pomodoros = new ObservableCollection<DateTime>(history);
            }
        }
    }
}
