using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTrainingAPI.ResultsModels
{
    public class GetUsersAllContragentsResultModel
    {
        [Required]
        [MaxLength(50)]
        public string ContragentName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Adress { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(12)]
        public string VatNumber { get; set; }

    }
}
