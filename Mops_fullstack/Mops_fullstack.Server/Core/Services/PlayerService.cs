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

        public List<Player> GetItems()
            => _unitOfWork.PlayerRepo.GetAllItems();

        public bool RemoveItem(Player entity)
            => _unitOfWork.PlayerRepo.Delete(entity);

        public bool UpdateItem(Player entity)
            => _unitOfWork.PlayerRepo.Update(entity);
    }
}
