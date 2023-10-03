using System;
using System.Collections.Generic;

#nullable disable

namespace demo.Models
{
    public partial class ProjectJob
    {
        public int TaskId { get; set; }
        public string ProjectKey { get; set; }
        public int ProjectId { get; set; }
        public string TitleTask { get; set; }
        public string DescriptionTask { get; set; }
        public string TypeTask { get; set; }
        public DateTime? DeadLineTask { get; set; }
        public string PriorityTask { get; set; }
        public string LevelTask { get; set; }
        public string UserCreate { get; set; }
        public string UserImplement { get; set; }
        public DateTime TaskCreateDate { get; set; }
        public DateTime? TaskUpdateDate { get; set; }
        public string StatusTask { get; set; }
    }
}
