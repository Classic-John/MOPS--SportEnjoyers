using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly UnitOfWork _unitOfWork;
        public MessageService(UnitOfWork unitOfWork)
           => _unitOfWork = unitOfWork;
        public bool AddItem(Message entity)
            => _unitOfWork.MessageRepo.Add(entity);

        public List<Message> GetItems()
            => _unitOfWork.MessageRepo.GetAllItems();

        public bool RemoveItem(Message entity)
            => _unitOfWork.MessageRepo.Delete(entity);

        public bool UpdateItem(Message entity)
            => _unitOfWork.MessageRepo.Update(entity);
    }
}
