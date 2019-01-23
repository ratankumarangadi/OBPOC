using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.Requests.TransactionResponse
{
    
        public class Holder
        {
            public string name { get; set; }
            public bool is_alias { get; set; }
        }

        public class Bank
        {
            public string national_identifier { get; set; }
            public string name { get; set; }
        }

        public class ThisAccount
        {
            public string id { get; set; }
            public List<Holder> holders { get; set; }
            public string number { get; set; }
            public string kind { get; set; }
            public string IBAN { get; set; }
            public string swift_bic { get; set; }
            public Bank bank { get; set; }
        }

        public class Holder2
        {
            public string name { get; set; }
            public bool is_alias { get; set; }
        }

        public class Bank2
        {
            public string national_identifier { get; set; }
            public string name { get; set; }
        }

        public class User
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class CorporateLocation
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public DateTime date { get; set; }
            public User user { get; set; }
        }

        public class User2
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class PhysicalLocation
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public DateTime date { get; set; }
            public User2 user { get; set; }
        }

        public class Metadata
        {
            public string public_alias { get; set; }
            public string private_alias { get; set; }
            public string more_info { get; set; }
            public string URL { get; set; }
            public string image_URL { get; set; }
            public string open_corporates_URL { get; set; }
            public CorporateLocation corporate_location { get; set; }
            public PhysicalLocation physical_location { get; set; }
        }

        public class OtherAccount
        {
            public string id { get; set; }
            public Holder2 holder { get; set; }
            public string number { get; set; }
            public string kind { get; set; }
            public string IBAN { get; set; }
            public string swift_bic { get; set; }
            public Bank2 bank { get; set; }
            public Metadata metadata { get; set; }
        }

        public class NewBalance
        {
            public string currency { get; set; }
            public string amount { get; set; }
        }

        public class Value
        {
            public string currency { get; set; }
            public string amount { get; set; }
        }

        public class Details
        {
            public string type { get; set; }
            public string description { get; set; }
            public DateTime posted { get; set; }
            public DateTime completed { get; set; }
            public NewBalance new_balance { get; set; }
            public Value value { get; set; }
        }

        public class User3
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class Comment
        {
            public string id { get; set; }
            public string value { get; set; }
            public DateTime date { get; set; }
            public User3 user { get; set; }
        }

        public class User4
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class Tag
        {
            public string id { get; set; }
            public string value { get; set; }
            public DateTime date { get; set; }
            public User4 user { get; set; }
        }

        public class User5
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class Image
        {
            public string id { get; set; }
            public string label { get; set; }
            public string URL { get; set; }
            public DateTime date { get; set; }
            public User5 user { get; set; }
        }

        public class User6
        {
            public string id { get; set; }
            public string provider { get; set; }
            public string display_name { get; set; }
        }

        public class Where
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public DateTime date { get; set; }
            public User6 user { get; set; }
        }

        public class Metadata2
        {
            public string narrative { get; set; }
            public List<Comment> comments { get; set; }
            public List<Tag> tags { get; set; }
            public List<Image> images { get; set; }
            public Where where { get; set; }
        }

        public class TransactionResponse
        {
            public string id { get; set; }
            public ThisAccount this_account { get; set; }
            public OtherAccount other_account { get; set; }
            public Details details { get; set; }
            public Metadata2 metadata { get; set; }
        }
    
}



