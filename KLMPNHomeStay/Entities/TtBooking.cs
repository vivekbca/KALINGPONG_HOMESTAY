using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtBooking
    {
        public TtBooking()
        {
            TtBookingRoomDetail = new HashSet<TtBookingRoomDetail>();
            TtHsFeedback = new HashSet<TtHsFeedback>();
        }

        public string GuId { get; set; }
        public string HsId { get; set; }
        public string HsBookingId { get; set; }
        public DateTime BkBookingDate { get; set; }
        public DateTime BkDateFrom { get; set; }
        public DateTime BkDateTo { get; set; }
        public byte BkNoPers { get; set; }
        public string BkRoomNumber { get; set; }
        public byte BkIsAvailed { get; set; }
        public int? TotalCost { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string Ifsc { get; set; }
        public string BkPaymentMode { get; set; }
        public string BkPaymentStatus { get; set; }
        public int? BkPaymentAmount { get; set; }
        public string BkPmtVoucharNo { get; set; }
        public DateTime? BkPmtVoucharDate { get; set; }
        public short? BkIsCancelled { get; set; }
        public string BkCancellationReason { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? BkCancelledDate { get; set; }
        public string BkRefundMode { get; set; }
        public short BkIsRefundStatus { get; set; }
        public string BkRefundStatus { get; set; }
        public int? BkRefundAmount { get; set; }
        public string BkRfdVoucharNo { get; set; }
        public DateTime? BkRfdVoucharDate { get; set; }
        public string BkRefundBy { get; set; }
        public DateTime? BkRefundOn { get; set; }
        public short IsCheckedByAdmin { get; set; }
        public short IsCheckedByBankUser { get; set; }
        public short IsCancelCheckedByAdmin { get; set; }
        public short IsCancelCheckedByBankUser { get; set; }
        public short? IsReportChecked { get; set; }

        public virtual TmUser BkRefundByNavigation { get; set; }
        public virtual TmGuestUser CancelledByNavigation { get; set; }
        public virtual TmGuestUser Gu { get; set; }
        public virtual TmHomestay Hs { get; set; }
        public virtual ICollection<TtBookingRoomDetail> TtBookingRoomDetail { get; set; }
        public virtual ICollection<TtHsFeedback> TtHsFeedback { get; set; }
    }
}
