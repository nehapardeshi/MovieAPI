using Microsoft.EntityFrameworkCore;
using Movies.Controllers;
using Movies.Entities;

namespace Movies.Test.MockData
{
    public class MockDataRepository : IDataRepository
    {
        private static List<Character> _characters = new List<Character>
        {
            new Character {Id= 1, Alias = "John", FullName = "John David Washington", Gender = "Male"},
            new Character {Id= 2, Alias = "Samuel", FullName = "Samuel Henry", Gender = "Male"},
            new Character {Id= 3, Alias = "Song Ho", FullName = "Song Kang Ho", Gender = "Male"},
        };

        private static List<Franchise> _franchises = new List<Franchise>
        {
            new Franchise {Id= 1, Name = "ABC Corp.", Description = "Most popular franchise for movies!" },
            new Franchise {Id= 2, Name = "DEF Corp.", Description = "worst franchise for movies!" },
            new Franchise {Id= 3, Name = "XYZ Corp.", Description = "Less popular franchise for movies!" },

        };
        private static List<Movie> _movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "TENET",Director= "Christopher Nolan", Genre = "Fiction", Trailer = "youtube", Picture = "asdsad", ReleaseYear = 2023, FranchiseId = 1, Characters = _characters },
            new Movie { Id = 2, Title = "AVATAR",Director= "James Cameron" ,Genre = "Adventure", Trailer = "youtube", Picture = "asdsad", ReleaseYear = 2022 , FranchiseId = 2},
            new Movie { Id = 3, Title = "PARASITE",Director= "Bon Joong Ho", Genre = "Thriller", Trailer = "youtube", Picture = "asdsad", ReleaseYear = 2019, FranchiseId = 3 }
        };

        



        public int AddCharacter(Character character)
        {
            var newCharacterId = _characters.Count + 1;
            character.Id = newCharacterId;
            _characters.Add(character);
            return newCharacterId;
        }

        public int AddFranchise(Franchise franchise)
        {
            var newFranchiseId = _franchises.Count + 1;
            franchise.Id = newFranchiseId;
            _franchises.Add(franchise);
            return newFranchiseId;
        }

        public int AddMovie(Movie movie)
        {
            int newMovieId = _movies.Count + 1;
            movie.Id = newMovieId;
            _movies.Add(movie);
            return newMovieId;
        }

        public void DeleteCharacter(int id)
        {
            var character = GetCharacter(id);
            _characters.Remove(character);
        }

        public void DeleteFranchise(int id)
        {
           var franchise = GetFranchise(id);
            _franchises.Remove(franchise);
        }

        public void DeleteMovie(int id)
        {
            var movie = GetMovie(id);
            _movies.Remove(movie);
        }

        public Character GetCharacter(int id)
        {
            return _characters.FirstOrDefault(x => x.Id == id);

        }

        public List<Character> GetCharacters()
        {
            return _characters;
        }

        public Franchise GetFranchise(int id)
        {
            return _franchises.FirstOrDefault(f => f.Id == id);
        }

        public List<Character> GetFranchiseCharacters(int franchiseId)
        {
            var movies = _movies.Where(movie => movie.FranchiseId == franchiseId).ToList();

            var characters = new List<Character>();
            foreach (var movie in movies)
            {
                characters.AddRange(movie.Characters);
            }

            return characters;
        }

        public List<Franchise> GetFranchises()
        {
            return _franchises;
        }

        public Movie GetMovie(int id)
        {
            return _movies.FirstOrDefault(m => m.Id == id);
        }

        public List<Character> GetMovieCharacters(int movieId)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == movieId);
            return movie.Characters;
        }

        public List<Movie> GetMovies()
        {
            return _movies;
        }

        public List<Movie> GetMovies(int franchiseId)
        {
            return _movies.Where(m => m.FranchiseId == franchiseId).ToList();
        }

        public void UpdateCharacter(Character character)
        {
            var updatedCharacter = GetCharacter(character.Id);
            var index = _characters.IndexOf(updatedCharacter);
            _characters[index] = character;
        }

        public void UpdateFranchise(Franchise franchise)
        {
            var updatedFranchise = GetFranchise(franchise.Id);
            var index = _franchises.IndexOf(updatedFranchise);
            _franchises[index] = franchise;
        }

        public void UpdateFranchiseMovies(int franchiseId, List<int> movieIds)
        {
            foreach (var id in movieIds)
            {
                var movie = _movies.FirstOrDefault(m => m.Id == id);
                movie.FranchiseId = franchiseId;
            } 
        }

        public void UpdateMovie(Movie movie)
        {
            var originalMovie = GetMovie(movie.Id);
            var index = _movies.IndexOf(originalMovie);
            _movies[index] = movie;
        }

        public void UpdateMovieCharacters(int movieId, List<int> characterIds)
        {
            var movie = GetMovie(movieId);

            if (movie.Characters == null)
            {
                movie.Characters = new List<Character>();
            }
            foreach (var  id in characterIds)
            {
                var character = _characters.FirstOrDefault(c => c.Id == id);
                movie.Characters.Add(character);
            }
        }
    }
}
