using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class PackageDateAddResponseModel
    {
    }
    public class PackageListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public short ? isActive { get; set; }

    }
}
