using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace showcase
{
    public class AlbumService
    {
        private readonly IAlbumRepository repository;

        public AlbumService() : this(new AlbumRepository())
        {
            // no op
        }

        public AlbumService(IAlbumRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> ProcessInput(string input)
        {
            var albumNumber = GetAlbumNumberFromInput(input);
            if (albumNumber < 0)
                return "please enter a valid album name and number. e.g. photo-album 1";

            var photoAlbums = await repository.getAlbum(albumNumber);

            if (photoAlbums.Count == 0)
                return "album not found";

            var sb = new System.Text.StringBuilder();
            foreach (var album in photoAlbums)
            {
                sb.AppendLine($"[{album.id}] {album.title}");
            }
            return sb.ToString();
        }

        long GetAlbumNumberFromInput(string input)
        {
            var validationPattern = @"photo-album\s+\d+$";
            if (!Regex.IsMatch(input, validationPattern, RegexOptions.IgnoreCase))
                return -1;

            var albumNumberPattern = @"\d+$";
            var match = Regex.Match(input, albumNumberPattern);
            return long.Parse(match.Value);
        }
    }
}