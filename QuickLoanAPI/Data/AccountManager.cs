using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Model;
using QuickLoanAPI.Model.DbEntiry;
using QuickLoanAPI.Model.DbEntity;
using QuickLoanAPI.Model.Requests;
using QuickLoanAPI.Model.Requests.LoginResponse;
using QuickLoanAPI.Model.Requests.TransactionResponse;

namespace QuickLoanAPI.Data
{
    public class AccountManager
    {
        QuickLoanDbContext _context = null;
        public AccountManager(QuickLoanDbContext context)
        {
            _context = context;
        }
        public RootObject GetAccountView(int memberId)
        {
            RootObject rootObject = new RootObject();

            var member = _context.Members.Select(item => item).Where(item => item.Id == memberId).SingleOrDefault();
            rootObject.MemberId = member.Id;
            rootObject.LastLogin = member.LastLogin;
            rootObject.MemberName = member.MemberName;
            rootObject.loanEligibility = member.LoanEligibility;
            member.LastLogin = DateTime.Now.ToString();
            _context.SaveChanges();

            rootObject.AccountView = new List<Portfolio>();
            Portfolio accountView = null;

            var result = _context.AccountDetails.Select(item => item).Where(item => item.MembersId == memberId);
            if (result.Any())
            {
                var resultList = result.ToList();
                foreach (AccountDetails detail in resultList)
                {

                    var primaryMemberId = _context.AccountDetails.Select(item => item).Where(item => item.AccountsId == detail.AccountsId && item.AccountTypesId == 1).SingleOrDefault().MembersId;
                    var primaryMemberName = _context.Members.Select(item => item).Where(item => item.Id == primaryMemberId).SingleOrDefault().MemberName;
                    var memberLoanEligibility = _context.Members.Select(item => item).Where(item => item.Id == primaryMemberId).SingleOrDefault().LoanEligibility;
                    var jointMember = _context.AccountDetails.Select(item => item).Where(item => item.AccountsId == detail.AccountsId && item.AccountTypesId == 2).SingleOrDefault();
                   
                    string jointMemberName = null;
                    if (jointMember != null)
                    {
                        jointMemberName = _context.Members.Select(item => item).Where(item => item.Id == jointMember.MembersId).SingleOrDefault().MemberName;
                    }

                    var accountNumbers = _context.Accounts.Select(item => item).Where(item => item.Id == detail.AccountsId).ToList();

                    accountView = new Portfolio();
                   
                    /* fixing response format */
                   // accountView.loanEligibility = memberLoanEligibility;
                    accountView.account = accountNumbers[0].AccountNumber;
                    accountView.Primary = primaryMemberName;
                    accountView.Joint = jointMemberName;
                    var shareList = _context.Shares.Select(item => item).Where(item => item.AccountsId == detail.AccountsId).ToList();
                    var loanList = _context.Loans.Select(item => item).Where(item => item.AccountsId == detail.AccountsId).ToList();
                    accountView.shares = new List<Share>();
                    accountView.loans = new List<Loan>();

                    foreach (Shares share in shareList)
                    {
                        accountView.shares.Add(new Share() { id = share.ShareNumber, balance = Convert.ToDouble(share.Balance), description = share.Description });
                        accountView.shares_total = accountView.shares_total + Convert.ToDouble(share.Balance);
                    }
                    foreach (Loans loan in loanList)
                    {
                        accountView.loans.Add(new Loan() { id = loan.Id, balance = Convert.ToDouble(loan.Balance), description = loan.Description,emiDue = loan.EmiDue,emiDueDate=loan.EmiDueDate });
                        accountView.loans_total = accountView.loans_total + Convert.ToDouble(loan.Balance);
                        
                    }

                    /* get transactions */
                    accountView.transactionList = new List<TransactionList>();
                    accountView.transactionList = GetTransactions(accountNumbers[0].Id);
                    /* get transactions */

                    /*get cards */
                    var cardDetails = _context.Cards.Select(item => item).Where(item => item.AccountsId == detail.AccountsId).ToList();
                    accountView.card = new Card();
                    accountView.card.Account = accountNumbers[0].AccountNumber;
                    accountView.card.CardNumber = cardDetails[0].CardNumber;
                    accountView.card.PaymentDue = cardDetails[0].PaymentDue;
                    accountView.card.PaymentDueDate = cardDetails[0].PaymentDueDate;
                    accountView.card.Type = cardDetails[0].Type;

                    /*get cards*/
                    rootObject.AccountView.Add(accountView);
                    accountView.message = accountView.shares.Count + " shares and " + accountView.loans.Count + " loans";
                }

            }

            return rootObject;
        }


