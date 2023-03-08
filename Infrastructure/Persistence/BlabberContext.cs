using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BlabberContext : IdentityDbContext<User>
{
    public BlabberContext(DbContextOptions<BlabberContext> options) : base(options) {}
}