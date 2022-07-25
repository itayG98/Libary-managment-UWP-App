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
    /// Only employye can acces this page
    /// validate and create new Libary Item in the MoockData class
    /// </summary>
    public sealed partial class AddItem : Page
    {
        public Logic logic;
        TextBox Price, Editors, ItemsName, SerialNum, Aouthors, Description, DiscountPercentage;
        ComboBox Freq, Publishers, Country;
        DatePicker PrintDate;
        bool? IsBook = null;

        public AddItem()
        {
            this.InitializeComponent();
            SignOut.Click += SignOut_Click;
            ChangePassword.Click += ChangePassword_Click;
            Libary.Click += Libary_Click;
            Submit.Click += Submit_Click;
            BookBtn.Click += BookBtn_Click;
            JournalBtn.Click += JournalBtn_Click;
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
            logic.SignOut();
            Frame.Navigate(typeof(MainPage), logic);
        }

        //Choose book or journal
        private void JournalBtn_Click(object sender, RoutedEventArgs e)
        {
            IsBook = false;
            Congrat.Text = "Create Journal";
            JournalBtn.Visibility = Visibility.Collapsed;
            BookBtn.Visibility = Visibility.Visible;
            LeftDeitails.Children.Clear();
            LeftDeitails.RowDefinitions.Clear();
            RightDeitails.Children.Clear();
            RightDeitails.RowDefinitions.Clear();
            GenerateJournalFields();
        }
        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            IsBook = true;
            Congrat.Text = "Create Book";
            JournalBtn.Visibility = Visibility.Visible;
            BookBtn.Visibility = Visibility.Collapsed;
            LeftDeitails.Children.Clear();
            LeftDeitails.RowDefinitions.Clear();
            RightDeitails.Children.Clear();
            RightDeitails.RowDefinitions.Clear();
            GenerateBookFields();
        }

        //Generate apropriate fields
        private void GenerateJournalFields()
        {
            //public Journal( name,  printedDate,  frequancy,  price , string editors)
            for (int i = 0; i < 3; i++)
            {
                LeftDeitails.RowDefinitions.Add(new RowDefinition());
                RightDeitails.RowDefinitions.Add(new RowDefinition());
            }
            //Left Side
            ItemsName = new TextBox() { Header = "Name" };
            Price = new TextBox() { Header = "Price" };
            Freq = new ComboBox() { Header = "Frequancy", ItemsSource = Enum.GetNames(typeof(Frequancy)) };
            Grid.SetRow(ItemsName, 0);
            Grid.SetRow(Price, 1);
            Grid.SetRow(Freq, 2);
            LeftDeitails.Children.Add(ItemsName);
            LeftDeitails.Children.Add(Price);
            LeftDeitails.Children.Add(Freq);
            //Right Side
            Editors = new TextBox() { Header = "Editoers" };
            PrintDate = new DatePicker() { MaxYear = DateTime.Now, Header = "Print Date" };
            Description = new TextBox() { Header="Description", TextWrapping = TextWrapping.Wrap };
            Grid.SetRow(Editors, 0);
            Grid.SetRow(PrintDate, 1);
            Grid.SetRow(Description, 2);
            RightDeitails.Children.Add(Editors);
            RightDeitails.Children.Add(PrintDate);
            RightDeitails.Children.Add(Description);
            RightDeitails.RowDefinitions[2].MinHeight = 150;
        }
        private void GenerateBookFields()
        {
            //public Book(int publisher, int serialNum ,int country = 965,  name,  printedDate,  authors,  description = "info", double price = DEFAULT_BOOK_PRICE, double discountRate = 0) 
            for (int i = 0; i < 5; i++)
            {
                LeftDeitails.RowDefinitions.Add(new RowDefinition());
                RightDeitails.RowDefinitions.Add(new RowDefinition());
            }
            //Left Side
            ItemsName = new TextBox() { Header = "Name" };
            Publishers = new ComboBox { Header = "Publishers Code", ItemsSource = ISBN.PublishersDict.Values };
            Country = new ComboBox() { Header = "Country Code", ItemsSource = ISBN.CountriesDict.Values };
            SerialNum = new TextBox() { Header = "Serial num Code" };
            PrintDate = new DatePicker() { MaxYear = DateTime.Now, Header = "Print Date" };
            Grid.SetRow(ItemsName, 0);
            Grid.SetRow(Publishers, 1);
            Grid.SetRow(Country, 2);
            Grid.SetRow(SerialNum, 3);
            Grid.SetRow(PrintDate, 4);
            LeftDeitails.Children.Add(ItemsName);
            LeftDeitails.Children.Add(Publishers);
            LeftDeitails.Children.Add(Country);
            LeftDeitails.Children.Add(SerialNum);
            LeftDeitails.Children.Add(PrintDate);
            //Diffult values
            Publishers.SelectedIndex = 0;
            Country.SelectedIndex = 0;

            //Right Side
            Aouthors = new TextBox { Header = "Aouthors" };
            Description = new TextBox { Header = "Descreption",TextWrapping=TextWrapping.Wrap };
            Price = new TextBox() { Header = "Price" };
            DiscountPercentage = new TextBox() { Header = "Discount Percentage" };
            Grid.SetRow(Price, 0);
            Grid.SetRow(DiscountPercentage, 1);
            Grid.SetRow(Aouthors, 2);
            Grid.SetRow(Description, 3);
            RightDeitails.Children.Add(Aouthors);
            RightDeitails.Children.Add(Description);
            RightDeitails.Children.Add(Price);
            RightDeitails.Children.Add(DiscountPercentage);
            RightDeitails.RowDefinitions[3].MinHeight = 150;
        }

        //Sumbit and validate
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (IsBook != null && IsBook == true)
            {
                if (TryAddBook())
                {
                    logic.UpdateLogicLists();
                    ShowAlert($"succesfully added Journal {ItemsName.Text}");
                    Frame.Navigate(typeof(AddItem), logic);
                }
            }
            else if (IsBook != null && IsBook == false)
            {
                if (TryAddJournal())
                {
                    logic.UpdateLogicLists();
                    ShowAlert($"succesfully added Book {ItemsName.Text}");
                    Frame.Navigate(typeof(AddItem), logic);
                }
            }
        }
        private bool TryAddJournal()
        {
            if (!double.TryParse(Price.Text, out double price))
                return false;
            if (!logic.NameValidity(ItemsName.Text))
                return false;
            if (Freq.SelectedItem == null || PrintDate.SelectedDate == null)
                return false;
            if (!Enum.TryParse(typeof(Frequancy), Freq.SelectedItem.ToString(), out Object frequancyObj))
                return false;
            Frequancy frequancy = (Frequancy)frequancyObj;
            if (logic.AddJournal(ItemsName.Text, PrintDate.Date.DateTime, frequancy, Editors.Text, price) != null)
                return true;
            else
                return false;
        }
        private bool TryAddBook()
        {
            //public Book(int publisher, int serialNum ,int country = 965,  name,  printedDate,  authors,  description = "info", double price = DEFAULT_BOOK_PRICE, double discountRate = 0) 

            if (!int.TryParse(Publishers.SelectedValue.ToString(), out int PublisherCode))
                return false;
            if (!int.TryParse(SerialNum.Text, out int SerialCode))
                return false;
            if (!int.TryParse(Country.SelectedItem.ToString(), out int CountryCode))
                return false;
            if (!logic.NameValidity(ItemsName.Text))
                return false;
            if (PrintDate.Date.Date == null)
                return false;
            if (!logic.NameValidity(Aouthors.Text))
                return false;
            if (!double.TryParse(Price.Text, out double price))
                return false;
            if (!double.TryParse(DiscountPercentage.Text, out double discount))
                return false;
            LibaryItem temp = logic.AddBook(PublisherCode, SerialCode, ItemsName.Text, PrintDate.Date.Date, Aouthors.Text, Description.Text, price, discount, CountryCode);
            if (temp != null)
            {
                temp.Description = Description.Text;
                return true;
            }
            else
                return false;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logic = e.Parameter as Logic;
            if (logic == null || logic.Signed as Employye == null)
            {
                logic.SignOut();
                Frame.Navigate(typeof(MainPage), logic);
            }
        }
        public async void ShowAlert(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }

    }
}
