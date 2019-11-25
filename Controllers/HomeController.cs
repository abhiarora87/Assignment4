using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4.Models;
using static Assignment4.Models.EF_Models;
using static Assignment4.Models.Sales_Model;
using Assignment4.DataAccess;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;
using System.Web.Helpers;

namespace Assignment4.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDBContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        static string BASE_URL = "https://developer.nrel.gov/api/vehicles/v1/";
        static string API_KEY = "A0ePJSdWCUI2FgRbJMxDYQWohIfdR5EGYkN2MVx4";

        HttpClient httpClient;

        public HomeController(ApplicationDBContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Automobile> GetInventory()
        {
            string Automobile_API_PATH = BASE_URL + "/light_duty_automobiles?&current=true&category_id=29&fuel_id=57";
            string automobileList = "";
            RootObject automobiles = null;
            List<Automobile> selectedData = new List<Automobile>();
            

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(Automobile_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(Automobile_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                automobileList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!automobileList.Equals(""))
            {
                automobiles = JsonConvert.DeserializeObject<RootObject>(automobileList);
                //automobiles = automobiles.GetRange(0, 50);
            }

            for(int i =0; i < automobiles.result.Count(); i++)
            {
                var obj = new Automobile();
                selectedData.Add(obj);
                selectedData[i].id = automobiles.result[i].id;
                selectedData[i].model = automobiles.result[i].model;
                selectedData[i].model_year = automobiles.result[i].model_year;
                selectedData[i].engine_size = automobiles.result[i].engine_size;
                selectedData[i].engine_cylinder_count = automobiles.result[i].engine_cylinder_count;
                selectedData[i].manufacturer_name = automobiles.result[i].manufacturer_name;
                selectedData[i].category_name = automobiles.result[i].category_name;
            }

            return selectedData;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Automobile> automobiles = GetInventory();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Automobiles"] = JsonConvert.SerializeObject(automobiles);

            return View(automobiles);
        }
        
        public IActionResult PopulateAutomobiles()
        {
            // Retrieve the AUTOMOBILE that were saved in the INVENTORY method
            List<Automobile> automobiles = JsonConvert.DeserializeObject<List<Automobile>>(TempData["Automobiles"].ToString());

            foreach(Automobile automobile in automobiles)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add vehicle only if it doesnt exist, check existence using id (PK)
                if (dbContext.Automobiles.Where(c => c.id.Equals(automobile.id)).Count() == 0)
                {
                    dbContext.Automobiles.Add(automobile);
                }
            }
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Automobile_DB.dbo.Automobiles ON;");
                dbContext.SaveChanges();
                dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Automobile_DB.dbo.Automobiles OFF;");
                transaction.Commit();
            }
            ViewBag.dbSuccessComp = 1;
            return View("Index", automobiles);
        }

        public IActionResult VehicleSales() { 
        //{
        //    var result = (from a in dbContext.Sales
        //                  join b in dbContext.Vehicle on a.vin_number equals b.vin_number
        //                  select new ChartTables
        //                  {
        //                      sales_amt = a.sales_amt,
        //                      model_year = b.model_year
        //                  }
        //                  ).ToList();

        //    var groupedCustomerList = result.GroupBy(u => u.model_year).ToList();
            //ArrayList xValue = new ArrayList();
            //ArrayList yValue = new ArrayList();

            //foreach (var item in groupedCustomerList)
            //{
            //    xValue.Add(item.Key);
            //    yValue.Add(item.Count());
            //}

            //    var key = new Chart(width: 300, height: 300)
            //    .AddTitle("Sales")
            //    .AddSeries(chartType: "Pie",
            //    name: "Some Name",
            //    xValue: xValue,
            //    yValues: yValue);
            //    return File(key.ToWebImage().GetBytes(), "image/jpeg");
            List<Vehicle> vehicleList = dbContext.Vehicle.ToList();
            return View(vehicleList);

            //return View(groupedCustomerList);
        }

        public IActionResult VehicleSold()
        {
            List<Vehicle> vehicleList = dbContext.Vehicle.ToList();
            return View(vehicleList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
