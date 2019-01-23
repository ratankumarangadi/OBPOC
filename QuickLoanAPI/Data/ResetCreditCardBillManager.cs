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
    public class ResetCreditCardManager
    {
        QuickLoanDbContext _context = null;
        public ResetCreditCardManager(QuickLoanDbContext context)
        {
            _context = context;
        }


        public ResetCreditCardResponse Reset(CardResetRequest values)
        {
            ResetCreditCardResponse resetResponse = new ResetCreditCardResponse();

            var member = _context.Members.Select(item => item).Where(item => item.Id == Convert.ToInt16(values.MemberId)).SingleOrDefault();
            var account = _context.AccountDetails.Select(item => item).Where(item => item.MembersId == Convert.ToInt16(values.MemberId));

            List<AccountDetails> accountDetails = new List<AccountDetails>();
            accountDetails = account.ToList();

            if(accountDetails.Count > 0)
            {
                var card = _context.Cards.Select(item => item).Where(item => item.AccountsId == accountDetails[0].AccountsId).ToList();
                
                if (card.Count > 0)
                {

                    card[0].PaymentDue = Convert.ToDecimal(values.Amount);
                    _context.SaveChanges();
                    resetResponse.StatusMessage = "Reset successful for " + values.MemberId + " and with " + values.Amount;

                } else
                    {
                    resetResponse.StatusMessage = " card does not exist for given member id ";
                }
            }
            else
            {
                resetResponse.StatusMessage = " card does not exist for given member id ";
            }


            return resetResponse;
        }


       
    }
}
