using MediaCornerWPF.Lib.API.Calls;
using MediaCornerWPF.Lib.API.Models;
using MediaCornerWPF.Lib.MongoDB.Models;
using MediaCornerWPF.Lib.MongoDB;
using MediaCornerWPF.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;
using System.Windows.Interop;

namespace MediaCornerWPF.View
{
    /// <summary>
    /// Logika interakcji dla klasy UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {

        public ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
        public UsersView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class Message
        {
            public string msg { get; set; }
            public int movieId { get; set; }
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var watchlist = DB.GetOthersWatchlist(LoggedUser.Id);

            foreach (WatchlistedModel movie in watchlist)
            {
                var movieModel = await MovieController.GetMovie(movie.MovieId);
                var username = DB.GetUsername(movie.UsersId);

                Message temp = new Message();

                temp.msg = $"{username} dodał to swojej watchlisty {movieModel.title}";
                temp.movieId = movieModel.id;

                _messages.Add(temp);
            }
        }

        public async void btnClick(object sender, RoutedEventArgs e)
        {
            Message movie = (Message)((Button)e.Source).DataContext;

            DB.AddToWatchlist(LoggedUser.Id, movie.movieId);

            _messages.Remove(movie);

            MessageBox.Show("Dodano do listy!");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your logic here
        }
    }
}
