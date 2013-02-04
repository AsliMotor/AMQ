using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AsliMotor.SI.Repository;
using AsliMotor.SI.Services;
using AsliMotor.Products;
using AsliMotor.Products.Models;
using AsliMotor.Customers;
using AsliMotor.Invoices.Services;
using AsliMotor.Invoices.Command;

namespace AsliMotor.ImportDataInvoice
{
    public class ImportDataInvoiceService
    {
        string branchId;
        string userName;
        StreamReader streamReader;
        public ISupplierInvoiceService SupplierInvoiceService { get; set; }
        public IProductService ProductService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IInvoiceService InvoiceService { get; set; }
        int index = 0;

        public ImportDataInvoiceService()
        {
        }

        public void Import(string branchId, string username, string fileLocation)
        {
            this.branchId = branchId;
            this.userName = username;
            ReadFile(fileLocation);
            ImportSupplierInvoice();
        }
        private void ReadFile(string fileLocation)
        {
            streamReader = new StreamReader(fileLocation);
        }
        private void ImportSupplierInvoice()
        {
            using (streamReader)
            {
                streamReader.ReadLine();
                while (true)
                {
                    string rawData = streamReader.ReadLine();
                    if (rawData == null || rawData.Trim() == string.Empty) break;

                    try
                    {
                        index++;
                        SupplierInvoice si = ParseSupplierInvoice(rawData);
                        Customer pelanggan = ParseCustomer(rawData);
                        Customer cust = SaveCustomer(pelanggan);
                        CreditCommand command = ParseCommandCreateInvoice(rawData, cust.id, si.ProductId);
                        SupplierInvoiceService.Create(si, userName);
                        InvoiceService.Credit(command, this.userName);
                        ProductService.ChangeStatus(si.ProductId, this.branchId, ParseStatusProduct(rawData), this.userName);
                        
                        int countPaid = ParseCountPaid(rawData);
                        for (int i = 0; i < countPaid; i++)
                        {
                            InvoiceService.BayarAngsuran(command.id, command.InvoiceDate.AddDays(i * 30), 1, command.AngsuranBulanan, this.userName);
                        }
                        InvoiceService.ChangeDueDate(command.id, command.InvoiceDate.AddMonths(countPaid + 1), userName);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private Customer SaveCustomer(Customer cust)
        {
            Customer c = CustomerRepository.GetByKTPNo(cust.KTPNo, cust.BranchId);
            if (c == null)
            {
                CustomerService.Create(cust);
                return cust;
            }
            return c;
        }

        private int ParseCountPaid(string rawData)
        {
            string[] itemRow = rawData.Split(',');
            int count = int.Parse(itemRow[16]);
            return count;
        }

        private CreditCommand ParseCommandCreateInvoice(string rawData, Guid custId, Guid productId)
        {
            string[] itemRow = rawData.Split(',');
            string branchId = this.branchId;
            string[] date = itemRow[1].Split('-');
            DateTime invoiceDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
            decimal price = decimal.Parse(itemRow[12]);
            decimal uangmuka = decimal.Parse(itemRow[8]);
            int lamaAngsuran = int.Parse(itemRow[9]);
            decimal angsuranBulanan = decimal.Parse(itemRow[10]);
            decimal plafon = price - uangmuka;
            decimal sukubunga = (((angsuranBulanan * lamaAngsuran) - plafon) / (plafon * decimal.Parse((lamaAngsuran / (double)12).ToString()) ) * 100);
            DateTime duedate = invoiceDate.AddDays(30);
            return new CreditCommand()
            {
                id = Guid.NewGuid(),
                BranchId = branchId,
                CustomerId = custId,
                DueDate = duedate,
                InvoiceDate = invoiceDate,
                LamaAngsuran = lamaAngsuran,
                Price = price,
                ProductId = productId,
                SukuBunga = sukubunga,
                UangMuka = uangmuka,
                AngsuranBulanan = angsuranBulanan
            };
        }

        private Customer ParseCustomer(string row)
        {
            string[] itemRow = row.Split(',');
            string customerName = itemRow[2];
            string billingAddress = itemRow[11];
            string city = itemRow[17];
            string phone = itemRow[18];
            string noKtp = (itemRow[3] == "-" || itemRow[3] == "") ? null : itemRow[3];
            return new Customer()
            {
                id = Guid.NewGuid(),
                BranchId = this.branchId,
                KTPNo = noKtp,
                KTPDate = DateTime.Now,
                Birthday = DateTime.Now,
                Name = customerName,
                Address = billingAddress,
                City = city,
                Phone = phone
            };
        }
        private SupplierInvoice ParseSupplierInvoice(string row)
        {
            string[] itemRow = row.Split(',');
            Guid id = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            string[] date = itemRow[1].Split('-');
            DateTime supplierInvoiceDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
            string supplierName = null;
            string noTelp = null;
            string supplierBillingAddress = null;
            string merk = itemRow[4];
            string type = itemRow[5];
            string tahun = null;
            string warna = itemRow[6];
            string noRangka = itemRow[13];
            string noMesin = itemRow[14];
            string noBPKB = null;
            string noPolisi = "BP " + itemRow[7];
            string note = null;
            decimal hargabeli = 0M;
            decimal charge = 0M;

            return new SupplierInvoice()
                {
                    BranchId = this.branchId,
                    Charge = charge,
                    HargaBeli = hargabeli,
                    id = id,
                    ProductId = productId,
                    Merk = merk,
                    NoBpkb = noBPKB,
                    NoMesin = noMesin,
                    NoPolisi = noPolisi,
                    NoRangka= noRangka,
                    Note = note,
                    NoTelp = noTelp,
                    SupplierBillingAddress = supplierBillingAddress,
                    SupplierInvoiceDate = supplierInvoiceDate,
                    SupplierName = supplierName,
                    Tahun = tahun,
                    Type = type,
                    Warna = warna
                };
        }
        private string ParseStatusProduct(string row)
        {
            string[] itemRow = row.Split(',');
            string status = itemRow[15];

            if (status.ToLower() == StatusProduct.AKTIF.ToLower())
                return StatusProduct.AKTIF;
            else if (status.ToLower() == StatusProduct.TERJUAL.ToLower())
                return StatusProduct.TERJUAL;
            else if (status.ToLower() == StatusProduct.NONAKTIF.ToLower())
                return StatusProduct.NONAKTIF;
            else
                return StatusProduct.AKTIF;
        }
    }
}
