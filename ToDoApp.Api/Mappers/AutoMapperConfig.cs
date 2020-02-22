using AutoMapper;
using ToDoApp.Api.Dtos.List;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ToDoList, ListGetDto>();
        }
    }
}
