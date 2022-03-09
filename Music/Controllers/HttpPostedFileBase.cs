namespace Music.Controllers
{
    public class HttpPostedFileBase
    {
        public string? FileName { get; internal set; }
        public int ContentLength { get; internal set; }
        public object InputStream { get; internal set; }
    }
}