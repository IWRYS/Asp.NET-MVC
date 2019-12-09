using System.ComponentModel.DataAnnotations;

namespace WebTrainingMVC.Models.ContragentModels
{
    public class ContragentResultModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$")]
        public string ContragentName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Adress { get; set; }

        [Required]
        [MaxLength(50)]
        //[RegularExpression(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}")]
        public string Email { get; set; }
    }
}