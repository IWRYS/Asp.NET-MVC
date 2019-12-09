using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using WebTrainingMVC.Models;

namespace WebTrainingMVC
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new System.NotImplementedException();

        public override bool EnablePasswordReset => throw new System.NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new System.NotImplementedException();

        public override string ApplicationName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new System.NotImplementedException();

        public override int PasswordAttemptWindow => throw new System.NotImplementedException();

        public override bool RequiresUniqueEmail => throw new System.NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new System.NotImplementedException();

        public override int MinRequiredPasswordLength => throw new System.NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new System.NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new System.NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            CustomMembershipUser resultUser = new CustomMembershipUser();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.GetAsync($"user/getuserbyid/{providerUserKey}");
                    responseTask.Wait();
                            
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<UserViewModel>();
                        readTask.Wait();

                       var user = readTask.Result;

                        resultUser.Data = user; 
                    }
                    else
                    {

                    }
                }

            }
            catch(Exception ex)
            {
                var err = ex.Message;
            }

            return resultUser;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var resultUser = new CustomMembershipUser();
            try
            {
       

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.GetAsync($"user/GetUserByUserNameAndPassword?username={username}");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CustomMembershipUser>();
                        readTask.Wait();

                        resultUser = readTask.Result;
                    }
                  
                }
            }
            catch (Exception ex)
            {

                var exception = ex;
            }
            return resultUser;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {


            try
            {
                var model = new LogInUserBindingModel()
                {
                    Username = username,
                    Password = password
                };


                using (var client = new HttpClient())
                {
                    string stringData = JsonConvert.SerializeObject(model);
                    var contentData = new StringContent
                    (stringData, System.Text.Encoding.UTF8,
                     "application/json");

                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.PostAsync("user/UserLogin", contentData);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<LogInUserBindingModel>();
                        readTask.Wait();

                        model = readTask.Result;
                    }

                }

                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password) )
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return true;


        }
    }
}