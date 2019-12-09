using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebTrainingMVC.Models
{
    public class GetUsersContragentsResultModel
    {
        [DisplayName("Contragent Name")]
        public string ContragentName { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        [DisplayName("Vat Number")]
        public string VatNumber { get; set; }

        public int UserId { get; set; }
    }
}