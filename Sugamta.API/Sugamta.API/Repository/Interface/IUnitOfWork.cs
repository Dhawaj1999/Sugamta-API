namespace Sugamta.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        IUserDetails UserDetails { get; }
        IUserLoginHistory UserLoginHistory { get; }
        void Save();
        
    }
}
