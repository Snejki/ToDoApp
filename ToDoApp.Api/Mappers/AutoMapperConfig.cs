using AutoMapper;
using ToDoApp.Api.Dtos.Element;
using ToDoApp.Api.Dtos.List;
using ToDoApp.Api.Dtos.User;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ToDoList, ListGetDto>();
            CreateMap<ToDoElement, ElementGetDto>();
            CreateMap<User, UserGetDto>();
        }
    }
}
