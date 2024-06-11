using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetCreateView()
        {


            ViewBag.AllPublishers = _context.Publishers.ToList();
            ViewBag.AllEducationYears = _context.EducationYears.ToList();
            ViewBag.AllSubjects = _context.Subjects.ToList();
            return View("Create");
        }

        public IActionResult GetHomeView()
        {
            return View("Home", _context.Books.Include(b=>b.EducationYear).Include(b => b.Publisher).Include(b => b.Subject).ToList());
        }


        [HttpPost]
        public IActionResult AddNew(Book book)
        {
            if (ModelState.IsValid == true)
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                return RedirectToAction("GethomeView");
            }
            else
            {
                ViewBag.AllPublishers = _context.Publishers.ToList();
                ViewBag.AllEducationYears = _context.EducationYears.ToList();
                ViewBag.AllSubjects = _context.Subjects.ToList();
                return View("Create", book);
            }
        }

        public IActionResult GetEditView(int id)
        {
            Book book = _context.Books.FirstOrDefault(d => d.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllPublishers = _context.Publishers.ToList();
                ViewBag.AllEducationYears = _context.EducationYears.ToList();
                ViewBag.AllSubjects = _context.Subjects.ToList();
                return View("Edit", book);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Book book)
        {
            if (ModelState.IsValid == true)
            {
                _context.Books.Update(book);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {

                ViewBag.AllPublishers = _context.Publishers.ToList();
                ViewBag.AllEducationYears = _context.EducationYears.ToList();
                ViewBag.AllSubjects = _context.Subjects.ToList();
                return View("Edit", book);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Book book = _context.Books.Include(b => b.EducationYear).Include(b => b.Publisher).Include(b => b.Subject).FirstOrDefault(d => d.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", book);
            }
        }

        public IActionResult GetDeleteView(int id)
        {
            Book book = _context.Books.Include(b => b.EducationYear).Include(b => b.Publisher).Include(b => b.Subject).FirstOrDefault(d => d.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", book);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Book book = _context.Books.Find(id);


            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("GetHomeView");
        }

    }
}
