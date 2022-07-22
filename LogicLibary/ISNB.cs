using System;
using System.Collections.Generic;
using System.Linq;

namespace LibaryModel
{
    public class ISBN
    {
        private const int PREFIX_NUM = 978;
        private const int UNKNOWN_DEFAULT_CODE = 000;
        private const int ISRAEL_CODE = 965;

        private int _country;
        private int _publisher;
        private int _serialNumber;

        public static Dictionary<string, int> CountriesDict { get; set; }
        public static Dictionary<string, int> PublishersDict { get; set; }
        public int SerialNumber { get { return _serialNumber; } private set { _serialNumber = value<9999 &&value>=0 ? value : UNKNOWN_DEFAULT_CODE; } }
        public int Control { get { return (Country + Publisher + SerialNumber) % 10; } }
        public int Country
        {
            get { return _country; }
            set
            {
                if (CountriesDict.ContainsValue(value))
                    _country = value;
                else
                    _country = UNKNOWN_DEFAULT_CODE;
            }
        }
        public int Publisher
        {
            get { return _publisher; }
            set
            {
                if (PublishersDict.ContainsValue(value))
                    _publisher = value;
                else
                    _publisher = UNKNOWN_DEFAULT_CODE;
            }
        }
        static ISBN()
        {
            PublishersDict = new Dictionary<string, int>();
            CountriesDict = new Dictionary<string, int>();
        }
        public ISBN(int publisher, int serialNumber, int countryInt = ISRAEL_CODE)
        {
            Publisher = publisher;
            SerialNumber = serialNumber;
            Country = countryInt;
        }
        public ISBN(string publisherSt, int serialNumber, string countrySt)
        {
            SerialNumber = serialNumber;
            if (CountriesDict.ContainsKey(countrySt))
                Country = CountriesDict[countrySt];
            else
                Country = UNKNOWN_DEFAULT_CODE;
            if (PublishersDict.ContainsKey(publisherSt))
                Publisher = PublishersDict[publisherSt];
            else
                Publisher = UNKNOWN_DEFAULT_CODE;
        }

        public override string ToString()
        {
            return $"{PREFIX_NUM}-{Country:D4}-{Publisher:D3}-{SerialNumber:D3}-{Control}";
        }

        public override bool Equals(object obj)
        {
            if (obj is ISBN other)
            {
                return Country == other.Country &&
                    Publisher == other.Publisher &&
                    SerialNumber == other.SerialNumber&&
                    Control==other.Control;
            }
            return false;
        }
    }
}

