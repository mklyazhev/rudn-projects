using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PhonesRepositoryLogger
{
    public class PhonesViewModel : INotifyPropertyChanged
    {
        private readonly Logger _logger;
        public Logger Logger { get { return _logger; } }

        private Phone selectedPhone;
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

        public PhonesViewModel(string phonesListFilePath, Logger logger)
        {
            _logger = logger;

            try
            {
                FileStream fileStream = File.OpenRead(phonesListFilePath);
                PhonesList = JsonSerializer.Deserialize<ObservableCollection<Phone>>(fileStream);
                _logger.Log($"Successfully loaded phones from {phonesListFilePath}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error loading phones: {ex.Message}");
                PhonesList = new ObservableCollection<Phone>();
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
