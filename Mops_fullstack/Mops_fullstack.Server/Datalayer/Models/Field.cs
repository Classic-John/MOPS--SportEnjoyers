using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;

namespace Mops_fullstack.Server.Datalayer.Models;

public partial class Field : BaseEntity, IField
{
    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public int OwnerId { get; set; }

    public Player Owner { get; set; } = null!;

    public List<Match> Matches { get; set; } = [];
}
