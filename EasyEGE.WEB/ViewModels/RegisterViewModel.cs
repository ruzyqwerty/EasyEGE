using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyEGE.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введён некорректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введён некорректное имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Роль")]
        public List<IdentityRole> AllRoles { get; set; }

        public RegisterViewModel()

        {
            AllRoles = new List<IdentityRole>();
        }

        [Display(Name = "Ошибка")]
        public string Error { get; set; }
    }
}
