using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Register.Application.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Register.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApi customerApi;

        public CustomerController(ICustomerApi customerApi)
        {
            this.customerApi = customerApi;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}