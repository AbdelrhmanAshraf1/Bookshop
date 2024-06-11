using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class publishersController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnviroment;

        public publishersController(ApplicationDbContext context, IWebHostEnvironment webHostEnviroment)
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
        public IActionResult AddNew(Publisher pub, IFormFile? imageFormFile)
        {
            //GUID (Globally Unique Identifier) -> FX789-10MNP-72cvx-tm9001b-23uzw.png     .jpg

            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName); //.png
                    Guid imgGuid = Guid.NewGuid();  //FX789-10MNP-72cvx-tm9001b-23uzw
                    string imgName = imgGuid + imgExtension;  //FX789-10MNP-72cvx-tm9001b-23uzw.png
                    string imgPath = "\\images\\" + imgName;
                    pub.ImagePath = imgPath;
                    string imgFullPath = _webHostEnviroment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();

                }
                else
                {
                    pub.ImagePath = "\\images\\No_Image.png";
                }

                _context.Publishers.Add(pub);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                return View("Create", pub);
            }
        }

        [HttpGet]
        public IActionResult GetHomeView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Home", _context.Publishers.ToList());
            }
            else
            {
                return View("Home", _context.Publishers.Where(d => d.Name.Contains(search)).ToList());
            }
        }


        public IActionResult GetDetailsView(int id)
        {
            Publisher Pub = _context.Publishers.Include(d => d.Books).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = Pub;
            if (Pub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", Pub);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Publisher Pub = _context.Publishers.Include(d => d.Books).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = Pub;
            if (Pub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", Pub);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Publisher Pub = _context.Publishers.Find(id);

            if (Pub != null && Pub.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnviroment.WebRootPath + Pub.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Publishers.Remove(Pub);
            _context.SaveChanges();

            return RedirectToAction("GetHomeView");
        }

        public IActionResult GetEditView(int id)
        {
            Publisher Pub = _context.Publishers.FirstOrDefault(d => d.Id == id);

            if (Pub == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", Pub);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Publisher pub, IFormFile? imageFormFile)
        {

            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (pub.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnviroment.WebRootPath + pub.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    pub.ImagePath = imgPath;
                    string imgFullPath = _webHostEnviroment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }


                _context.Publishers.Update(pub);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                return View("Edit", pub);
            }
        }

    }
}
