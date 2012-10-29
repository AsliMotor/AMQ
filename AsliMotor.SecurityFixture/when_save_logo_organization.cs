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
    [Subject("Save Logo Organiation")]
    public class when_save_logo_organization
    {
        static IOrganizationService orgService;
        static Image img;
        Establish context = () =>
        {
            orgService = new OrganizationService();
            img = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"image\logo.png"));
        };

        Because of = () =>
        {
            orgService.SaveLogo(img, "dny@gmail.com");
        };

        It should_be_save_logo_organization = () =>
        {
        };
    }
}
