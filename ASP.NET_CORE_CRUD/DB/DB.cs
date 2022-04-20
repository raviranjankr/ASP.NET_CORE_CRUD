using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ASP.NET_CORE_CRUD.DB
{
    public class DBCMD : IDB
    {        
        SqlConnection conn;
        SqlCommand mCom;
        SqlDataAdapter mDa;
        //MiscModel miscModel;
        #region OpenCloseDispose_Connection

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        private void OpenConnection()
        {
            conn = new SqlConnection(GetConfiguration().GetConnectionString("myDb"));
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            mCom = new SqlCommand();
            mCom.Connection = conn;
        }
        private void CloseConnection()
        {
            if (conn == null == false)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        private void DisposeConnection()
        {
            if (conn == null == false)
                conn.Dispose();
        }
        #endregion
        public DataSet GetData(string procName, Hashtable hashParam, int timeOut)
        {
            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                mCom.CommandText = procName;
                mCom.CommandType = CommandType.StoredProcedure;

                mDa = new SqlDataAdapter(mCom);
                foreach (DictionaryEntry de in hashParam)
                    mCom.Parameters.AddWithValue(de.Key.ToString(), de.Value);

                mDa.SelectCommand.CommandTimeout = timeOut;
                mDa.Fill(ds);
                mCom.Parameters.Clear();
                CloseConnection();
                DisposeConnection();
            }
            catch (Exception ex) { }
            return ds;
        }

        public int Save(string procName, Hashtable hashParam)
        {
            try
            {
                int res = 0;
                OpenConnection();
                mCom.CommandText = procName;
                mCom.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry de in hashParam)
                    mCom.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                res = mCom.ExecuteNonQuery();
                mCom.Parameters.Clear();
                CloseConnection();
                DisposeConnection();
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
