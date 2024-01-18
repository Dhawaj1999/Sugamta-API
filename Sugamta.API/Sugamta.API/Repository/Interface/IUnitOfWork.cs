namespace Sugamta.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        IUserDetails UserDetails { get; }
        IUserLoginHistory UserLoginHistory { get; }
        IRole Role { get; }
        ILinkGenerate LinkGenerate { get; }
        ICountry Country { get; }
        IState State { get; }
        IAgency Agency { get; }
        IPrimaryClient PrimaryClient { get; }
        IPrimaryClientDetails PrimaryClientDetails { get; }
        ISecondaryClient SecondaryClient { get; }
        ISecondaryClientDetails SecondaryClientDetail { get; }
        void Save();
        
    }
}
