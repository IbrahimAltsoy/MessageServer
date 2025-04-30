using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using S=Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    private static readonly Type[] HardDeleteEntities = { typeof(CustomerPhoto) };
    private static readonly MethodInfo SetQueryFilterMethod =
        typeof(BaseDbContext).GetMethod(nameof(SetQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)!;

    #region Security DbSets
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public DbSet<Notification> Nottifications { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public DbSet<PhoneVerificationToken> PhoneVerificationTokens { get; set; }
    public DbSet<CustomerPhoto> CustomerPhotos { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<NotificationSettings> NotificationSettings { get; set; }
    public DbSet<Sms> Smses { get; set; }
    public DbSet<S.SmsSettings> SmsSettingies { get; set; }
    public DbSet<SmsDefaultTemplate> SmsDefaultTemplates { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<MembershipPackage> MembershipPackages { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    #endregion
   

    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
        // Database.EnsureCreated(); // Gerekiyorsa açın (sadece development'ta)
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        ApplyGlobalQueryFilters(modelBuilder);
        ConfigureCustomRelationships(modelBuilder);
    }

    private static void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (ShouldApplyQueryFilter(entityType))
            {
                SetQueryFilterMethod
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(null, new object[] { modelBuilder });
            }
        }
    }

    private static bool ShouldApplyQueryFilter(IMutableEntityType entityType) =>
        typeof(Entity<Guid>).IsAssignableFrom(entityType.ClrType) &&
        !HardDeleteEntities.Contains(entityType.ClrType);

    private static void SetQueryFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : Entity<Guid> =>
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.DeletedDate == null);

    private static void ConfigureCustomRelationships(ModelBuilder modelBuilder)
    {
        // Örnek ilişki konfigürasyonları
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Feedbacks)
            .WithOne(f => f.Customer)
            .OnDelete(DeleteBehavior.Cascade);
    }

    #region Transaction Helper
    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        if (Database.CurrentTransaction != null)
        {
            await action();
            return;
        }

        await using var transaction = await Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    #endregion
}