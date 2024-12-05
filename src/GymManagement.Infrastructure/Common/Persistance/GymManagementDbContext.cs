using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.Infrastructure.Common.Persistance;

public class GymManagementDbContext : DbContext , IUnitOfWork
{
    public GymManagementDbContext(DbContextOptions options): base(options)
    {
    } 

    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Gym> Gyms { get; set; }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        base.OnModelCreating(modelBuilder);
    }
}
