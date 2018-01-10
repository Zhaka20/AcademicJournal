﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Assignment
    {
        public int Id { get; set; }       
        public string Title { get; set; }

        public int? TaskFileId { get; set; }
        [ForeignKey("TaskFileId")]
        public virtual TaskFile TaskFile { get; set; }

        public string CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual Mentor Creator { get; set; }

        public DateTime Created { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
