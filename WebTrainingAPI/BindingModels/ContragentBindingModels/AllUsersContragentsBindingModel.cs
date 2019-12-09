using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTrainingAPI.BindingModels.ContragentBindingModels
{
    public class AllUsersContragentsBindingModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Adress { get; set; }
    }
}
