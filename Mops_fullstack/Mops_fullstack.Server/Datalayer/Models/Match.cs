using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Match : BaseEntity, IMapperConvert<Match, MatchDTO>
{
    [Required]
    public DateTime MatchDate { get; set; }
    [Required]
    public int AssociatedGroupId { get; set; }
    public virtual Group AssociatedGroup { get; set; } = null!;
}
