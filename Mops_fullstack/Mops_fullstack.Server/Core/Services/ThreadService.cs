using Mops_fullstack.Server.Datalayer.Service_interfaces;
using System.Data.Entity;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Core.Services
{
    public class ThreadService : IThreadService
    {
        private readonly UnitOfWork _unitOfWork;
        public ThreadService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public Datalayer.Models.Thread? AddItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Add(entity);

        public Datalayer.Models.Thread? GetItem(int id)
            => GetItems().FirstOrDefault(item => item.Id == id);

        public List<Datalayer.Models.Thread> GetItems()
            => _unitOfWork.ThreadRepo.GetAllItems();

        public bool RemoveItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Delete(entity);

        public bool UpdateItem(Datalayer.Models.Thread entity)
            => _unitOfWork.ThreadRepo.Update(entity);

        public Thread? GetWithMessages(int id)
            => _unitOfWork.ThreadRepo.GetTable()
                .Where(thread => thread.Id == id)
                .Include(thread => thread.Messages)
                .FirstOrDefault();
    }
}
