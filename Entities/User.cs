using System.ComponentModel.DataAnnotations;

namespace ManageFinances.Entities
{
    public class User
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [Range(1,16, ErrorMessage = "The name must be 1 to 16 characters length")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "The name must be at least 8 characters length")]
        [MaxLength(40, ErrorMessage = "Your password is too long")]
        public string? Password { get; set; }
    }
}