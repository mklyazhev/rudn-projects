using Microsoft.EntityFrameworkCore;

namespace WpfSqliteTutorial.Models
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
            }
        }

        public StudentsDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
            "Data Source = university.db");
        }
    }
}
