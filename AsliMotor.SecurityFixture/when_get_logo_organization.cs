using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Organizations;
using System.Drawing;
using System.IO;

namespace AsliMotor.Fixture
{
    [Subject("Get Logo Organiation")]
    public class when_get_logo_organization
    {
        static IOrganizationRepository orgRepo;
        static LogoOrganization img;
        Establish context = () =>
        {
            orgRepo = new OrganizationRepository();
        };

        Because of = () =>
        {
            img = orgRepo.GetLogoOrganization("dny@gmail.com");
        };

        It should_be_save_logo_organization = () =>
        {
            img.ShouldNotBeNull();
        };
    }
}
