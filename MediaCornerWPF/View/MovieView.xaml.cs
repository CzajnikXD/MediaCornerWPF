﻿using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MediaCornerWPF.Lib;
using MediaCornerWPF.Lib.API.Calls;
using MediaCornerWPF.Lib.API.Models;
using MediaCornerWPF.Lib.MongoDB;

namespace MediaCornerWPF.View
{
    /// <summary>
    /// Logika interakcji dla klasy MovieView.xaml
    /// </summary>s
    public partial class MovieView : UserControl
    {
        public ObservableCollection<MovieModel> _movies = new ObservableCollection<MovieModel>();
        public ObservableCollection<MovieModel> Movies
        {
            get { return _movies; }
            set { _movies = value; }
        }
        public MovieView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var ll = await MovieController.GetPopular();

            foreach(MovieModel movie in ll)
            {
                _movies.Add(movie);
            }
        }

        public async void btnClick(object sender, RoutedEventArgs e)
        {
            MovieModel movie = (MovieModel)((Button)e.Source).DataContext;
            
            DB.AddToWatchlist(LoggedUser.Id, movie.id);

            MessageBox.Show("Dodano do listy!");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your logic here
        }
    }
}
