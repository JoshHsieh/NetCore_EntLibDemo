using EntLibDALDemo.Models;
using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Configuration;
using System.Data.Common;

namespace EntLibDALDemo.Util
{
    public class DbFactory
    {
        const string MySqlConnName = "MySqlConn";
        const string PgSqlConnName = "PgSqlConn";

        private readonly IConfiguration _configuration;

        private readonly DatabaseProviders _dbProviderSection;

        private readonly string? _mySqlconnString;

        private readonly string? _pgSqlconnString;

        public DbFactory( IConfiguration configuration, IOptions<DatabaseProviders> dbProviderSection )
        {
            DbProviderFactories.RegisterFactory( "MySql.Data.MySqlClient", MySqlClientFactory.Instance );
            DbProviderFactories.RegisterFactory( "Npgsql", NpgsqlFactory.Instance );

            _configuration = configuration;

            _dbProviderSection = dbProviderSection.Value;

            if ( string.IsNullOrEmpty( configuration.GetConnectionString( MySqlConnName ) ) != true )
            {
                _mySqlconnString = configuration.GetConnectionString( MySqlConnName );
            }

            if ( string.IsNullOrEmpty( configuration.GetConnectionString( PgSqlConnName ) ) != true )
            {
                _pgSqlconnString = configuration.GetConnectionString( PgSqlConnName );
            }
        }

        private Database? _mySqlDb { get; set; }

        public Database MySqlDb
        {
            get
            {
                if ( _mySqlDb == null )
                {
                    var mySqlProvider = DbProviderFactories.GetFactory( _dbProviderSection.MySqlProvider );

                    _mySqlDb = new GenericDatabase( _mySqlconnString, mySqlProvider );
                }

                return _mySqlDb;
            }
        }

        private Database? _pgSqlDb { get; set; }

        public Database PgSqlDb
        {
            get
            {
                if ( _pgSqlDb == null )
                {
                    var pgSqlProvider = DbProviderFactories.GetFactory( _dbProviderSection.PgSqlProvider );

                    _pgSqlDb = new GenericDatabase( _pgSqlconnString, pgSqlProvider );
                }

                return _pgSqlDb;
            }
        }
    }
}
