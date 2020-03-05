using System.ComponentModel.DataAnnotations;
using SMS.Domain.Abstract;

namespace SMS.Domain.Entities
{
    public class Student : IStudent
    {
        [Key]
        public int Id { get; set; }
        // custom attribute
        [CorrectUniqId(ErrorMessage = "Поле 'Уникальный идетификатор' может быть пустым либо должно содержать от 6 до 16 символов")]
        public string UniqId { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Имя является обязательным полем")]
        [CorrectName(new string[] { "admin" }, ErrorMessage = "Недопустимое имя")] // custom attribute
        public string Name { get; set; }

        [MaxLength(60)]
        public string MiddleName { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Фамилия является обязательным полем")]        
        public string LastName { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Укажите пол")]
        public Gender Gender { get; set; } 
    }

    public enum Gender : byte
    {
        Male,
        Female
    }
}
