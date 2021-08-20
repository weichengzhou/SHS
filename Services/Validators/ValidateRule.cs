using System;


namespace SHS.Services.Validators
{
    public class ValidateRule
    {
        public static bool IsDateFormat(string str)
        {
            if(str is null)
                return true;
            else if(str == "")
                return true;
            DateTime result;
            return DateTime.TryParse(str, out result);
        }
    }
}