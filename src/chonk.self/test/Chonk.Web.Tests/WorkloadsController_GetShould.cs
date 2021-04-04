using Chonk.Web.Controllers;
using Chonk.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Chonk.Services.Models;

namespace Chonk.Web.Tests
{
    [TestClass]
    public class WorkloadsController_GetShould
    {
        private readonly IManifestReader _manifestReader;
        private readonly WorkloadsController _controller;

        public WorkloadsController_GetShould()
        {
            _manifestReader = Mock.Of<IManifestReader>();
            _controller = new WorkloadsController(_manifestReader);
        }

        [TestMethod]
        public async Task Get_ReturnsEmptyWorkloadsArray()
        {
            // Arrange
            Mock.Get(_manifestReader)
                .Setup(x => x.Get())
                .Returns(Task.FromResult(default(Manifest)));

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(Workload[]));
        }

        [TestMethod]
        public async Task Get_ReturnsWorkloadsArray()
        {
            // Arrange
            // TODO: Consider adding builders for models
            Mock.Get(_manifestReader)
                .Setup(x => x.Get())
                .Returns(Task.FromResult(new Manifest
                {
                    Workloads = new Workload[]
                    {
                        new Workload { Image = "image1", Name = "name1", Description = "description1" },
                        new Workload { Image = "image2", Name = "name2", Description = "description2" }
                    }
                }));

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.AreEqual(2, result.Value.Length);
        }
    }
}
