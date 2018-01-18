using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.Services.Abstractions;
using AcademicJournal.ViewModels;

namespace AcademicJournal.Controllers
{
    public class AttendancesController : Controller
    {
        private IAttendancesControllerService _service;

        public AttendancesController(IAttendancesControllerService service)
        {
            this._service = service;
        }

        // GET: Attendances
        public async Task<ActionResult> Index()
        {
            AttendanceIndexViewModel viewModel = await _service.GetAttendancesIndexViewModelAsync();
            return View(viewModel);
        }

        // GET: Attendances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendancesDetailsViewModel viewModel = await _service.GetAttendancesDetailsViewModelAsync((int)id);
            return View(viewModel);
        }

        // GET: Attendances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _service.GetEditAttendanceViewModelAsync((int)id);
            return View(viewModel);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAttendanceViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAttendanceAsync(inputModel);
                return RedirectToAction("Index");
            }
           
            return View(inputModel);
        }

        // GET: Attendances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _service.GetDeleteAttendanceViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(DeleteAttendanceInputModel inputModel)
        {
            await _service.DeleteAttendanceAsync(inputModel);
            return RedirectToAction("Details", "WorkDays", new { id = inputModel.Attendance.WorkDayId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
