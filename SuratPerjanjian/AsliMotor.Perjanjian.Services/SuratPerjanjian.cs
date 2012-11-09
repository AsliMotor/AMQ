using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Perjanjian.Services
{
    public class SuratPerjanjian : IViewModel
    {
        public Guid InvoiceId { get; set; }
        public string SuratPerjanjianNo { get; set; }
        public DateTime SuratPerjanjianDate { get; set; }
    }
}
