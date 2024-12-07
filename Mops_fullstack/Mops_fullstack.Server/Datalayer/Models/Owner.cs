using System;
using System.Collections.Generic;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Owner : BaseEntity, IMapperConvert<Owner,OwnerDTO>
{
    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
