using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IPlayerService : IBaseService<Player>
    {
        public Player? GetPlayerWithEmail(string Email);

        public Player GetWithJoinRequests(int id);

        public ICollection<Group> GetOwnedGroups(int id);
    }
}
