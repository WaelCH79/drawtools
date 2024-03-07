using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MySQLConnection
    {
        public MySqlConnection connection;

        public MySqlConnection CreateConnection() //create mysql connection
        {


            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DrawTools.Properties.Settings.ConnectionString"].ConnectionString);

            return connection;
        }

        private bool OpenConnection() //connect open
        {
            try
            {
                connection = CreateConnection();
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        private bool CloseConnection() //conne ction close
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Executing(string query) //execute sql query
        {

            try
            {
                if (OpenConnection())
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { CloseConnection(); }
        }

        public DataSet LoadData(string Query) //get data using sql query
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.CommandType = CommandType.Text;

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }

                    CloseConnection();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { CloseConnection(); }
        }

        public DataSet ExucteSP(Dictionary<string, string> Params, string spName) // execute sql stored procedure 
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection()) //open connection
                {
                    using (MySqlCommand cmd = new MySqlCommand(spName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (KeyValuePair<string, string> item in Params)
                        {
                            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                        }
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(ds);
                        }
                    }
                    CloseConnection(); //close connection
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Exécuter une transaction
        /// </summary>
        /// <param name="queryList">Liste des requetes</param>
        public void ExucteTransaction(List<string> queryList)
        {
            MySqlConnection myConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DrawTools.Properties.Settings.ConnectionString"].ConnectionString);
            myConnection.Open();
            MySqlCommand myCommand = myConnection.CreateCommand();
            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = myConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                foreach( string query in queryList)
                {
                    myCommand.CommandText = query;
                    myCommand.ExecuteNonQuery();
                }

                myTrans.Commit();

            }
            catch (Exception e)
            {
                    myTrans.Rollback();               
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
