using AutoMapper;
using Entities;
using Entities.BizEntities;
using Repositories.DBEntities;

namespace Repositories.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeBizEntity, Employee>().ReverseMap();
            CreateMap<RoleBizEntity, Role>().ReverseMap();
        }
    }
}
