using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using Music.MultipleModel;
using System.Drawing;
using System.Web.Helpers;

namespace Music.Controllers
{
    public class SongController : Controller
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;

        public SongController(IdentityManager.Data.ApplicationDbContext db)
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
            SongAndPlayList songAndPlayList = new SongAndPlayList(_db);
            return View(songAndPlayList);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddSong(Song obj,IFormFile file, IFormFile a_file)
        {
            SongAndPlayList songAndPlayList = new SongAndPlayList(_db,obj);
            //Console.WriteLine("FILENAME"+file.FileName);
            //string path= "~/Images/Songs/" + file.FileName;
            string fileName = Path.GetFileName(file.FileName);
            string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Songs\\", fileName);
            var filestream = new FileStream(uploadfilepath, FileMode.Create);

            string fileName2 = Path.GetFileName(a_file.FileName);
            string uploadfilepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Audio", fileName2);
            var filestream2 = new FileStream(uploadfilepath2, FileMode.Create);

            file.CopyToAsync(filestream);
            a_file.CopyToAsync(filestream2);
            filestream.Close();
            filestream2.Close();
            obj.Image = file.FileName;
            obj.SongPath = a_file.FileName;
            if (ModelState.IsValid)
            {
               _db.Song.Add(obj);
               _db.SaveChanges();
               TempData["success"] = "Song added successfully";
               return RedirectToAction("Dashboard", "Home");
            }
            return View(songAndPlayList);
        }
        //GET
        public IActionResult EditSong(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Song.Find(id);
            SongAndPlayList songAndPlayList = new SongAndPlayList(_db,obj);
            if (obj == null)
            {


                return NotFound();
            }

            return View(songAndPlayList);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSong(Song obj, int? id, IFormFile file, IFormFile a_file)
        {
            Console.WriteLine("ID"+id);
            SongAndPlayList songAndPlayList = new SongAndPlayList(_db, obj);
        //    string fileName = Path.GetFileName(file.FileName);
            string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Songs\\", file.FileName);
            var filestream = new FileStream(uploadfilepath, FileMode.Create);

          //  string fileName2 = Path.GetFileName(a_file.FileName);
            string uploadfilepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Audio", a_file.FileName);
            var filestream2 = new FileStream(uploadfilepath2, FileMode.Create);

            file.CopyToAsync(filestream);
            a_file.CopyToAsync(filestream2);
            obj.Image = file.FileName;
            obj.SongPath = a_file.FileName;
            obj.SongId = (int)id;
            filestream2.Close();
            filestream.Close();
            if (ModelState.IsValid)
            {
                _db.Song.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Song Updated successfully";
                return RedirectToAction("Dashboard", "Home");
            }
            return View(songAndPlayList);
        }

        public IActionResult DeleteSong(int? id)
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
        [ValidateAntiForgeryToken,ActionName("DeleteSong")]
        public IActionResult DeleteSongs(int? id)
        {
            var obj = _db.Song.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Song.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Song deleted successfully";
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult PlaySong(int? id)
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
    }
}
