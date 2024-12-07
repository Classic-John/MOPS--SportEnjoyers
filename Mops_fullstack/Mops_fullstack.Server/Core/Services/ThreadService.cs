using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class ThreadService : IThreadService
    {
        private readonly UnitOfWork _unitOfWork;
        public ThreadService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public bool AddItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Add(entity);

        public List<Datalayer.Models.Thread> GetItems()
            => _unitOfWork.ThreadRepo.GetAllItems();

        public bool RemoveItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Delete(entity);

        public bool UpdateItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Update(entity);
    }
}
