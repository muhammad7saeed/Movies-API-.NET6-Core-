namespace MoviesAPI.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();

        Task<Genre> GetById(byte id);

        Task<Genre> Add(Genre genre);

        Genre update(Genre genre);

        Genre delete(Genre genre);


    }
}
