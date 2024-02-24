using Audium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace Audium.Persistance;

public class AudiumDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackForDate> TrackForDate { get; set; }
    public DbSet<UserGuess> UsersGuess { get; set; }

    private IConfiguration _configuration { get; set; }

    public AudiumDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _configuration.GetConnectionString("Audium");
        options.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>().UseTpcMappingStrategy();
    }
}