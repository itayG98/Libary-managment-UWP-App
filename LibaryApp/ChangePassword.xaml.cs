using ViewModel_MoockDB;
using LibaryModel;
using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangePassword : Page
    {
        public Logic logic;
        public ChangePassword()
        {
            this.InitializeComponent();
            ChangeBtn.Click += ChangeBtn_Click;
            SignOut.Click += SignOut_Click;
            ChangeDetails.Click += ChangeDetails_Click;
            Libary.Click += ToLibary_Click;
        }

        //AppBarButton functions
        private void ToLibary_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            if (logic.Signed as Costumer != null)
                Frame.Navigate(typeof(CostumerPage), logic);
            else
                Frame.Navigate(typeof(EmployePage), logic);
        }
        private void ChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(EditItem), logic);
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(MainPage), logic);
        }


        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Signed.ChangePassword(oldPasswordBox.Password, NewPasswordBox.Password))
            {
                ShowAlert("Succesfuly changed password");
                if (logic.Signed as Costumer != null)
                    Frame.Navigate(typeof(CostumerPage), logic);
                else
                    Frame.Navigate(typeof(EmployePage), logic);
            }
            else
            {
                oldPasswordBox.Password = string.Empty;
                NewPasswordBox.Password = string.Empty;
                oldPasswordBox.Foreground = new SolidColorBrush(Colors.Red);
                NewPasswordBox.Foreground = new SolidColorBrush(Colors.Red);
                Congrat.Text = "Please try again";
            }
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null)
            {
                Congrat.Text = logic.GetName;
            }
            else
            {
                Frame.Navigate(typeof(MainPage));
            }
            base.OnNavigatedTo(e);
        }
    }
}
