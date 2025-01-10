using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Player :BaseEntity, IPlayer
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Age { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    // public int? GoogleId { get; set; }
    // public int? FacebookId { get; set; }
    // public bool IsOrganizer { get; set; }
    // public int? AreaOwnerId { get; set; }
    // public virtual Owner? AreaOwner { get; set; }
    // public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public ICollection<Field> Fields { get; set; } = [];

    public ICollection<Group> Groups { get; set; } = [];

    public ICollection<Group> GroupRequests { get; set; } = [];

    public ICollection<Group> GroupsOwned { get; set; } = [];

    public ICollection<Message> Messages { get; set; } = [];
}
