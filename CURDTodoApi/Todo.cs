using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CURDTodoApi
{
    public class Todo
    {
            [Key] // Marks this property as the Primary Key
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime? DueDate { get; set; }
    }

}

