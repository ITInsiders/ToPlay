using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP.PL.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 30 символов")]
        [RegularExpression(@"[aA-zZ][aA-zZ0-9_-]{2,29}", ErrorMessage = "Некорректный логин")]
        [System.Web.Mvc.Remote("RCheckLogin", "Home", ErrorMessage = "Данный логин уже используется")]
        [DataType(DataType.Text, ErrorMessage = "Данный тип не верный")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 30 символов")]
        [RegularExpression(@"[aA-zZ0-9]{8,30}", ErrorMessage = "Некорректный пароль")]
        [DataType(DataType.Password, ErrorMessage = "Данный тип не верный")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password, ErrorMessage = "Данный тип не верный")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Введите фамилию, имя и отчество")]
        [StringLength(92, MinimumLength = 7, ErrorMessage = "Фамилия, имя и отчество должно быть от 3 до 30 символов")]
        [RegularExpression(@"[aA-zZаА-яЯ]{3,30} [aA-zZаА-яЯ]{3,30}( [aA-zZаА-яЯ]{0,30})?", ErrorMessage = "Некорректные данные")]
        [DataType(DataType.Text, ErrorMessage = "Данный тип не верный")]
        public string Name {
            get
            {
                return FirstName + " " + SecondName + " " + MiddleName;
            }
            set
            {
                List<string> list = value.Split(' ').ToList();
                SecondName = list[0];
                FirstName = list[1];
                if (list.Count == 3) MiddleName = list[2];
            }
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите номер сотового без +7")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Номер должн состоять из 10 цифр без +7")]
        [RegularExpression(@"[9]{1}[0-9]{9}", ErrorMessage = "Некорректный номер")]
        [System.Web.Mvc.Remote("RCheckPhone", "Home", ErrorMessage = "Данный номер уже используется")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Данный тип не верный")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите почту")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Почта должна быть от 8 до 30 символов")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [System.Web.Mvc.Remote("RCheckEmail", "Home", ErrorMessage = "Данный адрес уже используется")]
        [DataType(DataType.Text, ErrorMessage = "Данный тип не верный")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите свою дату рождения")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Данный тип не верный")]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistration => DateTime.Now;
        public DateTime DateOfLastVisit => DateTime.Now;
        public DateTime DateOfLastChange => DateTime.Now;
    }
}
