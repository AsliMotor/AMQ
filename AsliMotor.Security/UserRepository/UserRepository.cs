using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.Security
{
    public class UserRepository:IUserRepository
    {
        QueryObjectMapper _qryObjecyMapper;
        public IReportingRepository ReportingRepository { get; set; }
        public UserRepository()
        {
            _qryObjecyMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
            ReportingRepository = ContextRegistry.GetContext().GetObject("ReportingRepository") as IReportingRepository;
        }

        public Account GetUserByEmail(string email)
        {
            Account user = _qryObjecyMapper.Map<Account>("FindUserByEmail", new string[] { "email" }, new object[] { email }).FirstOrDefault();
            return user;
        }

        public Account GetUserByUserName(string username)
        {
            Account user = _qryObjecyMapper.Map<Account>("FindUserByUsername", new string[] { "username" }, new object[] { username }).FirstOrDefault();
            return user;
        }

        public void Save(Account user)
        {
            ReportingRepository.Save<Account>(user);
        }

        public Account GetUserByBranchId(string branchId)
        {
            Account user = _qryObjecyMapper.Map<Account>("FindUserByBranchId", new string[] { "branchid" }, new object[] { branchId }).FirstOrDefault();
            return user;
        }
    }
}
