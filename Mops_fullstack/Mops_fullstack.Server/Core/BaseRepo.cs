using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.Database;

namespace Mops_fullstack.Server.Core
{
    public class BaseRepo<T> where T : BaseEntity
    {
        private DbSet<T> _repoTable;
        private List<T> _repoItems;
        public BaseRepo() { }

        public void InitializeItemData(SportEnjoyersDatabaseContext context)
        {
            _repoTable = context.Set<T>();
            _repoItems = _repoTable.Select(item => item).ToListAsync().Result;
        }
        public T? Add(T repoItem)
        {
            try
            {
                EntityEntry<T> new_entity = _repoTable.Add(repoItem);
                _repoItems.Add(repoItem);
                return (_repoTable.GetDbContext().SaveChangesAsync().Result > -1 ? new_entity.Entity : null);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }
        public List<T> GetAllItems()
            => _repoItems;

        public DbSet<T> GetTable()
            => _repoTable;

        public bool Update(T repoItem)
        {
            try
            {
                T? itemFound = _repoItems.Find(item => item.Id == repoItem.Id);
                _repoTable.Entry(itemFound).CurrentValues.SetValues(itemFound);
                return _repoTable.GetDbContext().SaveChangesAsync().Result > -1;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
        public bool Delete(T repoItem)
        {
            try
            {
                _repoTable.Remove(repoItem);
                _repoItems.Remove(repoItem);
                return _repoTable.GetDbContext().SaveChangesAsync().Result > -1;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
    }
}
