using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Thread : BaseEntity, IThread
{
    [Required]
    public int GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public ICollection<Message> Messages { get; set; } = [];
}
