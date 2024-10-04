using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class DatabaseContext : DbContext
{
    private static bool _init = false;
    public DatabaseContext()
       => Database.EnsureCreated();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        if (!_init)
        {
            Database.EnsureCreated();
            _init = true;
        }
    }

    public DbSet<Client> Clients { get; set; }
}
