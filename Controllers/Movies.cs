
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Movies : ControllerBase
    {
        private readonly ApplactionDbContext _context;


        //B3ml Inject LL Applaction db Conext Gwa El Constructor Bta3 el Contraller DH

        //Lazm 3shan A3mal upload L File(Soura) Akhly Baly AllowedExtenstion , MaxallowedPostersize
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        public Movies(ApplactionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Include(c => c.Genre)
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificIdAsync(int id)
        {
            //Find bta5od paramter wa7ed
            var movies = await _context.Movies.Include(c => c.Genre).SingleOrDefaultAsync(c => c.Id == id);

            if (movies == null)
                return NotFound();

            return Ok(movies);
        }

        [HttpGet("GetGenreId")]
        public async Task<IActionResult> GetbyGenreIdAsync(byte genreID)
        {
            //Find bta5od paramter wa7ed w mbnst5dmhash m3a include
            var movies = await _context.Movies.Where(m => m.GenreId == genreID).Include(c => c.Genre).ToListAsync();

            if (movies == null)
                return NotFound();

            return Ok(movies);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is Required");

            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only png and jpg format file are allowed");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allower poster size is 1 MB");

            var isValidGenre = _context.Genres.Any(x => x.Id == dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Genre Id is out of Scope");

            //B3ml convert ll Ifile TO Byte
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = dataStream.ToArray(),
                Rate = dto.Rate,
                Storyline = dto.Storyline,
                Year = dto.Year,

            };

            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound($"No genre was Found with ID: {id}"); // pass paramter into string

 

            var isValidGenre = _context.Genres.Any(x => x.Id == dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Genre Id is out of Scope");

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only png and jpg format file are allowed");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allower poster size is 1 MB");

                //B3ml convert ll Ifile TO Byte
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();

            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Title = dto.Title;
            movie.Rate = dto.Rate;
            movie.Storyline = dto.Storyline;
            movie.Year = dto.Year;

            _context.SaveChanges();

            return Ok(movie);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteasync(int id)
        {
            var movie = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (movie == null)
                return NotFound($"No movie was Found with ID: {id}"); // pass paramter into string

            _context.Remove(movie);

            _context.SaveChanges();


            return Ok(movie);


        }


    }
}
