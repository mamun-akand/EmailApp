namespace EmailApp.IRepository
{
    public interface IEmailService
    {
        public Task<string> SendEmail(string toEmail, string subject, string body);
    }
}
