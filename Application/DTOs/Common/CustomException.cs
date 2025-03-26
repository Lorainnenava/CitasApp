using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }

        public CustomException(int statusCode, string errorMessage, string details = null)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Details = details;
        }
    }
}
