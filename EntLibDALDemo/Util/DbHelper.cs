using System.Data.Common;

namespace EntLibDALDemo.Util
{
    public class DbHelper
    {
        private DbFactory _dbFactory;
        public DbHelper(DbFactory dbFactory) 
        {
            _dbFactory = dbFactory;
        }

        public string TestPgQuery(string sqlText)
        {
            if ( string.IsNullOrEmpty(sqlText) )
            {
                throw new ArgumentNullException( nameof( sqlText ) );
            }

            string returnValue = string.Empty;

            try
            {
                var db = _dbFactory.PgSqlDb;

                using ( DbCommand dbCommand = db.GetSqlStringCommand( sqlText ) )
                {
                    returnValue = (string)db.ExecuteScalar( dbCommand );
                }
            }
            catch ( Exception ex)
            {

                throw ;
            }

            return returnValue;
        }
    }
}
