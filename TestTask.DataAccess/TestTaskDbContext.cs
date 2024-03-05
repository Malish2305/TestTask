using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DataAccess;

public class TestTaskDbContext : DbContext
{
    public const string ConnectionStringName = "TestTaskDbConnectionString";
    
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<TreeNode> TreeNodes { get; set; }
    public DbSet<JournalLog> JournalLogs { get; set; }
}