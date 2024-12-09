using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using System.ComponentModel.DataAnnotations;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class FieldDTO : BaseEntity
    {
        [Required]
        public int AreaOwnerId { get; set; }
        [Required]
        public string Location { get; set; } = null!;

        public virtual Owner AreaOwner { get; set; } = null!;
    }
}
