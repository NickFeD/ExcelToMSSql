using DB.Entites;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class DatabaseContext: DbContext
{
    private static bool _init = false;
    public DatabaseContext()
       => Database.EnsureCreated();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) => Database.EnsureCreated();

    public DbSet<Client> Clients { get; set; }
}
