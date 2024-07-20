using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using TaskScheduler.Data.Models;
using TaskScheduler.Dtos;

namespace TaskScheduler.Data.DBModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<TaskList> TaskLists { get; set; }
    }
}
