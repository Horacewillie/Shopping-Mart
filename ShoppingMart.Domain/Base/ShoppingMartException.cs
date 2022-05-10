using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Base
{
    public class ShoppingMartException : ApplicationException
    {
        //public ShoppingMartException() : base()
        //{

        //}

        public ShoppingMartException(string message, Exception innerException)
        {

        }

        public ShoppingMartException(string message) : base(message)
        {
            Errors.Add("", message);
        }


        public Dictionary<string, string> Errors { get; private set; } = new Dictionary<string, string>();

        private HttpStatusCode HttpStatusCode { get; set; }

        public ShoppingMartException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors.Add("", message);
        }

        public ShoppingMartException(Dictionary<string, string> errors, string message = null) : base(message ?? "One or more error occured")
        {
            if (errors != null)
            {
                Errors = errors;
            }
        }

        public ShoppingMartException(IList<ValidationFailure> errors,
        string errormessage = "One or more errored occurred") : base(errormessage)
        {
            if (errors == null)
                throw new ArgumentException("Validation errors cannot be null");
            Errors.Clear();
            foreach (var failure in errors)
            {
                if (Errors.ContainsKey(failure.PropertyName))
                    continue;
                Errors.Add(failure.PropertyName, failure.ErrorMessage);
            }
        }
    }
}
