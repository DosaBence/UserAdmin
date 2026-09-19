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
    /// Interaction logic for MembersPage.xaml
    /// </summary>
    public partial class MembersPage : Page
    {

        private readonly UserDbService _userDbService = new();
        private User? user;

        public MembersPage()
        {
            InitializeComponent();
            MembersGrid.ItemsSource = _userDbService.GetAll();
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kapcsolat: \nEmail: dosabence94@gmail.com\nTelefon: +36317882806","Kapcsolat", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HelpmenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Súgó: \nAz 'Új tag' gombbal új felhasználót vehetsz fel.\nA táblázat soraiban  'Szerkesztés'-sel módosíthatod, a 'Törlés'-sel eltávolíthatod a tagot.\nA 'Kijelentkezés ' gombbal visszatérhetsz a bejelentkező oldalra. ","Súgó", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MemberEditPage(_userDbService, null));
            MembersGrid.ItemsSource = _userDbService.GetAll();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var member = MembersGrid.SelectedItem as User;
            var User = new User
            {
                Id = member.Id,
                Username = member.Username,
                Email = member.Email,
                Password = member.Password,
            };

            NavigationService.Navigate(new MemberEditPage(_userDbService,User));

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var member = MembersGrid.SelectedItem as User;

            var User = new User
            {
                Id = member.Id,
                Username = member.Username,
                Email = member.Email,
                Password = member.Password,
            };

            _userDbService.Delete(User);

            MessageBox.Show("Sikeres törlés", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);

            MembersGrid.ItemsSource = _userDbService.GetAll();


        }
    }
}
