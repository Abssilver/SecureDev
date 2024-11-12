using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation;

public class CardStorageServiceDbContext : DbContext
{
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<CardEntity> Cards { get; set; }
    public virtual DbSet<AccountEntity> Accounts { get; set; }
    public virtual DbSet<AccountSessionEntity> AccountSessions { get; set; }

    public CardStorageServiceDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
        optionsBuilder.UseLazyLoadingProxies();
    }
}