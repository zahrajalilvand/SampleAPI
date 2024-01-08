using System.Net;
using System.Net.Mail;

namespace APISample.Services
{
	public class LocalMailService : IMailService
	{
		string mailTo = string.Empty;
		string mailFrom = string.Empty;

		public LocalMailService(IConfiguration configuration)
		{
			mailTo = configuration["MailInfo:MailToAddress"];
			mailFrom = configuration["MailInfo:MailFromAddress"];
		}

		public void Send(string subject, string message)
		{
			Console.WriteLine($"Mail from {mailFrom} to {mailTo}, "
				+ $"with {nameof(LocalMailService)}, ");
			Console.WriteLine($"Subject {subject} ");
			Console.WriteLine($"Message {message} ");
		}

		static void Email(string htmlString)
		{
			MailMessage mailMessage = new MailMessage();
			SmtpClient smtpClient = new SmtpClient();

			try
			{
				mailMessage.From = new MailAddress("FromMailAddress");
				mailMessage.To.Add(new MailAddress("ToMailAddress"));
				mailMessage.Subject = "Test Mail Service";
				mailMessage.Body = htmlString;
				mailMessage.IsBodyHtml = true;
				smtpClient.Port = 587;
				smtpClient.Host = "smtp.ymail.com";
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("FromMailAddress", "Password");
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Send(mailMessage);
			}
			catch (Exception)
			{
				throw;
			}


		}
	}
}
