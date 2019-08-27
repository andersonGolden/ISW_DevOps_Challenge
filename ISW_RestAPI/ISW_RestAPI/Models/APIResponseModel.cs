using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ISW_RestAPI.Models
{
    public class APIResponseModel
    {
        public string Message;
        public int StatusCode;
        public object Content;
        public bool Error;

        public APIResponseModel(string message, HttpStatusCode status, object content, bool error)
        {
            Message = message;
            StatusCode = (int)status;
            Content = content;
            Error = error;
        }

        public APIResponseModel()
        {
        }
    }
}