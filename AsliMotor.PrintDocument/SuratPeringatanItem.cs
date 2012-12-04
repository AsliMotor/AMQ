using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratPeringatanItem
    {
        public int No { get; set; }
        public long AngsuranKe { get; set; }
        public string DueDate { get; set; }
        public string StringAngsuran { get; set; }
        public decimal Angsuran { get; set; }
        public string StringDenda { get; set; }
        public decimal Denda { get; set; }
        public string StringTotal { get; set; }
        public decimal Total { get; set; }
    }
}
