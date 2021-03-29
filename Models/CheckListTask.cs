using CheckListApp.Areas.Identity.Data;
using CheckListApp.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    public class CheckListTask
    {
        public enum TASKPRIORITY { LOW, MEDIUM, HIGH, CRITICAL};
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [Display(Name = "Task Priority")]
        public TASKPRIORITY TaskPriority { get; set; }

        [Display(Name = "Task Due Date")]
        [DateDueAttributes]
        public DateTime DateDue { get; set; }

        [Display(Name = "Task Latest Modified Date")]
        public DateTime DateModified { get; set; }

        [Display(Name = "Task Status?")]
        public bool TaskStatus{ get; set; }

        public CheckListAppUser User { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

    }

}
