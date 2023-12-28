using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Linq.Expressions;

namespace Sugamta.API.Repository
{
    public class RoleRepo : Repository<RoleRepo>, IRole
    {
        private readonly UserDbContext _db;

        public RoleRepo(UserDbContext db) : base(db)
        {
            _db = db;
        }

        public Role GetRoleById(Expression<Func<Role, bool>> filter)
        {
            return _db.Roles.FirstOrDefault(filter);
        }
    }
}
