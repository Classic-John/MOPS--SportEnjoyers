using Mops_fullstack.Server.Datalayer.BaseClass;

namespace Mops_fullstack.Server.Core.BaseInterface
{
    public interface IBaseService<T> where T : class
    {
        public T? AddItem(T entity);
        public bool RemoveItem(T entity);
        public bool UpdateItem(T entity);
        public List<T> GetItems();
        public T GetItem(int? id);
    }
}
