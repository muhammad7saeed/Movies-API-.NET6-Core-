namespace MoviesAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplactionDbContext _context;

        public GenresService(ApplactionDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();

            return genres;
        }

        public async Task<Genre> GetById(byte id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            return genre;
        }

        public  Genre update(Genre genre)
        {

            _context.Update(genre);
            _context.SaveChanges();

            return genre;

        }
    }
}
