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
    /// An empty page that can be used on its own or navigated to within a Frame.
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
            LibaryItemsType.SelectionChanged += BooksOrJournal_SelectionChanged;
            CostumerOrEmployees.SelectionChanged += CostumerOrEmployees_SelectionChanged;
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

        private void Addbook_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Signed as Employye != null)
                Frame.Navigate(typeof(AddItem), logic);
        }
        public void UpdateMenu()
        {
            if (!ShowPersons)
                ItemsListVeiw.ItemsSource = logic.libaryItems;
            else
                ItemsListVeiw.ItemsSource = logic.persons;
        }
        private void ItemsListVeiw_ItemClick(object sender, ItemClickEventArgs e)
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
        private void CostumerOrEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CostumerOrEmployees.SelectedItem == AllPersons)
                ItemsListVeiw.ItemsSource = logic.persons;
            else if (CostumerOrEmployees.SelectedItem == OnlyCostumers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Costumer);
            else if (CostumerOrEmployees.SelectedItem == OnlyEmployees)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Employye);
            else if (CostumerOrEmployees.SelectedItem == ShowBorrowers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p.BorrowingCount > 0);
        }
        private void BooksOrJournal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LibaryItemsType.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (LibaryItemsType.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else if (LibaryItemsType.SelectedItem == AllItems)
                ItemsListVeiw.ItemsSource = logic.libaryItems;
        }
        private void SortPersonsChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortPerson.SelectedItem == SortByFName)
                logic.Sort(new IComparerFName());
            else if (SortPerson.SelectedItem == SortByFNameReverse)
                logic.Sort(new IComparerByFNameReverse());
            else
                return;

            if (CostumerOrEmployees.SelectedItem == OnlyCostumers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Costumer);
            else if (CostumerOrEmployees.SelectedItem == OnlyEmployees)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p is Employye);
            else if (CostumerOrEmployees.SelectedItem == ShowBorrowers)
                ItemsListVeiw.ItemsSource = logic.persons.FindAll(p => p.BorrowingCount > 0);
            else
            {
                CostumerOrEmployees.SelectedItem = AllPersons;
                ItemsListVeiw.ItemsSource = logic.persons;
            }
        }
        private void SortItemsChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortItems.SelectedItem == SortByName)
                logic.Sort(new IComparerByItemName());
            else if (SortItems.SelectedItem == SortByNameReverse)
                logic.Sort(new IComparerByItemNameReverse());
            else if (SortItems.SelectedItem == SortByPrice)
                logic.Sort(new IComparerByPrice());
            else if (SortItems.SelectedItem == SortByPriceReverse)
                logic.Sort(new IComparerByPriceReverse());

            if (LibaryItemsType.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (LibaryItemsType.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else
            {
                LibaryItemsType.SelectedItem = AllItems;
                ItemsListVeiw.ItemsSource = logic.libaryItems;
            }
        }
        private void PersonsOrItemsToggle(object sender, RoutedEventArgs e)
        {
            if (!ShowPersons) //Current LibaryItems  => Persons
            {
                //Hide LibaryItems CBX show Persons CBX
                SortItems.Visibility = Visibility.Collapsed;
                LibaryItemsType.Visibility = Visibility.Collapsed;
                SortPerson.Visibility = Visibility.Visible;
                SortPerson.SelectedItem = null;
                CostumerOrEmployees.Visibility = Visibility.Visible;
                CostumerOrEmployees.SelectedItem = null;
                MyItemsOrLibary.Visibility = Visibility.Collapsed;

                PersonsOrItems.Content = "Avilable items";
            }
            else //Current Persons => LibaryItems
            {
                //Hide Persons CBX show LibaryItems CBX
                SortItems.Visibility = Visibility.Visible;
                SortItems.SelectedItem = null;
                LibaryItemsType.Visibility = Visibility.Visible;
                LibaryItemsType.SelectedItem = null;
                SortPerson.Visibility = Visibility.Collapsed;
                CostumerOrEmployees.Visibility = Visibility.Collapsed;
                ItemsListVeiw.ItemsSource = null;
                MyItemsOrLibary.Visibility = Visibility.Visible;
                PersonsOrItems.Content = "Persons";
            }
            ShowPersons = !ShowPersons;
            UpdateMenu();
        }
        private void MyItemsOrLibaryToggle(object sender, RoutedEventArgs e)
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
        private void ChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditPerson), logic);
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            Frame.Navigate(typeof(ChangePassword), logic);
            return;
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            logic.SignOut();
            Frame.Navigate(typeof(MainPage), logic);
            return;
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
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }
    }
}
