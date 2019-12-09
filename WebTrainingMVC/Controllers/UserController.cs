using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebTrainingMVC.BindingModels;
using WebTrainingMVC.Models;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;
using MonetaWMSMobile.SystemClasses;
using System.Web.Security;

namespace WebTrainingMVC.Controllers
{
    public class UserController : Controller
    {

        // GET: User By ID
        public ActionResult GetUserById(int? id)
        {
            var resultUser = new UserViewModel();
            if (id > 0)
            {
                CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(id, true);
                resultUser.Username = user.Data.Username;
            }

            if (resultUser != null)
            {
                return View(resultUser);
            }

            return View("~/Views/Home/Index.cshtml");

        }


        // GET: User By Username and password

        public int GetUserByUsernameAndpassword(string user)
        {
            int userId = 0;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.GetAsync($"user/GetUserByUserNameAndPassword?username={user}");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<int>();
                        readTask.Wait();

                        userId = readTask.Result;
                    }

                }
            }
            catch (Exception ex)
            {

                var exception = ex;
            }
            return userId;
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View("CreateUserView");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateUser(CreateUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                CreateUserResultModel newUser = new CreateUserResultModel
                {
                    Username = model.Username,
                    Password = model.Password
                };

                try
                {
                    //var post = WebRequestManager.HttpPost("http://localhost:52680/user/createuser/",model);



                    using (HttpClient client = new HttpClient())
                    {

                        //  string stringData = JsonConvert.SerializeObject(model);
                        //  var contentData = new StringContent
                        //  (stringData, System.Text.Encoding.UTF8,
                        //   "application/json");


                        // var json = new JavaScriptSerializer().Serialize(newUser);
                        // var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                        // var res = client.PostAsJsonAsync("http://localhost:52680/user/createuser", content).Result;

                        //  client.BaseAddress = new Uri("http://localhost:52680/user/createuser");

                        var postTask = client.PostAsJsonAsync("http://localhost:52680/user/createuser/", newUser);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Title = "Success. Log in.";

                            return RedirectToAction("UserLogin", "User");
                        }
                        //  WebRequestManager.HttpPost("http://localhost:52680/user/createuser/", newUser);

                    }

                }
                catch (Exception ex)
                {

                    var err = ex;
                }
            }


            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditUser()
        {

            return View("EditUser");
        }

        [HttpPost]
        public ActionResult EditUser(EditUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                EditUserResultModel editedUser = new EditUserResultModel
                {
                    UserId = model.UserId,
                    Username = model.Username,
                    Password = model.Password
                };

                try
                {
                    // using (HttpClient client = new HttpClient())
                    // {
                    //     var postTask = client.PostAsJsonAsync("http://localhost:52680/user/edituser/", editedUser);
                    //     postTask.Wait();
                    //
                    //     var result = postTask.Result;
                    // }

                    WebRequestManager.HttpPost("http://localhost:52680/user/edituser/", editedUser);
                }
                catch (Exception ex)
                {

                    var error = ex;
                }


            }

            return View("~/Views/Home/Index.cshtml");

        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View("LoginView");
        }

        [AllowAnonymous]
        public ActionResult UserLogin(LogInUserBindingModel model)
        {
            if (ModelState.IsValid)
            {

                if (new CustomMembershipProvider().ValidateUser(model.Username, model.Password))
                {
                    //  var cookie = new HttpCookie("Login");
                    //  cookie["username"] = model.Username;
                    //  cookie.Expires.AddHours(1);
                    //  Response.Cookies.Add(cookie);
                    //  Response.Redirect("UserHomePage");

                    //Session["login_user"] = "[username]";
                    //string username = Session["login_user"].ToString().Trim();

                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    // return RedirectToAction("userLogin", "User");

                        Session["loggedUser"] = model.Username;
                        

                    return RedirectToAction("GetUsersContragents");
                    
                }
                else
                {
                    ModelState.AddModelError("", "the user name or password provided is incorrect.");
                    ViewBag.Title = "The user name or password provided is incorrect.";
                    return View("LogInView");

                }
            }

            return RedirectToAction("userLogIn");

        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
     
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUsersContragents()
        {
            var userId = GetUserByUsernameAndpassword(User.Identity.Name);
            var contragents = new List<GetUsersContragentsResultModel>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.GetAsync($"contragent/GetUsersContragents/{userId}");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<GetUsersContragentsResultModel>>();
                        readTask.Wait();

                        contragents = readTask.Result;

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "error");
                    }
                }
            }
            catch (Exception ex)
            {

                var exception = ex;
            }

            return View("LoggedUserView", contragents);
        }


        [Authorize]
        public ActionResult CheckVat(string vatNumber)
        {
            var model = new CheckVatResultModel();
            try
            {
                using (var client = new CheckVatService.checkVatPortTypeClient())
                {

                    string countryCode = "BG";

                    var name = string.Empty;
                    bool valid;
                    string address = string.Empty;
                    
                    var result = client.checkVat(ref countryCode, ref vatNumber, out valid, out name, out address);

                    string IsValid = valid ? "VALID" : "NOT VALID";

                    model.Valid = IsValid;
                    model.Name = name;
                    model.Adress = address;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return PartialView("_CheckVatPartialView", model);
        }

        
    }

}
