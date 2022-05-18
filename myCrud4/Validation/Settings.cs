using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCrud.Validation
{
    public class Settings
    {
        public const string TransactionNumberPattern = @"^[A-Z]{2}-\d{3}-[A-Z]{2}/\d{2}$";
    }
}
