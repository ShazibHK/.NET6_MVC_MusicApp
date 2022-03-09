using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Diagnostics;

namespace Music.Controllers
{
    public class HomeViewController : Controller
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;
        
        public IActionResult Homepage()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult StatisticSongPartial()
        {

            return View();
        }
        public IActionResult StatisticsSong()
        {
            return View();
        }
        public IActionResult test()
        {
            return View();
        }
    
        public IActionResult ViewSong(IdentityManager.Data.ApplicationDbContext db)
        {
            IEnumerable<Song> objCategoryList = _db.Song;
            return View(objCategoryList);
        }
    }
}