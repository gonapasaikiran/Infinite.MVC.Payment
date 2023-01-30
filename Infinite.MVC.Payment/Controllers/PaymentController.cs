using Infinite.MVC.Payment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infinite.MVC.Payment.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<PaymentViewModel> payments = new();
            using (var client=new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Payments/Payments");
                if(result.IsSuccessStatusCode)
                {
                    payments=await result.Content.ReadAsAsync<List<PaymentViewModel>>();
                }
            }
                return View(payments);
        }
     
        public async Task<IActionResult> Details(int id)
        {
            PaymentViewModel Payment = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Payments/GetPaymentById/{id}");
                if (result.IsSuccessStatusCode)
                {
                   Payment  = await result.Content.ReadAsAsync<PaymentViewModel>();
                }
            }
            return View(Payment);
        }


    }
}
