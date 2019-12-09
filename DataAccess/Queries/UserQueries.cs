namespace DataAccess.Queries
{
   public static class UserQueries
    {

        public static string GetUserByIdQuery = $"exec GetUserById @inUserId";
        public static string CreateUser = $"exec udp_CreateUser @inUserame,@inPassword";
        public static string DeleteUser = $"exec DeleteUser @inUserId";
        public static string EditUser = $"exec EditUser @inUserId,@inUsername,@inPassword";
        public static string GetUserByUsernameAndPassword = $"exec GetUserByNameAndPassword @inUsername";
        public static string UserLogin = $"exec Login @inUsername,@inPassword";





    }
}
