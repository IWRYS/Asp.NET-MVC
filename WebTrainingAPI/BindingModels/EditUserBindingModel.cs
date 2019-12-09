using System.ComponentModel.DataAnnotations;

namespace WebTrainingAPI.BindingModels
{
    public class EditUserBindingModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
