using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Field : BaseEntity, IMapperConvert<Field, FieldDTO>
{
    [Required]
    public int AreaOwnerId { get; set; }
    [Required]
    public string Location { get; set; } = null!;

    public virtual Owner AreaOwner { get; set; } = null!;
}
