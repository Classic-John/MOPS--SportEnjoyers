using Mops_fullstack.Server.Datalayer.BaseClass;

namespace Mops_fullstack.Server.Core.BaseInterface
{
    public interface IBaseService<T> where T : BaseEntity
    {
        public bool AddItem(T entity);
        public bool RemoveItem(T entity);
        public bool UpdateItem(T entity);
        public List<T> GetItems();
    }
}
