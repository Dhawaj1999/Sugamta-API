namespace Sugamta.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        void Save();
        
    }
}
