using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; } 

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime DateOfLastVisit { get; set; }
        public DateTime DateOfLastChange { get; set; }
        
        public UserRating Rating { get; set; }
        public UserVerification Verification { get; set; }
        public UserAdministrator Administrator { get; set; }

        public ICollection<UserPhoto> Photos { get; set; }
    }
}
