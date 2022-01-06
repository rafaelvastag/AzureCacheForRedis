using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Functions.Models
{
    public class ResponseDTO
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }

        public ResponseDTO(string message, object data, string token)
        {
            Message = message;
            Data = data;
            Token = token;
        }
    }
}
