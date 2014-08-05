using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public interface IPrintDocument
    {
        string PrintSI(Guid siId, string branchId);
        string PrintSuratPerjanjianJualBeli(Guid invId, string branchId);
        string PrintSuratPernyataanKredit(Guid invId, string branchId);
        string PrintSuratPernyataan(Guid invId, string branchid);
        string PrintKwitansiTandaJadi(Guid invId, string branchId);
        string PrintKwitansiKontan(Guid invId, string branchId);
        string PrintKwitansiUangMuka(Guid invId, string branchid);
        string PrintKwitansiAngsuranBulanan(Guid rcvId, string branchid);
        string PrintSuratPeringatan(Guid invId, string branchid);
        string PrintSuratPernyataanMampu(Guid invId, string branchid);
        string PrintSuratKuasa(Guid invId, string branchid);
        string PrintJBAngsuran(Guid invId, string branchid);
        string PrintJBFidusia(Guid invId, string branchid);
        string PrintTandaTerima(Guid invId, string branchid);
        string PrintKwitansiPelunasan(Guid rcvId, string branchid);
    }
}
