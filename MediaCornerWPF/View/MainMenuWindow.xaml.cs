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
using System.Windows.Shapes;

namespace MediaCornerWPF.View
{
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            // Obsługa okna użytkowników
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.Show();
        }

        private void MediaButton_Click(object sender, RoutedEventArgs e)
        {
            // Obsługa okna multimediów
            MediaWindow mediaWindow = new MediaWindow();
            mediaWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Obsługa wylogowania
            this.Close();
        }
    }
}