using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using QuickLoanAPI.Model;
using QuickLoanAPI.Model.DbEntiry;
using QuickLoanAPI.Model.DbEntity;
using QuickLoanAPI.Model.Requests;

namespace QuickLoanAPI.Data
{
    public class PaymentManager
    {
        QuickLoanDbContext _context = null;
        public PaymentManager(QuickLoanDbContext context)
        {
            _context = context;
        }

        public PaymentResponse CardPayment(CardPaymentRequest cardDetails)
        {
            var accountId = _context.Accounts.Select(item => item).Where(item => item.AccountNumber == cardDetails.AccountNumber.ToString()).SingleOrDefault();
            var cCardDetails = _context.Cards.Select(item => item).Where(item => item.AccountsId == accountId.Id && item.CardNumber == cardDetails.CardNumber.ToString()).SingleOrDefault();

            // var shareDetails = _context.Shares.Select(item => item).Where(item => item.AccountsId == accountId.Id).SingleOrDefault();
            var shareDetails = _context.Shares.Select(item => item).Where(item => item.AccountsId == accountId.Id).ToList()[0];
            Transactions transactions = null;
            if (shareDetails.Balance > Convert.ToDecimal(cardDetails.Amount))
            {

               
                if (Convert.ToDecimal(cardDetails.Amount) < Convert.ToDecimal(cCardDetails.PaymentDue))
                {
                    // partial payment
                    
                    shareDetails.Balance = shareDetails.Balance - Convert.ToDecimal(cardDetails.Amount);
                    _context.SaveChanges();
                    
                    transactions = new Transactions();
                    transactions.Amount = Convert.ToDecimal(cardDetails.Amount);
                    transactions.AccountsId = accountId.Id;
                    transactions.Description = "card payment";
                    transactions.Postdate = DateTime.Today.ToString();
                    transactions.RecordId = 3;
                    transactions.Type = "CARD";
                    _context.Add(transactions);
                    _context.SaveChanges();

                    var cardPayment = _context.Cards.Select(item => item).Where(item => item.AccountsId == accountId.Id && item.CardNumber == cardDetails.CardNumber.ToString()).SingleOrDefault();
                    cardPayment.PaymentDue = cardPayment.PaymentDue - Convert.ToDecimal(cardDetails.Amount);
                    _context.SaveChanges();

                    return new PaymentResponse()
                    {
                        Status = true,
                        Description = "Bill payment successful"
                    };
                }
                else
                {
                    // Full payment
                    transactions  = new Transactions();

                    shareDetails.Balance = shareDetails.Balance - Convert.ToDecimal(cardDetails.Amount);
                    //shareDetails.AccountsId = accountId.Id;

                    //_context.Update(share);
                    _context.SaveChanges();

                    transactions.Amount = Convert.ToDecimal(cardDetails.Amount);
                    transactions.AccountsId = accountId.Id;
                    transactions.Description = "card payment";
                    transactions.Postdate = DateTime.Today.ToString();
                    transactions.RecordId = 3;
                    transactions.Type = "Card payment";
                    _context.Add(transactions);
                    _context.SaveChanges();

                    var cardPayment = _context.Cards.Select(item => item).Where(item => item.AccountsId == accountId.Id && item.CardNumber == cardDetails.CardNumber.ToString()).SingleOrDefault();
                    cardPayment.PaymentDue = cardPayment.PaymentDue - Convert.ToDecimal(cardDetails.Amount);
                    cardPayment.PaymentDueDate = DateTime.Today.AddMonths(1).ToString();
                    cardPayment.Type = "CREDIT";
                    _context.SaveChanges();

                    return new PaymentResponse()
                    {
                        Status = true,
                        Description = "Bill payment successful"
                    };
                }
            }
            else
            {
                return new PaymentResponse()
                {
                    Status = false,
                    Description = "Share amount is less than payment amount"
                };
            }
            


           
        }
    }
}
