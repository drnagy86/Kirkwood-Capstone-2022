using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IEmailProvider
    {

        void SendEmail(string subject, string contentPlainText, string to);
    }
}
