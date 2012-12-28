using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace AsliMotor.Organizations
{
    public class OrganizationRepository:IOrganizationRepository
    {
        QueryObjectMapper _qryObjectMapper;
        public OrganizationRepository()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
        }

        public Organization GetOrganization(string branchId)
        {
            Organization org = _qryObjectMapper.Map<Organization>("findById", new string[] { "branchid" }, new object[] { branchId }).FirstOrDefault();
            return org;
        }

        public LogoOrganization GetLogoOrganization(string branchId)
        {
            LogoOrganization logoOrg = _qryObjectMapper.Map<LogoOrganization>("findById", new string[] { "branchid" }, new object[] { branchId }).FirstOrDefault();
            logoOrg.Image = Zip7.Decompress(logoOrg.Image);
            //LogoOrganization logoOrg = new LogoOrganization();
            //Image img = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\img\logoorg\" + branchId + ".png"));
            //MemoryStream ms = new MemoryStream();
            //img.Save(ms, ImageFormat.Png);
            //logoOrg.Id = "";
            //logoOrg.Image = ms.ToArray();
            return logoOrg;
        }
    }
}
