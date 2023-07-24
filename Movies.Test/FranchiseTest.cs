using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Controllers;
using Movies.DTO;
using Movies.DTOs;
using Movies.Test.MockData;

namespace Movies.Test
{
    /// <summary>
    /// Test for franchise
    /// </summary>
    public class FranchiseTest
    {
        private readonly FranchiseController _franchiseController;
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;
        public FranchiseTest()
        {
            _repository = new MockDataRepository();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MoviesProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _franchiseController = new FranchiseController(_repository, _mapper);
        }

        [Fact]
        public async void AddFranchise()
        {
            var actualCount = _repository.GetFranchises().Count();
            var actualId = actualCount + 1;
            // Arrange
            var newFranchise = new FranchiseDTO
            {
                Name = "Test Franchise",
                Description = "Description"
            };

            // Act
            var result = await _franchiseController.AddFranchise(newFranchise);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(actualId, successDTO.Id);
        }

        [Fact]
        public async void GetFranchise()
        {
            // Arrange
            var franchiseId = 3;

            // Act
            var result = await _franchiseController.GetFranchise(franchiseId);
            var okResult = result as OkObjectResult;
            var franchiseDTO = (FranchiseDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(franchiseDTO);
            Assert.Equal(3, franchiseDTO.Id);
        }
        [Fact]
        public async void GetFranchises()
        {
            // Arrange
            var actualCount = _repository.GetFranchises().Count();

            // Act
            var result = await _franchiseController.GetFranchises();
            var okResult = result as OkObjectResult;
            var franchisesDTO = (List<FranchiseDTO>)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(franchisesDTO);
            Assert.Equal(actualCount, franchisesDTO.Count);
        }
        [Fact]
        public async void UpdateFranchise()
        {
            // Arrange
            var franchise = _repository.GetFranchise(2);
            var franchiseDTO = new FranchiseDTO
            {
                Id = franchise.Id,
                Name = "Finn",
                Description = "New Description"
            };

            // Act
            var result = await _franchiseController.UpdateFranchise(franchiseDTO);
            var okResult = result as OkObjectResult;
            var successDTO = (SuccessDTO)okResult.Value;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(successDTO);
            Assert.Equal(2, successDTO.Id);
        }
        [Fact]
        public async void DeleteFranchise()
        {
            // Arrange

            var franchise = _repository.GetFranchise(3);

            // Act
            var result = await _franchiseController.DeleteFranchise(3);
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

