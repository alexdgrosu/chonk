using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Chonk.Services.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chonk.Services.Tests
{
    [TestClass]
    public class StreamJsonExtensions_FromJsonToAsyncShould
    {

        class NestedPoco
        {
            public string AString { get; set; }
            public int ANumber { get; set; }
            public IEnumerable<FlatPoco> SomePocos { get; set; }
        }

        class FlatPoco
        {
            public string AString { get; set; }
        }

        [TestMethod]
        public async Task FromJsonToAsync_StreamWithDefaultOptions_ReturnsFlatPoco()
        {
            // Arrange
            const string json = @"{""astring"": ""string""}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            // Act
            var flat = await stream.FromJsonToAsync<FlatPoco>();

            // Assert
            Assert.AreEqual("string", flat.AString);
        }

        [TestMethod]
        public async Task FromJsonToAsync_StreamWithDefaultOptions_ReturnsNestedPoco()
        {
            // Arrange
            const string json = @"{
                ""astring"": ""string"",
                ""anumber"": 1,
                ""somepocos"": [
                    {
                        ""astring"": ""one""
                    },
                    {
                        ""astring"": ""two""
                    }
                ]
            }";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            // Act
            var nested = await stream.FromJsonToAsync<NestedPoco>();

            // Assert
            Assert.IsNotNull(nested.SomePocos.SingleOrDefault(x => x.AString == "one"));
            Assert.IsNotNull(nested.SomePocos.SingleOrDefault(x => x.AString == "two"));
        }

        [TestMethod]
        public async Task FromJsonToAsync_StreamWithCaseSensitiveOptions_ReturnsPocoWithMissingValues()
        {
            // Arrange
            const string json = @"{""astring"": ""string""}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            // Act
            var flat = await stream.FromJsonToAsync<FlatPoco>(options);

            // Assert
            Assert.IsNull(flat.AString);
        }
    }
}
