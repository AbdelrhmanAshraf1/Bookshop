using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class EducationYearsController : Controller
    {

        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnviroment;

        public EducationYearsController(ApplicationDbContext context, IWebHostEnvironment webHostEnviroment)
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
        public IActionResult AddNew(EducationYear EduYear)
        {
            if (ModelState.IsValid == true)
            {
                _context.EducationYears.Add(EduYear);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                ViewBag.AllDeprtments = _context.EducationYears.ToList();
                return View("Create", EduYear);
            }
        }

        [HttpGet]
        public IActionResult GetHomeView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Home", _context.EducationYears.ToList());
            }
            else
            {
                return View("Home", _context.EducationYears.Where(d => d.Name.Contains(search)).ToList());
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            EducationYear EduYear = _context.EducationYears.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = EduYear;
            if (EduYear == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", EduYear);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            EducationYear EduYear = _context.EducationYears.Find(id);


            _context.EducationYears.Remove(EduYear);
            _context.SaveChanges();

            return RedirectToAction("GetHomeView");
        }

        public IActionResult GetEditView(int id)
        {
            EducationYear EduYear = _context.EducationYears.FirstOrDefault(d => d.Id == id);

            if (EduYear == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", EduYear);
            }
        }


        [HttpPost]
        public IActionResult EditCurrent(EducationYear EduYear)
        {
            if (ModelState.IsValid == true)
            {
                _context.EducationYears.Update(EduYear);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {

                ViewBag.AllDeprtments = _context.EducationYears.ToList();
                return View("Edit", EduYear);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            EducationYear EduYear = _context.EducationYears.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = EduYear;
            if (EduYear == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", EduYear);
            }
        }
    }
}
