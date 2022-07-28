using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EmailProviderFake : IEmailProvider
    {

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Function that creates a fake email as a text file saved at wpf/debug/Test.txt
        /// </summary>
        /// <param name="subject">The email subjec</param>
        /// <param name="contentPlainText">Content for the email</param>
        /// <param name="to">email address to be sent</param>
        private static void Execute(string subject, string contentPlainText, string to)
        {
            // fake email goes here: \WPFPresentation\bin\Debug\
            string destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Test.txt");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("To: " + to);
            stringBuilder.AppendLine("Subject: " + subject);
            stringBuilder.AppendLine(contentPlainText);


            try
            {
                File.WriteAllText(destination, stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Function that processes a the email contents before sending it to be executed
        /// </summary>
        /// <param name="subject">The email subjec</param>
        /// <param name="contentPlainText">Content for the email</param>
        /// <param name="to">email address to be sent</param>
        public void SendEmail(string subject, string contentPlainText, string to)
        {

            Execute(subject, contentPlainText, to);


        }

    }
}
