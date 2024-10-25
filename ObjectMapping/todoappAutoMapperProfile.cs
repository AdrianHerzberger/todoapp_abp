using AutoMapper;
using todoapp.Entities.Models;
using todoapp.Services.Dtos;
namespace todoapp.ObjectMapping;

public class todoappAutoMapperProfile : Profile
{
    public todoappAutoMapperProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
    }
}