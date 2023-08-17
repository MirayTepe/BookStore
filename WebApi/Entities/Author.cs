using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{

    public class Author{
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id {get; set;}
      public string FirstName {get; set;}

        public string LastName {get; set;}

       public DateTime DateOfBirth { get; set; }

       public Author(){}

        public Author(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
         public override string ToString()
        {
            return new string(FirstName + " "+ LastName + " BD: "+DateOfBirth.Date);
        }

    }
}