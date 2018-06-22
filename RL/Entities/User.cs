using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP.ML.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime DateOfRegistration { get; set; }
        public DateTime DateOfLastVisit { get; set; }
        public DateTime DateOfLastChange { get; set; }

        public User()
        {
            this.DateOfRegistration = DateTime.Now;
            this.DateOfLastVisit = DateTime.Now;
            this.DateOfLastChange = DateTime.Now;
        }

        public virtual List<Comment> PostedComments { get; set; }
        public virtual List<UserComment> AcceptedComments { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<UserImage> Images { get; set; }
        public UserImage MainImage => Images?.FirstOrDefault(x => x.Main) ??
            new UserImage() { Main = true, URL = "/Resources/IMG/System/ToPlayFox_250px.svg", User = this, UserId = this.Id };

        protected virtual object Child => this;
        public T Get<T>() where T : User, new() => this.Child is T ? (T)this.Child : null;
    }
}
