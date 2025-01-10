using System.ComponentModel.DataAnnotations;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    /*public class PlayerDTO : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Age { get; set; }
        public int? GoogleId { get; set; }
        public int? FacebookId { get; set; }
        public bool IsOrganizer { get; set; }
        public int? AreaOwnerId { get; set; }
        public virtual Owner? AreaOwner { get; set; }
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }*/

    public class PlayerRegisterDTO
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Age { get; set; }
    }

    public class PlayerLoginDTO
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }

    public class LoggedPlayerDTO
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Token { get; set; } = null!;

        LoggedPlayerDTO() { }

        public LoggedPlayerDTO(Player player, string token)
        {
            Email = player.Email;
            Name = player.Name;
            Age = player.Age;
            Token = token;
        }
    }

    public class PlayerDTO
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Age { get; set; }
    }

    public class JoinRequestVerdict
    {
        public bool Accepted { get; set; }
    }
}
