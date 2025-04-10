using System.ComponentModel.DataAnnotations;

namespace Company.Project.PL.Dtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required !! ")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
