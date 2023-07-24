using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Entities
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }

        public List<Movie>? Movies { get; } = new List<Movie>();
    }
}
