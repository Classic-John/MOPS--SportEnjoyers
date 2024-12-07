using Mops_fullstack.Server.Datalayer.Database;
using Mops_fullstack.Server.Datalayer.Repositories;

namespace Mops_fullstack.Server.Core
{
    public class UnitOfWork
    {
        FieldRepo? FieldRepo { get; set; }
        GroupRepo? GroupRepo { get; set; }
        MatchRepo? MatchRepo { get; set; }
        MessageRepo? MessageRepo { get; set; }
        OwnerRepo? OwnerRepo { get; set; }
        PlayerRepo? PlayerRepo { get; set; }
        ThreadRepo? ThreadRepo { get; set; }

        public UnitOfWork(FieldRepo? fieldRepo, GroupRepo? groupRepo, MatchRepo? matchRepo, MessageRepo? messageRepo, OwnerRepo? ownerRepo,
            PlayerRepo? playerRepo, ThreadRepo? threadRepo,SportEnjoyersDatabaseContext context)
        {
            FieldRepo = fieldRepo;
            GroupRepo = groupRepo;
            MatchRepo = matchRepo;
            MessageRepo = messageRepo;
            OwnerRepo = ownerRepo;
            PlayerRepo = playerRepo;
            ThreadRepo = threadRepo;
            FieldRepo.InitializeItemData(context);
            GroupRepo.InitializeItemData(context);
            MatchRepo.InitializeItemData(context);
            MessageRepo.InitializeItemData(context);
            OwnerRepo.InitializeItemData(context);
            PlayerRepo.InitializeItemData(context);
            ThreadRepo.InitializeItemData(context);
        }
    }
}
