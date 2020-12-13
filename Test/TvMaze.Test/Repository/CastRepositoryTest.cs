using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;

namespace TvMaze.Test.Repository
{
    public class CastRepositoryTest
    {
        private Mock<ICastRepository<Cast>> castRepoMock;

        [SetUp]
        public void Setup()
        {
            castRepoMock = new Mock<ICastRepository<Cast>>();
        }

        [Test]
        public async Task Cast_AddAsync()
        {
            // Arrange
            var castEntity = new Cast(Guid.NewGuid().ToString(), DateTime.UtcNow, 1);
            castRepoMock.Setup(s => s.AddAsync(castEntity)).ReturnsAsync(() => true);

            // Act
            var cast = await castRepoMock.Object.AddAsync(castEntity);

            // Assert
            castRepoMock.Verify(m => m.AddAsync(It.IsAny<Cast>()), Times.Once());
        }

        [Test]
        public async Task Cast_AnyAsync()
        {
            // Arrange
            castRepoMock.Setup(s => s.AnyAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => true);

            // Act
            var cast = await castRepoMock.Object.AnyAsync(a => a.Name == Guid.NewGuid().ToString());

            // Assert
            castRepoMock.Verify(m => m.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Cast, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Cast_GetAsync()
        {
            // Arrange
            var castEntity = new Cast(Guid.NewGuid().ToString(), DateTime.UtcNow, 1);
            castRepoMock.Setup(s => s.GetAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => castEntity);

            // Act
            var cast = await castRepoMock.Object.GetAsync(a => a.Name == Guid.NewGuid().ToString());

            // Assert
            castRepoMock.Verify(m => m.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Cast, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Cast_CountAsync()
        {
            // Arrange
            var count = 1;
            castRepoMock.Setup(s => s.CountAsync()).ReturnsAsync(() => count);

            // Act
            var cast = await castRepoMock.Object.CountAsync();

            // Assert
            castRepoMock.Verify(m => m.CountAsync(), Times.Once());
        }
    }
}
