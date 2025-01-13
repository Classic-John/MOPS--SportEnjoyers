using AutoMapper;
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
        private readonly IMapper _mapper;
        public PlayerService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Player? AddItem(Player entity)
            => _unitOfWork.PlayerRepo.Add(entity);

        public Player? GetItem(int id)
            => GetItems().FirstOrDefault(player => player.Id==id);

        public Player? GetPlayerWithEmail(string Email)
            => GetItems().FirstOrDefault(player => player.Email == Email);

        public List<Player> GetItems()
            => _unitOfWork.PlayerRepo.GetAllItems();

        public bool RemoveItem(Player entity)
            => _unitOfWork.PlayerRepo.Delete(entity);

        public bool UpdateItem(Player entity)
            => _unitOfWork.PlayerRepo.Update(entity);

        public Player GetWithJoinRequests(int id)
            => _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == id)
                .Include(player => player.GroupsOwned)
                .ThenInclude(group => group.PlayerRequests)
                .First();

        public ICollection<Group> GetOwnedGroups(int id)
        {
            Player? player = _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == id)
                .Include(player => player.GroupsOwned)
                .FirstOrDefault();

            if (player == null)
            {
                return [];
            }
            return player.GroupsOwned;
        }
        
        public bool DeleteAccount(int id)
        {
            Player? player = _unitOfWork.PlayerRepo.GetTable()
                .Where(player => player.Id == id)
                .FirstOrDefault();

            if (player is null)
            {
                return false;
            }
            return RemoveItem(player);
        }
    }
}
