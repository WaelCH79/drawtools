using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySqlConnector;

namespace BLL
{
    public class ApplicationRepo
    {
        public void CreateApplication(List<string> listQuery)
        {
            MySQLConnection myConnection = new MySQLConnection();

            myConnection.ExucteTransaction(listQuery);
        }
    }
}
