using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Linq.Expressions;

namespace Sugamta.API.Repository
{
    public class UserLoginHistoryRepo : Repository<UserLoginHistory>, IUserLoginHistory
    {
        private UserDbContext _db;

        public UserLoginHistoryRepo(UserDbContext db) : base(db)
        {
            _db = db;
        }

        public void CreateLoginHistory(UserLoginHistory entity)
        {
            _db.Add(entity);
        }

        public void DeleteLoginHistory(UserLoginHistory entity)
        {
            _db.Remove(entity);
        }

        public IEnumerable<UserLoginHistory> GetAllLoginHistory()
        {
            IEnumerable<UserLoginHistory> list = _db.UserLoginHistory.ToList();
            return list;
        }

        public IEnumerable<UserLoginHistory> GetLoginHistoryByEmail(Expression<Func<UserLoginHistory, bool>> filter)
        {
            IQueryable<UserLoginHistory> query = _db.UserLoginHistory.Where(filter);
            return query.ToList();
        }

        public void UpdateLoginHistory(UserLoginHistory entity)
        {
            _db.Update(entity);
        }
    }
}
