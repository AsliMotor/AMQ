using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AsliMotor.Organizations
{
    public interface IOrganizationService
    {
        void SaveLogo(Image img, string branchid);
        void Update(Organization org);
    }
}
