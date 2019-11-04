using System.ComponentModel.DataAnnotations;

namespace SMS.Domain.Entities
{
    public class CorrectUniqId : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string uniqId = value as string;
            return uniqId == "" ? true : uniqId.Length > 5 || uniqId.Length < 17 ? true : false;
        }
    }
}
