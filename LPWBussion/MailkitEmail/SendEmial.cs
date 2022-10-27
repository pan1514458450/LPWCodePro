using LPWService;
using LPWService.BaseRepostiory;
using NETCore.MailKit.Core;
using NETCore.MailKit.Infrastructure.Internal;
using System.Text;

namespace LPWBussion.MailkitEmail
{
    public sealed class SendEmail : ISendEmail
    {
        private readonly IEmailService _EmailService;
        private readonly ICsredisHelp _csredis;
        public SendEmail(IEmailService emailService, ICsredisHelp csredis)
        {
            _EmailService = emailService;
            _csredis = csredis;
        }
        SenderInfo sendinfo = new SenderInfo()
        {
            SenderEmail = ConstCode.SendEmail,
        };
        public async Task MySendEmail(string email)
        {
           
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < ConstCode.VerificationCode; i++)
            {
                builder.Append(random.Next(0, 9));
            }
            await _csredis.SetRedis(email, builder.ToString(), 300);
            await _EmailService.SendAsync(email, "验证码", builder.ToString(), false, sendinfo);
        }
    }
}
