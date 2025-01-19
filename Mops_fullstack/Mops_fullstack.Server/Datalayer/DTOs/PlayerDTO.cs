using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using Mops_fullstack.Server.Datalayer.BaseClass;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Swashbuckle.AspNetCore.Annotations;

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

    public class GoogleAuthDTO
    {
        public string Key { get; set; } = null!;

        [SwaggerIgnore]
        public string? Email { get; set; }

        [SwaggerIgnore]
        public string? Name { get; set; }
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

    public class UpdatePlayerDTO
    {
        public string? Name { get; set; }

        public string? OldPassword { get; set; }

        public string? Password { get; set; }

        public int? Age { get; set; }
    }

    public class PlayerDTO
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Age { get; set; }
    }

    public class JoinRequestDTO
    {
        public PlayerDTO Player { get; set; }
        
        public GroupDTO Group { get; set; }

        public JoinRequestDTO(PlayerDTO player, GroupDTO group)
        {
            Player = player;
            Group = group;
        }
    }

    public class JoinRequestVerdict
    {
        public int PlayerId { get; set; }

        public bool Accepted { get; set; }
    }

    public class VerifyEmailDTO
    {
        public string VerificationCode { get; set; } = null!;
    }
}
