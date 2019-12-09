using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using System.Collections.Generic;
using DataAccess.Queries.ContragentQueries;

using WebTrainingAPI.BindingModels.ContragentBindingModels;
using WebTrainingAPI.Controllers;
using WebTrainingAPI.ResultsModels;
using WebTrainingAPI.ResultsModels.ContragentResultModels;

namespace WebTraining.Controllers
{
    public class ContragentController : BaseController
    {
        [HttpGet]
        public List<GetUsersAllContragentsResultModel> GetUsersContragents(int id)
        {
            var userContragents = new List<GetUsersAllContragentsResultModel>();
            try
            {
                if (id > 0)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                    {
                        SqlCommand command = new SqlCommand(ContragentQueries.GetUsersContragents, connection);
                        command.Parameters.AddWithValue("@userId", id);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string name = (string)(reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString());
                            string adress = (string)(reader["Adress"] == DBNull.Value ? "" : (reader["Adress"].ToString()));
                            string email = (string)(reader["Email"] == DBNull.Value ? "" : (reader["Email"].ToString()));
                            string vat = (string)(reader["VatNumber"] == DBNull.Value ? "" : (reader["VatNumber"].ToString()));


                            var contragent = new GetUsersAllContragentsResultModel()
                            {
                                ContragentName = name,
                                Adress = adress,
                                Email = email,
                                VatNumber = vat
                                
                            };

                            userContragents.Add(contragent);
                        }

                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return userContragents;
        }

        [HttpPost]
        public AddContragentResultModel AddContragent([FromBody]AddContragentBindingModel model)
        {
            var contragent = new AddContragentResultModel();

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                    {
                        SqlCommand command = new SqlCommand(ContragentQueries.AddContragentToUser, connection);
                        command.Parameters.AddWithValue("@inContragentName", model.ContragentName);
                        command.Parameters.AddWithValue("@inAdress", model.Adress);
                        command.Parameters.AddWithValue("@inEmail", model.Email);
                        command.Parameters.AddWithValue("@inVatNumber", model.VatNumber);
                        command.Parameters.AddWithValue("@inUserId", model.UserId);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string name = (string)(reader["ContragentName"] == DBNull.Value ? "" : reader["ContragentName"].ToString());
                            string adress = (string)(reader["Adress"] == DBNull.Value ? "" : reader["Adress"].ToString());
                            string mail = (string)(reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString());
                            string vatNum = (string)(reader["VatNumber"] == DBNull.Value ? "" : reader["VatNumber"].ToString());
                            int userId = (int)(reader["UserId"] == DBNull.Value ? -1 : int.Parse(reader["UserId"].ToString()));

                            int code = (int)(reader["Code"] == DBNull.Value ? -1 : int.Parse(reader["Code"].ToString()));
                            string message = (string)(reader["Msg"] == DBNull.Value ? "" : (reader["Msg"].ToString()));

                            contragent = new AddContragentResultModel()
                            {
                                ContragentName = name,
                                Adress = adress,
                                Email = mail,
                                VatNumber = vatNum,
                                UserId = userId
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }


            return contragent;
        }

        [HttpGet]
        public List<GetAllContragentsResultModel> GetAllContragents()
        {
            var contragents = new List<GetAllContragentsResultModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.connectionString))
                {
                    SqlCommand command = new SqlCommand(ContragentQueries.GetAllContragents, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = (string)(reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString());
                        string adress = (string)(reader["Adress"] == DBNull.Value ? "" : reader["Adress"].ToString());
                        string mail = (string)(reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString());

                        var contragent = new GetAllContragentsResultModel()
                        {
                            ContragentName = name,
                            Adress = adress,
                            Email = mail,
                        };

                        contragents.Add(contragent);
                    }
                }
            }
            catch (Exception ex)
            {

                var error = ex.Message;
            }



            return contragents;
        }
    }
}