using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Services;
using Dot.Net.WebApi.Domain;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace P7CreateRestApi.Tests.Controllers
{
    public class BidListControllerTests
    {
        private readonly Mock<IBidService> _serviceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<BidListController>> _loggerMock = new();
        private readonly BidListController _controller;

        public BidListControllerTests()
        {
            _controller = new BidListController(_serviceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        #region GetBidLists
        [Fact]
        public async Task GetAllBids_ReturnsOkResult_WithListOfBidDtos()
        {
            // Arrange
            var bids = new List<BidList>
            {
                new BidList { BidListId = 1, Account = "Test1", BidType = "Type1", BidQuantity = 10 },
                new BidList { BidListId = 2, Account = "Test2", BidType = "Type2", BidQuantity = 20 }
            };

            var bidsDto = new List<BidListDto>
            {
                new BidListDto { BidListId = 1, Account = "Test1", BidType = "Type1", BidQuantity = 10 },
                new BidListDto { BidListId = 2, Account = "Test2", BidType = "Type2", BidQuantity = 20 }
            };

            _serviceMock.Setup(s => s.GetBidLists()).ReturnsAsync(bids);
            _mapperMock.Setup(m => m.Map<IEnumerable<BidListDto>>(bids)).Returns(bidsDto);

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<BidListDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetAllBids_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetBidLists()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
        #endregion

        # region GetById
        [Fact]
        public async Task GetBidById_ReturnsOkResult_WithBidDto()
        {
            // Arrange
            int bidId = 1;
            var bid = new BidList { BidListId = bidId, Account = "Test1", BidType = "Type1", BidQuantity = 10 };
            var bidDto = new BidListDto { BidListId = bidId, Account = "Test1", BidType = "Type1", BidQuantity = 10 };

            _serviceMock.Setup(s => s.GetBidList(bidId)).ReturnsAsync(bid);
            _mapperMock.Setup(m => m.Map<BidListDto>(bid)).Returns(bidDto);

            // Act
            var result = await _controller.GetBidById(bidId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BidListDto>(okResult.Value);
            Assert.Equal(bidId, returnValue.BidListId);
        }

        [Fact]
        public async Task GetBidById_ReturnsNotFound_WhenBidDoesNotExist()
        {
            // Arrange
            int bidId = 99;
            _serviceMock.Setup(s => s.GetBidList(bidId)).ReturnsAsync((BidList?)null);

            // Act
            var result = await _controller.GetBidById(bidId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBidById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int bidId = 1;
            _serviceMock.Setup(s => s.GetBidList(bidId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetBidById(bidId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
        #endregion

        #region AddBid
        [Fact]
        public async Task AddBid_ReturnsResult()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "Test15",
                BidType = "Type15",
                BidQuantity = 100
            };

            _serviceMock.Setup(service => service.AddBidList(It.IsAny<BidList>()))
                .ReturnsAsync(new BidList
                {
                    BidListId = 1,
                    Account = "Test15",
                    BidType = "Type15",
                    BidQuantity = 100
                });

            _mapperMock.Setup(mapper => mapper.Map<BidList>(It.IsAny<BidListDto>()))
                .Returns(new BidList());

            _mapperMock.Setup(mapper => mapper.Map<BidListDto>(It.IsAny<BidList>()))
                .Returns(bidDto);

            // Act
            var result = await _controller.AddBid(bidDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            var returnValue = result.Value as BidListDto;
            Assert.NotNull(returnValue);
            Assert.Equal(1, returnValue.BidListId);
        }

          [Fact]
        public async Task AddBid_WhenArgumentExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "Test15",
                BidType = "Type15",
                BidQuantity = 100
            };

            _mapperMock.Setup(mapper => mapper.Map<BidList>(It.IsAny<BidListDto>()))
                .Returns(new BidList());

            _serviceMock.Setup(service => service.AddBidList(It.IsAny<BidList>()))
                .ThrowsAsync(new ArgumentException("Invalid bid data"));

            // Act
            var result = await _controller.AddBid(bidDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("Invalid bid data", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task AddBid_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "Test15",
                BidType = "Type15",
                BidQuantity = 100
            };

            _mapperMock.Setup(mapper => mapper.Map<BidList>(It.IsAny<BidListDto>()))
                .Returns(new BidList());

            _serviceMock.Setup(service => service.AddBidList(It.IsAny<BidList>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.AddBid(bidDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Contains("Error while adding bid", objectResult.Value.ToString());
        }

        #endregion
        #region Update
        [Fact]
        public async Task UpdateBid_WhenUpdateIsSuccessful_ReturnsOk()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "TestUpdate",
                BidType = "TypeUpdate",
                BidQuantity = 200
            };

            var bidEntity = new BidList();
            var updatedEntity = new BidList();

            _mapperMock.Setup(mapper => mapper.Map<BidList>(bidDto)).Returns(bidEntity);
            _serviceMock.Setup(service => service.UpdateBidList(1, bidEntity)).ReturnsAsync(updatedEntity);
            _mapperMock.Setup(mapper => mapper.Map<BidListDto>(updatedEntity)).Returns(bidDto);

            // Act
            var result = await _controller.UpdateBid(1, bidDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateBid_WhenArgumentExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "TestUpdate",
                BidType = "TypeUpdate",
                BidQuantity = 200
            };

            _mapperMock.Setup(mapper => mapper.Map<BidList>(bidDto)).Returns(new BidList());
            _serviceMock.Setup(service => service.UpdateBidList(1, It.IsAny<BidList>()))
                .ThrowsAsync(new ArgumentException("Invalid update data"));

            // Act
            var result = await _controller.UpdateBid(1, bidDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("Invalid update data", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateBid_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var bidDto = new BidListDto
            {
                BidListId = 1,
                Account = "TestUpdate",
                BidType = "TypeUpdate",
                BidQuantity = 200
            };

            _mapperMock.Setup(mapper => mapper.Map<BidList>(bidDto)).Returns(new BidList());
            _serviceMock.Setup(service => service.UpdateBidList(1, It.IsAny<BidList>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.UpdateBid(1, bidDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Contains("Error while updating bid", objectResult.Value.ToString());
        }

        #endregion
        #region Delete
        [Fact]
        public async Task DeleteBid_WhenDeletionIsSuccessful_ReturnsNoContent()
        {
            // Arrange
            _serviceMock.Setup(service => service.DeleteBidList(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBid_WhenBidNotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(service => service.DeleteBidList(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Contains("Bid not found for deletion", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteBid_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _serviceMock.Setup(service => service.DeleteBidList(1)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Contains("Error while deleting bid", objectResult.Value.ToString());
        }

        #endregion

    }
}
