using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace CarsRepository
{
    public class CarsViewModel : INotifyPropertyChanged
    {
        private Car selectedCar;
        private string jsonPath;
        private string selectedBrand;

        public ObservableCollection<Car> CarsList { get; set; } = new ObservableCollection<Car>();
        public ObservableCollection<Car> FilteredCarsList { get; set; } = new ObservableCollection<Car>();
        public ObservableCollection<string> BrandsList { get; set; } = new ObservableCollection<string>();

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        public string SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
                UpdateFilter();
            }
        }

        public CarsViewModel(string jsonPath)
        {
            this.jsonPath = jsonPath;
            LoadData();
            UpdateBrandsList();
            SelectedBrand = "Все"; // Инициализация фильтра
        }

        private void LoadData()
        {
            FileStream fileStream = File.OpenRead(jsonPath);
            CarsList = JsonSerializer.Deserialize<ObservableCollection<Car>>(fileStream);
            UpdateFilter();
        }

        public void PrepareNewCar()
        {
            SelectedCar = null;
        }

        public void AddNewCar(string brand, string model, string yearText, string mileageText)
        {
            if (string.IsNullOrWhiteSpace(brand) ||
                string.IsNullOrWhiteSpace(model) || 
                !int.TryParse(yearText, out int year) || 
                !int.TryParse(mileageText, out int mileage))
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }

            var newCar = new Car
            {
                Brand = brand,
                Model = model,
                Year = year,
                Mileage = mileage
            };

            CarsList.Add(newCar);
            UpdateBrandsList();
            SelectedCar = newCar;
            SaveData();
        }

        public void UpdateSelectedCar(string brand, string model, string yearText, string mileageText)
        {
            if (SelectedCar == null) return;
            if (!int.TryParse(yearText, out int year))
            {
                MessageBox.Show("Введите корректный год");
                return;
            }
            if (!int.TryParse(mileageText, out int mileage))
            {
                MessageBox.Show("Введите корректный пробег");
                return;
            }

            SelectedCar.Brand = brand;
            SelectedCar.Model = model;
            SelectedCar.Year = year;
            SelectedCar.Mileage = mileage;
        }

        public void RemoveSelectedCar()
        {
            if (SelectedCar == null) return;

            CarsList.Remove(SelectedCar);
            UpdateBrandsList();
            SaveData();
        }

        public void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Читабельный формат с отступами
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Игнорировать null значения
                };

                var json = JsonSerializer.Serialize(CarsList, options);
                File.WriteAllText(jsonPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBrandsList()
        {
            BrandsList.Clear();
            BrandsList.Add("Все");
            foreach (var brand in CarsList.Select(c => c.Brand).Distinct())
            {
                BrandsList.Add(brand);
            }
        }

        private void UpdateFilter()
        {
            FilteredCarsList.Clear();
            var filtered = (SelectedBrand == "Все")
                ? CarsList
                : CarsList.Where(c => c.Brand == SelectedBrand);

            foreach (var car in filtered)
            {
                FilteredCarsList.Add(car);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
