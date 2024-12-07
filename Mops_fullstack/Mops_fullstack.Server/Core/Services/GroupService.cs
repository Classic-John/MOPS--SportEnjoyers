using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly UnitOfWork _unitOfWork;
        public GroupService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public bool AddItem(Group entity)
            => _unitOfWork.GroupRepo.Add(entity);

        public List<Group> GetItems()
            => _unitOfWork.GroupRepo.GetAllItems();

        public bool RemoveItem(Group entity)
            => _unitOfWork.GroupRepo.Delete(entity);

        public bool UpdateItem(Group entity)
            => _unitOfWork.GroupRepo.Update(entity);
    }
}
