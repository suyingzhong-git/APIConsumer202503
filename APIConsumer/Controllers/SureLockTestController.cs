using APIConsumer.BL.Interfaces;
using APIConsumer.BL.services;
using APIConsumer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIConsumer.Controllers
{
    public class SureLockTestController : Controller
    {
        //local variables for dependency injection
        private IConfiguration _configuration;
        private IHttpClientForExternalAPILocksService _locksAPIService;
        private ILogger<SureLockTestController> _logger;
        
        //constructor with dependency injection
        public SureLockTestController(IConfiguration config, IHttpClientForExternalAPILocksService locksAPIService, ILogger<SureLockTestController> logger)
        {
            _configuration = config;
            _locksAPIService = locksAPIService;
            _logger = logger;
        }

        // GET: SureLockTestController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Lock> locks = null;
            try
            {
                HttpResponseMessage result = await _locksAPIService.GetLocksMessagesAsync();

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                    readTask.Wait();
                    locks = readTask.Result;
                }
                else
                {
                    locks = Enumerable.Empty<Lock>();
                    ModelState.AddModelError(string.Empty, $"Server API Error: {result.StatusCode}");
                }
            }
            catch 
            {
                throw;
            }
            return View(locks);
        }

        public async Task<ActionResult> SearchByName(string queryString="")
        {
            IEnumerable<Lock> locks = null;
            try
            {
                HttpResponseMessage result = await _locksAPIService.GetLocksMessagesRangeAsync("name="+queryString);

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                    readTask.Wait();
                    locks = readTask.Result;
                }
                else
                {
                    locks = Enumerable.Empty<Lock>();
                    ModelState.AddModelError(string.Empty, $"Server API Error: {result.StatusCode}");
                }
            }
            catch
            {
                throw;
            }
            return View("Index", locks);

        }

        /// <summary>
        /// queryString example: price=5,10, id=1;
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<ActionResult> Range(string queryString)
        {
            IEnumerable<Lock> locks = null;
            try
            {
                HttpResponseMessage result = await _locksAPIService.GetLocksMessagesRangeAsync(queryString);

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                    readTask.Wait();
                    locks = readTask.Result;
                }
                else
                {
                    locks = Enumerable.Empty<Lock>();
                    ModelState.AddModelError(string.Empty, $"Server API Error: {result.StatusCode}");
                }
            }
            catch
            {
                throw;
            }
            return View("Index", locks);
            
        }

        // GET: SureLockTestController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Lock locks = null;
            HttpResponseMessage result = await _locksAPIService.GetLocksMessagesRangeAsync($"id={id}");

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                readTask.Wait();
                locks = readTask.Result.FirstOrDefault<Lock>();
                return View(locks);
            }
            else
            {
                return View();
            }
        }

        // GET: SureLockTestController/Create
        public IActionResult Create()
        {
            //reden the empty Create webpage with a form to collect use input. 
            return View();
        }

        // POST: SureLockTestController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,price,quantity,is_active,created_at,updated_at")]Lock lockInput)
        {
            try
            {
                if (lockInput.created_at is null) //same as == null
                {
                    lockInput.created_at = DateTime.Now;
                }
                //functions the same as above
                lockInput.updated_at = lockInput.updated_at ?? DateTime.Now;
                //create Lock record and get the result in Http Response
                HttpResponseMessage result = await _locksAPIService.CreateLockMessageAsync(lockInput);
                //check if the StatusCode is Successful
                if (result.IsSuccessStatusCode)
                {
                    //render Index.cshtml webpage with the products list after the record has been created
                    return RedirectToAction(nameof(Index)); //View("Index");
                }
                else
                {
                    //display errors contained Http Response Message result
                    return View(result.ToString());
                }
                    
            }
            catch
            {
                throw;  
            }
        }

        // GET: SureLockTestController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Lock locks = null;
            HttpResponseMessage result = await _locksAPIService.GetLocksMessagesRangeAsync($"id={id}");

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                readTask.Wait();
                locks = readTask.Result.FirstOrDefault<Lock>();
                return View(locks);
            }
            else
            {
                return View();
            }
        }

        // POST: SureLockTestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("id,name,price,quantity,is_active,created_at,updated_at")] Lock lockInput)
        {
            try
            {
                if (lockInput.created_at is null) //same as == null
                {
                    lockInput.created_at = DateTime.Now;
                }
                //functions the same as above
                lockInput.updated_at = DateTime.Now;
                
                //update Lock record and get the result in Http Response
                HttpResponseMessage result = await _locksAPIService.UpdateLockMessageAsync(lockInput);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(result.ToString());
                }
            }
            catch
            {
                throw;  
            }
        }

        // GET: SureLockTestController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Lock locks = null;
            HttpResponseMessage result = await _locksAPIService.GetLocksMessagesRangeAsync($"id={id}&is_active=true");

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadFromJsonAsync<IList<Lock>>();
                readTask.Wait();
                locks = readTask.Result.FirstOrDefault<Lock>();
                if (locks.is_active == false) { return RedirectToAction(nameof(Index)); }
                return View(locks);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: SureLockTestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, [Bind("id,name,price,quantity,created_at,updated_at")] Lock lockInput)
        {
            try
            {
                //soft delete Lock record using external API and get the result in Http Response
                HttpResponseMessage result = await _locksAPIService.DeleteLockMessageAsync(id);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(result.ToString());
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
