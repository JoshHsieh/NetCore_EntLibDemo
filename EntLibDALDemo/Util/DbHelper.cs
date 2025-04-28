using System.Data;
using System.Data.Common;

namespace EntLibDALDemo.Util
{
    public class DbHelper
    {
        private DbFactory _dbFactory;
        public DbHelper( DbFactory dbFactory )
        {
            _dbFactory = dbFactory;
        }

        public List<string> TestPgQueryCmd( string sqlText )
        {
            if ( string.IsNullOrEmpty( sqlText ) )
            {
                throw new ArgumentNullException( nameof( sqlText ) );
            }

            var l = new List<string>();

            try
            {
                var db = _dbFactory.PgSqlDb;

                using ( DbCommand dbCommand = db.GetSqlStringCommand( sqlText ) )
                using ( IDataReader rdr = db.ExecuteReader( dbCommand ) )
                {
                    while ( rdr.Read() )
                    {
                        if( rdr["agent_name"] != null )
                        {
                            l.Add( rdr["agent_name"].ToString() );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {

                throw;
            }

            return l;
        }

        public List<string> TestMySqlQueryCmd( string sqlText )
        {
            if ( string.IsNullOrEmpty( sqlText ) )
            {
                throw new ArgumentNullException( nameof( sqlText ) );
            }

            var l = new List<string>();

            try
            {
                var db = _dbFactory.MySqlDb;

                using ( DbCommand dbCommand = db.GetSqlStringCommand( sqlText ) )
                using ( IDataReader rdr = db.ExecuteReader( dbCommand ) )
                {
                    while ( rdr.Read() )
                    {
                        if( rdr["ROLE_NAME"] != null )
                        {
                            l.Add( rdr["ROLE_NAME"].ToString() );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {

                throw;
            }

            return l;
        }
    }
}
