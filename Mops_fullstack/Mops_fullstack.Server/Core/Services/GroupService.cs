using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using NuGet.DependencyResolver;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly UnitOfWork _unitOfWork;
        public GroupService(UnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public Group? AddItem(Group entity)
            => _unitOfWork.GroupRepo.Add(entity);

        public Group? GetItem(int id)
            => GetItems().FirstOrDefault(group => group.Id == id);

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
                .Where(group =>
                    Filter.PlayerId == null ||
                    group.OwnerId == Filter.PlayerId ||
                    group.Players.Any(player => player.Id == Filter.PlayerId)
                );
        }

        public Group? GetGroupData(int id)
            => _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == id)
                .Include(group => group.Players)
                .FirstOrDefault();

        public bool AddJoinRequest(int groupId, Player player)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players)
                .Include(group => group.PlayerRequests)
                .FirstOrDefault();

            if (
                group == null ||
                group.Players.FirstOrDefault(gplayer => player.Id == gplayer.Id) != null ||
                group.PlayerRequests.FirstOrDefault(gplayer => player.Id == gplayer.Id) != null
            )
            {
                return false;
            }

            group.PlayerRequests.Add(player);
            return UpdateItem(group);
        }

        public bool AddJoinVerdict(int groupId, int ownerId, JoinRequestVerdict verdict)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players)
                .Include(group => group.PlayerRequests)
                .FirstOrDefault();

            if (
                group == null ||
                group.OwnerId != ownerId ||
                group.Players.FirstOrDefault(gplayer => gplayer.Id == verdict.PlayerId) != null ||
                group.PlayerRequests.FirstOrDefault(gplayer => gplayer.Id == verdict.PlayerId) is not Player player
            )
            {
                return false;
            }

            group.PlayerRequests.Remove(player);
            if (verdict.Accepted)
            {
                group.Players.Add(player);
            }
            return UpdateItem(group);
        }

        public GroupJoinStatus? GetJoinStatus(int groupId, int playerId)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players.Where(player => player.Id == playerId))
                .Include(group => group.PlayerRequests.Where(player => player.Id == playerId))
                .FirstOrDefault();

            if (group == null)
            {
                return null;
            }

            if (group.Players.FirstOrDefault() != null)
            {
                return new GroupJoinStatus { Status = GroupJoinStatusTypes.Joined };
            }
            else if (group.PlayerRequests.FirstOrDefault() != null)
            {
                return new GroupJoinStatus { Status = GroupJoinStatusTypes.Pending };
            }
            else
            {
                return new GroupJoinStatus { Status = GroupJoinStatusTypes.NoRequest };
            }
        }

        public bool RemoveFromGroup(int groupId, int playerId)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players)
                .FirstOrDefault();

            if (
                group == null ||
                group.OwnerId == playerId ||
                group.Players.FirstOrDefault(gplayer => gplayer.Id == playerId) == null
            )
            {
                return false;
            }

            group.Players = group.Players.Where(player => player.Id != playerId).ToList();
            return UpdateItem(group);
        }

        public bool IsOwnedBy(int groupId, int playerId)
            => _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId && group.OwnerId == playerId)
                .FirstOrDefault() != null;

        public bool HasMember(int groupId, int playerId)
            => _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players)
                .Where(group => group.Players.Where(player => player.Id == playerId).FirstOrDefault() != null)
                .FirstOrDefault() != null;

        public bool DeleteGroup(int groupId)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .FirstOrDefault();

            if (group == null)
            {
                return false;
            }
            return _unitOfWork.GroupRepo.Delete(group);
        }

        public ICollection<Match>? GetMatches(int groupId)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Matches)
                .FirstOrDefault();

            return group == null ? null : group.Matches;
        }

        public bool IsMember(int groupId, int playerId)
            => _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Players)
                .Where(group => group.OwnerId == playerId || group.Players.Any(player => player.Id == playerId))
                .FirstOrDefault() != null;

        public ICollection<Thread>? GetThreads(int groupId)
        {
            Group? group = _unitOfWork.GroupRepo.GetTable()
                .Where(group => group.Id == groupId)
                .Include(group => group.Threads)
                .ThenInclude(thread => thread.Messages)
                .FirstOrDefault();

            return group == null ? null : group.Threads;
        }
    }
}
