using System;
using System.Collections.Generic;
using System.Linq;
using QuickLoanAPI.Model.DbEntiry;
using QuickLoanAPI.Model.DbEntity;

namespace QuickLoanAPI.Data
{

    public static class DbInitializer
    {
        public static void Initialize(QuickLoanDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Accounts.Any())
                {
                    return;   // DB has been seeded
                }

            InitialiseAccountTypeMster(context);
            InitialiseAccountsMster(context);
            InitialiseMembersMster(context);
            InitialiseAccountMemberDetailsMster(context);
            InitialiseLoansMster(context);
            InitialiseShareMster(context);
            InitialiseTransaction(context);
            InitialiseCards(context);
            InitialiseBankNames(context);
        }

        private static void InitialiseBankNames(QuickLoanDbContext context)
        {
            var BankNames = new BankNames[]
            {
               new BankNames
               {
                   BankId=10,
                   NationalIdentifier = "ABC",
                   BankName ="OBC1"
               },
               new BankNames
               {
                   BankId=10,
                   NationalIdentifier = "ABC",
                   BankName ="OBC1"
               }
            };
            }

            private static void InitialiseAccountsMster(QuickLoanDbContext context)
        {
            var accountEntries = new Accounts[]
            {
                new Accounts
                {
                   
                    AccountNumber = "5000",

                },
                new Accounts
                {
                    
                    AccountNumber = "6000"
                },
                new Accounts
                {
                     
                    AccountNumber = "7000"
                },
                new Accounts
                {
                    
                    AccountNumber = "8000"
                }
            };

            foreach (Accounts accountEntry in accountEntries)
            {
                context.Accounts.Add(accountEntry);

            }
            context.SaveChanges();
        }

        private static void InitialiseMembersMster(QuickLoanDbContext context)
        {
            var memberEntries = new Members[]
            {
                new Members
                {
                    MemberName = "Member1",
                    LoanEligibility = 3000,
                    LastLogin="01/01/2018",
                    IsEditable=false,
                    InUse = true
                },
                new Members
                {
                     MemberName = "Member2",
                     LoanEligibility = 4000,
                     LastLogin="01/02/2018",
                     IsEditable=false,
                     InUse=true
                },
                new Members
                {
                     MemberName = "Member3",
                     LoanEligibility = 1000,
                     LastLogin="01/03/2018",
                     IsEditable=true,
                     InUse=false
                },
                new Members
                {

                     MemberName = "Member4",
                     LoanEligibility = 2000,
                     LastLogin="01/04/2018",
                     IsEditable=true,
                     InUse=false
                }
            };

            foreach (Members memberEntry in memberEntries)
            {
                context.Members.Add(memberEntry);

            }
            context.SaveChanges();
        }

        private static void InitialiseAccountTypeMster(QuickLoanDbContext context)
        {
            var accountTypeEntries = new AccountTypes[]
            {
                 new AccountTypes
                 {
                     //Id=1,
                     Type ="Primary"
                 },
                 new AccountTypes
                 {
                     //Id=2,
                     Type = "Joint"
                 }
            };

            foreach (AccountTypes accountTypeEntry in accountTypeEntries)
            {
                context.AccountTypes.Add(accountTypeEntry);

            }
            context.SaveChanges();
        }

        private static void InitialiseAccountMemberDetailsMster(QuickLoanDbContext context)
        {
            var accountDetailsEntries = new AccountDetails[]
            {
                new AccountDetails
                {
                    AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    MembersId =context.Members.Select(item => item).Where(item => item.MemberName=="Member1").ToList<Members>()[0].Id,
                    AccountTypesId =context.AccountTypes.Select(item => item).Where(item => item.Type=="Primary").ToList<AccountTypes>()[0].Id,
                },
                new AccountDetails
                {
                     AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    MembersId =context.Members.Select(item => item).Where(item => item.MemberName=="Member2").ToList<Members>()[0].Id,
                    AccountTypesId =context.AccountTypes.Select(item => item).Where(item => item.Type=="Primary").ToList<AccountTypes>()[0].Id,
                    
                },
                new AccountDetails
                {
                    AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    MembersId =context.Members.Select(item => item).Where(item => item.MemberName=="Member1").ToList<Members>()[0].Id,
                    AccountTypesId =context.AccountTypes.Select(item => item).Where(item => item.Type=="Joint").ToList<AccountTypes>()[0].Id,
                },
                 new AccountDetails
                {
                    AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    MembersId =context.Members.Select(item => item).Where(item => item.MemberName=="Member3").ToList<Members>()[0].Id,
                    AccountTypesId =context.AccountTypes.Select(item => item).Where(item => item.Type=="Primary").ToList<AccountTypes>()[0].Id,
                },
                   new AccountDetails
                {
                    AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    MembersId =context.Members.Select(item => item).Where(item => item.MemberName=="Member4").ToList<Members>()[0].Id,
                    AccountTypesId =context.AccountTypes.Select(item => item).Where(item => item.Type=="Primary").ToList<AccountTypes>()[0].Id,
                }
            };

            foreach (AccountDetails accountDetailsEntry in accountDetailsEntries)
            {
                context.AccountDetails.Add(accountDetailsEntry);

            }
            context.SaveChanges();
        }
        
