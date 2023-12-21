using Models.Models;
using System.Linq.Expressions;

namespace Sugamta.API.Repository.Interface
{
    public interface IUserLoginHistory : IRepository<UserLoginHistory>
    {
        IEnumerable<UserLoginHistory> GetAllLoginHistory();
        IEnumerable<UserLoginHistory> GetLoginHistoryByEmail(Expression<Func<UserLoginHistory, bool>> filter);
        void CreateLoginHistory(UserLoginHistory entity);
        void UpdateLoginHistory(UserLoginHistory entity);
        void DeleteLoginHistory(UserLoginHistory entity);
    }
}
