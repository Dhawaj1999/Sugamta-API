using DataAccessLayer.DAL;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UserDbContext _db;
        public Repository(UserDbContext db)
        {
            _db = db;
        }
    }
}
