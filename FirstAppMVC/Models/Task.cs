using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FirstAppMVC.Models
{
    public class Task
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        [DefaultValue("getutcdate()")]
        public DateTime Created { get; set; }
    }

    public class TaskDBContext : DbContext
    {
        public TaskDBContext(string connString)
        {
            this.Database.Connection.ConnectionString = connString;
        }
        public DbSet<Task> Tasks { get; set; }
    }
}