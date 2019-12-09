using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using WebTrainingMVC.BindingModels.ContragentBindingModels;
using WebTrainingMVC.Models.ContragentModels;

namespace WebTrainingMVC.Controllers
{
    public class ContragentController : Controller
    {

       public ActionResult AddContragent()
        {
            return View("AddContragentView");
        }
        

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddContragent(AddContragentBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var newContragent = new AddContragentResultModel
                {
                   ContragentName = model.ContragentName,
                   Adress = model.Adress,
                   Email = model.Email,
                   VatNumber = model.VatNumber,
                   UserId = model.UserId
                };
                try
                {
                    using (var client = new HttpClient())
                    {

                        var postTask = client.PostAsJsonAsync("http://localhost:52680/contragent/AddContragent/", newContragent);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<AddContragentResultModel>();
                            readTask.Wait();

                            newContragent = readTask.Result;
                            ViewBag.Title = "Successfully added contragent";
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
            }

            return RedirectToAction("Index","Home");
        }

    }
}