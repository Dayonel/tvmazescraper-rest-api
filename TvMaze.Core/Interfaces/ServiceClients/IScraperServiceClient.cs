using System.Threading.Tasks;

namespace TvMaze.Core.Interfaces.ServiceClients
{
    public interface IScraperServiceClient
    {
        Task ExecuteScraping();
    }
}
