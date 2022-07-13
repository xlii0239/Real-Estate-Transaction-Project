using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Assignment1.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "";

        public void Send(String toEmail, String subject, String contents, string fileName, string dataAsString)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("email@address", "Sender Name");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Add attachment as txt/plain

            msg.AddAttachment(fileName, dataAsString);

            //byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(filePath));
            //
            //msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(byteData),
            //            Filename = "test.txt",
            //            Type = "txt/plain",
            //            Disposition = "attachment"
            //        }
            //};

            var response = client.SendEmailAsync(msg);
        }

        public void Send_Multiple(List<String> toEmails, String subject, String contents, string fileName, string dataAsString)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("charleneloove@gmail.com", "Bing Wang");
            var tos = new List<EmailAddress>();

            foreach (String emailAddress in toEmails)
            {
                tos.Add(new EmailAddress(emailAddress, "My Customer"));
            }

            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);

            msg.AddAttachment(fileName, dataAsString);

            var response = client.SendEmailAsync(msg);
        }
    }
}