        private static void InitialiseLoansMster(QuickLoanDbContext context)
        {
            var loanDetailsEntries = new Loans[]
            {
              new Loans()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                  LoanNumber = 0,
                  Balance =10000,
                  EmiDue = 400,
                  EmiDueDate = DateTime.Now.AddDays(3).ToString("MM/dd/yyyy"),
                  Description = "Description"
              },

              new Loans()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                  LoanNumber = 1,
                  Balance = 20000,
                  EmiDue = 100,
                  EmiDueDate = DateTime.Now.AddDays(4).ToString("MM/dd/yyyy"),
                  Description = "Description"
              },

              new Loans()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                  LoanNumber = 3,
                  Balance = 20000,
                  EmiDue = 200,
                  EmiDueDate = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"),
                  Description = "Description"
              },
              new Loans()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                  LoanNumber = 3,
                  Balance = 60000,
                  EmiDue = 300,
                  EmiDueDate = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"),
                  Description = "Description"
              },
               new Loans()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                  LoanNumber = 3,
                  Balance = 70000,
                  EmiDue = 700,
                  EmiDueDate = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"),
                  Description = "Description"
              }
            };

            foreach (Loans loanDetailsEntry in loanDetailsEntries)
            {
                context.Loans.Add(loanDetailsEntry);

            }
            context.SaveChanges();
        }

        private static void InitialiseShareMster(QuickLoanDbContext context)
        {
            var shareDetailsEntries = new Shares[]
            {
              new Shares()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                  ShareNumber = 0,
                  Balance =10000,
                  Description = "Description"
              },

              new Shares()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                  ShareNumber = 1,
                  Balance = 20000,
                  Description = "Description"
              },

              new Shares()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                  ShareNumber = 3,
                  Balance = 30000,
                  Description = "Description"
              },
                new Shares()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                  ShareNumber = 3,
                  Balance = 40000,
                  Description = "Description"
              },
                  new Shares()
              {
                  AccountsId = context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                  ShareNumber = 3,
                  Balance = 50000,
                  Description = "Description"
              }
            };

            foreach (Shares shareDetailsEntry in shareDetailsEntries)
            {
                context.Shares.Add(shareDetailsEntry);

            }
            context.SaveChanges();
        }

        private static void InitialiseTransaction(QuickLoanDbContext context)
        {
            var transactions = new Transactions[]
            {
                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =0,
                    Postdate = "01/09/2018",
                    Amount = 300
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    Description ="Loan advance",
                    Type = "LOAN",
                    RecordId=0,
                    Postdate = "02/08/2018",
                    Amount = 700
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =0,
                    Postdate = "03/09/2018",
                    Amount = 300
                },

                new Transactions
                {
                   AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =0,
                    Postdate = "04/09/2018",
                    Amount = 200
                },       

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="5000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =0,
                    Postdate = "05/09/2018",
                    Amount = 700
                },

               //-----------------------------------

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =1,
                    Postdate = "01/09/2018",
                    Amount = 300
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    Description ="Loan advance",
                    Type = "LOAN",
                    RecordId=1,
                    Postdate = "02/08/2018",
                    Amount = 700
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "03/09/2018",
                    Amount = 300
                },

                new Transactions
                {
                   AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =1,
                    Postdate = "04/09/2018",
                    Amount = 200
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="6000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "05/09/2018",
                    Amount = 700
                },

                //-----------------------------------
                 new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "02/09/2018",
                    Amount = 300
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    Description ="Loan advance",
                    Type = "LOAN",
                    RecordId=3,
                    Postdate = "02/18/2018",
                    Amount = 700
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "03/20/2018",
                    Amount = 300
                },

                new Transactions
                {
                   AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "04/09/2018",
                    Amount = 200
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="7000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "06/09/2018",
                    Amount = 700
                },
                 //-----------------------------------
                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "02/09/2018",
                    Amount = 500
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    Description ="Loan advance",
                    Type = "LOAN",
                    RecordId=3,
                    Postdate = "02/18/2018",
                    Amount = 900
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "03/20/2018",
                    Amount = 390
                },

                new Transactions
                {
                   AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "04/09/2018",
                    Amount = 200
                },

                new Transactions
                {
                    AccountsId =  context.Accounts.Select(item => item).Where(item => item.AccountNumber=="8000").ToList<Accounts>()[0].Id,
                    Description ="cash deposit",
                    Type = "Share",
                    RecordId =3,
                    Postdate = "06/09/2018",
                    Amount = 700
                },
            };

            foreach (Transactions transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }
            context.SaveChanges();
        }

        private static void InitialiseCards(QuickLoanDbContext context)
        {
            var cards = new Cards[]
            {
                new Cards()
                {
                    CardNumber="7890",
                    AccountsId=1,
                    Type ="CREDIT",
                    PaymentDueDate="04/09/2018",
                    PaymentDue=100
                   
                },

                new Cards()
                {
                    CardNumber="8942",
                    AccountsId=2,
                    Type ="CREDIT",
                    PaymentDueDate="03/03/2018",
                    PaymentDue=200

                },
                new Cards()
                {
                    CardNumber="8945",
                    AccountsId=3,
                    Type ="CREDIT",
                    PaymentDueDate="06/06/2018",
                    PaymentDue=90

                },
                   new Cards()
                {
                    CardNumber="7842",
                    AccountsId=4,
                    Type ="CREDIT",
                    PaymentDueDate="05/03/2019",
                    PaymentDue=50

                }
            };
            foreach (Cards card in cards)
            {
                context.Cards.Add(card);
            }
            context.SaveChanges();
        }

        }
}
