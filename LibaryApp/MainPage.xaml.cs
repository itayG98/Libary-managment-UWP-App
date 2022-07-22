using DB_Libary;
using LibaryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Logic logic;
        public MainPage()
        {
            this.InitializeComponent();
            SignInBtn.Click += SignInBtn_Click;
            SignUpBtn.Click += SignUp_Click;
        }
        private void SignUp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage), logic);
        }
        private void SignInBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            logic.TrySignIn(IDBox.Text, PasswordBox.Password);
            if (logic.Signed != null)
            {
                if (logic.Signed as Costumer != null)
                {
                    Frame.Navigate(typeof(CostumerPage), logic);
                    return;
                }
                else
                {
                    Frame.Navigate(typeof(EmployePage), logic);
                    return;    
                }
            }
            else
            {
                IDBox.Foreground = new SolidColorBrush(Colors.Red);
                PasswordBox.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as Logic != null)
            {
                logic = (Logic)e.Parameter;
                logic.SignOut();
                logic.ClearItem();
                logic.PersonToEdit = null;
            }
            else
            {
                logic = new Logic();
            }
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }
    }
}
