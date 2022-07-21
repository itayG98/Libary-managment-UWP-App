using DB_Libary;
using LibaryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class EditPerson : Page
    {
        public Logic logic;
        public EditPerson()
        {
            this.InitializeComponent();
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            Libary.Click += Libary_Click;
            Submit.Click += Submit_Click;
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {

            if (logic.NameValidity(FirstName.Text))
                logic.PersonToEdit.FirstName = FirstName.Text;
            if (logic.NameValidity(LastName.Text))
                logic.PersonToEdit.LastName = LastName.Text;
            if (logic.NameValidity(City.Text))
                logic.PersonToEdit.City = City.Text;
            if (logic.NameValidity(Street.Text))
                logic.PersonToEdit.Street = Street.Text;
            if (HouseNumber.Text.All(ch => char.IsDigit(ch)) && HouseNumber.Text.Length < 5)
            {
                int.TryParse(HouseNumber.Text, out int houseNum);
                logic.PersonToEdit.HouseNumber = houseNum;
            }
            if (Discaount.Text.All(ch => char.IsDigit(ch)))
            {
                int.TryParse(Discaount.Text, out int discountPer);
                if (discountPer > 0 && discountPer < 100)
                    logic.PersonToEdit.DiscountPerCent = discountPer;
            }
            LoadData();
        }
        private void Libary_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearPerson();
            if (logic.Signed as Costumer != null)
                Frame.Navigate(typeof(CostumerPage), logic);
            else
                Frame.Navigate(typeof(EmployePage), logic);
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearPerson();
            Frame.Navigate(typeof(ChangePassword), logic);
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearPerson();
            Frame.Navigate(typeof(MainPage), logic);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null)
            {
                if (logic.PersonToEdit == null)
                    logic.PersonToEdit = logic.Signed;
                ModifyFields();
                LoadData();
            }
            else
            {
                logic.ClearPerson();
                logic.SignOut();
                Frame.Navigate(typeof(MainPage), logic);
            }
        }
        private void ModifyFields()
        //Insert field's names and disableed some
        {
            ID.IsEnabled = false;
            if (logic.Signed as Costumer != null)
                //Disabled if it is a Costumer
                Discaount.IsEnabled = false;
        }
        public void LoadData()
        {
            Congrat.Text = logic.GetName;
            FirstName.Text = logic.PersonToEdit.FirstName;
            LastName.Text = logic.PersonToEdit.LastName;
            City.Text = logic.PersonToEdit.City;
            Street.Text = logic.PersonToEdit.Street;
            HouseNumber.Text = logic.PersonToEdit.HouseNumber.ToString();
            Discaount.Text = $"{logic.PersonToEdit.DiscountPerCent}";
            ID.Text = $"{logic.PersonToEdit.Id}";
            Borowws.ItemsSource = logic.libaryItems.FindAll(lib => logic.PersonToEdit.BorrowingList.Contains(lib.ItemId));
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }
    }
}