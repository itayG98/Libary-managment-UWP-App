using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryModel
{
    /// <summary>
    /// A class represent Employee in the libary system
    /// </summary>
    public class Employee : Person
    {
        private const string EMPLOYE_SIGNING_CODE = "12345"; //Deafult first password for Employye
        private Employee(string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD) : base(id, firstName, lastName, city, street, houseNumber, password)
        {
        }

        public static Employee EmloyeSigning(string managerPassword, string id, string firstName, string lastName, string city, string street, int houseNumber = -1, string password = DEFAULT_PASSWORD)
        //Can sign Employee only if inserted a correct password
        {
            if (EMPLOYE_SIGNING_CODE == managerPassword)
            {
                return new Employee(id, firstName, lastName, city, street, houseNumber, password);
            }
            return null;
        }
    }
}
