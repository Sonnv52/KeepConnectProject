using System.ComponentModel.DataAnnotations;

namespace Chat.Application.Models.UserApp
{
    public class SignUpUserModel
    {
        public string? Name { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Email",
        ErrorMessage = "Verify email is invalid")]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        //[DataType(DataType.Password)]
        //   [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? PasswordConfirmed { get; set; }
        public string? Adress { get; set; }
    }
}
