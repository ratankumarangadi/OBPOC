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
    public class TransactionController : Controller
    {
        private readonly QuickLoanDbContext _context = null;
        private readonly IConfiguration _configuration;

        public TransactionController(QuickLoanDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }       

       
        [HttpGet("{bankId}/{accountId}/{view_id}/{transactionId}")]
        public IActionResult GetTransactionById(int bankId, int accountId,int view_Id, int transactionId)
        {
            var accountManger = new AccountManager(_context);
            return Ok(accountManger.GetTransactionById(bankId,accountId,view_Id,transactionId));
        }

      

    }
}
