using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class OwnerDTO : BaseEntity, IMapperConvert<OwnerDTO,Owner>
    {
        public virtual ICollection<Field> Fields { get; set; } = new List<Field>();
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
