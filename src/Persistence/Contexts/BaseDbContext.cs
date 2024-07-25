using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using S=Domain.Entities;
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
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<NotificationSettings> NotificationSettings { get; set; }
    public DbSet<Sms> Smses { get; set; }
    public DbSet<S.SmsSettings> SmsSettingies { get; set; }
    public DbSet<SmsDefaultTemplate> SmsDefaultTemplates { get; set; }
    public DbSet<SmsCustomTemplate> SmsCustomTemplates { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<VisitHistory> VisitHistories { get; set; }
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

