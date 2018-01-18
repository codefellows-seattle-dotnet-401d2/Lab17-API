using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class TaskItem
    {
        //Timestamp when the TaskItem is created
        public int Created { get; }

        //Optional "Must be complete by" timestamp
        public int DueBy { get; set; }

        //Optional time to remind the user
        public int RemindAt { get; set; }

        //Required description
        public string Description { get; set; }


        public TaskItem(String Description)
        {

        }
    }
}
