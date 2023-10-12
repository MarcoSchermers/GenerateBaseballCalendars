using System.Collections.Generic;

namespace GenerateBaseballCalendars
{
    public class FromTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Person
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string link { get; set; }
    }

    public class TransactionsRoot
    {
        public string copyright { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class ToTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Transaction
    {
        public int id { get; set; }
        public Person person { get; set; }
        public ToTeam toTeam { get; set; }
        public string date { get; set; }
        public string effectiveDate { get; set; }
        public string resolutionDate { get; set; }
        public string typeCode { get; set; }
        public string typeDesc { get; set; }
        public string description { get; set; }
        public FromTeam fromTeam { get; set; }

        public int personId
        {
            get
            {
                return person.id;
            }
        }

        public string personFullname
        {
            get
            {
                return person.fullName;
            }
        }

        public override string ToString()
        {
            return date + "-" + description;
        }
    }
}
