using BlablaCore.BlablaCore.Database.Entity;
using System;
using System.Data.Entity;

namespace BlablaCore.Database
{
    public class BlablaCoreContext : DbContext
    {
        public virtual DbSet<Character> Character { get; set; }
    }
}
