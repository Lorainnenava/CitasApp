using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        bool SendEmail(string recipientAddress, string subject, string templatePath, Dictionary<string, string> placeholders, List<(string cid, string path)> images = null);
    }
}
