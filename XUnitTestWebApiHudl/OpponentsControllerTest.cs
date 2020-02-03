using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiHudl.Contracts;
using WebApplicationHudl.Controllers;
using WebApplicationHudl.Model;
using Xunit;

namespace XUnitTestWebApiHudl
{
    public class OpponentsControllerTest
    {
        readonly IOpponentsService _service;
        readonly OpponentsController _controller;
        const int FAKE_ITEM_COUNT = 3;
        const int EXISTING_GAME_ID = 1234567;
        const int UNKNOWN_GAME_ID = 6106106;
        const int WRONG_GAME_ID = -1234567;

        public OpponentsControllerTest()
        {
            _service = new OpponentsServiceFake();
            _controller = new OpponentsController(_service);
        }

        #region Testing the Get method

        [Fact]
        public void Get_ReturnsOk()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_ReturnsAllOpponents()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<OpponentsItem>>(okResult.Value);
            Assert.Equal(FAKE_ITEM_COUNT, items.Count);
        }

        #endregion

        #region Testing the GetById method

        [Fact]
        public void GetById_UnknownGameId_ReturnsNotFound()
        {
            // Act
            var notFoundResult = _controller.Get(UNKNOWN_GAME_ID);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_NegativeGameId_ReturnsBadRequest()
        {
            // Act
            var badResponse = _controller.Get(WRONG_GAME_ID);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse.Result);
        }

        [Fact]
        public void GetById_ExistingGameId_ReturnsOk()
        {
            // Act
            var okResult = _controller.Get(EXISTING_GAME_ID);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGameId_ReturnsRightItem()
        {
            // Act
            var okResult = _controller.Get(EXISTING_GAME_ID).Result as OkObjectResult;

            // Assert
            Assert.IsType<OpponentsItem>(okResult.Value);
            Assert.Equal(EXISTING_GAME_ID, (okResult.Value as OpponentsItem).GameId);
        }

        #endregion

        #region Testing the Add method

        [Fact]
        public void Add_DateMissing_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };
            _controller.ModelState.AddModelError("Date", "Required");

            // Act
            var badResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_OpponentMissing_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };
            _controller.ModelState.AddModelError("Opponent", "Required");

            // Act
            var badResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_OpponentIdMissing_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };
            _controller.ModelState.AddModelError("OpponentId", "Required");

            // Act
            var badResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_IsHomeMissing_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                GameType = 2,
                Categories = new string[] { }
            };
            _controller.ModelState.AddModelError("IsHome", "Required");

            // Act
            var badResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_GameTypeMissing_ReturnsBadRequest()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                Categories = new string[] { }
            };
            _controller.ModelState.AddModelError("GameType", "Required");

            // Act
            var badResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_NullItem_ReturnsBadRequest()
        {
            // Act
            var badResponse = _controller.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Add_ValidItem_ReturnsCreatedResponse()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };

            // Act
            var createdResponse = _controller.Post(newItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidItem_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var newItem = new OpponentsItem()
            {
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };

            // Act
            var createdResponse = _controller.Post(newItem) as CreatedAtActionResult;
            var item = createdResponse.Value as OpponentsItem;

            // Assert
            Assert.IsType<OpponentsItem>(item);
            Assert.Equal(FAKE_ITEM_COUNT + 1, _service.GetOpponents().Count());
            Assert.Equal("TestOpponentW", item.Opponent);
        }

        #endregion

        #region Testing the Remove method

        [Fact]
        public void Remove_UnknownGameId_ReturnsNotFound()
        {
            // Act
            var badResponse = _controller.Remove(UNKNOWN_GAME_ID);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_NegativeGameId_ReturnsNotFound()
        {
            // Act
            var badResponse = _controller.Remove(WRONG_GAME_ID);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGameId_ReturnsOk()
        {
            // Act
            var okResponse = _controller.Remove(EXISTING_GAME_ID);

            // Assert
            Assert.IsType<NoContentResult>(okResponse);
        }

        [Fact]
        public void Remove_ExistingGameId_RemovesGame()
        {
            // Act
            _controller.Remove(EXISTING_GAME_ID);

            // Assert
            Assert.Null(_service.GetById(EXISTING_GAME_ID));
            Assert.Equal(FAKE_ITEM_COUNT - 1, _service.GetOpponents().Count());
        }

        #endregion

        #region Testing the Update method

        [Fact]
        public void Update_UnknownGame_ReturnsNotFound()
        {
            // Arrange
            var updateItem = new OpponentsItem()
            {
                GameId = UNKNOWN_GAME_ID,
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };

            // Act
            var badResponse = _controller.Put(updateItem);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Update_NullItem_ReturnsBadRequest()
        {
            // Act
            var badResponse = _controller.Put(null);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Update_ExistingGame_ReturnsOk()
        {
            // Arrange
            var updateItem = new OpponentsItem()
            {
                GameId = EXISTING_GAME_ID,
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };

            // Act
            var okResponse = _controller.Put(updateItem);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Update_ExistingGame_UpdateGame()
        {
            // Arrange
            var updateItem = new OpponentsItem()
            {
                GameId = EXISTING_GAME_ID,
                Date = DateTime.Now.AddDays(10),
                Opponent = "TestOpponentW",
                OpponentId = 6666666,
                IsHome = true,
                GameType = 2,
                Categories = new string[] { }
            };

            // Act
            _controller.Put(updateItem);

            // Assert
            Assert.NotNull(_service.GetById(EXISTING_GAME_ID));
            Assert.Equal(FAKE_ITEM_COUNT, _service.GetOpponents().Count());
        }

        #endregion
    }
}
