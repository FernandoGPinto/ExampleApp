using AutoMapper;
using ExampleApp.Shared.Models;

namespace ExampleApp.Server.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerApplicationDTO, CustomerApplication>();
        }
    }
}
