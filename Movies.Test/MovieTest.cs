using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Controllers;
using Movies.DTO;
using Movies.DTOs;
using Movies.Test.MockData;

namespace Movies.Test
{
    /// <summary>
    /// Test for movies
    /// </summary>
    public class MovieTest
    {
        private readonly MovieController _movieController;
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;
        public MovieTest()
        {
            _repository = new MockDataRepository();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MoviesProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _movieController = new MovieController(_repository, _mapper);
        }

        [Fact]
        public async void AddMovie()
        {
            // Arrange
            var actualCount = _repository.GetMovies().Count();
            var actualId = actualCount + 1;
            var newMovie = new MovieDTO
            {
                Picture = "TENET",
                Title = "TENET",
                Genre = "Sci-Fi",
                Director = "Christopher Nolan"
            };

            // Act
            var result = await _movieController.AddMovie(newMovie);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(actualId, successDTO.Id);
        }

        [Fact]
        public async void GetMovie()
        {
            // Arrange
            var movieId = 3;


            // Act
            var result = await _movieController.GetMovie(movieId);
            var okResult = result as OkObjectResult;
            var movieDTO = (MovieDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(movieDTO);
            Assert.Equal(3, movieDTO.Id);
        }

        [Fact]
        public async void GetMovies()
        {
            // Arrange
            var actualCount = _repository.GetMovies().Count();

            // Act
            var result = await _movieController.GetMovies();
            var okResult = result as OkObjectResult;
            var moviesDTO = (List<MovieDTO>)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(moviesDTO);
            Assert.Equal(actualCount, moviesDTO.Count);
        }
        [Fact]
        public async void UpdateMovie()
        {
            // Arrange
            var movieId = 2;
            var actualTitleName = "Afwaah";
            
            var getResult = await _movieController.GetMovie(movieId);
            var okGetResult = getResult as OkObjectResult;
            var movieDTO = (MovieDTO)okGetResult.Value;
            movieDTO.Title = actualTitleName;
            
            // Act
            var result = await _movieController.UpdateMovie(movieDTO);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;
            var updatedMovie = _repository.GetMovie(movieId);

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(movieId, successDTO.Id);
            Assert.Equal(actualTitleName, updatedMovie.Title);
        }

        [Fact]
        public async void DeleteMovie()
        {
            // Arrange
            var movieId = 3;

            // Act
            var result = await _movieController.DeleteMovie(movieId);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(3, successDTO.Id);
        }

        [Fact]
        public async void GetMovieCharactersTest()
        {
            // Arrange
            var movieId = 1;

            // Act
            var result = await _movieController.GetMovieCharacters(movieId);
            var okResult = result as OkObjectResult;
            var characters = (List<CharacterDTO>)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(characters);
            Assert.Equal(3, characters.Count);
        }
    }
}