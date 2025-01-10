using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mops_fullstack.Server.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly UnitOfWork _unitOfWork;
        public GroupService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public bool AddItem(Group entity)
            => _unitOfWork.GroupRepo.Add(entity);

        public Group GetItem(int? id)
            => GetItems().FirstOrDefault(group => group.Id == id.Value);

        public List<Group> GetItems()
            => _unitOfWork.GroupRepo.GetAllItems();

        public bool RemoveItem(Group entity)
            => _unitOfWork.GroupRepo.Delete(entity);

        public bool UpdateItem(Group entity)
            => _unitOfWork.GroupRepo.Update(entity);

        public IEnumerable<Group> GetItemsThatMatch(GroupFilterDTO Filter)
        {
            return _unitOfWork.GroupRepo.GetTable()
                .Where(group => Filter.Name == null || Filter.Name == group.Name)
                .Where(group => Filter.Owner == null || Filter.Owner == group.Owner.Name)
                .Include(group => group.Players)
                .Where(group => Filter.PlayerId == null || group.Players.Any(player => player.Id == Filter.PlayerId));
        }

        public Group? GetGroupData(int id)
        {
            return _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == id)
                .Include(group => group.Players)
                .FirstOrDefault();
        }
    }
}
