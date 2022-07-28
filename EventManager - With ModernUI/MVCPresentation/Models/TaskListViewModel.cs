using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class TaskListViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string EventName { get; set; }
        public int EventID { get; set; }
    }
}