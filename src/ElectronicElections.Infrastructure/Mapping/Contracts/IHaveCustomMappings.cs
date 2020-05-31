using AutoMapper;

namespace ElectronicElections.Infrastructure.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
