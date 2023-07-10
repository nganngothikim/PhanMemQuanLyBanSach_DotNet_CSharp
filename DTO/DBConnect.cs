using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DTO
{
    public class DBConnect
    {
        private DataSet ds = new DataSet();
        public string ID_USER = "";
        public DataSet DS
        {
            get { return ds; }
            set { ds = value; }
        }
        string strPass;

        public string StrPass
        {
            get { return strPass; }
            set { strPass = value; }
        }
        string strUserName;

        public string StrUserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        string strSeverName;

        public string StrSeverName
        {
            get { return strSeverName; }
            set { strSeverName = value; }
        }
        string strDataBaseName;

        public string StrDataBaseName
        {
            get { return strDataBaseName; }
            set { strDataBaseName = value; }
        }
        string strConnection;
        public string StrConnection
        {
            get { return strConnection; }
            set { strConnection = value; }
        }
        public SqlConnection Connect;
        public DBConnect()
        {
            strSeverName = @"DESKTOP-QCTVOKN\SQLEXPRESS"; //DESKTOP-QCTVOKN\SQLEXPRESS  HT-20220824SFHY\SQLEXPRESS  ADMIN\SQLEXPRESS
            strDataBaseName = "QL_NhaSach";
            strUserName = "sa";
            strPass = "123";
            strConnection = @"Data Source=" + strSeverName + ";Initial Catalog=" + strDataBaseName + ";User ID=" + strUserName + ";Password=" + strPass;
            Connect = new SqlConnection(strConnection);

        }
        public DBConnect(string StrSeverName, string StrDataBaseName, string StrUserName, string StrPass)
        {
        }

        public void openConnection()
        {
            if (Connect.State.ToString() == "Closed")
                Connect.Open();
        }
        public void closeConnection()
        {
            if (Connect.State.ToString() == "Open")
                Connect.Close();
        }

        public int execNonQuery(string sql)
        {
            openConnection();
            SqlCommand cmd = new SqlCommand(sql, Connect);
            int count = cmd.ExecuteNonQuery();
            closeConnection();
            return count;
        }
        public int getCount_ExecuteScalar(string sql)//truyền vào chuỗi truy vấn
        {
            openConnection();
            SqlCommand cmd = new SqlCommand(sql, Connect);//thực hiện truy vấn
            int count = (int)cmd.ExecuteScalar();//trả về số nguyên của ô đầu dòng đầu
            closeConnection();
            return count;
        }

        public SqlDataReader getCount_ExecuteReader(string sql)//truyền vào chuỗi truy vấn
        {
            openConnection();
            SqlCommand cmd = new SqlCommand(sql, Connect);//thực hiện truy vấn
            SqlDataReader rd = cmd.ExecuteReader();
            return rd;
        }

        //public SqlDataAdapter getDataAdapter(string strSQL, string tableName)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter(strSQL, Connect);

        //}
        public DataTable getDataTable(string strSQL, string tableName)
        {
            //openConn();
            //DS = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Connect);
            ada.Fill(DS, tableName);
            //closeConn();
            return DS.Tables[tableName];
        }

    }
}
