using AutoMapper;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Datalayer.IMapperConverter
{
    /*public static class MapperConvert<T, U>
    {
        public static U ConvertItem(T input)
        {
            MapperConfiguration config = new(cfg => {
                cfg.CreateMap<T, U>();
            });
            return new Mapper(config).Map<U>(input);
        }
    }*/

    internal class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Player, PlayerDTO>(MemberList.Destination);
            CreateMap<PlayerDTO, Player>(MemberList.Source);
            CreateMap<PlayerRegisterDTO, Player>(MemberList.Source);
            CreateMap<GoogleAuthDTO, Player>();
            CreateMap<PlayerLoginDTO, Player>(MemberList.Source);

            CreateMap<Group, GroupDTO>();
            CreateMap<Group, GroupSearchDTO>(MemberList.Destination);
            CreateMap<CreateGroupDTO, Group>(MemberList.Source);

            CreateMap<Field, FieldDTO>();
            CreateMap<Field, FieldSearchDTO>(MemberList.Destination);
            CreateMap<CreateFieldDTO, Field>(MemberList.Source);

            CreateMap<Match, MatchDTO>(MemberList.Destination);
            CreateMap<CreateMatchDTO, Match>(MemberList.Source);

            CreateMap<Thread, ThreadDTO>(MemberList.Destination);
            CreateMap<Thread, ThreadSummaryDTO>();
            CreateMap<CreateThreadDTO, Thread>();

            CreateMap<Message, MessageDTO>(MemberList.Destination);
            CreateMap<CreateMessageDTO, Message>(MemberList.Source);
        }

        public static U ConvertItem<T, U>(T input)
        {
            MapperConfiguration config = new(cfg => {
                cfg.CreateMap<T, U>();
            });
            return new Mapper(config).Map<U>(input);
        }
    }
}