        public MemberResponse GetMemberId(string memberName)
        {
            Members member = _context.Members.Where(item => item.MemberName.ToUpper() == memberName.ToUpper()).SingleOrDefault();

            if (member != null)
            {
                return new MemberResponse()
                {
                    MemberId = member.Id.ToString()
                };              
            }
            else
            {
                var members = _context.Members.Select(item => item).Where(item => item.IsEditable == true && item.InUse == false).ToList();
                if(members.Any())
                {
                    members[0].IsEditable = false;
                    members[0].InUse = true;
                    members[0].MemberName = memberName;
                    _context.SaveChanges();
                    return new MemberResponse()
                    {
                        MemberId = members[0].Id.ToString()
                    };
                }
                else
                {
                    return new MemberResponse()
                    {
                        MemberId = null,
                        ErrorMessage="Credit Union member size limit exceeded"
                    };
                }               
            }      
        }

        public List<TransactionList> GetTransactions(int accountId)
        {
            
            List<TransactionList> transactions = new List<TransactionList>();
            TransactionList transaction = null;

            //var transactionRecords = _context.Transactions.OrderByDescending(item => item.Postdate).Where(item => item.AccountsId == accountId).ToList();

            List<Transactions> trans = new List<Transactions>();
             trans = _context.Transactions.OrderByDescending(item => item.Id).Where(item => item.AccountsId == accountId).ToList();
            var transactionRecords = from t in trans orderby t.Id descending select t;
            int transactionCount = 0;
           
            foreach (Transactions tr in trans)
            {
                if (transactionCount == 30)
                {
                    break;
                }
                transaction = new TransactionList();
                transaction.Description = tr.Description;
                transaction.Amount = tr.Amount;
                transaction.id = tr.RecordId;
                transaction.PostDate = tr.Postdate;
                transaction.Type = tr.Type;
                transactions.Add(transaction);
                transactionCount = transactionCount + 1;
            }
            return transactions;
        }

        public TransactionResponse GetTransactionById(int bankId, int accountId, int viewId, int transactionId)
        {
            TransactionResponse response = new TransactionResponse();
            
            List<TransactionList> transactions = new List<TransactionList>();
            
            List<Transactions> trans = new List<Transactions>();
            trans = _context.Transactions.OrderByDescending(item => item.Id).Where(item => item.AccountsId == accountId).ToList();
            var transactionRecords = from t in trans orderby t.Id descending select t;

            foreach (Transactions tr in trans)
            {
                var account = _context.Accounts.Select(item => item).Where(item => item.Id == accountId).SingleOrDefault();
                var otherAccount = _context.Accounts.Select(item => item).Where(item => item.Id == tr.AccountsId).SingleOrDefault();
                List<AccountDetails> accountDetails = _context.AccountDetails.Select(item => item).Where(item => item.AccountsId == accountId).ToList<AccountDetails>();
                List<AccountDetails> otherAccountDetails = _context.AccountDetails.Select(item => item).Where(item => item.AccountsId == tr.OtherAccountId).ToList<AccountDetails>();

                List<Members> memberDetails = _context.Members.Select(item => item).Where(item => item.Id == accountDetails[0].MembersId).ToList<Members>();
                List<Members> otherMemberDetails = _context.Members.Select(item => item).Where(item => item.Id == otherAccountDetails[0].MembersId).ToList<Members>();

                BankNames bankDetails = _context.BankNames.Select(item => item).Where(item => item.BankId == account.bankId).ToList<BankNames>()[0];
                BankNames otherBankDetails = _context.BankNames.Select(item => item).Where(item => item.BankId == account.bankId).ToList<BankNames>()[0];
                response.this_account = new ThisAccount();
                response.this_account.IBAN = account.IBAN;
                response.this_account.swift_bic = account.swift_bic;
                response.this_account.number = account.AccountNumber;
                response.this_account.holders = new List<Holder>();
                response.this_account.holders.Add(new Holder() { name = memberDetails[0].MemberName,is_alias=true});
                response.this_account.bank = new Bank() { name = bankDetails.BankName, national_identifier = bankDetails.NationalIdentifier };
                response.this_account.kind = "AC";

                /*other account details */
                response.other_account = new OtherAccount();
                response.other_account.number = otherAccount.AccountNumber;
                response.other_account.IBAN = otherAccount.IBAN;
                response.other_account.swift_bic = otherAccount.swift_bic;
                response.other_account.bank = new Bank2() { name = otherBankDetails.BankName, national_identifier = otherBankDetails.NationalIdentifier };
                response.other_account.kind = "AC";
                response.other_account.holder = new Holder2() {name = otherMemberDetails[0].MemberName,is_alias=true};
               

                /* metadata */
                response.other_account.metadata = new Metadata();
                /* metadata*/

                response.details = new Details();
                response.details.type = "AC";
                response.details.completed = Convert.ToDateTime(tr.Completed);
                response.details.posted = Convert.ToDateTime(tr.Postdate);
                response.details.description = tr.Description;

                response.details.new_balance = new NewBalance() {currency = "EUR" , amount = "10"};
                response.details.value = new Value() { currency = "EUR", amount = "10" };

            }
            

            return response;
        }

