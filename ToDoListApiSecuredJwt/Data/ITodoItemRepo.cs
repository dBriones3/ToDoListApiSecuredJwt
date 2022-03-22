using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApiSecuredJwt.Models;

namespace ToDoListApiSecuredJwt.Data
{
    public interface ITodoItemRepo
    {
        bool SaveChanges();
        void CreateItem(TodoItem item);
        IEnumerable<TodoItem> GetItems();
        TodoItem GetItemById(int id);
        void UpdateItem(TodoItem item);
        void RemoveItem(TodoItem item);
    }
}
