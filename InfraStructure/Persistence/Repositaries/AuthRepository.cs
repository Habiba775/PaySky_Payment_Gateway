using Application.Interfaces.Repos;
using Domain.Entities.Custom_Entities;
using InfraStructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<APPUser> _userManager;
    private readonly AppDbContext _context;

    public AuthRepository(UserManager<APPUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<APPUser?> GetUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var isValid = await _userManager.CheckPasswordAsync(user, password);
        return isValid ? user : null;
    }

    public async Task<APPUser> CreateUserAsync(APPUser user)
    {
        var result = await _userManager.CreateAsync(user, user.PasswordHash!); 
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return user;
    }

    
    public async Task<APPUser?> GetByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task<IEnumerable<APPUser>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task InsertAsync(APPUser entity)
        => await _context.Users.AddAsync(entity);

    public Task UpdateAsync(APPUser entity)
    {
        _context.Users.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
            _context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
