using AcademicJournal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            List<DataModel.Models.Student> students = db.Students.ToList();
            return View(students);
        }

        public ActionResult GetStudent(string id)
        {
            return View();
        }

        public ActionResult CreateStudent()
        {
            return View();
        }

        public ActionResult DeleteStudent(string id)
        {
            return View();
        }

        public ActionResult Mentors()
        {
            return View();
        }

        public ActionResult GetMentor(string id)
        {
            return View();
        }

        public ActionResult CreateMentor()
        {
            return View();
        }

        public ActionResult DeleteMentor(string id)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}