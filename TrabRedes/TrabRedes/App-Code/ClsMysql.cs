using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace TrabRedes.App_Code
{
    public class ClsMysql
    {
        private MySqlConnection mConn;

        public ClsMysql()
        {
            MysqlConstruction();
        }

        public void MysqlConstruction()
        {
            try
            {
                MySqlConnectionStringBuilder sMysqlBuilder = new MySqlConnectionStringBuilder();
                sMysqlBuilder.Server = "localhost";
                sMysqlBuilder.UserID = "root";
                sMysqlBuilder.Password = "";
                sMysqlBuilder.Database = "trabrede";
                sMysqlBuilder.PersistSecurityInfo = false;

                mConn = new MySqlConnection(sMysqlBuilder.ToString());

            }
            catch (Exception)
            {
                throw;
            }

        }


        public DataTable MySqlReturnData(string sQuery)
        {
            DataTable DtbReturn = new DataTable();
            mConn.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(sQuery, mConn);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(DtbReturn);
            mConn.Close();
            mySqlDataAdapter.Dispose();
            return DtbReturn;
        }

        public void MySqlExecutaData(string sQuery)
        {
            try
            {
                mConn.Open();
                MySqlCommand mySqlCommand = new MySqlCommand(sQuery, mConn);
                mySqlCommand.ExecuteNonQuery();
                mConn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        
        
        
        }



    }
}