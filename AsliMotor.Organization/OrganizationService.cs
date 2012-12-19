using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Reporting;
using Spring.Context.Support;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace AsliMotor.Organizations
{
    public class OrganizationService:IOrganizationService
    {
        IReportingRepository _reportingRepository;
        IOrganizationRepository _repository;
        public OrganizationService()
        {
            _reportingRepository = ContextRegistry.GetContext().GetObject("ReportingRepository") as IReportingRepository;
            _repository = new OrganizationRepository();
        }

        public void SaveLogo(Image img, string branchid)
        {
            MemoryStream imgStream = new MemoryStream();
            img.Save(imgStream, ImageFormat.Png);
            byte[] logoByte = readImageAndCompress(imgStream);
            
            LogoOrganization logoOrg = _repository.GetLogoOrganization(branchid);
            if (logoOrg == null)
            {
                logoOrg = new LogoOrganization() { Id = branchid, Image = logoByte };
                _reportingRepository.Save<LogoOrganization>(logoOrg);
            }
            else
            {
                logoOrg.Image = logoByte;
                _reportingRepository.Update<LogoOrganization>(logoOrg, new { Id = logoOrg.Id });
            }
        }


        public void Update(Organization org)
        {
            _reportingRepository.Update<Organization>(org, new { BranchId = org.BranchId });
        }

        private byte[] readImageAndCompress(Stream stream)
        {
            var image = Image.FromStream(stream);
            image = resizeImage(image, new Size(300, 150));
            MemoryStream imgStream = new MemoryStream();
            image.Save(imgStream, ImageFormat.Png);
            var comppressedImg = Zip7.Compress(imgStream.ToArray());
            return comppressedImg;
        }
        static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
    }
}
