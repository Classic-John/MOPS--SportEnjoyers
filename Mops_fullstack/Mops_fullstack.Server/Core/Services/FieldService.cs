using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Core.Services
{
    public class FieldService : IFieldService
    {
        private readonly UnitOfWork _unitOfWork;
        public FieldService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public Field? AddItem(Field entity)
            => _unitOfWork.FieldRepo.Add(entity);

        public Field GetItem(int? id)
            => GetItems().FirstOrDefault(item => item.Id == id.Value);

        public List<Field> GetItems()
            => _unitOfWork.FieldRepo.GetAllItems();

        public bool RemoveItem(Field entity)
            => _unitOfWork.FieldRepo.Delete(entity);

        public bool UpdateItem(Field entity)
            => _unitOfWork.FieldRepo.Update(entity);
    }
}
