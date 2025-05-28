using System;
using System.Runtime.Remoting.Lifetime;
using System.Windows;
using System.Windows.Controls;

namespace CarsRepository
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarsViewModel carsViewModel;
        private string jsonPath;

        public MainWindow()
        {
            InitializeComponent();

            // Базовый каталог приложения
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Поднимаемся на 2 уровня вверх из bin\Debug\
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, @"..\.."));
            // Формируем путь к файлу относительно корня проекта
            jsonPath = System.IO.Path.Combine(projectRoot, "carsList.json");

            carsViewModel = new CarsViewModel(jsonPath);
            DataContext = carsViewModel;
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            carsViewModel.PrepareNewCar();
            ClearFields();

            // Устанавливаем фокус на первое поле
            Brand.Focus();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (carsViewModel.SelectedCar == null)
            {
                MessageBox.Show("Выберите автомобиль!");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Удалить {carsViewModel.SelectedCar.Brand} {carsViewModel.SelectedCar.Model}?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                carsViewModel.RemoveSelectedCar();
                ClearFields();
                carsViewModel.SaveData();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (carsViewModel.SelectedCar != null)
            {
                carsViewModel.UpdateSelectedCar(
                     Brand.Text,
                     Model.Text,
                     Year.Text,
                     Mileage.Text);
            }
            else
            {
                carsViewModel.AddNewCar(
                     Brand.Text,
                     Model.Text,
                     Year.Text,
                     Mileage.Text);
                ClearFields();
            }

            // После создания/изменения можно сразу сохранять
            carsViewModel.SaveData();
        }

        private void ClearFields()
        {
            Brand.Clear();
            Model.Clear();
            Year.Clear();
            Mileage.Clear();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            carsViewModel.SaveData();
        }
    }
}