using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XappPomodoro.ViewModels
{
    public class RootPageViewModel : NotificationObject
    {
        private ObservableCollection<string> menuItems;

        public ObservableCollection<string> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value;
                OnPropertyChanged();
            }
        }

        private string selectedMenuItems;

        public string SelectedMenuItems
        {
            get { return selectedMenuItems; }
            set { selectedMenuItems = value;
                OnPropertyChanged();
            }
        }

        public RootPageViewModel()
        {
            MenuItems = new ObservableCollection<string>();
            MenuItems.Add("Pomodoro");
            MenuItems.Add("Histórico");
            MenuItems.Add("Configuración");

            PropertyChanged += RootPageViewModel_PropertyChanged;
        }

        public void RootPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedMenuItems))
            {
                if (SelectedMenuItems == "Configuración")
                {
                    MessagingCenter.Send(this, "GotoConfiguration");
                }

                if (SelectedMenuItems == "Histórico")
                {
                    MessagingCenter.Send(this, "GotoHistory");
                }

                if (SelectedMenuItems == "Pomodoro")
                {
                    MessagingCenter.Send(this, "GotoPomodoro");
                }
            }
        }
    }
}
