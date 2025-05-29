using System.ComponentModel.DataAnnotations;

namespace WpfSqliteTutorial.Models
{
    public class Student
    {
        [Key] public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }
    }
}
