using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Group : BaseEntity, IGroup
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int OwnerId { get; set; }

    public Player Owner { get; set; } = null!;

    public ICollection<Match> Matches { get; set; } = [];

    public ICollection<Thread> Threads { get; set; } = [];

    public ICollection<Player> Players { get; set; } = new List<Player>();

    public ICollection<Player> PlayerRequests { get; set; } = [];
}
