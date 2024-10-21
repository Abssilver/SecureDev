using DAL.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation;

public class CardStorageServiceDbContext : DbContext
{
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<CardEntity> Cards { get; set; }

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