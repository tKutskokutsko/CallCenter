using CallCenterApi.Infrastructure.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallCenterApi.Infrastructure.DB;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<AgentInfo> AgentInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=callcenter; Username=postgres; Password=070593");
    }
}