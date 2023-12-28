using Models.Models;
using System.Linq.Expressions;

namespace Sugamta.API.Repository.Interface
{
    public interface IRole
    {
        Role GetRoleById(Expression<Func<Role, bool>> filter);
    }
}
