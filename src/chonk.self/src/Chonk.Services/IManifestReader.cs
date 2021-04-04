using System.Threading.Tasks;
using Chonk.Services.Models;

namespace Chonk.Services
{
    public interface IManifestReader
    {
         Task<Manifest> Get();
    }
}