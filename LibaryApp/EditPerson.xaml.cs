using DB_Libary;
using LibaryModel;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryApp
{
    /// <summary>
    /// Edit valid persons details page according to the fieldes
    /// 
    /// if it is a Costumer user let him edit his fields
    /// if it is employee user let him edit more fields of himself 
    /// or others if navigated through the list view of persons
    /// 
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
            Delete.Click += Delete_Person_Click;
        }

        //AppBarButton functions
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        //validate the data if so update
        {
            if (logic.CharectersOnly(FirstName.Text))
                logic.PersonToEdit.FirstName = FirstName.Text;
            if (logic.CharectersOnly(LastName.Text))
                logic.PersonToEdit.LastName = LastName.Text;
            if (logic.CharectersOnly(City.Text))
                logic.PersonToEdit.City = City.Text;
            if (logic.CharectersOnly(Street.Text))
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

        private void Delete_Person_Click(object sender, RoutedEventArgs e)
        //Try delete current person
        //Cant delete signed user
        {
            try
            {
                ShowAlert(logic.DeletePersonToEdit());
                Frame.Navigate(typeof(EmployePage), logic);
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
                logic.ClearPerson();
                Frame.Navigate(typeof(EmployePage), logic);
            }
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
            {
                //Disabled if it is a Costumer
                Discaount.IsEnabled = false;
                Delete.IsEnabled = false;
                Delete.Visibility = Visibility.Collapsed;
            }
        }
        public void LoadData()
        //loade current data of personToEdit
        {
            Congrat.Text = logic.PersonToEdit.FirstName + " " + logic.PersonToEdit.LastName;
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