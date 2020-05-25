using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace WebApiSample.Models
{
    
    /// <summary>
    ///  SQLite 操作类
    /// </summary>
    public class SQLiteHelper : ISQLiteHelper
    {

         // 数据库连接定义
        private SQLiteConnection dbConnection;

        // SQL命令定义
        private SQLiteCommand dbCommand;

        // 数据读取定义
        private SQLiteDataReader dataReader;

        public SQLiteHelper(string connectionString)
        {
            try
            {
                dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
            }
            catch(Exception e)
            {
                Log(e.ToString());
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            return dataReader;
        }

        public void CloseConnection()
        {
            // 销毁Command
            if (dbCommand != null) dbCommand.Cancel();
            dbCommand = null;
            
            // 销毁Reader
            if (dataReader != null) dataReader.Close();
            dataReader = null;

            // 销毁Connection
            if (dbConnection != null) dbConnection.Close();
            dbConnection = null;
        }

        public SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queruString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for( int i = 1; i< colNames.Length; i++)
            {
                queruString += ", " + colNames[i] + " " + colTypes[i];
            }
            queruString += " )";
            return ExecuteQuery(queruString);
        }

        public SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length");
            }
            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "AND " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }

        public SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            if(colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length");
            }
            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for(int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }

        public SQLiteDataReader InsertValues(string tableName, string[] values)
        {
            int fieldCount = ReadFullTable(tableName).FieldCount;
            if(values.Length != fieldCount)
            {
                throw new SQLiteException("values.Length!= fieldCount");
            }

            string queryString = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";

            for(int i = 1; i< values.Length; i++)
            {
                queryString = ", " + "'" + values[i] + "'";
            }
            queryString += " )";

            return ExecuteQuery(queryString);
        }

        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        public SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
        {
            string queryString = "SELECT " + items[0];
            for(int i = 1; i< items.Length; i++)
            {
                queryString += ", " + items[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
            }
            return ExecuteQuery(queryString);
        }

        public SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=")
        {
            if(colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length != colValues.Lenght");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";

            for(int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key + operation + "'" + value + "'";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 本类log
        /// </summary>
        /// <param name="s"></param>
        static void Log(string s)
        {
            System.Diagnostics.Debug.WriteLine("class SqLiteHelper:::" + s);
        }
    }
}
