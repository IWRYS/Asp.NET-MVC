using WebTrainingAPI.Results;

namespace WebTrainingAPI.ResultsModels
{
    public class CreateUserResultModel : BaseResultModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
