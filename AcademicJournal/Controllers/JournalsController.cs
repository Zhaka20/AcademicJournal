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
using AcademicJournal.ViewModels;
using Microsoft.AspNet.Identity;
using AcademicJournal.Services.Abstractions;

namespace AcademicJournal.Controllers
{

    public class JournalsController : Controller
    {
        private IJournalsControllerService _service;
        public JournalsController(IJournalsControllerService service)
        {
            this._service = service;
        }

        public async Task<ActionResult> Fill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            JournalFillVM viewModel = await _service.GetJournalFillViewModelAsync((int)id);
            return View(viewModel);
        }
        
        public async Task<ActionResult> CreateWorkDay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = _service.GetCreateWorkDayViewModel((int)id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateWorkDay(CreateWorkDayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateWorkDayAsync(viewModel);
                return RedirectToAction("Fill", new { id = viewModel.JournalId });
            }
            return View(viewModel);
        }


        // GET: Journals
        public async Task<ActionResult> Index()
        {
            JournalIndexViewModel viewModel = await _service.GetJournalsIndexViewModelAsync();
            return View(viewModel);
        }

        // GET: Journals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JournalDetailVM viewModel = await _service.GetJournalDetailsViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Journals/Create
        public ActionResult Create()
        {
            var mentorId = User.Identity.GetUserId();
            CreateJournalVM viewModel = _service.GetCreateJournalViewModel(mentorId);
            return View(viewModel);
        }

        // POST: Journals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateJournalVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var journalId = await _service.CreateJournalAsync(viewModel);
                return RedirectToAction("Fill","Journals", new { id = journalId});
            }
            return View(viewModel);
        }

        // GET: Journals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditJournalVM viewModel = await _service.GetEditJournalViewModelAsync((int)id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditJournalVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateJournalAsync(viewModel);
                return RedirectToAction("Details", "Journals", new { id = viewModel.Id });
            }
            return View(viewModel);
        }

        // GET: Journals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _service.GetDeleteJournalViewModelAsync((int)id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteJournalAsync(id);
            return RedirectToAction("Index");
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
