using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace PhonesRepository
{
    public class PhonesViewModel : INotifyPropertyChanged
    {
        private Phone selectedPhone;
        private Phone newPhone;
        private string _jsonPath;

        public ObservableCollection<Phone> PhonesList { get; set; }
        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public PhonesViewModel(string jsonPath)
        {
            _jsonPath = jsonPath;
            LoadData();
        }

        public void LoadData()
        {
            FileStream fileStream = File.OpenRead(_jsonPath);
            PhonesList = JsonSerializer.Deserialize<ObservableCollection<Phone>>(fileStream);
        }

        public void PrepareNewPhone()
        {
            newPhone = new Phone();
            SelectedPhone = null;
        }

        public void AddNewPhone(string title, string company, string priceText)
        {
            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(company) ||
                !int.TryParse(priceText, out int price))
            {
                MessageBox.Show("Заполните все поля корректно");
                return;
            }

            newPhone.Title = title;
            newPhone.Company = company;
            newPhone.Price = price;

            PhonesList.Add(newPhone);
            SelectedPhone = newPhone;
            newPhone = null;
        }

        public void UpdateSelectedPhone(string title, string company, string priceText)
        {
            if (SelectedPhone == null) return;
            if (!int.TryParse(priceText, out int price))
            {
                MessageBox.Show("Введите корректную цену");
                return;
            }

            SelectedPhone.Title = title;
            SelectedPhone.Company = company;
            SelectedPhone.Price = price;
        }

        public void RemoveSelectedPhone()
        {
            if (SelectedPhone == null) return;

            PhonesList.Remove(SelectedPhone);
            SelectedPhone = null;
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

                string jsonString = JsonSerializer.Serialize(PhonesList, options);
                File.WriteAllText(_jsonPath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
