using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTrainingAPI.Results;

namespace WebTrainingAPI.ResultsModels
{
    public class DeleteUserResultModel : BaseResultModel
    {
        public string Username { get; set; }
    }
}
