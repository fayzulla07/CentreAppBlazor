using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentreAppBlazor.Shared.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(50, ErrorMessage = "Логин должно быть с максимальной длиной 50.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Пароль должно быть с минимальной длиной 3 и максимальной длиной 50.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}
