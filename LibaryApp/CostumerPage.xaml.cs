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
    /// The page let the user look for a libary items and borrow
    /// </summary>
    public sealed partial class CostumerPage : Page
    {
        public Logic logic;
        private bool _viewToggle;
        public CostumerPage()
        {
            this.InitializeComponent();
            Sort.SelectionChanged += SortComparerChanged;
            Type.SelectionChanged += Type_SelectionChanged;
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            ChangeDetails.Click += ChangeDetails_Click;
            ItemsListVeiw.IsItemClickEnabled = true;
            ItemsListVeiw.ItemClick += ItemsListVeiw_ItemClick;
            View.Click += ViewToggle;
            _viewToggle = false;
        }

        //AppBarButton functions
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            logic.ClearItem();
            logic.SignOut();
            Frame.Navigate(typeof(MainPage), logic);
            return;
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

        //List veiw functions
        public void UpdateMenu()
        {
            ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib.IsBorrowed == false);
        }
        private void ItemsListVeiw_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                logic.ChooseItem(e.ClickedItem);
            }
            catch (NullReferenceException ex)
            {
                ShowAlert(ex.Message);
            }
            Frame.Navigate(typeof(LibaryItemOrderPage), logic);
        }

        //Sorting or show partion of libary items
        private void ViewToggle(object sender, RoutedEventArgs e)
        {
            if (!_viewToggle) //Current My items => libary
            {
                ItemsListVeiw.ItemsSource = logic.MyItems();
                View.Content = "Avilable items";
            }
            else //Current libary => My items
            {
                ItemsListVeiw.ItemsSource = null;
                ItemsListVeiw.ItemsSource = logic.libaryItems;
                View.Content = "My items";
            }
            _viewToggle = !_viewToggle;
        }
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Type.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (Type.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else if (Type.SelectedItem == All)
                ItemsListVeiw.ItemsSource = logic.libaryItems;
        }
        private void SortComparerChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sort.SelectedItem == SortByName)
                logic.Sort(new IComparerByItemName());
            else if (Sort.SelectedItem == SortByNameReverse)
                logic.Sort(new IComparerByItemNameReverse());
            else if (Sort.SelectedItem == SortByPrice)
                logic.Sort(new IComparerByPrice());
            else if (Sort.SelectedItem == SortByPriceReverse)
                logic.Sort(new IComparerByPriceReverse());
            else
                return;

            if (Type.SelectedItem == OnlyBooks)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Book);
            else if (Type.SelectedItem == OnlyJournals)
                ItemsListVeiw.ItemsSource = logic.libaryItems.FindAll(lib => lib is Journal);
            else
                UpdateMenu();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic != null && logic.Signed is Costumer)
            {
                logic.UpdateLogicLists();
                UpdateMenu();
                logic.ClearItem();
            }
            else
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }
            Congrat.Text = logic.GetName;
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }

    }

}

