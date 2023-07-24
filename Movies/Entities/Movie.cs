using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Entities
{
    public class Movie
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; } 
        public List<Character> Characters { get; set; } = new List<Character>();
        public int FranchiseId { get; set; }
        public  Franchise Franchise {get; set;} = null!;
    }
}
