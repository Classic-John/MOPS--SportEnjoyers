namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }

        public FieldSearchDTO Field { get; set; } = null!;

        public GroupSearchDTO Group { get; set; } = null!;

        public string MatchDate { get; set; } = null!;
    }

    public class CreateMatchDTO
    {
        public int FieldId { get; set; }

        public int GroupId { get; set; }

        public string MatchDate { get; set; } = null!;
    }
}
