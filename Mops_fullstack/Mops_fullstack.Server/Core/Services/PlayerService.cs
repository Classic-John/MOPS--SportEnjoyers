using Microsoft.EntityFrameworkCore;
using Mops_fullstack.Server.Datalayer.Database;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Interfaces;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly UnitOfWork _unitOfWork;
        public PlayerService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public bool AddItem(Player entity)
            => _unitOfWork.PlayerRepo.Add(entity);

        public Player GetItem(int? id)
            => GetItems().FirstOrDefault(player => player.Id==id.Value);

        public Player? GetPlayerWithEmail(String Email)
            => GetItems().FirstOrDefault(player => player.Email == Email);

        public List<Player> GetItems()
            => _unitOfWork.PlayerRepo.GetAllItems();

        public bool RemoveItem(Player entity)
            => _unitOfWork.PlayerRepo.Delete(entity);

        public bool UpdateItem(Player entity)
            => _unitOfWork.PlayerRepo.Update(entity);

        public bool IsMemberOf(int playerId, int groupId)
            => _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == playerId)
                .Include(player => player.Groups.Where(group => group.Id == groupId))
                .First().Groups.FirstOrDefault() != null;

        public bool IsOwnerOf(int playerId, int groupId)
            => _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId && group.OwnerId == playerId)
                .FirstOrDefault() != null;

        public bool IsPendingRequest(int playerId, int groupId)
            => _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == playerId)
                .Include(player => player.GroupRequests.Where(group => group.Id == groupId))
                .First().GroupRequests.FirstOrDefault() != null;

        public bool SendJoinRequest(int playerId, int groupId)
        {
            Player player = _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == playerId)
                .Include(player => player.Groups)
                .Include(player => player.GroupRequests)
                .First();

            if (
                _unitOfWork.GroupRepo.GetTable().Where(group => group.Id == groupId).FirstOrDefault() is not Group group ||
                player.Groups.Where(group => group.Id == groupId).FirstOrDefault() != null ||
                player.GroupRequests.Where(group => group.Id == groupId).FirstOrDefault() != null
            )
            {
                return false;
            }

            player.GroupRequests.Add(group);
            return UpdateItem(player);
        }

        public bool ResolveJoinRequest(int playerId, int groupId, JoinRequestVerdict verdict)
        {
            Player player = _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == playerId)
                .Include(player => player.Groups)
                .Include(player => player.GroupRequests)
                .First();

            if (player.GroupRequests.Where(group => group.Id == groupId).FirstOrDefault() is not Group group)
            {
                return false;
            }

            player.GroupRequests.Remove(group);
            if (verdict.Accepted)
            {
                player.Groups.Add(group);
            }
            return UpdateItem(player);
        }
    }
}
