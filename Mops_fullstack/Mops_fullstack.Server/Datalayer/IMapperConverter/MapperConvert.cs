using AutoMapper;

namespace Mops_fullstack.Server.Datalayer.IMapperConverter
{
    public static class MapperConvert<T, U>
    {
        public static U ConvertItem(T input)
        {
            MapperConfiguration config = new(cfg => cfg.CreateMap<T, U>());
            return new Mapper(config).Map<U>(input);
        }
    }
}

