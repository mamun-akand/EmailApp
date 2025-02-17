using System.Numerics;
using EmailApp.IRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;

namespace EmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(string ToEmail, string MailSubject, string MailBody)
        {
            var result = await _emailService.SendEmail(ToEmail, MailSubject, MailBody);
            return Ok(new { message = result });
        }
    }
}
