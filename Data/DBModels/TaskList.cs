using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskScheduler.Data.DBModels;

namespace TaskScheduler.Data.Models
{
    public class TaskList
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [MaxLength(450)]
        public virtual string UserId { get; set; }

        public virtual  ApplicationUser ApplicationUser { get; set; }

    }
}
