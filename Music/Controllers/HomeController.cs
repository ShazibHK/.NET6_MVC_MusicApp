using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Music.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdentityManager.Data.ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IdentityManager.Data.ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            IEnumerable<PlayList> objCategoryList = _db.PlayList;
            myDynamicmodel.PlayList = objCategoryList;
            IEnumerable<Song> objCategoryList2 = _db.Song;
            myDynamicmodel.Song = objCategoryList2;
            return View(myDynamicmodel);
        }
        
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin")]
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
        [Authorize(Roles="Admin")]
        public IActionResult Dashboard()
        {

            var countFreeSong= (from o in _db.Song where o.type == "Free" select o).Count();
            ViewBag.countFreeSong = countFreeSong;
            Console.WriteLine("free" + countFreeSong);


            var countPaidSong = (from o in _db.Song where o.type == "Paid" select o).Count();
            ViewBag.countPaidSong = countPaidSong;
            Console.WriteLine("free" + countPaidSong);


            var countFreePlayList = (from o in _db.PlayList where o.type == "Free" select o).Count();
            ViewBag.countFreePlayList = countFreePlayList;
            Console.WriteLine("free" + countFreePlayList);


            var countPaidPlayList = (from o in _db.PlayList where o.type == "Paid" select o).Count();
            ViewBag.countPaidPlayList = countPaidPlayList;
            Console.WriteLine("free" + countPaidPlayList);

            dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            IEnumerable<PlayList> objCategoryList = _db.PlayList;
            myDynamicmodel.PlayList = objCategoryList;
            IEnumerable<Song> objCategoryList2= _db.Song;
            myDynamicmodel.Song = objCategoryList2;
            return View(myDynamicmodel);
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Search()
        {
            dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            IEnumerable<PlayList> objCategoryList = _db.PlayList;
            myDynamicmodel.PlayList = objCategoryList;
            IEnumerable<Song> objCategoryList2 = _db.Song;
            myDynamicmodel.Song = objCategoryList2;
            return View(myDynamicmodel);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Search(string s)
        {
            Console.WriteLine("Got from Search" + s);
            ViewBag.SongNameGiven = s;
            dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
            IEnumerable<PlayList> objCategoryList = _db.PlayList;
            myDynamicmodel.PlayList = objCategoryList;
            IEnumerable<Song> objCategoryList2 = _db.Song;
            myDynamicmodel.Song = objCategoryList2;
            return View(myDynamicmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}