using System.ComponentModel.DataAnnotations;

namespace WebTrainingMVC.BindingModels
{
    public class EditUserBindingModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

    }
}