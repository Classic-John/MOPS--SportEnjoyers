using Mops_fullstack.Server.Datalayer.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    /*public class GroupDTO : BaseEntity
    {
        [Required]
        public int? GroupCreator { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }*/

    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public PlayerDTO Owner { get; set; } = null!;

        public bool? IsYours { get; set; } = null;

        public virtual ICollection<PlayerDTO> Players { get; set; } = [];
    }

    public class GroupSearchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual PlayerDTO Owner { get; set; } = null!;
    }

    public class GroupFilterDTO
    {
        public string? Name { get; set; } = null;

        public string? Owner { get; set; } = null;

        public bool Yours { get; set; } = false;

        [SwaggerIgnore]
        public int? PlayerId { get; set; } = null;
    }

    public class CreateGroupDTO
    {
        public string Name { get; set; } = null!;

        [SwaggerIgnore]
        public int? OwnerId { get; set; } = null;

        [SwaggerIgnore]
        public virtual ICollection<Player> Players { get; set; } = [];
    }

    public class GroupJoinStatus
    {
        public GroupJoinStatusTypes Status { get; set; }
    }

    public enum GroupJoinStatusTypes
    {
        NoRequest = 0,
        Pending = 1,
        Joined = 2,
    }
}
