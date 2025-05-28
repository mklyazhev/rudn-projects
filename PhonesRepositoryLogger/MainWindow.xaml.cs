using System.Windows;

namespace PhonesRepositoryLogger
{
    public partial class MainWindow : Window
    {
        private PhonesViewModel phonesViewModel;
        public MainWindow(PhonesViewModel viewModel)
        {
            InitializeComponent();
            phonesViewModel = viewModel;
            DataContext = phonesViewModel;
        }

        private void Price_Edit(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PhonePrice.Text, out int newPrice))
            {
                phonesViewModel.SelectedPhone.Price = newPrice;
                phonesViewModel.Logger.Log($"Price changed to {newPrice}");
            }
        }
    }
}
