﻿using NTC_Lego.Shared;

namespace NTC_Lego.Server.Data
{
    public interface IAuthRepository
    {
        //Task<ServiceResponse<int>> Register(User user, string password, int startUnitId);
        //Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }

    public class AuthRepository
    {
    }
}
