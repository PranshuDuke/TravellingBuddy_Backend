using Microsoft.EntityFrameworkCore;
using TravellingBuddy.Application.DTOs.Auth;
using TravellingBuddy.Application.Interfaces;
using TravellingBuddy.Domain.Entities;
using TravellingBuddy.Infrastructure.Data;

namespace TravellingBuddy.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher _hasher;
    private readonly JwtService _jwt;

    public AuthService(AppDbContext context, PasswordHasher hasher, JwtService jwt)
    {
        _context = context;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var exists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email);

        if (exists)
            throw new Exception("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordHash = _hasher.Hash(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwt.GenerateToken(user.Id, user.Email);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null || !_hasher.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var token = _jwt.GenerateToken(user.Id, user.Email);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email
        };
    }
}