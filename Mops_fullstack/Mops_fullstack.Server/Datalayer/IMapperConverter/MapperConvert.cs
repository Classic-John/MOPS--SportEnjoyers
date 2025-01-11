using AutoMapper;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Datalayer.IMapperConverter
{
    /*public static class MapperConvert<T, U>
    {
        public static U ConvertItem(T input)
        {
            MapperConfiguration config = new(cfg => {
                cfg.CreateMap<Player, PlayerDTO>();
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
            CreateMap<PlayerLoginDTO, Player>(MemberList.Source);

            CreateMap<Group, GroupDTO>();
            CreateMap<Group, GroupSearchDTO>(MemberList.Destination);
            CreateMap<CreateGroupDTO, Group>(MemberList.Source);
        }
    }
}

