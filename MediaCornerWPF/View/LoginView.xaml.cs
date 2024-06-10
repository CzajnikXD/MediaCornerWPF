﻿using MediaCornerWPF.Lib.MongoDB;
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
using System.Diagnostics;

namespace MediaCornerWPF.View
{
    /// <summary>
    /// Logika interakcji dla klasy LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DB.InitDB();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Password;

            Debug.WriteLine(username);
            Debug.WriteLine(password);

            bool isAuthorized = DB.AuthorizeUser(username, password);

            if (isAuthorized)
            {
                MessageBox.Show("Zalogowano pomyślnie!");

                MainMenuWindow mainMenuWindow = new MainMenuWindow();
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