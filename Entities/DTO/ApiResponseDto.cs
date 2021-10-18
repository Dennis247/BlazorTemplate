using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Entities.DTO
{
    public class ApiResponse<T>
    {
        public T Payload { get; set; }
        public bool IsSucessFull { get; set; }
        public string Message { get; set; }
      //  public int httpStatusCode { get; set; }
    }
}
