using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTrainingMVC.Models
{
    public class LogInUserBindingModel
    {
        [Required]
        [DisplayName("User name")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}