using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Match : BaseEntity, IMatch
{
    [Required]
    public DateTime MatchDate { get; set; }

    [Required]
    public int GroupId { get; set; }

    public Group Group { get; set; } = null!;

    [Required]
    public int FieldId { get; set; }

    public Field Field { get; set; } = null!;
}
