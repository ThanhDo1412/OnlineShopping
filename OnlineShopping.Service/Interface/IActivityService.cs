using OnlineShopping.Database.Entity;
using System.Threading.Tasks;

namespace OnlineShopping.Service.Interface
{
    public interface IActivityService
    {
        Task CreateNewActivity(string activity, ActivityType type);
    }
}
