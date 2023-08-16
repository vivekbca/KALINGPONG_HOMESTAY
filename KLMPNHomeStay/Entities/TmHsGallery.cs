using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmHsGallery
    {
        public string HsId { get; set; }
        public string HsLi1 { get; set; }
        public string HsLi2 { get; set; }
        public string HsLi3 { get; set; }
        public string HsLi4 { get; set; }
        public string HsLi5 { get; set; }
        public string HsLi6 { get; set; }
        public string HsLi7 { get; set; }
        public string HsLi8 { get; set; }
        public string HsLi9 { get; set; }
        public string HsLi10 { get; set; }
        public string HsRi1 { get; set; }
        public string HsRi2 { get; set; }
        public string HsRi3 { get; set; }
        public string HsRi4 { get; set; }
        public string HsRi5 { get; set; }
        public string HsRi6 { get; set; }
        public string HsRi7 { get; set; }
        public string HsRi8 { get; set; }
        public string HsRi9 { get; set; }
        public string HsRi10 { get; set; }
        public decimal? HsMapLat { get; set; }
        public decimal? HsMapLong { get; set; }

        public virtual TmHomestay Hs { get; set; }
    }
}
