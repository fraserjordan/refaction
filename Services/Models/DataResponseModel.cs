using System;
using System.Collections.Generic;
using Service.Interfaces;

namespace Service.Models
{
    public class DataResponseModel : IDataResponseModel
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
