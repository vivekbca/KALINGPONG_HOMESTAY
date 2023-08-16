using System;
using System.Collections.Generic;

namespace KLMPNHomeStay.Entities
{
    public partial class TmFinancialYear
    {
        public TmFinancialYear()
        {
            TmTender = new HashSet<TmTender>();
        }

        public string FinancialYearId { get; set; }
        public string FinancialYear { get; set; }

        public virtual ICollection<TmTender> TmTender { get; set; }
    }
}
