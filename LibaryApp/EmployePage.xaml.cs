using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DB_Libary;
using LibaryModel;
using Windows.UI.Popups;
using System.Collections;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibaryApp
{
    /// <summary>
    /// Employye page
    /// An Employye can brorrow/buy a libay items or check edit or delete persons or Items
    /// 
    /// The list view has 2 states
    /// Showing users of the systems
    /// showing all libary items includes the ones which currently borrowed
    /// </summary>
    public sealed partial class EmployePage : Page
    {
        public Logic logic;
        private bool ShowMyItems;
        private bool ShowPersons;
        public EmployePage()
        {
            this.InitializeComponent();
            SortItems.SelectionChanged += SortItemsChanged;
            SortPerson.SelectionChanged += SortPersonsChanged;
            LibaryItemsType.SelectionChanged += LibaryItem_SelectionChanged;
            CostumerOrEmployees.SelectionChanged += Persons_SelectionChanged;
            PersonsOrItems.Click += PersonsOrItemsToggle;
            MyItemsOrLibary.Click += MyItemsOrLibaryToggle;
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            ChangeDetails.Click += ChangeDetails_Click;
            AddItem.Click += Addbook_Click;
            ItemsListVeiw.IsItemClickEnabled = true;
            ItemsListVeiw.ItemClick += ItemsListVeiw_ItemClick;

            ShowMyItems = false;
            ShowPersons = false;
        }

        //AppBarButton
        private void Addbook_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Signed as Employye != null)
                Frame.Navigate(typeof(AddItem), logic);
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(ChangePassword), logic);
            return;
        }
        private void ChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditPerson), logic);
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            logic.SignOut();
            Frame.Navigate(typeof(MainPage), logic);
            return;
        }

        //List veiw functions
        public void UpdateMenu()
        {
            if (!ShowPersons)
                ItemsListVeiw.ItemsSource = logic.libaryItems;
            else
                ItemsListVeiw.ItemsSource = logic.persons;
        }
        private void ItemsListVeiw_ItemClick(object sender, ItemClickEventArgs e)
        //Clicking on specific item in the list view. It can be Person or Libary Item   
        {
            if (e.ClickedItem as LibaryItem != null)
            {
                logic.ChooseItem(e.ClickedItem);
                Frame.Navigate(typeof(LibaryItemOrderPage), logic);
            }
            else if (e.ClickedItem as Person != null)
            {
                logic.PersonToEdit = e.ClickedItem as Person;
                Frame.Navigate(typeof(EditPerson), logic);
            }
        }
        private void Persons_SelectionChanged(object sender, SelectionChangedEventArgs e)
            // Selecet partion of persons
        {
            if (CostumerOrEmployees.SelectedItem == OnlyCostumers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Costumer);
            else if (CostumerOrEmployees.SelectedItem == OnlyEmployees)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Employye);
            else if (CostumerOrEmployees.SelectedItem == ShowBorrowers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p.BorrowingCount > 0);
            else
                ItemsListVeiw.ItemsSource = logic.persons;
        }
        private void LibaryItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // Selecet partion of Libary Items
        {
            if (LibaryItemsType.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (LibaryItemsType.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else
                ItemsListVeiw.ItemsSource = logic.libaryItems;
        }
        private void SortPersonsChanged(object sender, SelectionChangedEventArgs e)
            //Sort the persons list and show it
        {
            if (SortPerson.SelectedItem == SortByFNameReverse)
                logic.Sort(new IComparerByFisrtNameReverse());
            else
                logic.Sort(new IComparerFirstName());

            if (CostumerOrEmployees.SelectedItem == OnlyCostumers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Costumer);
            else if (CostumerOrEmployees.SelectedItem == OnlyEmployees)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Employye);
            else if (CostumerOrEmployees.SelectedItem == ShowBorrowers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p.BorrowingCount > 0);
            else
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Person);
        }
        private void SortItemsChanged(object sender, SelectionChangedEventArgs e)
        //Sort the Items list and show it
        {
            if (SortItems.SelectedItem == SortByNameReverse)
                logic.Sort(new IComparerByItemNameReverse());
            else if (SortItems.SelectedItem == SortByPrice)
                logic.Sort(new IComparerByPrice());
            else if (SortItems.SelectedItem == SortByPriceReverse)
                logic.Sort(new IComparerByPriceReverse());
            else
            {
                SortItems.SelectedItem = SortByName;
                logic.Sort(new IComparerByItemName());
            }

            if (LibaryItemsType.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (LibaryItemsType.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is LibaryItem);
        }
        private void PersonsOrItemsToggle(object sender, RoutedEventArgs e)
            //Toggle btn wether to show persons or Items
        {
            if (!ShowPersons) //Current LibaryItems  => Persons
            {
                //Hide LibaryItems CBX show Persons CBX
                SortItems.Visibility = Visibility.Collapsed;
                LibaryItemsType.Visibility = Visibility.Collapsed;
                SortPerson.Visibility = Visibility.Visible;
                CostumerOrEmployees.Visibility = Visibility.Visible;
                MyItemsOrLibary.Visibility = Visibility.Collapsed;
                PersonsOrItems.Content = "Items";
            }
            else //Current Persons => LibaryItems
            {
                //Hide Persons CBX show LibaryItems CBX
                SortItems.Visibility = Visibility.Visible;
                LibaryItemsType.Visibility = Visibility.Visible;
                SortPerson.Visibility = Visibility.Collapsed;
                CostumerOrEmployees.Visibility = Visibility.Collapsed;
                MyItemsOrLibary.Visibility = Visibility.Visible;
                PersonsOrItems.Content = "Persons";
            }
            ShowPersons = !ShowPersons;
            UpdateMenu();
        }
        private void MyItemsOrLibaryToggle(object sender, RoutedEventArgs e)
        //Toggle btn wether to show All libary Items or users Items
        {
            if (!ShowPersons)
            {
                if (!ShowMyItems) //Current My items => libary
                {
                    logic.UpdateLogicLists();
                    ItemsListVeiw.ItemsSource = logic.MyItems();
                    MyItemsOrLibary.Content = "Avilable items";
                }
                else //Current libary => My items
                {
                    logic.UpdateLogicLists();
                    ItemsListVeiw.ItemsSource = null;
                    ItemsListVeiw.ItemsSource = logic.libaryItems;
                    MyItemsOrLibary.Content = "My items";
                }
                ShowMyItems = !ShowMyItems;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null)
            {
                logic.UpdateLogicLists();
                UpdateMenu();
                Congrat.Text = logic.GetName;
            }
            else
            {
                Frame.Navigate(typeof(MainPage));
            }
            Congrat.Text = logic.GetName;
        }
    }
}
