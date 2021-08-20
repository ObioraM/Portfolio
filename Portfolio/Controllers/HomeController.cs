using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Data;
using Portfolio.Interface;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, IEmailService emailService)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _appDbContext.Add(contact);

            await _appDbContext.SaveChangesAsync();

            var html = $"<p>{contact.FullName} checked out your portfolio and wants to contact you. </p><p>Message: {contact.Message}<p><h3>{contact.FullName} Contact details:</h3><p>Phone Number: {contact.PhoneNumber}</p><p>Email address: {contact.Email}</p>";

            var sender = contact.FullName;
            var receiver = "obioramaduakor@gmail.com";
            var subject = $"Portofolio: {contact.FullName} wants to contact you.";

            _emailService.Send(sender, receiver, subject, html);

            ViewBag.SuccessStatus = true;

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
