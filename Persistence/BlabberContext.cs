using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class BlabberContext : IdentityDbContext<AuthUser>
{
    public BlabberContext(DbContextOptions<BlabberContext> options) : base(options) {}
}