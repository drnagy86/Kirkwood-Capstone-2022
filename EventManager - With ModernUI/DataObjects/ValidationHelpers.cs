using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WPFPresentation
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Boolean value for checking if we are currently editing. Made public by EditOngoing.
        /// </summary>
        private static bool editing = false;
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Boolean value for checking if we are currently editing. 
        /// </summary>
        public static bool EditOngoing {
            get {
                return editing;
            }
            set
            {
                editing = value;
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// String validation method. Uses a simple regex to check for roughly-valid email addresses.
        /// 
        /// </summary>
        public static bool IsValidEmailAddress(this string email)
        {
            bool isValid = false;
            Regex rx = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+[.][a-zA-Z]+");
            if (rx.IsMatch(email))
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// String validation method. Uses a few regexes to check for relatively simple password complexity.
        /// 
        /// </summary>
        public static bool IsValidPassword(this string password)
        {
            bool isValid = false;

            Regex rxLower = new Regex(@"[a-z]+");
            Regex rxUpper = new Regex(@"[A-Z]+");
            Regex rxNumber = new Regex(@"[0-9]+");
            Regex rxSpecial = new Regex("[ !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]+");

            if (rxLower.IsMatch(password) && rxUpper.IsMatch(password) &&
                rxNumber.IsMatch(password) && rxSpecial.IsMatch(password))
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// String validation method. Checks if string is empty. 
        /// 
        /// </summary>
        /// <param name="name">A string to be checked for validity as a name</param>
        /// <returns></returns>
        public static bool IsValidName(this string name)
        {
            return name.Length > 0;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// String validation method. Checks if string is empty. 
        /// 
        /// </summary>
        /// <param name="city">A string to be checked for validity as a name</param>
        /// <returns></returns>
        public static bool IsValidCityName(this string city)
        {
            return city.Length > 0;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// String validation method. Just checks if the string is empty for now.
        /// 
        /// </summary>
        /// <param name="state">A string to be checked for validity as a name</param>
        /// <returns></returns>
        public static bool IsValidStateName(this string state)
        {
            return state.Length > 0;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// String validation method. Uses regex to check for zip code validity
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static bool IsValidZipCode(this string zip)
        {
            bool isValid = false;
            Regex rx = new Regex(@"^[0-9\-]{5,12}");
            if (rx.IsMatch(zip))
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/3/18
        /// 
        /// Description:
        /// String validation method, checks for valid phone number format.
        /// Regex found at https://stackoverflow.com/questions/16699007/regular-expression-to-match-standard-10-digit-phone-number
        /// 
        /// </summary>
        public static bool IsValidPhone(this string phone)
        {
            bool isValid = false;
            Regex rx = new Regex(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
            if (rx.IsMatch(phone))
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
