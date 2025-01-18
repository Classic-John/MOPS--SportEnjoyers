using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Core.Mail
{
    public interface IMailUtil
    {
        public void SendVerificationMail(Player player);
    }
}
