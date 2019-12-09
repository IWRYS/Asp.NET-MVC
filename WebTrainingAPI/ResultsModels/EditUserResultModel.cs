using WebTrainingAPI.Results;

namespace WebTrainingAPI.ResultsModels
{
    public class EditUserResultModel : BaseResultModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
