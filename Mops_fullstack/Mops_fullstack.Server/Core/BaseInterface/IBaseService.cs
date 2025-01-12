using Mops_fullstack.Server.Datalayer.BaseClass;

namespace Mops_fullstack.Server.Core.BaseInterface
{
    public interface IBaseService<T> where T : class
    {
        public T? AddItem(T entity);
        public bool RemoveItem(T entity);
        public bool UpdateItem(T entity);
        public List<T> GetItems();
        public T? GetItem(int id);

        public bool IsValidDate(string date)
        {
            int[] parts = date.Split("/", 3).Select(int.Parse).ToArray();
            return
                parts.Length == 3 &&
                parts[0] > 0 && parts[0] < 10000 &&
                parts[1] > 0 && parts[1] < 13 &&
                parts[2] > 0 && parts[2] <= DateTime.DaysInMonth(parts[0], parts[1]);
        }
    }
}
