namespace SMS.Domain.Abstract
{
    public interface IStudent : IEntity
    {       
        string UniqId     { get; set; } 
        string Name       { get; set; } 
        string MiddleName { get; set; } 
        string LastName   { get; set; }         
    }    
}
