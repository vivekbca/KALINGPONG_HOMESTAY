using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmTender
    {
        public string TenderId { get; set; }
        public string Subject { get; set; }
        public string FinancialYearId { get; set; }
        public DateTime PublishingDate { get; set; }
        public string MemoNo { get; set; }
        public short IsPublished { get; set; }
        public DateTime ClosingDate { get; set; }
        public string FileName { get; set; }

        public virtual TmFinancialYear FinancialYear { get; set; }
    }
}