        public AuthenticateResponse Authenticate(string x)
        {
            return new AuthenticateResponse() { Token = "xyz" } ;
        }


        public LoginResponse Login(string BankId, string AccountId, string UserName, string Password)
        {

            LoginResponse loginResponse = new LoginResponse();
            var users = _context.Users.Select(item => item).Where(item => item.UserName.ToUpper() == UserName.ToUpper() && item.Password == Password.ToUpper());

            if (!(users.Any()))
            {

                loginResponse.status = new Status { LoginStatus = "Fail", LoginStatusMessage = "User credentils not matching" };
                return loginResponse;

            }
            else
            {
                var permissions = _context.Permissions.Select(item => item).Where(item => item.AccountId == Convert.ToInt16(AccountId) && item.UserId == users.ToList<Users>()[0].Id);
                if (permissions.Any())
                {
                    var bankDetails = _context.Permissions.Select(item => item).Where(item => item.AccountId == Convert.ToInt16(AccountId) && item.UserId == users.ToList<Users>()[0].Id);
                }
                else
                {
                    loginResponse.status = new Status { LoginStatus = "Failed", LoginStatusMessage = "User credentils are not matching" };
                    return loginResponse;
                }
            }

            var account = _context.Accounts.Select(item => item).Where(item => item.Id == Convert.ToInt16(AccountId));

            if(account.ToList<Accounts>()[0].bankId == Convert.ToInt16(BankId))
            {
                loginResponse.status = new Status { LoginStatus = "Successful", LoginStatusMessage = "User credentils are correct" };
            }
            else
            {
                loginResponse.status = new Status { LoginStatus = "Failed", LoginStatusMessage = "User credentils are not matching" };
                return loginResponse;
            }

            List<Transactions> transactions = new List<Transactions>();
            transactions =_context.Transactions.Select(item => item).Where(item => item.AccountsId == Convert.ToInt16(AccountId)).ToList();
            AccTransactions accTransactions = null;           
           
            if(account.Any())
            {
                loginResponse.AccountNumber = account.ToList<Accounts>()[0].AccountNumber;
                loginResponse.AccountId = account.ToList<Accounts>()[0].Id;
                if(transactions.Any())
                {
                    loginResponse.Statement = new List<AccTransactions>();
                    foreach (Transactions tr in transactions)
                    {
                        
                        accTransactions = new AccTransactions() { TransactionId = tr.Id.ToString(),
                            Type = tr.Type,
                            PostedDate = tr.Posted,
                            OtherAccountNumber = _context.Accounts.Select(item => item).Where(item => item.Id == Convert.ToInt16(tr.OtherAccountId)).ToList()[0].AccountNumber,
                            PostDate = tr.Postdate,
                            Completed = tr.Completed,
                            Description=tr.Description};
                        loginResponse.Statement.Add(accTransactions);   
                    }
                   
                }
                
            }
            return loginResponse;
            
        }
       

        
    }
}
