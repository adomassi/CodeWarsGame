using System.Data.Entity;

namespace CodeWarsGame.Models
{
    public class CodeWarsGameContext : DbContext
    {
        public CodeWarsGameContext() : base("name=CodeWarsGameContext")
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<SolvedTask> SolvedTasks { get; set; }
    }
}
