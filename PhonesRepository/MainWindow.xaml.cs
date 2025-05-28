using System;
using System.Windows;

namespace PhonesRepository
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PhonesViewModel phonesViewModel;
        private string jsonPath;

        public MainWindow()
        {
            InitializeComponent();

            // Базовый каталог приложения
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Поднимаемся на 2 уровня вверх из bin\Debug\
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, @"..\.."));
            // Формируем путь к файлу относительно корня проекта
            jsonPath = System.IO.Path.Combine(projectRoot, "phonesList.json");

            phonesViewModel = new PhonesViewModel(jsonPath);
            DataContext = phonesViewModel;
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем временный новый телефон
            phonesViewModel.PrepareNewPhone();

            // Очищаем привязку и поля
            ClearFields();

            // Устанавливаем фокус на первое поле
            PhoneTitle.Focus();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (phonesViewModel.SelectedPhone == null)
            {
                MessageBox.Show("Выберите телефон для удаления");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Удалить телефон {phonesViewModel.SelectedPhone.Title}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                phonesViewModel.RemoveSelectedPhone();
                ClearFields();
                phonesViewModel.SaveData();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (phonesViewModel.SelectedPhone != null)
            {
                phonesViewModel.UpdateSelectedPhone(
                    PhoneTitle.Text,
                    PhoneCompany.Text,
                    PhonePrice.Text);
            }
            else
            {
                phonesViewModel.AddNewPhone(
                     PhoneTitle.Text,
                     PhoneCompany.Text,
                     PhonePrice.Text);
                ClearFields();
            }

            // После создания/изменения можно сразу сохранять
            phonesViewModel.SaveData();
        }

        private void ClearFields()
        {
            PhoneTitle.Clear();
            PhoneCompany.Clear();
            PhonePrice.Clear();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            phonesViewModel.SaveData();
        }
    }
}
