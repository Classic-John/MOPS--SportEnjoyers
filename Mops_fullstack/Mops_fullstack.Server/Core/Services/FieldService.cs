using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using System.Data.Entity;

namespace Mops_fullstack.Server.Core.Services
{
    public class FieldService : IFieldService
    {
        private readonly UnitOfWork _unitOfWork;
        public FieldService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public Field? AddItem(Field entity)
            => _unitOfWork.FieldRepo.Add(entity);

        public Field? GetItem(int id)
            => _unitOfWork.FieldRepo.GetTable()
                .Where(field => field.Id == id)
                .Include(field => field.Matches)
                .FirstOrDefault();

        public List<Field> GetItems()
            => _unitOfWork.FieldRepo.GetAllItems();

        public bool RemoveItem(Field entity)
            => _unitOfWork.FieldRepo.Delete(entity);

        public bool UpdateItem(Field entity)
            => _unitOfWork.FieldRepo.Update(entity);

        public IEnumerable<Field> GetItemsThatMatch(FieldFilterDTO filter)
            => _unitOfWork.FieldRepo.GetTable()
                .Where(field => filter.Name == null || filter.Name == field.Name)
                .Where(field =>
                    filter.OwnerId == null ?
                    (filter.Owner == null || filter.Owner == field.Owner.Name) :
                    filter.OwnerId == field.OwnerId
                )
                .Where(field => filter.Location == null || filter.Location == field.Location)
                .Include(field => field.Matches)
                .Where(field =>
                    filter.FreeOnDay == null ||
                    field.Matches.FirstOrDefault(match => filter.FreeOnDay == match.MatchDate) == null
                );

        public bool IsOwnedBy(int fieldId, int playerId)
            => _unitOfWork.FieldRepo.GetTable()
                .Where(field => field.Id == fieldId && field.OwnerId == playerId)
                .FirstOrDefault() != null;

        public bool DeleteField(int id)
        {
            Field? field = _unitOfWork.FieldRepo.GetTable()
                .Where(field => field.Id == id)
                .FirstOrDefault();

            if (field == null)
            {
                return false;
            }
            _unitOfWork.FieldRepo.Delete(field);
            return true;
        }
    }
}
