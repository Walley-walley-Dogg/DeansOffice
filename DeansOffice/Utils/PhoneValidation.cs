using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DeansOffice.Utils
{
    public class PhoneValidation
    {
        static public bool is_good(string phone)
        {
            string phonePattern = @"^\+?\d{10,15}$";

            if (!Regex.IsMatch(phone, phonePattern))
            {
                return false;
            }
            return true;
        }
    }
}
