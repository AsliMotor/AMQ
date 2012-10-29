using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr3.ST;
using AsliMotor.Organizations;
using AsliMotor.SI.Repository;

namespace AsliMotor.PrintDocuments
{
    public class PrintDocument : IPrintDocument
    {
        IOrganizationRepository _orgRepo;
        ISupplierInvoiceRepository _siRepo;
        public PrintDocument()
        {
            _orgRepo = new OrganizationRepository();
            _siRepo = new SupplierInvoiceRepository();
        }

        public string PrintSI(Guid siId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(SIStringTemplate.DEFAULT);
            SupplierInvoice si = _siRepo.GetById(siId, branchId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("Merk", (si.Merk == "") ? "-": si.Merk);
            template.SetAttribute("NoBpkb", (si.NoBpkb == "") ? "-": si.NoBpkb);
            template.SetAttribute("NoMesin", (si.NoMesin == "") ? "-": si.NoMesin);
            template.SetAttribute("NoPolisi", (si.NoPolisi == "") ? "-" : si.NoPolisi);
            template.SetAttribute("NoRangka", (si.NoRangka == "") ? "-" : si.NoRangka);
            template.SetAttribute("Note", (si.Note == "") ? "-" : si.Note);
            template.SetAttribute("Tahun", (si.Tahun == "") ? "-" : si.Tahun);
            template.SetAttribute("SupplierInvoiceNo", si.SupplierInvoiceNo);
            template.SetAttribute("Type", si.Type);
            template.SetAttribute("SIDate", si.SupplierInvoiceDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("Total", si.HargaBeli.ToString("###,###,###,##0.#0"));
            template.SetAttribute("terbilang", SayNumber.Terbilang(si.HargaBeli) + "Rupiah");
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            return template.ToString();
        }

        public string PrintSuratPerjanjianJualBeli(Guid invId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(PerjanjianStringTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            return template.ToString();
        }

        public string PrintSuratPernyataanKredit(Guid invId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(SuratPernyataanKreditTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            return template.ToString();
        }
    }
}
