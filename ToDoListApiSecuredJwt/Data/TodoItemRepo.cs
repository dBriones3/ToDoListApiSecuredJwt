using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public class TodoItemRepo : ITodoItemRepo
    {
        private readonly ApiDbContext _context;

        public TodoItemRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateItem(TodoItem item)
        {
            if (item == null) throw new ArgumentException(nameof(item));

            _context.Add(item);
        }

        public TodoItem GetItemById(int id)
        {
            return _context.TodoItems.Find(id);
        }

        public IEnumerable<TodoItem> GetItems()
        {
            return _context.TodoItems.ToList();
        }

        public void RemoveItem(TodoItem item)
        {
            if (item == null) throw new ArgumentException(nameof(item));

            _context.Remove(item);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateItem(TodoItem item)
        {
            if (item == null) throw new ArgumentException(nameof(item));

            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
