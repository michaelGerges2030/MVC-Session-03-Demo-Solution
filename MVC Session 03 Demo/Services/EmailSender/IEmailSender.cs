using System.Threading.Tasks;

namespace MVC_Session_03_Demo.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
