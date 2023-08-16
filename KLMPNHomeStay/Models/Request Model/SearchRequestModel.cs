using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class SearchRequestModel
    {
    }
    public class SearchRequestByPriceModel
    {
        public int fromPrice { get; set; }
        public int toPrice { get; set; }
    }
    public class SessionTable
    {
        public string PropertyId { get; set; }
    }
}
