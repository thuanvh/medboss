namespace Nammedia.Medboss
{
    using Nammedia.Medboss.Log;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Odbc;

    public class DBDriver
    {
        private OdbcCommand _command;
        private OdbcConnection _connect;

        public DBDriver()
        {
            try
            {
                string connectString = ConfigurationManager.AppSettings["ConnectionString"];
                this._connect = new OdbcConnection(connectString);
                this._command = new OdbcCommand();
                this._command.Connection = this._connect;
                this._connect.Open();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
        }

        public IDbDataParameter GetParameter(string name, object value)
        {
            return new OdbcParameter(name, value);
        }

        public IDbCommand Command
        {
            get
            {
                if (this._command.Connection.State == ConnectionState.Closed)
                {
                    this._connect.Open();
                    this._command.Connection = this._connect;
                }
                return this._command;
            }
            set
            {
                this._command = (OdbcCommand) value;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                if (this._connect.State == ConnectionState.Closed)
                {
                    this._connect.Open();
                }
                return this._connect;
            }
            set
            {
                this._connect = (OdbcConnection) value;
            }
        }
    }
}
