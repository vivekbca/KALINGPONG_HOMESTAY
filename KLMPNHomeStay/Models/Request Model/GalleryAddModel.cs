using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Request_Model
{
    public class GalleryAddModel
    {

        public string HsId { get; set; }
        public IFormFile HsLi1 { get; set; }
        public IFormFile HsLi2 { get; set; }
        public IFormFile HsLi3 { get; set; }
        public IFormFile HsLi4 { get; set; }
        public IFormFile HsLi5 { get; set; }
        public IFormFile HsLi6 { get; set; }
        public IFormFile HsLi7 { get; set; }
        public IFormFile HsLi8 { get; set; }
        public IFormFile HsLi9 { get; set; }
        public IFormFile HsLi10 { get; set; }
        public IFormFile HsRi1 { get; set; }
        public IFormFile HsRi2 { get; set; }
        public IFormFile HsRi3 { get; set; }
        public IFormFile HsRi4 { get; set; }
        public IFormFile HsRi5 { get; set; }
        public IFormFile HsRi6 { get; set; }
        public IFormFile HsRi7 { get; set; }
        public IFormFile HsRi8 { get; set; }
        public IFormFile HsRi9 { get; set; }
        public IFormFile HsRi10 { get; set; }
        public decimal? HsMapLat { get; set; }
        public decimal? HsMapLong { get; set; }
    }
}
