
using Swashbuckle.AspNetCore.Annotations;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class FieldSearchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public PlayerDTO Owner { get; set; } = null!;

        public string Location { get; set; } = null!;
    }

    public class FieldDTO
    {
        public string Name { get; set; } = null!;

        public PlayerDTO Owner { get; set; } = null!;

        public string Location { get; set; } = null!;

        public ICollection<string> ReservedDates { get; set; } = [];

        public bool? IsYours { get; set; } = false;
    }

    public class CreateFieldDTO
    {
        public string Name { get; set; } = null!;

        [SwaggerIgnore]
        public int? OwnerId { get; set; }

        public string Location { get; set; } = null!;
    }

    public class FieldFilterDTO
    {
        public string? Name { get; set; }

        public string? Owner { get; set; }

        public string? Location { get; set; }

        public bool Yours { get; set; } = false;

        [SwaggerIgnore]
        public int? OwnerId { get; set; }

        public string? FreeOnDay { get; set; }
    }
}
