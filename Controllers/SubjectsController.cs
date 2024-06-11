using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class SubjectsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnviroment;

        public SubjectsController(ApplicationDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        // HTTPVerbs -> HTTPGET - HTTPPOST
        [HttpPost]
        public IActionResult AddNew(Subject Sub)
        {
            if (ModelState.IsValid == true)
            {
                _context.Subjects.Add(Sub);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                ViewBag.AllDeprtments = _context.Subjects.ToList();
                return View("Create", Sub);
            }
        }

        [HttpGet]
        public IActionResult GetHomeView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Home", _context.Subjects.ToList());
            }
            else
            {
                return View("Home", _context.Subjects.Where(d => d.Name.Contains(search)).ToList());
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Subject Sub = _context.Subjects.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = Sub;
            if (Sub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", Sub);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Subject Sub = _context.Subjects.Find(id);


            _context.Subjects.Remove(Sub);
            _context.SaveChanges();

            return RedirectToAction("GetHomeView");
        }

        public IActionResult GetEditView(int id)
        {
            Subject Sub = _context.Subjects.FirstOrDefault(d => d.Id == id);

            if (Sub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", Sub);
            }
        }


        [HttpPost]
        public IActionResult EditCurrent(Subject Sub)
        {
            if (ModelState.IsValid == true)
            {
                _context.Subjects.Update(Sub);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {

                ViewBag.AllDeprtments = _context.Subjects.ToList();
                return View("Edit", Sub);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Subject Sub = _context.Subjects.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = Sub;
            if (Sub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", Sub);
            }
        }

    }
}
