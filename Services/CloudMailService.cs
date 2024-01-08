namespace APISample.Services
{
	public class CloudMailService:IMailService
	{
		string mailTo = string.Empty;
		string mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
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
	}
}