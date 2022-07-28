using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation
{
    public static class StringValidationHelpers
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Checks to see if a string contains only integers
        /// </summary>
        /// <param name="testString">A string that needs to be checked to see if it only has integers</param>
        /// <returns>True if only integers, false if containing other characters</returns>
        public static bool ContainsOnlyIntegers(this string testString)
        {
            bool result = false;

            foreach (char item in testString)
            {
                // check to see if it is a number
                if ((int)item > 47 && (int)item <= 57)
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Checks to see if a string contains could be a valid hour in integers
        /// </summary>
        /// <param name="time">An hour represented as a string</param>
        /// <returns>True if it is a valid hour, false if not</returns>
        public static bool IsValidHour(this string time)
        {
            bool result = false;

            if (time.Length <= 2)
            {
                if (time.ContainsOnlyIntegers())
                {
                    result = true;
                }
            }

            if (time.Length == 2)
            {
                int hour = 0;
                // check to see if it is an allowed time
                try
                {
                    hour = Int32.Parse(time);
                }
                catch (Exception)
                {
                    result = false;
                }
                if (hour.IsValidHour())
                {
                    result = true;
                }
            }
            else if (time.Length > 2)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Checks to see if a string contains could be valid mintutes in integers
        /// </summary>
        /// <param name="time">Minutes represented as a string</param>
        /// <returns>True if it is a valid value for minutes, false if not</returns>
        public static bool IsValidMinute(this string time)
        {
            bool result = false;

            if (time.Length <= 2)
            {
                if (time.ContainsOnlyIntegers())
                {
                    result = true;
                }
            }

            if (time.Length == 2)
            {
                int minute = 0;
                // check to see if it is an allowed time
                try
                {
                    minute = Int32.Parse(time);
                }
                catch (Exception)
                {
                    result = false;
                }
                if (minute.IsValidMintue())
                {
                    result = true;
                }
            }
            else if (time.Length > 2)
            {
                result = false;
            }
            return result;
        }

    }
}
