using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;

namespace TvMaze.Test.Repository
{
    public class ShowRepositoryTest
    {
        private Mock<IShowRepository<Show>> showRepoMock;

        [SetUp]
        public void Setup()
        {
            showRepoMock = new Mock<IShowRepository<Show>>();
        }

        [Test]
        public async Task Show_AddAsync()
        {
            // Arrange
            var showEntity = new Show(Guid.NewGuid().ToString());
            showRepoMock.Setup(s => s.AddAsync(showEntity)).ReturnsAsync(() => true);

            // Act
            var show = await showRepoMock.Object.AddAsync(showEntity);

            // Assert
            showRepoMock.Verify(m => m.AddAsync(It.IsAny<Show>()), Times.Once());
        }

        [Test]
        public async Task Show_AnyAsync()
        {
            // Arrange
            showRepoMock.Setup(s => s.AnyAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => true);

            // Act
            var show = await showRepoMock.Object.AnyAsync(a => a.Name == Guid.NewGuid().ToString());

            // Assert
            showRepoMock.Verify(m => m.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Show, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Show_GetAsync()
        {
            // Arrange
            var showEntity = new Show(Guid.NewGuid().ToString());
            showRepoMock.Setup(s => s.GetAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => showEntity);

            // Act
            var show = await showRepoMock.Object.GetAsync(a => a.Name == Guid.NewGuid().ToString());

            // Assert
            showRepoMock.Verify(m => m.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Show, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Show_CountAsync()
        {
            // Arrange
            var count = 1;
            showRepoMock.Setup(s => s.CountAsync()).ReturnsAsync(() => count);

            // Act
            var show = await showRepoMock.Object.CountAsync();

            // Assert
            showRepoMock.Verify(m => m.CountAsync(), Times.Once());
        }

        [Test]
        public async Task Show_PaginatedList()
        {
            // Arrange
            var queryDTO = new PaginatedQueryDTO(1, 1);
            var paginatedList = new List<Show>() { new Show(Guid.NewGuid().ToString()) };
            showRepoMock.Setup(s => s.PaginatedList(queryDTO)).ReturnsAsync(() => paginatedList);

            // Act
            var show = await showRepoMock.Object.PaginatedList(queryDTO);

            // Assert
            showRepoMock.Verify(m => m.PaginatedList(It.IsAny<PaginatedQueryDTO>()), Times.Once());
        }
    }
}
