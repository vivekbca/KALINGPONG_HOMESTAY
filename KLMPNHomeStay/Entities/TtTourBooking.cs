using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TtTourBooking
    {
        public TtTourBooking()
        {
            TtPackageFeedback = new HashSet<TtPackageFeedback>();
            TtTourBookingDetail = new HashSet<TtTourBookingDetail>();
        }

        public string Id { get; set; }
        public string TourId { get; set; }
        public string TourDateId { get; set; }
        public string GuId { get; set; }
        public DateTime BookingDate { get; set; }
        public int TotalRate { get; set; }
        public int NoOfPerson { get; set; }
        public short IsCompleted { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string Ifsc { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
        public int? PaymentAmount { get; set; }
        public string PaymentVoucherNo { get; set; }
        public DateTime? PaymentVoucherDate { get; set; }
        public short IsCancel { get; set; }
        public string CancellationReason { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }
        public short RefundStatus { get; set; }
        public DateTime? RefundOn { get; set; }
        public string RefundBy { get; set; }
        public string RfdVoucherMode { get; set; }
        public string RfdVoucherStatus { get; set; }
        public int? RfdVoucherAmount { get; set; }
        public string RfdVoucherNo { get; set; }
        public DateTime? RfdVoucherDate { get; set; }
        public short IsCheckedByAdmin { get; set; }
        public short IsCheckedByBankUser { get; set; }
        public short IsCancelCheckedByAdmin { get; set; }
        public short IsCancelCheckedByBankUser { get; set; }
        public short? IsReportChecked { get; set; }

        public virtual TmGuestUser CancelledByNavigation { get; set; }
        public virtual TmGuestUser Gu { get; set; }
        public virtual TmUser RefundByNavigation { get; set; }
        public virtual TmTour Tour { get; set; }
        public virtual TtTourDate TourDate { get; set; }
        public virtual ICollection<TtPackageFeedback> TtPackageFeedback { get; set; }
        public virtual ICollection<TtTourBookingDetail> TtTourBookingDetail { get; set; }
    }
}
