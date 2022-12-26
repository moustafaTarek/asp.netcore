using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessage(statusCode);
        }
        
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "UnAuthorized",
                404 => "Resource was not found",
                500 => "Server Error",
                _=>null
            };
        }
    }
}