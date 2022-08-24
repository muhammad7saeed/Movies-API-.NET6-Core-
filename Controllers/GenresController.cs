using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly ApplactionDbContext _context;


        //B3ml Inject LL Applaction db Conext Gwa El Constructor Bta3 el Contraller DH
        public GenresController(ApplactionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();

            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> updateAsync(int id , [FromBody] CreateGenreDto dto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if(genre == null)
                return NotFound($"No genre was Found with ID: {id}"); // pass paramter into string

            genre.Name = dto.Name;
            _context.SaveChanges();

            return Ok(genre);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteasync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"No genre was Found with ID: {id}"); // pass paramter into string

            _context.Remove(genre);

            _context.SaveChanges();


            return Ok(genre);


        }




    }
}
