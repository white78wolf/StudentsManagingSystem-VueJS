using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SMS.Domain.Entities
{
    public class CorrectName : ValidationAttribute
    {
        string[] _names;

        public CorrectName(string[] names)
        {
            _names = names;
        }

        public override bool IsValid(object value)
        {
            return !_names.Contains(value.ToString().ToLower()) ? true : false;
        }
    }
}
