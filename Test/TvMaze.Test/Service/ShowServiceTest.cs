using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Core.DTO;
using TvMaze.Core.Interfaces.Services;

namespace TvMaze.Test.Service
{
    public class ShowServiceTest
    {
        private Mock<IShowService> showServiceMock;

        [SetUp]
        public void Setup()
        {
            showServiceMock = new Mock<IShowService>();
        }

        [Test]
        public async Task Show_AddAsync()
        {
            // Arrange
            var queryDTO = new PaginatedQueryDTO(1, 1);
            var paginatedList = new List<ShowDTO>();
            showServiceMock.Setup(s => s.PaginatedList(queryDTO)).ReturnsAsync(() => paginatedList);

            // Act
            var show = await showServiceMock.Object.PaginatedList(queryDTO);

            // Assert
            showServiceMock.Verify(m => m.PaginatedList(It.IsAny<PaginatedQueryDTO>()), Times.Once());
        }
    }
}
