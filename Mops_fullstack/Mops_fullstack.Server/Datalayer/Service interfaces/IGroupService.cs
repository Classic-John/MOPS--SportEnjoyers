using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        public IEnumerable<Group> GetItemsThatMatch(GroupFilterDTO filter);

        public Group? GetGroupData(int id);
    }
}
