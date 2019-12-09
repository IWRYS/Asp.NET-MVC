
namespace DataAccess.Queries.ContragentQueries
{
   public class ContragentQueries
    {
        public static string GetUsersContragents = $"SELECT * FROM udf_GetAllUsersContragents(@userId)";
        public static string AddContragentToUser = $"EXEC udp_AddContragentToUser @inContragentName,@inAdress,@inEmail,@inVatNumber,@inUserId";
        public static string GetAllContragents = $"SELECT * FROM udf_GetAllContragents ()";

    }
}
