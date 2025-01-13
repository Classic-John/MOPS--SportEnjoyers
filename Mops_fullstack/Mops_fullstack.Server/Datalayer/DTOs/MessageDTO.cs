using Swashbuckle.AspNetCore.Annotations;

namespace Mops_fullstack.Server.Datalayer.DTOs
{
    public class CreateMessageDTO
    {
        public string Text { get; set; } = null!;

        public int ThreadId { get; set; }

        [SwaggerIgnore]
        public int? PlayerId { get; set; }

        public CreateMessageDTO() { }

        public CreateMessageDTO(string text, int playerId, int threadId)
        {
            Text = text;
            ThreadId = threadId;
            PlayerId = playerId;
        }
    }

    public class MessageDTO
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public PlayerDTO Player { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public bool IsInitial { get; set; }

        public bool IsYours { get; set; } = false;
    }
}
