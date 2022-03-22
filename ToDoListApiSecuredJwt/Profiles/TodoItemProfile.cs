using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Dtos;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemCreateDto, TodoItem>();
            CreateMap<TodoItem, TodoItemReadDto>();
        }
    }
}
