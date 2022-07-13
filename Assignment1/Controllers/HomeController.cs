using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Assignment1.Models;
using Assignment1.Utils;

namespace Assignment1.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About All House";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page for administrator";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        public ActionResult Send_Email_Multiple()
        {
            return View(new SendEmailViewModel());
        }

        //Send email to single address
        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string theFileName = Path.GetFileName(postedFile.FileName);

                    byte[] fileBytes = new byte[postedFile.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(postedFile.InputStream))
                    {
                        fileBytes = theReader.ReadBytes(postedFile.ContentLength);
                    }

                    string dataAsString = Convert.ToBase64String(fileBytes);

                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents, theFileName, dataAsString);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        //Send bulk Email to multiple address(all the customer)
        [HttpPost]
        public ActionResult Send_Email_Multiple(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string theFileName = Path.GetFileName(postedFile.FileName);

                    //Convert attachment to byte code
                    byte[] fileBytes = new byte[postedFile.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(postedFile.InputStream))
                    {
                        fileBytes = theReader.ReadBytes(postedFile.ContentLength);
                    }

                    //Convert byte code to string
                    string dataAsString = Convert.ToBase64String(fileBytes);

                    List<String> toEmails = new List<string>();

                    //Get all the customer email address
                    var allCustomers = db.AspNetUsers.ToList();
                    foreach (AspNetUsers users in allCustomers)
                    {
                        toEmails.Add(users.Email);
                    }

                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send_Multiple(toEmails, subject, contents, theFileName, dataAsString);

                    ViewBag.Result = "Bulk Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}