﻿using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Assignments
{
    public class CreateGroupAssignmentVM
    {
        public IEnumerable<Student> Students { get; set; }
        public CreateAssigmentVM Assignment { get; set; }
        public Student Student { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<string> studentId { get; set; }
    }
}