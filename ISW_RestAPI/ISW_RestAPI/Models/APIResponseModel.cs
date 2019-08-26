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
        public object Content;
        public bool Error;

        public APIResponseModel(string message, object content, bool error)
        {
            Message = message;
            Content = content;
            Error = error;
        }

        public APIResponseModel()
        {
        }
    }
}