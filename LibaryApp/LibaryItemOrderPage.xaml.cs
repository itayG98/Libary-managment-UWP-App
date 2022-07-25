using DB_Libary;
using Windows.UI.Xaml.Media;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using LibaryModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// A page to borrow buy or return
    /// If the signed user is employee the edit item is navigating to EditItem Page
    /// if the user is costumer it navigates to edit peron
    /// </summary>
    public sealed partial class LibaryItemOrderPage : Page
    {
        public Logic logic;
        string msg;
        public LibaryItemOrderPage()
        {
            this.InitializeComponent();
            Retrun.Click += Retrun_Click;
            Buy.Click += Buy_Click;
            Borrow.Click += Borrow_Click;
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            Libary.Click += Libary_Click;
            ChangeDetails.Click += ChangeDetails_Click;
        }

        //AppBarButton funcions
        private void ChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Signed as Employee != null && logic.libaryItems.Contains(logic.CurrentItem))
                Frame.Navigate(typeof(EditItem), logic);
            else
            {
                logic.ClearItem();
                logic.PersonToEdit = logic.Signed;
                Frame.Navigate(typeof(EditPerson), logic);
            }
        }
        private void Libary_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
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
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(ChangePassword), logic);
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.SignOut();
            Frame.Navigate(typeof(MainPage), logic);
        }
        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            if (logic.CurrentItem != null && logic.Borrow())
            {
                Avilability.Fill = new SolidColorBrush(Colors.Red);
                ShowAlert($"Borrowed {msg} sucssefuly");
            }
            else
            {
                ShowAlert($"Could'nt borrow {msg}");
            }
        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Buy())
            {
                Avilability.Fill = new SolidColorBrush(Colors.Red);
                ShowAlert($"Bought {msg} sucssefuly");
            }
            else
            {
                ShowAlert($"Could'nt buy {msg}");
            }
        }
        private void Retrun_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Return())
                Avilability.Fill = new SolidColorBrush(Colors.Green);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null)
            {
                description.Text = logic.CurrentItem.Description;
                description.TextWrapping = TextWrapping.Wrap;
                Congrat.Text = $"Hello {logic.GetName}";
                Name.Text = logic.CurrentItem.Name;
                Price.Text = $"{logic.CurrentItem.Price:C}";
                PrintDate.Text = logic.CurrentItem.PrintedDate.ToLongDateString();
                ID.Text = logic.CurrentItem.ItemId.ToString();
                AuthorOrEditors.Text = logic.AuthorOrEditors();
                if (!logic.libaryItems.Contains(logic.CurrentItem) || logic.CurrentItem.IsBorrowed)
                    Avilability.Fill = new SolidColorBrush(Colors.Red);
                else
                    Avilability.Fill = new SolidColorBrush(Colors.Green);
                Congrat.Text = logic.GetName;
                msg = logic.CurrentItem.ToString();
            }
            else
                Frame.Navigate(typeof(MainPage), logic);
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }

    }
}
