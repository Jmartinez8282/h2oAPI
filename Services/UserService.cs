using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using h2oAPI.Data;

namespace h2oAPI.Services;

public class UserService : IUserService
{
    private readonly IAuthRepository _authRepository;

    public UserService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public int Register(string username, string password)
    {
        return _authRepository.Register(username, password);
    }
}
