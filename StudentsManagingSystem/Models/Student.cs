using System.ComponentModel.DataAnnotations;

namespace StudentsManagingSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        public string UniqId { get; set; }
        
        [Required(ErrorMessage = "Имя является обязательным полем")]
        public string Name { get; set; }
        
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Фамилия является обязательным полем")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите пол")]
        public string Gender { get; set; }
    }
}
