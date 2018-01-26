using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AcademicJournal.ViewModels.Controller.WorkDays;
using AcademicJournal.Services.Abstractions;

namespace AcademicJournal.Controllers
{
    public class WorkDaysController : Controller
    {
        private IWorkDaysControllerService _service;

        public WorkDaysController(IWorkDaysControllerService service)
        {
            this._service = service;
        }

        // GET: WorkDays
        public async Task<ActionResult> Index()
        {
            IndexViewModel viewModel = await _service.GetWorkDaysIndexViewModel();
            return View(viewModel);
        }

        // GET: WorkDays/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsViewModel vieModel = await _service.GetWorkDayDetailsViewModelAsync((int)id);
            return View(vieModel);
        }

        // GET: WorkDays/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateViewModel viewModel = _service.GetCreateWorkDayViewModel((int)id);
            return View(viewModel);
        }

        // POST: WorkDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                int workDayId = await _service.CreateWorkDayAsync(inputModel);
                return RedirectToAction("Details", new { id = workDayId });
            }

            return View(inputModel);
        }

        // GET: WorkDays/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditViewModel viewModel = await _service.GetWorkDayEditViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: WorkDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _service.WorkDayUpdateAsync(inputModel);
                return RedirectToAction("Details", new { id = inputModel.WorkDayToEdit.Id });
            }
            return View(inputModel);
        }

        // GET: WorkDays/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeleteViewModel viewModel = await _service.GetWorkDayDeleteViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: WorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(DeleteInputModel inputModel)
        {
            await _service.WorkDayDeleteAsync(inputModel.Id);
            return RedirectToAction("Journals", "Details", new { id = inputModel.JournalId });
        }

        public async Task<ActionResult> AddAttendees(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddAttendeesViewModel viewModel = await _service.GetWorDayAddAttendeesViewModelAsync((int)id);

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAttendees(int? id, List<string> studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _service.AddWorkDayAttendeesAsync((int)id, studentId);

            return RedirectToAction("Details", "WorkDays", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckAsLeft(int? id, List<int> attendanceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _service.CheckAsLeftAsync((int)id, attendanceId);

            return RedirectToAction("Details", "WorkDays", new { id = id });
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
