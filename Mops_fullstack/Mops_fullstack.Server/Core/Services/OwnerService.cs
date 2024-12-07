using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly UnitOfWork _unitOfWork;
        public OwnerService(UnitOfWork unitOfWork)
           => _unitOfWork = unitOfWork;
        public bool AddItem(Owner entity)
            => _unitOfWork.OwnerRepo.Add(entity);

        public List<Owner> GetItems()
            => _unitOfWork.OwnerRepo.GetAllItems();

        public bool RemoveItem(Owner entity)
            => _unitOfWork.OwnerRepo.Delete(entity);

        public bool UpdateItem(Owner entity)
            => _unitOfWork.OwnerRepo.Update(entity);
    }
}
