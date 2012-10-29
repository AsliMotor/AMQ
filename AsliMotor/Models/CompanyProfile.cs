using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsliMotor.Models
{
    public class CompanyProfile
    {
        const string COMPANY_PROFILE_FIELD = "companyprofile";
        HttpSessionStateBase context;
        public CompanyProfile(HttpContextBase context)
        {
            this.context = context.Session;
        }
        public CompanyProfile(HttpSessionStateBase context)
        {
            this.context = context;
        }
        public string OwnerId
        {
            get { return GetCompanyProfile().OwnerId; }
            set { GetCompanyProfile().OwnerId = value; }
        }
        public string BranchId
        {
            get { return GetCompanyProfile().BranchId; }
            set { GetCompanyProfile().BranchId = value; }
        }
        public int Role
        {
            get { return GetCompanyProfile().Role; }
            set { GetCompanyProfile().Role = value; }
        }
        public string RoleName
        {
            get { return GetCompanyProfile().RoleName; }
            set { GetCompanyProfile().RoleName = value; }
        }
        public string HomePage
        {
            get { return GetCompanyProfile().HomePage; }
            set { GetCompanyProfile().HomePage = value; }
        }
        public string UserName
        {
            get { return GetCompanyProfile().UserName; }
            set { GetCompanyProfile().UserName = value; }
        }
        private CompanyProfileModel GetCompanyProfile()
        {
            if (context[COMPANY_PROFILE_FIELD] == null)
                context[COMPANY_PROFILE_FIELD] = new CompanyProfileModel();
            return (CompanyProfileModel)context[COMPANY_PROFILE_FIELD];
        }
    }

    public class CompanyProfileModel
    {
        public string OwnerId { get; set; }
        public string BranchId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public string RoleName { get; set; }
        public string HomePage { get; set; }
    }
}