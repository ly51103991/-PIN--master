using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Data.SQLite;
namespace DataConn
{
    class DataBaseSys
    {
        public DataBaseSys() { }

        public static SQLiteConnection conn;
        public static SQLiteCommand cmd = null;

        //打开数据库连接
        public static void OpenConn()
        {
            string SqlCon = "data source=H:/sqliteDatabase/test.s3db";
            conn = new SQLiteConnection(SqlCon);
            conn.Open();
            cmd = new SQLiteCommand();
            cmd.Connection = conn;

        }
        //关闭数据库连接
        public static void CloseConn()
        {
            cmd.Dispose();
            conn.Close();
        }
        //读取数据
        public static SQLiteDataReader GetDataReaderValue(string sql, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            SQLiteDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        //返回DataSet
        public static DataSet GetDataSetValue(string sql, string tableName, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tableName);
            CloseConn();
            return ds;
        }
        //返回DataView
        public static DataView GetDataViewValue(string sql, string tableName, params MySqlParameter[] dic)
        {
            OpenConn();
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds, "temp");
            CloseConn();
            return ds.Tables[0].DefaultView;
        }
        //返回DataTable
        public static DataTable GetDataTableValue(string sql, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CloseConn();
            return dt;
        }
        //执行一个Sql操作：添加，删除，更新操作
        public static bool ExecuteNonQuery(string sql, params MySqlParameter[] dic)
        {
            try
            {
                OpenConn();
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(dic);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                CloseConn();
            }
        }
        //执行一个Sql操作：添加，删除，更新操作，返回受影响行数
     /*   public int ExecuteNonQueryCount(string sql, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            int value = cmd.ExecuteNonQuery();
            return value;
        }

        //执行一条返回第一条记录第一列的SqlCommand命令
        public object ExecuteScalar(string sql, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(dic);
            object value = cmd.ExecuteScalar();
            return value;
        }
        //返回记录数
        public int SqlServerRecordCount(string sql, params MySqlParameter[] dic)
        {
            OpenConn();
            cmd.Connection = conn;
            cmd.Parameters.AddRange(dic);
            SQLiteDataReader dr;
            dr = cmd.ExecuteReader();
            int RecordCount = 0;
            while (dr.Read())
            {
                RecordCount++;
            }
            CloseConn();
            return RecordCount;
        }
        //判断是否为数字
        public static bool GetSafeValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            foreach (char ch in value)
            {
                if (!char.IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }*/
    }
}

