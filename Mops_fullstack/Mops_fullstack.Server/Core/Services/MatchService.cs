using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly UnitOfWork _unitOfWork;
        public MatchService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public bool AddItem(Match entity)
            => _unitOfWork.MatchRepo.Add(entity);

        public Match GetItem(int? id)
            => GetItems().FirstOrDefault(match => match.Id==id.Value);

        public List<Match> GetItems()
            => _unitOfWork.MatchRepo.GetAllItems();

        public bool RemoveItem(Match entity)
            => _unitOfWork.MatchRepo.Delete(entity);

        public bool UpdateItem(Match entity)
            => _unitOfWork.MatchRepo.Update(entity);
    }
}
