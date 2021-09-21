using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace COVID_19_Vaccination_System.Models {
    public class ExternalLoginConfirmationViewModel {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запамти го овој пребарувач?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel {
        [Required(ErrorMessage = "Полето за email е задолжително")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel {
        [Required(ErrorMessage = "Полето за email е задолжително")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето за лозинка е задолжително")]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [Display(Name = "Запамти ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel {
        [Required(ErrorMessage = "Полето за email е задолжително")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето за лозинка е задолжително")]
        [StringLength(100, ErrorMessage = "{0} мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Лозинката мора да се составена од голема буква, бројка и специјален знак")]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинката и потврдата не се еднакви")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Полето за име е задолжително")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето за презиме е задолжително")]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето за матичен број е задолжително")]
        [Display(Name = "Матичен број")]
        [RegularExpression("^\\d{13}", ErrorMessage = "Невалиден матичен број")]
        public string EMBG { get; set; }

        [Required(ErrorMessage = "Полето за датум на раѓање е задолжително")]
        [Display(Name = "Дата на раѓање")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Полето за мобилен телефон е задолжително")]
        [Display(Name = "Мобилен телефон")]
        [RegularExpression("^07[0-8]\\d{6}", ErrorMessage = "Телефонскиот број не е валиден")]
        public string Number { get; set; }
    }

    public class ResetPasswordViewModel {
        [Required(ErrorMessage = "Полето за email е задолжително")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето за лозинка е задолжително")]
        [StringLength(100, ErrorMessage = "{0} мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Лозинката мора да се составена од голема буква, бројка и специјален знак")]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Лозинката мора да се составена од голема буква, бројка и специјален знак")]
        [Display(Name = "Потврди Лозинка")]
        [Compare("Password", ErrorMessage = "Лозинката и потврдата не се еднакви")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel {
        [Required(ErrorMessage = "Полето за email е задолжително")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
