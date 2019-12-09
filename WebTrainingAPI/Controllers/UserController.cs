using DataAccess;
using DataAccess.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

using WebTrainingAPI.BindingModels;
using WebTrainingAPI.Controllers;
using WebTrainingAPI.Results;
using WebTrainingAPI.ResultsModels;

namespace WebTraining.Controllers
{
   

    public class UserController : BaseController
    {
        [HttpGet]
        public GetUserByIdResultModel GetUserById(int id)
        {
            var user = new GetUserByIdResultModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                {
                    SqlCommand command = new SqlCommand(UserQueries.GetUserByIdQuery, connection);
                    command.Parameters.AddWithValue("@inUserId", id);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = (string)(reader["User"] == DBNull.Value ? "" : reader["User"].ToString());
                        int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                        string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));


                        user = new GetUserByIdResultModel()
                        {
                            Code = code,
                            Message = message,
                            Username = username
                        };
                    }

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return user;
        }

        [HttpGet]
        public int GetUserByUserNameAndPassword(string username)
        {
            GetUserByUsernameAndPasswordResultModel user = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                {
                    SqlCommand command = new SqlCommand(UserQueries.GetUserByUsernameAndPassword, connection);
                    command.Parameters.AddWithValue("@inUsername", username);
                    //command.Parameters.AddWithValue("@inPassword", password);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                        string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));
                        int UserId = (int)(reader["UserId"] == DBNull.Value ? -1 : int.Parse(reader["UserId"].ToString()));

                        user = new GetUserByUsernameAndPasswordResultModel()
                        {
                            Code = code,
                            Message = message,
                            UserId = UserId
                        };

                    }

                    reader.Close();

                }

            }
            catch (Exception ex)
            {
                var error = ex;
            }


            return user.UserId;
        }

         [HttpPost]
        public CreateUserResultModel CreateUser([FromBody]CreateUserBindingModel userModel)
        {
            var newUser = new CreateUserResultModel();

            if (ModelState.IsValid)
            {
                newUser.Username = userModel.Username;
                newUser.Password = userModel.Password;
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                    {
                        SqlCommand command = new SqlCommand(UserQueries.CreateUser, connection);
                        command.Parameters.AddWithValue("@inUserame", newUser.Username);
                        command.Parameters.AddWithValue("@inPassword", newUser.Password);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string pass = (string)(reader["Password"] == DBNull.Value ? "" : reader["Password"].ToString());
                            string user = (string)(reader["User"] == DBNull.Value ? "" : reader["User"].ToString());

                            int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                            string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));


                            newUser = new CreateUserResultModel()
                            {
                                Code = code,
                                Message = message,
                                Username = user,
                                Password = pass

                            };
                        }

                        reader.Close();

                    }
                }
                catch (Exception ex)
                {

                    var error = ex.Message;
                }
            }



            return newUser;
        }

         [HttpDelete]
        public DeleteUserResultModel DeleteUser(int id)
        {
            DeleteUserResultModel deletedUser = null;

            string result = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                {
                    SqlCommand command = new SqlCommand(UserQueries.DeleteUser, connection);
                    command.Parameters.AddWithValue("@inUserId", id);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = (string)(reader["User"] == DBNull.Value ? "" : reader["User"].ToString());
                        int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                        string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));


                        deletedUser = new DeleteUserResultModel
                        {
                            Code = code,
                            Message = message,
                            Username = username
                        };

                    }

                    reader.Close();

                }

            }
            catch (Exception ex)
            {
                var error = ex;
            }


            return deletedUser;
        }

         [HttpPost]
        public EditUserResultModel EditUser([FromBody]EditUserBindingModel model)
        {
            var editedUser = new EditUserResultModel();

            if (ModelState.IsValid)
            {
                editedUser.Username = model.Username;
                editedUser.Password = model.Password;
                editedUser.UserId = model.UserId;

                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                    {
                        SqlCommand command = new SqlCommand(UserQueries.EditUser, connection);
                        command.Parameters.AddWithValue("@inUserId", editedUser.UserId);
                        command.Parameters.AddWithValue("@inUsername", editedUser.Username);
                        command.Parameters.AddWithValue("@inPassword", editedUser.Password);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));

                            string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));

                            string username = (string)(reader["User"] == DBNull.Value ? "" : (reader["User"].ToString()));

                            string pass = (string)(reader["Password"] == DBNull.Value ? "" : (reader["Password"].ToString()));

                            int userId = (int)(reader["UserId"] == DBNull.Value ? -1 : int.Parse((reader["UserId"].ToString())));

                            editedUser = new EditUserResultModel
                            {
                                Username = username,
                                Message = message,
                                Code = code,
                                UserId = userId,
                                Password = pass
                            };

                        }

                        reader.Close();


                    }

                }
                catch (Exception ex)
                {
                    var error = ex;
                }
            }
     

            return editedUser;
        }

         [HttpPost]
        public LoginResultModel UserLogin([FromBody]LogInUserBindingModel model)
        {
            var login = new LoginResultModel();

            if (ModelState.IsValid)
            {
                login.Username = model.Username;
                login.Password = model.Password;

                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                    {
                        SqlCommand command = new SqlCommand(UserQueries.UserLogin, connection);
                        command.Parameters.AddWithValue("@inUsername", login.Username);
                        command.Parameters.AddWithValue("@inPassword", login.Password);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));
                            int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                            string user = (string)(reader["User"] == DBNull.Value ? "" : (reader["User"].ToString()));
                            string pass = (string)(reader["Password"] == DBNull.Value ? "" : (reader["Password"].ToString()));


                            login = new LoginResultModel()
                            {
                                Username = user,
                                Password = pass
                            };
                        }

                        reader.Close();

                    }

                }
                catch (Exception ex)
                {
                    var error = ex;
                }
            }

            return login;
        }


       
    }
}