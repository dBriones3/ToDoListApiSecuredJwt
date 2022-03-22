using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Data;
using ToDoListApiSecuredJwt.Dtos;
using ToDoListApiSecuredJwt.Models;
using static ToDoListApiSecuredJwt.Constants.PermissionConst;

namespace ToDoListApiSecuredJwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoItemRepo _repo;
        private readonly IMapper _mapper;

        public TodoListController(ITodoItemRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItemReadDto>> GetAllItems()
        {
            var items = _repo.GetItems();

            return Ok(_mapper.Map<IEnumerable<TodoItemReadDto>>(items));
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItemReadDto> GetItemById(int id)
        {
            var item = _repo.GetItemById(id);

            if (item == null) return NotFound();

            return Ok(_mapper.Map<TodoItemReadDto>(item));
        }

        [HttpPost]
        [Authorize(Policy = AllowCreateItem)]
        public ActionResult<TodoItemReadDto> CreateItem(TodoItemCreateDto item)
        {
            var itemToSave = _mapper.Map<TodoItem>(item);
            _repo.CreateItem(itemToSave);
            _repo.SaveChanges();
            var itemSaved = _mapper.Map<TodoItemReadDto>(itemToSave);

            return CreatedAtAction(nameof(GetItemById), new { id = itemSaved.Id}, itemSaved);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AllowDeleteItem)]
        public ActionResult<TodoItemReadDto> DeleteItem(int id)
        {
            var itemToDelete = _repo.GetItemById(id);

            if (itemToDelete == null) return NotFound();

            _repo.RemoveItem(itemToDelete);
            _repo.SaveChanges();

            return Ok(_mapper.Map<TodoItemReadDto>(itemToDelete));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = AllowUpdateItem)]
        public ActionResult UpdateItem(int id, TodoItemCreateDto item)
        {
            var itemToUpdate = _repo.GetItemById(id);

            if (itemToUpdate == null) return NotFound();

            _mapper.Map(item, itemToUpdate);

            _repo.UpdateItem(itemToUpdate);
            _repo.SaveChanges();

            return Ok();
        }
    }
}
