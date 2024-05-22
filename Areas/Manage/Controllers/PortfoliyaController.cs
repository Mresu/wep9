using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication9.DAL;
using WebApplication9.Models;

namespace WebApplication9.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var data = _context.portfolios.ToList();

            return View(data);
        }
        public IActionResult Update(int id)
        {
            var portfolio = _context.portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }


        [HttpPost]
        public IActionResult Update(Portfolio portfolios)
        {
            if (!ModelState.IsValid) return View();


            var oldportfolio = _context.portfolios.FirstOrDefault(x => x.Id == portfolios.Id);
            if (portfolios.ImgFile != null)
            {


                string path = _environment.WebRootPath + @"\Upload\";
                FileInfo fileInfo = new FileInfo(path + oldportfolio.ImgUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                string filename = Guid.NewGuid() + portfolios.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    portfolios.ImgFile.CopyTo(stream);
                }
                oldportfolio.ImgUrl = filename;
            }


            oldportfolio.Description = portfolios.Description;
            oldportfolio.Name = portfolios.Name;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {

            if (!portfolio.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImgFile", "File tipini duzgun daxil edin");
                return View();
            }
            string path = _environment.WebRootPath + @"\Upload\";


            string filename = Guid.NewGuid() + portfolio.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                portfolio.ImgFile.CopyTo(stream);
            }
            portfolio.ImgUrl = filename;
            _context.portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var portfolio = _context.portfolios.FirstOrDefault(x=>x.Id==id);
            if (portfolio != null)
            {
                return View();
            }
            _context.portfolios.Remove(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
