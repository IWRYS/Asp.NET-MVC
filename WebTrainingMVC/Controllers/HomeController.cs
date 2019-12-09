using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebTrainingMVC.Models.ContragentModels;

namespace WebTrainingMVC.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var contragents = new List<ContragentResultModel>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri($"http://localhost:52680/");

                    var responseTask = client.GetAsync($"contragent/GetAllContragents");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<ContragentResultModel>>();
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

            return View("AllContragentsView", contragents);
        }

       
       
    }
}