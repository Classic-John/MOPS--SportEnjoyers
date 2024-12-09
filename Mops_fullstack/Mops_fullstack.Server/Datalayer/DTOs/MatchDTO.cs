using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class MatchDTO : BaseEntity
    {
        [Required]
        public DateTime MatchDate { get; set; }
        [Required]
        public int AssociatedGroupId { get; set; }
        public virtual Group AssociatedGroup { get; set; } = null!;
    }
}
