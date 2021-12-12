using BettingApp.BLL.Dto;
using System.Threading.Tasks;

namespace BettingApp.BLL
{
    public interface IFixtureService
    {
        Task<string> CreateFixture(CreateFixtureModel model);
    }
}