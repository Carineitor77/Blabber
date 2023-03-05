using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TelegramContext : IdentityDbContext<User>
{
    public TelegramContext(DbContextOptions<TelegramContext> options) : base(options) {}
}