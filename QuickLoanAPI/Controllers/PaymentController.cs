using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuickLoanAPI.Data;
using QuickLoanAPI.Model.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickHomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly QuickLoanDbContext _context = null;
        private readonly IConfiguration _configuration;

        public PaymentController(QuickLoanDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        [HttpPost("cardpayment")]
        public IActionResult CardPayment([FromBody]CardPaymentRequest value)
        {
            var paymentManger = new PaymentManager(_context);
            return Ok(paymentManger.CardPayment(value));
        }

    }
}
