using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        public IEnumerable<Group> GetItemsThatMatch(GroupFilterDTO filter);

        public Group? GetGroupData(int id);

        public bool AddJoinRequest(int groupId, Player player);

        public bool AddJoinVerdict(int groupId, int ownerId, JoinRequestVerdict verdict);

        public GroupJoinStatus? GetJoinStatus(int groupId, int playerId);

        public bool RemoveFromGroup(int groupId, int playerId);

        public bool IsOwnedBy(int groupId, int playerId);

        public bool HasMember(int groupId, int playerId);

        public bool DeleteGroup(int groupId);

        public ICollection<Match>? GetMatches(int groupId);
    }
}
