using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService  _genreseService;

        public GenresController(IGenresService genreseService)
        {
            _genreseService = genreseService;
        }

        //B3ml Inject LL Applaction db Conext Gwa El Constructor Bta3 el Contraller DH
        //public GenresController(ApplactionDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // var genres = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
            var genres = await _genreseService.GetAll();

            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _genreseService.Add(genre);

            return Ok(genre);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> updateAsync(byte id , [FromBody] CreateGenreDto dto)
        {
            var genre = await _genreseService.GetById(id);

            if(genre == null)
                return NotFound($"No genre was Found with ID: {id}"); // pass paramter into string

            genre.Name = dto.Name;
            _genreseService.update(genre);

            return Ok(genre);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteasync(byte id)
        {
            var genre = await _genreseService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was Found with ID: {id}"); // pass paramter into string

            _genreseService.delete(genre);

            return Ok(genre);

        }


    }
}
