using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Base
{
    public class Envelope<T>
    {
        public T Data { get; set; }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }
        [JsonIgnore]
        public Dictionary<string, string> ErrorDictionary { get; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        public Envelope()
        {
        }

        public Envelope(string message)
        {
            Message = message;
        }

        public Envelope(Dictionary<string, string> errors, string message = "One or more errors occured")
            : this(message)
        {
            ErrorDictionary = errors;
            Status = ResultStatus.Failed;
        }


        public static Envelope<T> Ok(T data, string message = null)
        {
            return new Envelope<T>
            {
                Data = data,
                Message = message,
                Status = ResultStatus.Complete
            };
        }

        public static Envelope<T> Error(string message, Exception ex = null)
        {
            return new Envelope<T>
            {
                Message = message,
                Exception = ex,
                Status = ResultStatus.Failed
            };
        }

        public static Envelope<T> Error(string message, Dictionary<string, string> errors, Exception ex = null)
        {
            return new Envelope<T>(errors, message)
            {
                Message = message,
                Exception = ex
            };
        }
    }

    public static class Envelope
    {
        public static Envelope<TData> Ok<TData>(TData data, string message = null)
        {
            return new Envelope<TData>
            {
                Data = data,
                Status = ResultStatus.Complete,
                Message = message
            };
        }

        public static Envelope<object> Ok()
        {
            return new Envelope<object>
            {
                Status = ResultStatus.Complete
            };
        }

        public static Envelope<object> Error(string errorMessage)
        {
            return new Envelope<object>(errorMessage)
            {
                Message = errorMessage,
                Status = ResultStatus.Failed
            };
        }

        public static Envelope<object> Error(Dictionary<string, string> error, 
            string message = "One or more errors occured")
        {
            return new Envelope<object>(error, message)
            {
                Status = ResultStatus.Failed
            };
        }
    }
}
