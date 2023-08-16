using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Common
{
    public enum ResponseTypes
    {
        Success = 0,
        Error = 1,
        Info = 2,
        ModelErr = 3
    }
    public class ApiResponseModel
    {
        public ResponseTypes Result { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

    }
    public class ApiResponseModelFinal
    {
        public string Result { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
