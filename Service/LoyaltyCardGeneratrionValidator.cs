using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LoyaltyCardGeneratrionValidator
    {
        public bool ValidateLength(int length, string code)
        {
            bool isValidated = false;

            if (code.Length > 0)
            {
                if (length < code.Length)
                { isValidated = false; }
                else
                { isValidated = true; }
            }
            else
            { isValidated = false; }

            return isValidated;
        }
    }
}
