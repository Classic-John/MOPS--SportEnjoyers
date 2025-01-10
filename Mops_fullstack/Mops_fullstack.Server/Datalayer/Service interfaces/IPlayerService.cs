using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IPlayerService : IBaseService<Player>
    {
        public Player? GetPlayerWithEmail(string Email);

        public bool IsMemberOf(int playerId, int groupId);

        public bool IsPendingRequest(int playerId, int groupId);

        public bool IsOwnerOf(int playerId, int groupId);

        public bool SendJoinRequest(int playerId, int groupId);

        public bool ResolveJoinRequest(int playerId, int groupId, JoinRequestVerdict verdict);
    }
}
