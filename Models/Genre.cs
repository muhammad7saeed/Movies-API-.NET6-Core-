
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public String Name { get; set; }


    }
}
