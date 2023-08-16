using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtBankTransaction
    {
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public string HsId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int? TransAmount { get; set; }
        public string TransMode { get; set; }
        public string TransStatus { get; set; }
        public byte IsMailed { get; set; }

        public virtual TmHomestay Hs { get; set; }
        public virtual TmUser User { get; set; }
    }
}
