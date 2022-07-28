
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation
{
    public static class IntegerValidationHelpers
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Checks to see if the integer is between 0 and 12 inclusive
        /// </summary>
        /// <param name="hour">One to two digit number</param>
        /// <returns>True if between 0 and 12 inclusive, false if not</returns>
        public static bool IsValidHour(this int hour)
        {
            bool result = false;

            if (hour >= 0 && hour <= 12)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Checks to see if the integer is between 0 and 59 inclusive
        /// </summary>
        /// <param name="minute">One to two digit number</param>
        /// <returns>True if between 0 and 59 inclusive, false if not</returns>
        public static bool IsValidMintue(this int minute)
        {
            bool result = false;

            if (minute >= 0 && minute <= 59)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Converts integers to a 24 hour clock
        /// </summary>
        /// <param name="hour">Hour to check and convert</param>
        /// <param name="isAM">true to covert for AM, False to convert for PM</param>
        /// <returns></returns>
        public static int ConvertTo24HourTime(this int hour, bool isAM)
        {
            // add 12 if the start time is in the PM
            if (!isAM && hour != 12)
            {
                hour += 12;
            }
            if (isAM && hour == 12)
            {
                hour = 0;
            }
            return hour;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Validation method for determining if the start time is before the end time
        /// </summary>
        /// <param name="startHour"></param>
        /// <param name="startMinutes"></param>
        /// <param name="endHour"></param>
        /// <param name="endMinutes"></param>
        /// <returns></returns>
        public static bool ValidateStartTimeBeforeEndTime(int startHour, int startMinutes, int endHour, int endMinutes)
        {
            bool result = false;

            // check to see that one time comes after the other
            if (startHour > endHour)
            {                
                throw new ApplicationException("The end time is before the start time. Please change.");                
            }
            else if (startHour == endHour && startMinutes >= endMinutes)
            {
                throw new ApplicationException("The end time is before the start time or at the same time. Please change.");
            }

            return result;
        }


    }
}
