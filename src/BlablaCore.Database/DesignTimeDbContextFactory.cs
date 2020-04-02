using System.IO;
using BlablaCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlablaCore.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlablaCoreContext>
    {
        private const string ConfigurationPath = "../../../configuration";

        public BlablaCoreContext CreateDbContext(string[] args)
        {
            var databaseConfiguration = new SqlConnectionConfiguration();
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory() + ConfigurationPath)
                .AddYamlFile("database.yml", false)
                .Build()
                .Bind(databaseConfiguration);
            var optionsBuilder = new DbContextOptionsBuilder<BlablaCoreContext>();
            optionsBuilder.UseNpgsql(databaseConfiguration.ConnectionString);
            return new BlablaCoreContext(optionsBuilder.Options);
        }
    }
}
