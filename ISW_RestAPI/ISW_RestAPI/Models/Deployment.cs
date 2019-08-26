using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISW_RestAPI.Models
{
    public class Deployment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string Status { get; set; }
        public string IssuesEncountered { get; set; }
        public string Description { get; set; }
        public string DurationOfDeployment { get; set; }

    }
}