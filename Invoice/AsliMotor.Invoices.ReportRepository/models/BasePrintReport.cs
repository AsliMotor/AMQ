using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    public class BasePrintReport : IViewModel
    {
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Warna { get; set; }
        public string Tahun { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoPolisi { get; set; }
        public string CustomerName { get; set; }
        public string NoKtp { get; set; }
        public DateTime KtpDate { get; set; }
        public string KtpPublisher { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string Job { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
    }
}
