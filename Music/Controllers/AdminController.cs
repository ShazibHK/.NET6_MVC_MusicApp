using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Diagnostics;

namespace Music.Controllers
{
    public class AdminController : Controller
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;

        public AdminController(IdentityManager.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ViewSong()
        {
            IEnumerable<Song> objCategoryList = _db.Song;
            return View(objCategoryList);
        }

        //GET
        public IActionResult AddSong()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddSong(Song obj)
        {
           
           if (ModelState.IsValid)
           {
              _db.Song.Add(obj);
              _db.SaveChanges();
              TempData["success"] = "Song added successfully";
              return RedirectToAction("Dashboard","Home");
           }
           return View(obj);
           
        }
        //GET
        public IActionResult EditSong(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Song.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSong(Song obj,int? id)
        {
            
                Console.WriteLine(obj.SongId);
            Console.WriteLine("obj"+obj.SongName);
            obj.SongId= (int)id;
            if (ModelState.IsValid)
            {
                _db.Song.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Song Updated successfully";
                return RedirectToAction("ViewSong","Admin");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Song.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Song.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Song.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
