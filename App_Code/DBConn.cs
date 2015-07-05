using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// DBConn의 요약 설명입니다.
/// </summary>
public class DBConn
{
    string connectionString = WebConfigurationManager.ConnectionStrings["ASPNET"].ConnectionString;
    public SqlConnection dbConn;

	public DBConn()
	{
        dbConn = new SqlConnection(connectionString);
        dbConn.Open();
	}

    public void Close()
    {
        dbConn.Close();
    }

    public SqlConnection GetConn()
    {
        return dbConn;
    }

    public void ExecuteNonQuery(string queryString)
    {
        SqlCommand cmd = new SqlCommand(queryString, dbConn);
        cmd.ExecuteNonQuery();
    }

    public SqlDataReader ExecuteReader(string queryString)
    {
        SqlCommand cmd = new SqlCommand(queryString, dbConn);
        return cmd.ExecuteReader();
    }

    public object ExecuteScalar(string queryString)
    {
        SqlCommand cmd = new SqlCommand(queryString, dbConn);
        return cmd.ExecuteScalar();
    }
}