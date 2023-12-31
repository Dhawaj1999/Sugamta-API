﻿namespace Sugamta.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        IUserDetails UserDetails { get; }
        IUserLoginHistory UserLoginHistory { get; }
        IRole Role { get; }
        ICountry Country { get; }
        IState State { get; }
        void Save();
        
    }
}
