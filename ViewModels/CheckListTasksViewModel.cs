using CheckListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CheckListApp.Models.CheckListTask;

namespace CheckListApp.ViewModels
{
    public class CheckListTasksViewModel
    {
        public enum SORTOPTIONS{ DescriptionAscending, DescriptionDescending, DateDueAscending, DateDueDescending, PriorityAscending, PriorityDescending }
        public List<CheckListTask> CheckListTasks { get; set; }
        public string SelectedPriorityFilter { get; set; }
        public string SelectedSortOption { get; set; }

    }
}
