namespace Sugamta.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        IUserLoginHistory UserLoginHistory { get; }
        void Save();
        
    }
}
