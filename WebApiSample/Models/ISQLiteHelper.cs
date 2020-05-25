using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace WebApiSample.Models
{
    interface ISQLiteHelper
    {
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        SQLiteDataReader ExecuteQuery(string queryString);

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        SQLiteDataReader ReadFullTable(string tableName);

        /// <summary>
        /// 像指定数据表中插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        SQLiteDataReader InsertValues(string tableName, string[] values);

        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...默认“=”</param>
        /// <returns></returns>
        SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=");

            /// <summary>
            /// 删除指定数据表内的数据
            /// </summary>
            /// <param name="tableName">数据表名称</param>
            /// <param name="colNames">字段名</param>
            /// <param name="colValues">字段名对应的数据</param>
            /// <param name="operations">运算符</param>
            /// <returns></returns>
        SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations);

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="operations">运算符</param>
        /// <returns></returns>
        SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations);

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        /// <returns></returns>
        SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes);

        /// <summary>
        /// 读取表名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="items"></param>
        /// <param name="colNames"></param>
        /// <param name="operations"></param>
        /// <param name="colValues"></param>
        /// <returns></returns>
        SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues);

    }
}
