using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace COVID_19_Vaccination_System.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова Лозинка")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди нова лозинка")]
        [Compare("NewPassword", ErrorMessage = "Новата лозинка и потврдата не се еднакви.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Сегашна лозинка")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова лозинка")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвррди нова лознка")]
        [Compare("NewPassword", ErrorMessage = "Новата лозинка и потврдата не се еднакви.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Телефонски број")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Телефонски број")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class EditUserRoleViewModel
    {
        [Required(ErrorMessage = "Полето за email не смее да е празно!")]
        [EmailAddress(ErrorMessage = "Невалиден email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Улоги")]
        public readonly List<string> Roles = new List<string>() {
            "Administrator",
            "Doctor",
            "User"
        };

        public string SelectedRole { get; set; }
    }
}