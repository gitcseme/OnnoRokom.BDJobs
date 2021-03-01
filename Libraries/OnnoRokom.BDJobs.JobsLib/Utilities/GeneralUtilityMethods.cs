using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Utilities
{
    public class GeneralUtilityMethods
    {
        public static string GetFormattedDate(DateTime dateTime)
        {
            var day = dateTime.Day;
            var month = dateTime.ToString("MMM");
            var year = dateTime.Year;
            var time = dateTime.ToString("hh:mm ") + dateTime.ToString("tt").ToLower();
            string formattedDate = $"{month} {day} {year}, {time}";

            return formattedDate;
        }
    }
}
