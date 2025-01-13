using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Mops_fullstack.Server.Datalayer.BaseClass
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
