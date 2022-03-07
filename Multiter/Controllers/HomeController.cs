using BusinssAccessLayer;
using CommanLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multiter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Multiter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BLEmployeeBusiness business = new BLEmployeeBusiness();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var emp = business.GetEmployees();
            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employees employee)
        {
            var result = business.CreateEmployee(employee);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("error", "Error in create employee !");
                return View(employee);
            }

        }

        public IActionResult DeleteEmployees(int Id)
        {

            bool result = business.DeleteEmployee(Id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        public IActionResult Update(int Id)
        {

            var result = business.GetEmployeesById(Id);
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(Employees employees)
        {

            var result = business.UpdateEmployees(employees);
            return RedirectToAction("Index");
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
