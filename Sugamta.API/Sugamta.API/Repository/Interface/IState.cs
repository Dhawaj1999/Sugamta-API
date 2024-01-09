using Models.Models;
using System.Linq.Expressions;

namespace Sugamta.API.Repository.Interface
{
    public interface IState
    {
        //State GetStateById(Expression<Func<State, bool>> filter);
        //Task<List<State>> GetStatesAsync();
        List<State> GetStates();
        State GetStateById(int StateId);
    }
}
