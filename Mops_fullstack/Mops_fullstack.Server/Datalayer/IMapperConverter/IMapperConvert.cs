using AutoMapper;

namespace Mops_fullstack.Server.Datalayer.IMapperConverter
{
    public interface IMapperConvert<T,U>
    {
        public U Convert(T input)
        {
            MapperConfiguration config = new(cfg => cfg.CreateMap<T, U>());
            return new Mapper(config).Map<U>(input);
        }
    }
}
