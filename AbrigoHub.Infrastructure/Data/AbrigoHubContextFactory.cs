using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Oracle.EntityFrameworkCore.Infrastructure;

namespace AbrigoHub.Infrastructure.Data
{
    public class AbrigoHubContextFactory : IDesignTimeDbContextFactory<AbrigoHubContext>
    {
        public AbrigoHubContext CreateDbContext(string[] args)
        {
            // Constrói a configuração para obter a string de conexão
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<AbrigoHubContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Aqui você deve usar o provedor de banco de dados correto (Oracle no seu caso)
            builder.UseOracle(connectionString, oracleOptions =>
                oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion21));

            return new AbrigoHubContext(builder.Options);
        }
    }
} 