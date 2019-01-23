
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuickLoanAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickHomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    public class ObLoginController : Controller
    {
        private readonly QuickLoanDbContext _context = null;
        private readonly IConfiguration _configuration;

        public ObLoginController(QuickLoanDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{BankId}/{AccountId}/{UserName}/{Password}")]

        public IActionResult Authenticate(string BankId, string AccountId, string UserName, string Password)
        {
            var accountManger = new AccountManager(_context);
            return Ok(accountManger.Login(BankId,AccountId,UserName,Password));
           
        }

        
      

    }
}
