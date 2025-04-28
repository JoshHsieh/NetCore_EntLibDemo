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
                    var connSection = new ConnectionStringsSection();

                    var connSetting = new ConnectionStringSettings();

                    connSetting.Name = MySqlConnName;
                    connSetting.ConnectionString = _mySqlconnString;
                    connSetting.ProviderName = _dbProviderSection.MySqlProvider;

                    connSection.ConnectionStrings.Add( connSetting );


                    var configSource = new DictionaryConfigurationSource();

                    configSource.Add( "dbConn", connSection );

                    DatabaseProviderFactory factory = new DatabaseProviderFactory( configSource );

                    _mySqlDb = factory.Create( MySqlConnName );
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
                    var dbSettings = new DatabaseSettings();

                    var connSection = new ConnectionStringsSection();

                    var connSetting = new ConnectionStringSettings();

                    var dbProvider = new DbProviderMapping("Npgsql", "Npgsql.NpgsqlFactory, Npgsql");

                    connSetting.Name = PgSqlConnName;
                    connSetting.ConnectionString = _pgSqlconnString;
                    connSetting.ProviderName = _dbProviderSection.PgSqlProvider;

                    connSection.ConnectionStrings.Add( connSetting );
                    dbProvider.DatabaseTypeName = "Npgsql.NpgsqlFactory, Npgsql";
                    dbSettings.ProviderMappings.Add( dbProvider );
                    dbSettings.DefaultDatabase = PgSqlConnName;

                    var configSource = new DictionaryConfigurationSource();

                    configSource.Add( DatabaseSettings.SectionName, dbSettings );
                    configSource.Add( "connectionStrings", connSection );

                    var pgSqlProvider = DbProviderFactories.GetFactory( "Npgsql" );

                    //DatabaseProviderFactory factory = new DatabaseProviderFactory( configSource.GetSection );
                    //_pgSqlDb = factory.Create( PgSqlConnName );
                    _pgSqlDb = new GenericDatabase( _pgSqlconnString, pgSqlProvider );


                }

                return _pgSqlDb;
            }
        }
    }
}
