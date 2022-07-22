using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryModel
{
    public class Employye : Person
    {
        private const string EMPLOYE_SIGNING_CODE = "12345";
        private Employye(string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD) : base(id, firstName, lastName, city, street, houseNumber, password)
        {
        }

        public static Employye EmloyeSigning(string managerPassword, string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD)
        {
            if (EMPLOYE_SIGNING_CODE == managerPassword)
            {
                return new Employye(id, firstName, lastName, city, street, houseNumber, password);
            }
            return null;
        }
    }
}
