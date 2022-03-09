using IdentityManager.Data;
using Music.Models;

namespace Music.MultipleModel
{
    public class PlayListDisplay
    {
        public readonly IdentityManager.Data.ApplicationDbContext _db;
        public IEnumerable<Song> Song { get; set; }
        public PlayList PlayList { get; set; }

        public PlayListDisplay(IdentityManager.Data.ApplicationDbContext db,PlayList obj)
        {
            _db = db;
            PlayList = obj;
            Console.WriteLine("IsideModel->playlistname"+PlayList.PlayListName);
            Song = _db.Song;
        }

     
    }
}
