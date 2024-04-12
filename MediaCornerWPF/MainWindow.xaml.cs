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

namespace MediaCornerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Testowa walidacja logowania, bez podpiętej bazy danych
            if (username == "admin" && password == "admin")
            {
                MessageBox.Show("Zalogowano pomyślnie!");

                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.Width = 800;
                mainMenuWindow.Height = 475;
                mainMenuWindow.Show();
                this.Close();
            }
            else
            {

                MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło. Spróbuj ponownie.");
            }
        }
    }
}