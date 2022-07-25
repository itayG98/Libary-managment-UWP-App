using DB_Libary;
using LibaryModel;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// Edit Details of libaryItem
    /// Avilable only for Employees
    /// </summary>
    public sealed partial class EditItem : Page
    {
        public Logic logic;
        public EditItem()
        {
            this.InitializeComponent();
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            Libary.Click += Libary_Click;
            Submit.Click += Submit_Click;
            Delete.Click += Delete_Click;
            Delete.IsTabStop = false;
        }

        //AppBarButton functions
        private void Libary_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            if (logic.Signed as Costumer != null)
                Frame.Navigate(typeof(CostumerPage), logic);
            else
                Frame.Navigate(typeof(EmployePage), logic);
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(ChangePassword), logic);
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(MainPage), logic);
        }


        public void UpdateData()
        //Update the fields
        {
            Congrat.Text = logic.CurrentItem.Name;
            DiscountF.Text = $"{logic.CurrentItem.DiscountPercentage}";
            PrintedInF.Date = logic.CurrentItem.PrintedDate;
            StutusF.Text = logic.CurrentItem.IsBorrowed ? "Unavilable" : "Avilable";
            CodeF.Text = $"{logic.CurrentItem.ItemId}";
            PriceF.Text = $"{logic.CurrentItem.Price}";
            NameF.Text = logic.CurrentItem.Name;
            DescriptionF.Text = logic.CurrentItem.Description;
            DescriptionF.TextWrapping = TextWrapping.Wrap;
        }
        private void DisableFields()
        //disabled important field's 
        {
            if (logic.CurrentItem != null)
            {
                //Disabled
                CodeF.IsReadOnly = true;
                StutusF.IsReadOnly = true;
            }
            else
            {
                if (logic.Signed as Employee != null)
                    Frame.Navigate(typeof(EmployePage), logic);
                else
                    Frame.Navigate(typeof(CostumerPage), logic);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowAlert(logic.DeleteCurrentItem());
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
            finally
            {
                Frame.Navigate(typeof(EmployePage), logic);
            }
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var temp = logic.CurrentItem;
            temp.Name = NameF.Text;
            if (int.TryParse(PriceF.Text, out int Ivalue))
                temp.Price = Ivalue;
            if (int.TryParse(DiscountF.Text, out Ivalue))
                temp.DiscountPercentage = Ivalue;
            temp.PrintedDate = PrintedInF.Date.Date;
            temp.Description = DescriptionF.Text;
            UpdateData();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null && logic.Signed as Employee != null)
            {
                DisableFields();
                UpdateData();
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

