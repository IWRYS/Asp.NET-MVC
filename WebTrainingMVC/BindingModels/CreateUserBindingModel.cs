using System.ComponentModel.DataAnnotations;

namespace WebTrainingMVC.BindingModels
{
    public class CreateUserBindingModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}