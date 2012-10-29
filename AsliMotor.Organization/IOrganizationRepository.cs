using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Organizations
{
    public interface IOrganizationRepository
    {
        Organization GetOrganization(string branchId);
        LogoOrganization GetLogoOrganization(string branchId);
    }
}
