using CityGuideAPI.Models;
using System.Threading.Tasks;

namespace CityGuideAPI.DataAccess
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string paassword);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
