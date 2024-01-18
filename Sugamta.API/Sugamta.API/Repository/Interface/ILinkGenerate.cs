using Models.Models;
using System.Linq.Expressions;

namespace Sugamta.API.Repository.Interface
{
    public interface ILinkGenerate: IRepository<LinkGeneration>
    {
        LinkGeneration GetGeneratedLinkByLink(Expression<Func<LinkGeneration, bool>> filter);
        void GenerateLink(LinkGeneration entity);
        IEnumerable<LinkGeneration> GetLinksWhere(Expression<Func<LinkGeneration, bool>> predicate);
    }
}
