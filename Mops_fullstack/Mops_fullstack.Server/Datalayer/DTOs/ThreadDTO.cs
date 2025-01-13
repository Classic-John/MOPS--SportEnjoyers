
namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class CreateThreadDTO
    {
        public int GroupId { get; set; }

        public string InitialMessage { get; set; } = null!;
    }

    public class ThreadDTO
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public ICollection<MessageDTO> Messages { get; set; } = [];
    }

    public class ThreadSummaryDTO
    {
        public int Id { get; set; }

        public MessageDTO InitialMessage { get; set; } = null!;
    }
}
