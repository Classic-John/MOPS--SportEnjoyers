using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Message : BaseEntity, IMessage
{
    public string? Text { get; set; }
    [Required]
    public int AssociatedThreadId { get; set; }
    public virtual Thread AssociatedThread { get; set; } = null!;
}
