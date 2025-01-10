using Mops_fullstack.Server.Datalayer.Database;
using Mops_fullstack.Server.Datalayer.Repositories;

namespace Mops_fullstack.Server.Core
{
    public class UnitOfWork
    {
        public FieldRepo? FieldRepo { get; set; }
        public GroupRepo? GroupRepo { get; set; }
        public MatchRepo? MatchRepo { get; set; }
        public MessageRepo? MessageRepo { get; set; }
        // public OwnerRepo? OwnerRepo { get; set; }
        public PlayerRepo? PlayerRepo { get; set; }
        public ThreadRepo? ThreadRepo { get; set; }

        public UnitOfWork(FieldRepo? fieldRepo, GroupRepo? groupRepo, MatchRepo? matchRepo, MessageRepo? messageRepo/*, OwnerRepo? ownerRepo*/,
            PlayerRepo? playerRepo, ThreadRepo? threadRepo,SportEnjoyersDatabaseContext context)
        {
            FieldRepo = fieldRepo;
            GroupRepo = groupRepo;
            MatchRepo = matchRepo;
            MessageRepo = messageRepo;
            // OwnerRepo = ownerRepo;
            PlayerRepo = playerRepo;
            ThreadRepo = threadRepo;
            FieldRepo.InitializeItemData(context);
            GroupRepo.InitializeItemData(context);
            MatchRepo.InitializeItemData(context);
            MessageRepo.InitializeItemData(context);
            // OwnerRepo.InitializeItemData(context);
            PlayerRepo.InitializeItemData(context);
            ThreadRepo.InitializeItemData(context);
        }
    }
}
