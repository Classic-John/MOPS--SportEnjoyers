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
    [Required]
    public string Text { get; set; } = null!;

    [Required]
    public bool IsInitial { get; set; } = false;

    [Required]
    public int ThreadId { get; set; }

    public Thread Thread { get; set; } = null!;

    [Required]
    public int PlayerId { get; set; }

    public Player Player { get; set; } = null!;
}
