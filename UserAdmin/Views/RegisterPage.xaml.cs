using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserAdmin.Models;
using UserAdmin.Services;

namespace UserAdmin.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly UserDbService _userDbService = new UserDbService();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text.Trim();
            var email = EmailBox.Text.Trim();
            var password = PasswordBoxInput.Text;
            var confirmpassword = ConfirmPasswordBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmpassword))
            {
                ErrorText.Text = "Minden mező kitöltése kötelező.";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (password != confirmpassword)


            {

                ErrorText.Text = "A két jelszó nem egyezik.";
                ErrorText.Visibility = Visibility.Visible;

                return;

            }

            if (password.Length < 6)

            {
                ErrorText.Text = "A jelszónak legalább 6 karakterből kell hogy álljon";
                ErrorText.Visibility = Visibility.Visible;

                return;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                RegisteredAt = DateTime.Now,
            };
            _userDbService.Add(user);
            MessageBox.Show("Sikeres regisztráció. ");
        }




        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
