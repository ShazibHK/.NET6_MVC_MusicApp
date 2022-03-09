using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using Music.MultipleModel;
using System.Drawing;
using System.Web.Helpers;


namespace Music.Controllers
{
    public class PlayListController : Controller
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;

        public PlayListController(IdentityManager.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ViewPlayList()
        {
            IEnumerable<PlayList> objCategoryList = _db.PlayList;
            return View(objCategoryList);
        }
        //GET
        public IActionResult AddPlayList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPlayList(PlayList obj,IFormFile file)
        {
            string fileName = Path.GetFileName(file.FileName);
            string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Songs\\", fileName);
            var filestream = new FileStream(uploadfilepath, FileMode.Create);
            file.CopyToAsync(filestream);
            obj.Image = file.FileName;
            if (ModelState.IsValid)
            {
                _db.PlayList.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "PlayList added successfully";
                return RedirectToAction("Dashboard", "Home");
            }
            return View(obj);

        }
        //GET
        public IActionResult EditPlayList(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.PlayList.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPlayList(PlayList obj, int? id, IFormFile file)
        {

            string fileName = Path.GetFileName(file.FileName);
            string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Songs\\", fileName);
            var filestream = new FileStream(uploadfilepath, FileMode.Create);
            file.CopyToAsync(filestream);
            obj.Image = file.FileName;
            obj.PlayListId = (int)id;
            if (ModelState.IsValid)
            {
                _db.PlayList.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Song Updated successfully";
                return RedirectToAction("Dashboard", "Home");
            }
            return View(obj);
        }

        public IActionResult DeletePlayList(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.PlayList.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("DeletePlayList")]
        public IActionResult DeletePlayLists(int? id)
        {
            var obj = _db.PlayList.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.PlayList.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Song deleted successfully";
            return RedirectToAction("Dashboard", "Home");

        }
        public IActionResult Play_PlayList(int? id)
        {
            var obj = _db.PlayList.Find(id);
            Console.WriteLine("Name" + obj.PlayListId);
            PlayListDisplay playListDisplay = new PlayListDisplay(_db, obj);

            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.PlayList.Find(id);

            if (playListDisplay == null)
            {
                return NotFound();
            }
            Console.WriteLine("->" + playListDisplay.PlayList.PlayListName);
            return View(playListDisplay);
        }
    }
}
