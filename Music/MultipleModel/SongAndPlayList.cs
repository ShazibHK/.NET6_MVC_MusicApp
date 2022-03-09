using IdentityManager.Data;
using Music.Models;

namespace Music.MultipleModel
{
    public class SongAndPlayList
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;
        public Song Song { get; set; }
        public IEnumerable<PlayList> PlayList { get; set; }
        public SongAndPlayList(IdentityManager.Data.ApplicationDbContext db)
        {
            _db = db;
            PlayList = _db.PlayList;
        }
        public SongAndPlayList(IdentityManager.Data.ApplicationDbContext db,Song obj)
        {
            _db = db;
            PlayList = _db.PlayList;
            Song = obj;
        }

    }
}