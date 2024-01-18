using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Linq.Expressions;

namespace Sugamta.API.Repository
{
    public class LinkGenerateRepo: Repository<LinkGeneration>, ILinkGenerate
    {
        private UserDbContext _db;
        public LinkGenerateRepo(UserDbContext db) : base(db)
        {
            _db = db;
        }

        public void GenerateLink(LinkGeneration entity)
        {
            _db.Add(entity);
        }

        public LinkGeneration GetGeneratedLinkByLink(Expression<Func<LinkGeneration, bool>> filter)
        {
            IQueryable<LinkGeneration> query = _db.GeneratedRegisterLinks.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<LinkGeneration> GetLinksWhere(Expression<Func<LinkGeneration, bool>> predicate)
        {
            IQueryable<LinkGeneration> query = _db.GeneratedRegisterLinks.Where(predicate);
            return query;
        }
    }
}

