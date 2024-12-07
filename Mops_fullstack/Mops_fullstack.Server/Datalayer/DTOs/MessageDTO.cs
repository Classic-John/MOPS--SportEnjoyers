using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class MessageDTO : BaseEntity, IMapperConvert<MessageDTO,Message>
    {
        public string? Text { get; set; }
        [Required]
        public int AssociatedThreadId { get; set; }
        public virtual Thread AssociatedThread { get; set; } = null!;
    }
}
