using Microsoft.EntityFrameworkCore;

namespace MowerManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Các DbSet sẽ thêm sau khi có model (ví dụ: Mowers, Telemetry, ...)
    // public DbSet<Mower> Mowers => Set<Mower>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Cấu hình entity sau
    }
}
