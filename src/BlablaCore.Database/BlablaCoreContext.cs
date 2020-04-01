using Microsoft.EntityFrameworkCore;
using BlablaCore.BlablaCore.Database.Entity;

namespace BlablaCore.Database
{
    public class BlablaCoreContext : DbContext
    {
        public BlablaCoreContext(DbContextOptions? options) : base(options)
        {
        }

        public virtual DbSet<Character> Character { get; set; }
    }
}
