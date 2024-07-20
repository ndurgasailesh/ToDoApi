using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskScheduler.Data.DBModels;

namespace TaskScheduler.Dtos
{

    public class TaskListDto
    {
       
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public string? UserId { get; set; }

        public string? UserName { get; set; }


    }
}
