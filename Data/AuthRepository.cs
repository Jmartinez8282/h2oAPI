using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using h2oAPI.Models;

namespace h2oAPI.Data;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public int Register(string username, string password)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = new UserLog ();

        newUser.Username = username;
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;

        _context.Add(newUser);
        _context.SaveChanges();

        return newUser.Id;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

    }
}
