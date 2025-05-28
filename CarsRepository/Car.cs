using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarsRepository
{
    public class Car : INotifyPropertyChanged
    {
        private string model;
        private string brand;
        private int year;
        private int mileage;

        public string Model
        {
            get { return model; }
            set 
            { 
                model = value; 
                OnPropertyChanged("Model"); 
            }
        }

        public string Brand
        {
            get { return brand; }
            set 
            { 
                brand = value; 
                OnPropertyChanged("Brand"); 
            }
        }

        public int Year
        {
            get { return year; }
            set 
            { 
                year = value; 
                OnPropertyChanged("Year"); 
            }
        }

        public int Mileage
        {
            get { return mileage; }
            set 
            { 
                mileage = value; 
                OnPropertyChanged("Mileage"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
