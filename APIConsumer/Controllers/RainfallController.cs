using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIConsumer.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using APIConsumer.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace APIConsumer.Controllers
{
    public class RainfallController : Controller
    {   private IConfiguration _configuration;
        private IHttpClientExternalAPIRainfallService _rainfallAPIService;
        private ILogger<RainfallController> _logger;
        public RainfallController(IConfiguration config, IHttpClientExternalAPIRainfallService rainfallAPIService,ILogger<RainfallController> logger) { 
             _configuration = config;
            _rainfallAPIService= rainfallAPIService;
            _logger= logger;
        }
        // GET: RainfallController
        //// Require authorization for a specific action.
        // [Authorize]
        // Restrict by user:
        // [Authorize(Users = "Alice,Bob")]
        // Restrict by role:
        //[Authorize(Roles = "Administrators")]
        //anyone can access - revoke quthorisation restriction
        //[AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Rainfall> rainfalls = null;
            //string baseUriInConfig = _configuration.GetSection("WebAPI").GetSection("RainfallBaseUri").Value;
            //client.BaseAddress = new Uri(baseUriInConfig);
            //client.DefaultRequestHeaders.Clear();
            //Task<HttpResponseMessage> clientTask =client.GetAsync("rainfallapi");
            //clientTask.Wait();
            //HttpResponseMessage result=clientTask.Result;
            //Get the Http Message from HttpClient pool 
             HttpResponseMessage result = await _rainfallAPIService.GetRainfallMessageAsync();
              if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Rainfall>>();
                    readTask.Wait();
                    rainfalls = readTask.Result;
                }
                else
                {
                    rainfalls = Enumerable.Empty<Rainfall>();
                    ModelState.AddModelError(string.Empty, "Server APIerror");
                }
              return View(rainfalls);
           
        }

        // GET: RainfallController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RainfallController/Create - When the user navigates to the page, it returns a view, probably containing a form for the user to fill out.
        public ActionResult Create()
        {
            return View();
        }

        // POST: RainfallController/Create - When the user submits the form on the page, it will post to the same URL.  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RainfallController/Edit/5 - When the user navigates to the page, it returns a view, probably containing a form for the user to fill out.
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RainfallController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RainfallController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RainfallController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
