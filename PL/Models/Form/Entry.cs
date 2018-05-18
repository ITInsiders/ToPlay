using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP.PL.Models
{
    public class Entry
    {
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 30 символов")]
        [RegularExpression(@"[aA-zZ][aA-zZ0-9_-]{2,29}", ErrorMessage = "Некорректный логин")]
        [System.Web.Mvc.Remote("ECheckLogin", "Home", ErrorMessage = "Данного логина не существует")]
        [DataType(DataType.Text, ErrorMessage = "Данный тип не верный")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 30 символов")]
        [RegularExpression(@"[aA-zZ0-9]{8,30}", ErrorMessage = "Некорректный пароль")]
        [DataType(DataType.Password, ErrorMessage = "Данный тип не верный")]
        public string Password { get; set; }
    }
}
