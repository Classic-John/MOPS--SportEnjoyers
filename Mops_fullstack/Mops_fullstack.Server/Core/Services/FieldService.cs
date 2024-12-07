using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class FieldService : IFieldService
    {
        private readonly UnitOfWork _unitOfWork;
        public FieldService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public bool AddItem(Field entity)
            => _unitOfWork.FieldRepo.Add(entity);

        public List<Field> GetItems()
            => _unitOfWork.FieldRepo.GetAllItems();

        public bool RemoveItem(Field entity)
            => _unitOfWork.FieldRepo.Delete(entity);

        public bool UpdateItem(Field entity)
            => _unitOfWork.FieldRepo.Update(entity);
    }
}
