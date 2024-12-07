using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class GroupDTO : BaseEntity, IMapperConvert<GroupDTO,Group>
    {
        [Required]
        public int? GroupCreator { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
