using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApiSecuredJwt.Dtos
{
    public class TodoItemCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
