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
        public Employye(string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD) : base(id, firstName, lastName, city, street, houseNumber, password)
        {
        }

        public bool EmloyeSigning(string password)
        {
            return EMPLOYE_SIGNING_CODE == password;
        }
    }
}
