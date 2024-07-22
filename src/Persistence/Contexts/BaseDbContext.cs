using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public IConfiguration Configuration { get; set; }

    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }   
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public DbSet<Notification> Nottifications { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public DbSet<PhoneVerificationToken> PhoneVerificationTokens { get; set; }
    #region
    

    #endregion

    public BaseDbContext(DbContextOptions<BaseDbContext> dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {            
        }




        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    private static void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : Entity<Guid>
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.DeletedDate.HasValue);
    }


}

