To demostrate how Enterprise LIbrary Data Access Block reads appsettings.json other than app.config to generate non MSSQL, Oracle Database instance.
This demo project includes MySql, PostgreSql
Following 2 steps:
1.    Register database provider name and type mapping
'''DbProviderFactories.RegisterFactory( "MySql.Data.MySqlClient", MySqlClientFactory.Instance );'''
2.    Invoke DbProvide and initialize database instance
'''var mySqlProvider = DbProviderFactories.GetFactory( _dbProviderSection.MySqlProvider );
    _mySqlDb = new GenericDatabase( _mySqlconnString, mySqlProvider );
'''

All codes are contained in Util/DatabaseFactory.cs
