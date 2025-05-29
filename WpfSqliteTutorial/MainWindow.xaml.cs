using System.Windows;
using System.Windows.Data;
using WpfSqliteTutorial.Models;

namespace WpfSqliteTutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentsDbContext dbContext = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
            DataContext = dbContext;
        }

        private void LoadStudents()
        {
            var students = dbContext.Students.ToList();
            StudentsList.ItemsSource = students;
            CollectionViewSource.GetDefaultView(StudentsList.ItemsSource).Refresh();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsList.SelectedItem is Student selectedStudent)
            {
                dbContext.SaveChanges();
                LoadStudents();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(FirstName.Text) ||
                string.IsNullOrWhiteSpace(LastName.Text) ||
                !int.TryParse(Age.Text, out int age))
                {
                    MessageBox.Show("Поля заполнены неверно");
                    return;
                }

                var student = new Student
                {
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    Age = age,
                    Group = Group.Text
                };

                dbContext.Students.Add(student);
                dbContext.SaveChanges();

                LoadStudents();

                ClearFields();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsList.SelectedItem is Student selectedStudent)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить {selectedStudent.FirstName} {selectedStudent.LastName}?", 
                    "Подтверждение удаления", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    dbContext.Students.Remove(selectedStudent);
                    dbContext.SaveChanges();
                    LoadStudents();
                }
            }
            else
            {
                MessageBox.Show("Выберите студента для удаления");
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            StudentsList.SelectedItem = null;
            FirstName.Focus();
        }

        private void ClearFields()
        {
            FirstName.Clear();
            LastName.Clear();
            Age.Clear();
            Group.Clear();
        }
    }
}
