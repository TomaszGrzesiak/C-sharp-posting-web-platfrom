using Domain.DataAccessContracts;
using Domain.ModelClasses;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class UserSqliteDAO : IUserDao
{
    private readonly UserContext context;
    
    public UserSqliteDAO(UserContext context)
    {
        this.context = context;
    }
    public async Task<bool> TryLogin(User user)
    {
        User? userLogin = null;
        userLogin = context.Users.FirstOrDefault(p =>
            p.UserName.Equals(user.UserName) && p.Password.Equals(user.Password));

        return userLogin != null; // returns true if there were a user with matching username and password.
    }

    public async Task<User> AddUserAsync(User user)
    {
        int largestId = 0;
        if (context.Users.Count() != 0)
        {
            largestId = context.Users.Max(t => t.Id);
        }

        int nextId = largestId + 1;
        user.Id = nextId;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(string userName)
    {
        var firstOrDefault = context.Users.FirstOrDefault(p => p.UserName.Equals(userName));
        if (firstOrDefault != null)
        {
            context.Users.Remove(firstOrDefault);
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        User updateUser = context.Users.FirstOrDefault(t => t.Id == user.Id)!;
        if (updateUser == null) throw new Exception("The user does not exist.");
        else
        {
            context.Users.Remove(updateUser);
            context.Users.Add(user);

        }
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<User?> GetUserByUsername(string userName)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        if (user != null) return user;
        return null;
    }
}