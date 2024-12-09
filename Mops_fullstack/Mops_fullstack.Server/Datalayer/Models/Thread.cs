using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Thread : BaseEntity, IThread
{
    [Required]
    public int AssociatedGroupId { get; set; }
    public virtual Group AssociatedGroup { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
