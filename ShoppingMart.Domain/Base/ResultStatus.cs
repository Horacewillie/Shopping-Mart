using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Base
{
    public enum ResultStatus
    {
        Complete,
        Failed
    }

    public enum LogStatus
    {
        Info,
        Error,
        Critical
    }
}
