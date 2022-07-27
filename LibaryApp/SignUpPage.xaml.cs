using ViewModel_MoockDB;
using LibaryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public Logic logic;
        public SignUpPage()
        {
            this.InitializeComponent();
            SignUp.Click += SignUp_Click;
            SignUpAsManager.Click += SignUpAsManager_Click;
            SignIn.Click += SignIn_Click;
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), logic);
        }
        private void SignUpAsManager_Click(object sender, RoutedEventArgs e)
        {
            int HouseNum = -1;
            if (ValidateAndColor(ref HouseNum))
            {
                try
                {
                    logic.EmployeSignUp(ID.Text, Fname.Text, Lname.Text, City.Text, Street.Text, Password.Password, HouseNum, Password.Password);
                    Frame.Navigate(typeof(EmployePage), logic);
                }
                catch (Exception ex)
                {
                    ShowAlert(ex.Message);
                    Password.Password = string.Empty;
                    ConiformPassword.Password = string.Empty;
                }
            }
            else
            {
                Congrat.Text = "Sorry please try again";
            }
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            int HouseNum = -1;
            if (ValidateAndColor(ref HouseNum))
            {
                try
                {
                    logic.CostumerSignUp(ID.Text, Fname.Text, Lname.Text, City.Text, Street.Text, HouseNum, Password.Password);
                    Frame.Navigate(typeof(CostumerPage), logic);
                }
                catch (Exception ex)
                {
                    ShowAlert(ex.Message);
                }
            }
            else
                Congrat.Text = "Sorry please try again";
        }
        private bool ValidateAndColor(ref int houseNum)
        {
            if (!Person.Validation(ID.Text))
            {
                ID.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            ID.Foreground = new SolidColorBrush(Colors.Black);
            if (Password.Password != ConiformPassword.Password && Password.Password.All(c => char.IsLetterOrDigit(c))
                &&Password.Password.Length<Person.MAX_PASSWOD_LENGTH)
            {
                Password.Foreground = new SolidColorBrush(Colors.Red);
                ConiformPassword.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            Password.Foreground = new SolidColorBrush(Colors.Black);
            ConiformPassword.Foreground = new SolidColorBrush(Colors.Black);
            if (!logic.CharectersOnly(Fname.Text) || !logic.CharectersOnly(Lname.Text))
            {
                Lname.Foreground = new SolidColorBrush(Colors.Red);
                Fname.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            Lname.Foreground = new SolidColorBrush(Colors.Black);
            Fname.Foreground = new SolidColorBrush(Colors.Black);
            if (!logic.CharectersOnly(City.Text))
            {
                City.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            City.Foreground = new SolidColorBrush(Colors.Black);
            if (!logic.CharectersOnly(Street.Text))
            {
                Street.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            Street.Foreground = new SolidColorBrush(Colors.Black);
            if (!int.TryParse(HouseNumber.Text, out houseNum) && houseNum < 1000)
            {
                HouseNumber.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            HouseNumber.Foreground = new SolidColorBrush(Colors.Black);
            return true;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as Logic != null)
            {
                logic = (Logic)e.Parameter;
                Password.Description = $"Up to {Person.MAX_PASSWOD_LENGTH} charecrers or digits";
                ConiformPassword.Description = $"Coniform Password";
            }
            else
                Frame.Navigate(typeof(MainPage));
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }
    }
}
