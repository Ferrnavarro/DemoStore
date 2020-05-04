using System;
using System.Threading.Tasks;

namespace DemoStore.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(IEmailMessage message);
    }
}
