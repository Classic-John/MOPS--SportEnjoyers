using Mops_fullstack.Server.Core.BaseInterface;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.Service_interfaces
{
    public interface IFieldService : IBaseService<Field>
    {
        public IEnumerable<Field> GetItemsThatMatch(FieldFilterDTO filter);

        public bool IsOwnedBy(int fieldId, int playerId);

        public bool DeleteField(int id);
    }
}
