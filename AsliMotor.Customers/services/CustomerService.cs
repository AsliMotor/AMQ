using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.Customers
{
    public class CustomerService : ICustomerService
    {
        public IReportingRepository ReportingRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        
        public void Create(Customer cust)
        {
            FailIfExistEmail(cust);
            FailIfExistKTPNo(cust);
            ReportingRepository.Save<Customer>(cust);
        }

        public void Update(Customer cust)
        {
            Customer exist = CustomerRepository.GetById(cust.id);
            if (exist.Email != cust.Email)
                FailIfExistEmail(cust);
            if(exist.KTPNo != cust.KTPNo)
                FailIfExistKTPNo(cust);
            Customer custUpdated = new Customer
            {
                id = exist.id,
                BranchId = exist.BranchId,
                Address = cust.Address,
                City = cust.City,
                Email = cust.Email,
                Name = cust.Name,
                Outstanding = exist.Outstanding,
                Phone = cust.Phone,
                Status = exist.Status,
                Region = cust.Region,
                KTPNo = cust.KTPNo,
                KTPDate = cust.KTPDate,
                KTPPublisher = cust.KTPPublisher,
                Job = cust.Job,
                Gender = cust.Gender,
                Birthday = cust.Birthday
            };
            ReportingRepository.Update<Customer>(custUpdated, new { id = custUpdated.id });
        }

        public void UpdateCreditNote(Guid custId, decimal creditNote)
        {
            Customer cust = CustomerRepository.GetById(custId);
            cust.Deposit = creditNote;
            ReportingRepository.Update<Customer>(cust, new { id = cust.id });
        }

        public void UploadImage(Guid id, byte[] image)
        {
            if (CustomerRepository.GetImage(id) == null)
            {
                ReportingRepository.Save<CustomerImage>(new CustomerImage { id = id, Image = image });
            }
            else
            {
                ReportingRepository.Update<CustomerImage>(new CustomerImage { id = id, Image = image }, new { id = id });
            }
        }

        public void ChangeStatus(Guid id, StatusCustomer status)
        {
            Customer exist = CustomerRepository.GetById(id);
            exist.Status = (int) status;
            ReportingRepository.Update<Customer>(exist, new { id = exist.id });
        }

        private void FailIfExistEmail(Customer cust)
        {
            if (cust.Email != null && CustomerRepository.GetByEmail(cust.Email, cust.BranchId) != null)
                throw new Exception(string.Format("Email {0} telah terdaftar di perusahaan anda", cust.Email));
        }

        private void FailIfExistKTPNo(Customer cust)
        {
            if (CustomerRepository.GetByKTPNo(cust.KTPNo, cust.BranchId) != null)
                throw new Exception(string.Format("Pelanggan dengan Nomor KTP {0} telah terdaftar di perusahaan anda", cust.KTPNo));
        }
    }
}
