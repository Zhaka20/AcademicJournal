using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AcademicJournal.ViewModels.Mentors;
using Microsoft.AspNet.Identity;
using AcademicJournal.App_Start;
using AcademicJournal.Services.Abstractions;

namespace AcademicJournal.Controllers
{
    [Authorize]
    public class MentorsController : Controller
    {

        IMentorsControllerService _service;
        ApplicationUserManager userManager;
        public MentorsController(IMentorsControllerService service,ApplicationUserManager userManager)
        {
            this.userManager = userManager;
            this._service = service;
        }
       
        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> Home()
        {
            string mentorId = User.Identity.GetUserId();
            MentorsHomeViewModel viewModel = await _service.GetHomeViewModelAsync(mentorId);
            return View(viewModel);
        }

        // GET: Mentors
        [ActionName("Index")]
        public async Task<ActionResult> ListAllMentors()
        {
            MentorsListViewModel viewModel = await _service.GetMentorsListViewModelAsync();
            return View(viewModel);
        }

        // GET: Mentors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MentorDetailsViewModel viewModel = await _service.GetDetailsViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public async Task<ActionResult> AcceptStudent()
        {
            string mentorId = User.Identity.GetUserId();
            MentorAcceptStudentViewModel viewModel = await _service.GetAcceptStudentViewModelAsync(mentorId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptStudent(string id)
        {
            await _service.AcceptStudentAsync(id, User.Identity.GetUserId());
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public async Task<ActionResult> ExpelStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MentorExpelStudentViewModel viewModel = await _service.GetExpelStudentViewModelAsync(id);
            if(viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(viewModel);
        }

        [ActionName("ExpelStudent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveStudent(string id)
        {
            await _service.RemoveStudentAsync(id, User.Identity.GetUserId());
            return RedirectToAction("Home", "Mentors", new { id = id });
        }

        public async Task<ActionResult> Student(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMentorViewModel viewModel = await _service.GetStudentViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Mentors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            CreateMentorViewModel viewModel = _service.GetCreateMentorViewModel();
            return View(viewModel);
        }

        // POST: Mentors/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMentorViewModel viewModel)
        {
            userManager = StaticConfig.ConfigureApplicationUserManager(userManager);

            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.CreateMentorAsync(viewModel);
               
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(viewModel);
        }

        // GET: Mentors/Edit/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditMentorViewModel viewModel =await _service.GetEditViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Mentors/Edit/5    
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditMentorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   await _service.UpdateMentorAsync(viewModel);
                   return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Could not save changes! Please try again!");
                }
            }
            return View(viewModel);
        }

        // GET: Mentors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeleteMentorViewModel viewModel = await _service.GetDeleteViewModel(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Mentors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteMenotorAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Could not delete the Mentor! Please try again!";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
