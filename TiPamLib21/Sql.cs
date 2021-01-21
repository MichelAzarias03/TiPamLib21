using Devart.Data.Oracle;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiPamLib21
{
    public class Sql
    {
        public ChoiceOptions Choice { get; set; }
        public string ConnectionString { get; set; }

        public enum ChoiceOptions
        {
            MySQL,
            Oracle,
            SqlServer,
            MsAccess
        }

        public enum DbType
        {
            Char,
            Varchar,
            Bit,
            Smallint,
            Int,
            Long,
            Float,
            Double,
            Binary,
            Date,
            DateTime,
            Time
        }

        public Sql(ChoiceOptions choice, string connectionString)
        {
            Choice = choice;
            ConnectionString = connectionString;
        }

        public void Execute(string commandText, IEnumerable<Sql.Paramater> paramaters)
        {
            using(IDbConnection con = GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = GetCommand()) 
                {
                    cmd.CommandText = commandText;
                    cmd.Connection = con;
                    foreach (var p in paramaters)
                        cmd.Parameters.Add(GetParamater(p));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IDbConnection GetConnection()
        {
            IDbConnection connection = null;
            switch (Choice)
            {
                case ChoiceOptions.MySQL:
                    connection = new MySqlConnection(ConnectionString);
                    break;
                case ChoiceOptions.Oracle:
                    connection = new OracleConnection(ConnectionString);
                    break;
                case ChoiceOptions.SqlServer:
                    connection = new SqlConnection(ConnectionString);
                    break;
                case ChoiceOptions.MsAccess:
                    connection = new OleDbConnection(ConnectionString);
                    break;
                default:
                    throw new ArgumentException(nameof(Choice));
            }
            return connection;
        }

        private IDbCommand GetCommand()
        {
            IDbCommand command = null;
            switch (Choice)
            {
                case ChoiceOptions.MySQL:
                    command = new MySqlCommand();
                    break;
                case ChoiceOptions.Oracle:
                    command = new OracleCommand();
                    break;
                case ChoiceOptions.SqlServer:
                    command = new SqlCommand();
                    break;
                case ChoiceOptions.MsAccess:
                    command = new OleDbCommand();
                    break;
                default:
                    throw new ArgumentException(nameof(Choice));
            }
            return command;
        }

        private IDataParameter GetParamater(Sql.Paramater parameter)
        {
            IDataParameter p = null;
            switch (Choice)
            {
                case ChoiceOptions.MySQL:
                    p = new MySqlParameter(parameter.Name, (MySqlDbType)GetDbType(parameter.Type));
                    p.Value = parameter.Value;
                    break;
                case ChoiceOptions.Oracle:
                    p = new OracleParameter(parameter.Name, (OracleDbType)GetDbType(parameter.Type));
                    p.Value = parameter.Value;
                    break;
                case ChoiceOptions.SqlServer:
                    p = new SqlParameter(parameter.Name, (SqlDbType)GetDbType(parameter.Type));
                    p.Value = parameter.Value;
                    break;
                case ChoiceOptions.MsAccess:
                    p = new OleDbParameter(parameter.Name, (OleDbType)GetDbType(parameter.Type));
                    p.Value = parameter.Value;
                    break;
                default:
                    throw new ArgumentException(nameof(Choice));
            }
            return p;
        }

        private object GetDbType(Sql.DbType type)
        {
            switch (Choice)
            {
                case ChoiceOptions.MySQL:
                    switch (type)
                    {
                        case DbType.Binary:
                            return MySqlDbType.Binary;
                        case DbType.Bit:
                            return MySqlDbType.Bit;
                        case DbType.Varchar:
                        case DbType.Char:
                            return MySqlDbType.VarChar;
                        case DbType.Date:
                            return MySqlDbType.Date;
                        case DbType.DateTime:
                            return MySqlDbType.DateTime;
                        case DbType.Double:
                            return MySqlDbType.Double;
                        case DbType.Float:
                            return MySqlDbType.Float;
                        case DbType.Int:
                            return MySqlDbType.Int32;
                        case DbType.Long:
                            return MySqlDbType.Int64;
                        case DbType.Smallint:
                            return MySqlDbType.Int16;
                        case DbType.Time:
                            return MySqlDbType.Time;
                        default:
                            return MySqlDbType.VarChar;
                    }
                case ChoiceOptions.Oracle:
                    switch (type)
                    {
                        case DbType.Binary:
                            return OracleDbType.Blob;
                        case DbType.Bit:
                            return OracleDbType.Boolean;
                        case DbType.Varchar:
                            return OracleDbType.VarChar;
                        case DbType.Char:
                            return OracleDbType.Char;
                        case DbType.Date:
                        case DbType.DateTime:
                        case DbType.Time:
                            return OracleDbType.Date;
                        case DbType.Double:
                            return OracleDbType.Double;
                        case DbType.Float:
                            return OracleDbType.Float;
                        case DbType.Int:
                            return OracleDbType.Integer;
                        case DbType.Long:
                            return OracleDbType.Int64;
                        case DbType.Smallint:
                            return OracleDbType.Int16;
                        default:
                            return OracleDbType.VarChar;
                    }
                case ChoiceOptions.SqlServer:
                    switch (type)
                    {
                        case DbType.Binary:
                            return SqlDbType.Binary;
                        case DbType.Bit:
                            return SqlDbType.Bit;
                        case DbType.Varchar:
                            return SqlDbType.VarChar;
                        case DbType.Char:
                            return SqlDbType.Char;
                        case DbType.Date:
                            return SqlDbType.Date;
                        case DbType.DateTime:
                            return SqlDbType.DateTime;
                        case DbType.Double:
                            return SqlDbType.Real;
                        case DbType.Float:
                            return SqlDbType.Float;
                        case DbType.Int:
                            return SqlDbType.Int;
                        case DbType.Long:
                            return SqlDbType.BigInt;
                        case DbType.Smallint:
                            return SqlDbType.SmallInt;
                        case DbType.Time:
                            return SqlDbType.Time;
                        default:
                            return SqlDbType.VarChar;
                    }
                case ChoiceOptions.MsAccess:
                    switch (type)
                    {
                        case DbType.Binary:
                            return OleDbType.Binary;
                        case DbType.Bit:
                            return OleDbType.Boolean;
                        case DbType.Varchar:
                            return OleDbType.VarChar;
                        case DbType.Char:
                            return OleDbType.Char;
                        case DbType.Date:
                        case DbType.Time:
                        case DbType.DateTime:
                            return OleDbType.Date;
                        case DbType.Double:
                            return OleDbType.Double;
                        case DbType.Float:
                            return OleDbType.Decimal;
                        case DbType.Int:
                            return OleDbType.Integer;
                        case DbType.Long:
                            return OleDbType.BigInt;
                        case DbType.Smallint:
                            return OleDbType.SmallInt;
                        default:
                            return OleDbType.VarChar;
                    }
                default:
                    throw new ArgumentException(nameof(Choice));
            }
        }

        public class Paramater
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public Sql.DbType Type { get; set; }

            public Paramater(string name, object value, Sql.DbType type)
            {
                Name = name;
                Value = value;
                Type = type;
            }
        }
    }
}
