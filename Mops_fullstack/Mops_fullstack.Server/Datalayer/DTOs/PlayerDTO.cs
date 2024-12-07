using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class PlayerDTO : BaseEntity, IMapperConvert<PlayerDTO,Player>
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Age { get; set; }
        public int? GoogleId { get; set; }
        public int? FacebookId { get; set; }
        public bool IsOrganizer { get; set; }
        public int? AreaOwnerId { get; set; }
        public virtual Owner? AreaOwner { get; set; }
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
