using AutoMapper;
using Movies.DTO;
using Movies.Entities;

namespace Movies
{
    /// <summary>
    /// Mapper class for Entities and DTO
    /// </summary>
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<Character, CharacterDTO>();
            CreateMap<Franchise, FranchiseDTO>();

            CreateMap<MovieDTO, Movie>();
            CreateMap<CharacterDTO, Character>();
            CreateMap<FranchiseDTO, Franchise>();
        }
    }
}
