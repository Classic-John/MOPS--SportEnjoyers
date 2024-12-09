using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class ThreadDTO : BaseEntity
    {
        [Required]
        public int AssociatedGroupId { get; set; }
        public virtual Group AssociatedGroup { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
