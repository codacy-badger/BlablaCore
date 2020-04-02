// ,-----.  ,--.        ,--.   ,--.         ,-----.                     
// |  |) /_ |  | ,--,--.|  |-. |  | ,--,--.'  .--./ ,---. ,--.--. ,---. 
// |  .-.  \|  |' ,-.  || .-. '|  |' ,-.  ||  |    | .-. ||  .--'| .-. :
// |  '--' /|  |\ '-'  || `-' ||  |\ '-'  |'  '--'\' '-' '|  |   \   --.
// `------' `--' `--`--' `---' `--' `--`--' `-----' `---' `--'    `----'
// 
// Copyright (C) 2020 - BlablaCore
// 
// BlablaCore is a free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
