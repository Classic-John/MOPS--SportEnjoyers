using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;
namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IThreadService : IBaseService<Models.Thread>
    {
        public Thread? GetWithMessages(int id);
    }
}
