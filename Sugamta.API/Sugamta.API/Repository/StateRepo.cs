using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Linq.Expressions;

namespace Sugamta.API.Repository
{
    public class StateRepo:IState
    {
        private readonly UserDbContext _context;

        public StateRepo(UserDbContext context)
        {
            _context=context;
        }

        public State GetStateById(int StateId)
        {
            return _context.States.FirstOrDefault(x => x.StateId==StateId);
        }

        public List<State> GetStates()
        {
            return _context.States.ToList();
        }
       

        /*public State GetStateById(Expression<Func<State, bool>> filter)
        {
            return _context.States.FirstOrDefault(filter);
        }*/
    }
}
