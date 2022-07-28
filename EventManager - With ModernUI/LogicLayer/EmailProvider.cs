using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;



namespace LogicLayer
{
    public class EmailProvider : IEmailProvider
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Function that executes the email to be sent using the api key.
        /// If you would like to make this work, get the api key from Derrick and create an environment variable with it
        /// For testing purposes, put the email that you would like it sent to. I am leaving mine as an example.
        /// </summary>
        /// <param name="subject">The email subjec</param>
        /// <param name="contentPlainText">Content for the email</param>
        /// <param name="to">email address to be sent</param>
        private static async void Execute(string subject, string contentPlainText, string to = null)
        {
            // example
            var apiKey = Environment.GetEnvironmentVariable("SEND_GRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var frm = new EmailAddress("tadpole.events.kirkwood.2022@gmail.com", "Tadpole Events");
            var sbjct = subject;

            //var tO = new EmailAddress(to);
            // replace your email for testing purposes
            var tO = new EmailAddress("drnagy86@gmail.com");
            var plainTextContent = contentPlainText;
            
            // intentionally left empty for an example but also because the method requires it
            var htmlContent = "";

            SendGridMessage msg = null;

            try
            {
                msg = MailHelper.CreateSingleEmail(frm, tO, sbjct, plainTextContent, htmlContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var response = await client.SendEmailAsync(msg);

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
