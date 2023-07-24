using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Controllers;
using Movies.DTO;
using Movies.DTOs;
using Movies.Test.MockData;

namespace Movies.Test
{
    /// <summary>
    /// Test for Characters
    /// </summary>
    public class CharacterTest
    {
        private readonly CharactersController _characterController;
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;
        public CharacterTest()
        {
            _repository = new MockDataRepository();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MoviesProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _characterController = new CharactersController(_repository, _mapper);
        }

        [Fact]
        public async void AddCharacter()
        {
            var actualCount = _repository.GetCharacters().Count();
            var actualId = actualCount + 1;
            // Arrange
            var newCharacter = new CharacterDTO
            {
                FullName = "Ranbeer Kapoor",
                Alias = "Ranbeer",
                Gender = "Male",
                Picture = "Picture"
            };

            // Act
            var result = await _characterController.AddCharacter(newCharacter);
            var okResult = result as OkObjectResult;
            var characterDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(characterDTO);
            Assert.Equal(actualId, characterDTO.Id);
        }

        [Fact]
        public async void GetCharacter()
        {


            // Arrange
            var characterId = 3;

            // Act
            var result = await _characterController.GetCharacter(characterId);
            var okResult = result as OkObjectResult;
            var characterDTO = (CharacterDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(characterDTO);
            Assert.Equal(3, characterDTO.Id);
        }
        [Fact]
        public async void Getcharacters()
        {
            // Arrange
            var actualCount = _repository.GetCharacters().Count();

            // Act
            var result = await _characterController.GetCharacters();
            var okResult = result as OkObjectResult;
            var charactersDTO = (List<CharacterDTO>)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(charactersDTO);
            Assert.Equal(actualCount, charactersDTO.Count);
        }
        [Fact]
        public async void UpdateCharacter()
        {
            // Arrange
            var character = _repository.GetCharacter(1);
            var characterDTO = new CharacterDTO
            {
                Id = character.Id,
                FullName = "Amitabh Bachhan",
                Alias = "Amit",
                Gender = "Male",
                Picture = "Picture"
            };

            // Act
            var result = await _characterController.UpdateCharacter(characterDTO);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(1, successDTO.Id);
        }
        [Fact]
        public async void DeleteCharacter()
        {
            // Arrange

            var character = _repository.GetCharacter(3);

            // Act
            var result = await _characterController.DeleteCharacter(3);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(3, successDTO.Id);
        }

    }
}

