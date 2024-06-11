using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class OwnersController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnviroment;

        public OwnersController(ApplicationDbContext context, IWebHostEnvironment webHostEnviroment)
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
        public IActionResult AddNew(Owner owner, IFormFile? imageFormFile)
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
                    owner.ImagePath = imgPath;
                    string imgFullPath = _webHostEnviroment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();

                }
                else
                {
                    owner.ImagePath = "\\images\\No_Image.png";
                }

                _context.Owners.Add(owner);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                return View("Create", owner);
            }
        }

        [HttpGet]
        public IActionResult GetHomeView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Home", _context.Owners.ToList());
            }
            else
            {
                return View("Home", _context.Owners.Where(d => d.Name.Contains(search)).ToList());
            }
        }


        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Owner owner = _context.Owners.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = owner;
            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", owner);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Owner owner = _context.Owners.Find(id);

            if (owner != null && owner.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnviroment.WebRootPath + owner.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Owners.Remove(owner);
            _context.SaveChanges();

            return RedirectToAction("GetHomeView");
        }

        public IActionResult GetEditView(int id)
        {
            Owner owner = _context.Owners.FirstOrDefault(d => d.Id == id);

            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", owner);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Owner owner, IFormFile? imageFormFile)
        {

            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (owner.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnviroment.WebRootPath + owner.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    owner.ImagePath = imgPath;
                    string imgFullPath = _webHostEnviroment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }


                _context.Owners.Update(owner);
                _context.SaveChanges();

                return RedirectToAction("GetHomeView");
            }
            else
            {
                return View("Edit", owner);
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Owner owner = _context.Owners.FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = owner;
            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", owner);
            }
        }

    }
}
