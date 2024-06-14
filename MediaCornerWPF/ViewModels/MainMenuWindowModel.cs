using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FontAwesome.Sharp;
using System.Windows.Input;
using MediaCornerWPF.Lib.MongoDB;
using MediaCornerWPF.Lib;
using MediaCornerWPF.Lib.API;
using System.Diagnostics;

namespace MediaCornerWPF.ViewModels
{
    public class MainMenuWindowModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private string username;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        //---> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowWatchlistViewCommand { get; }
        public ICommand ShowMovieViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainMenuWindowModel()
        {
            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowWatchlistViewCommand = new ViewModelCommand(ExecuteShowWatchlistCommand);
            ShowMovieViewCommand = new ViewModelCommand(ExecuteShowMovieViewCommand);
            ShowUsersViewCommand = new ViewModelCommand(ExecuteShowUsersViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);
            ExecuteShowLoggedUser(null);
        }

        private void ExecuteShowLoggedUser(object obj)
        {
            Username = LoggedUser.Username;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowWatchlistCommand(object obj)
        {
            CurrentChildView = new WatchlistViewModel();
            Caption = "Watchlist";
            Icon = IconChar.Tv;
        }

        private void ExecuteShowMovieViewCommand(object obj)
        {
            CurrentChildView = new MovieViewModel();
            Caption = "Movie";
            Icon = IconChar.Film;
        }

        private void ExecuteShowUsersViewCommand(object obj)
        {
            CurrentChildView = new UsersViewModel();
            Caption = "Users";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Settings";
            Icon = IconChar.Tools;
        }
    }
}
