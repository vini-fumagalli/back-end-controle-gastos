using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context;

public class MyContext : DbContext
{
    public DbSet<UsuarioEntity> USUARIO { get; set; } = default!;
    public DbSet<GastoEntity> GASTO { get; set; } = default!;
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioEntity>()
        .HasKey(u => u.Usuario);

        modelBuilder.Entity<GastoEntity>()
        .HasKey(g => new { g.Usuario, g.Tipo });
    }
}