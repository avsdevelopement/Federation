
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Net.NetworkInformation;

public class DbConnection
{
    string sql, sResult = "";

    public DbConnection()
    {

    }

    public string DbName()// Amruta 08/08/2017
    {
        SqlConnection conn1 = new SqlConnection();
        string CS = "";
        try
        {
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con = line.Split("\n".ToCharArray());
            string Datasource = Decrypt1(con[6].ToString());
            string Catelog = Decrypt1(con[2].ToString());
            string UserId = Decrypt1(con[3].ToString());
            string Password = Decrypt1(con[4].ToString());
            string Date = Decrypt1(con[5].ToString());

            //// string Date1=CheckDate();
            // if (DateTime.Now.Date > Convert.ToDateTime(ConvertDate(Date)))
            // {
            //     System.Windows.Forms.MessageBox.Show("DataBase Memory Full");
            //     goto pol;
            // }
            // else
            // {
            // CS = "Data Source=192.168.1.52;Initial Catalog=HSFM_0811;User ID=sa;Password=Pa55w@rd;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            //CS = "Data Source=103.228.152.232;Initial Catalog=HSFM_110820;User ID=fed;Password=Passw0rd;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            //CS = "Data Source=103.228.152.232;Initial Catalog=HSFM;User ID=sa;Password=IiB)5^`t;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            // CS = "Data Source=103.228.152.232;Initial Catalog=HSFM;User ID=fed;Password=Passw0rd;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            // CS = "Data Source=192.168.1.52;Initial Catalog=HSFM_0811;User ID=sa;Password=Pa55w@rd;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            //CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            // CS = "Data Source=AVS-SERVER;Initial Catalog=Emp_mseb;User ID=sa;Password=Pa55w@rd@2019;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            // CS = "Data Source=103.228.152.231;Initial Catalog=SBC60_09032020;User ID=sa;Password=I491}WKrTc;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            //  CS = "Data Source=103.228.152.233;Initial Catalog=SBC003;User ID=sbc;Password=qF?G|!x_x;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            // CS = "Data Source=103.228.152.232;Initial Catalog=HSFM_250220;User ID=sa;Password=Pass@123;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            // CS = "Data Source=103.228.152.232;Initial Catalog=HSFM;User ID=sa;Password=Pass@123;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=300000;";
            // CS = "Data Source=103.228.152.233;Initial Catalog=SBC005;User ID=sbc;Password=5d?f!%~Phdk;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            // CS = "Data Source=AVS-SERVER;Initial Catalog=HSFM;User ID=sa;Password=sa$2020;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CS;
    }


    public string DbNamePH()// Amruta 28/11/2017
    {
        SqlConnection conn1 = new SqlConnection();
        string CS = "";
        try
        {
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS1.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con = line.Split("\n".ToCharArray());
            string Datasource = Decrypt1(con[6].ToString());
            string Catelog = Decrypt1(con[2].ToString());
            string UserId = Decrypt1(con[3].ToString());
            string Password = Decrypt1(con[4].ToString());

            CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";

            //   CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            // CS = "Data Source=103.228.152.231;Initial Catalog=SHIVJYOTIIMAGE;User ID=sa;Password=sa@123;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            // CS = "Data Source=103.228.152.233;Initial Catalog=SAMN_Image;User ID=sbc;Password=qF?G|!x_x;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CS;
    }

    public string DBNameMob()//Amruta 05/12/2017
    {
        SqlConnection con = new SqlConnection();
        string CS = "";
        try
        {
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS2.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con1 = line.Split("\n".ToCharArray());
            string Datasource = Decrypt1(con1[6].ToString());
            string Catelog = Decrypt1(con1[2].ToString());
            string UserId = Decrypt1(con1[3].ToString());
            string Password = Decrypt1(con1[4].ToString());

            // CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            //CS = "Data Source=192.168.1.52;Initial Catalog=SBC005_-0711;User ID=sa;Password=Pa55w@rd;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            CS = "Data Source=103.228.152.231;Initial Catalog=MobApps;User ID=sa;Password=sa@123;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return CS;
    }

    public string DbNameMob1()//Amruta 05/12/2017
    {
        SqlConnection con = new SqlConnection();
        string CS = "";
        try
        {
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS4.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con1 = line.Split("\n".ToCharArray());
            string Datasource = Decrypt1(con1[6].ToString());
            string Catelog = Decrypt1(con1[2].ToString());
            string UserId = Decrypt1(con1[3].ToString());
            string Password = Decrypt1(con1[4].ToString());

            CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
            //  CS = "Data Source=103.228.152.231;Initial Catalog=MobApps;User ID=sa;Password=sa@123;Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return CS;
    }

    public DataTable GetDatatableMob1(string sql)
    {
        DataTable Retdt = new DataTable();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = sql;
            objCmd.Connection = GetDBConnectionMob1();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 5000000;
            if (objDA != null)
                objDA.Fill(Retdt);
            else
                Retdt = null;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            Retdt = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return Retdt;
    }

    public int sExecuteQueryMob1(string sQuery)
    {
        //int retVal = 0;
        int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnectionMob1();
            retVal = objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = -1;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public string CheckDate()
    {
        string sql = "";
        string Date = "";
        try
        {
            sql = "select Convert(varchar(10),getdate(),121)";
            Date = sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Date;
    }

    public string DBNameChd()//Amruta 05/12/2017
    {
        SqlConnection con = new SqlConnection();
        string CS = "";
        try
        {
            string txtpath = System.Web.HttpContext.Current.Server.MapPath("~/SqlConn/SYS3.dat");
            StreamReader sr = new StreamReader(txtpath);
            string line = sr.ReadToEnd();
            string[] con1 = line.Split("\n".ToCharArray());
            string Datasource = Decrypt1(con1[6].ToString());
            string Catelog = Decrypt1(con1[2].ToString());
            string UserId = Decrypt1(con1[3].ToString());
            string Password = Decrypt1(con1[4].ToString());
            CS = "Data Source=" + Datasource + ";Initial Catalog=" + Catelog + ";User ID=" + UserId + ";Password=" + Password + ";Min Pool Size=5; Max Pool Size=1000;Connect Timeout=30000;";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return CS;
    }
    public string sExecuteScalarNew(SqlCommand objCmd)
    {
        string retVal = "";
        try
        {
            objCmd.Connection = GetDBConnection();
            objCmd.CommandTimeout = 500000;
            retVal = (string)objCmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public SqlConnection GetDBConnection()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content, strconn;
            content = strconn = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(DbName());
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public SqlConnection GetDBConnectionMob1()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content, strconn;
            content = strconn = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(DbNameMob1());
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public SqlConnection GetDBConnectionMob()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content, strconn;
            content = strconn = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(DBNameMob());
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public SqlConnection GetDBConnection1()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content, strconn;
            content = strconn = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(DBNameChd());
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public void FillList(ListBox ddl, string dQuery)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.CommandText = dQuery;
            cmd.Connection = GetDBConnection();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = sdr["Name"].ToString();
                    item.Value = sdr["Id"].ToString();
                    ddl.Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

        finally
        {
            cmd.Dispose();
        }
    }

    public SqlConnection GetPhotoConnection()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content, strconn;
            content = strconn = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(DbNamePH());
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public SqlConnection CloseDBConnection()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            string content;
            content = "";
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return null;
        }
        return conn;
    }

    public string GetconnectionSTR(string Conn)
    {
        string[] STR = Conn.Split(',');
        //Conn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + STR[0].ToString() + ")(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + STR[1].ToString() + ")));User Id=" + STR[2].ToString() + ";Password=" + STR[3].ToString() + ";";//providerName=Oracle.DataAccess.Client";
        Conn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + STR[0].ToString() + ")(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + STR[1].ToString() + ")));User Id=" + STR[2].ToString() + ";Password=" + STR[3].ToString() + ";Min Pool Size=5; Max Pool Size=1000;";
        return Conn;
    }

    public DataTable GetDatatable(string sql)
    {
        DataTable Retdt = new DataTable();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = sql;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 5000000;
            if (objDA != null)
                objDA.Fill(Retdt);
            else
                Retdt = null;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            Retdt = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return Retdt;
    }

    public DataTable GetDatatableMob(string sql)
    {
        DataTable Retdt = new DataTable();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = sql;
            objCmd.Connection = GetDBConnectionMob();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 5000000;
            if (objDA != null)
                objDA.Fill(Retdt);
            else
                Retdt = null;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            Retdt = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return Retdt;
    }

    public DataTable GetPhotoTable(string sql)
    {
        DataTable Retdt = new DataTable();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();
            objCmd.CommandText = sql;
            objCmd.Connection = GetPhotoConnection(); //GetDBConnection();
            objDA.SelectCommand = objCmd;

            if (objDA != null)
                objDA.Fill(Retdt);
            else
                Retdt = null;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            Retdt = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return Retdt;
    }

    public int sExecuteQuery(string sQuery)
    {
        int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection();
            objCmd.CommandTimeout = 5000000;
            retVal = objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = -1;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public int sExecuteQueryMob(string sQuery)
    {
        int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnectionMob();
            retVal = objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = -1;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public int sExecuteQueryPH(string sQuery)
    {
        int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetPhotoConnection();
            retVal = objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = -1;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public int sExecuteQuery1(string sQuery)
    {
        int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection1();
            retVal = objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = -1;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public string sExecuteScalar(string sQuery)
    {
        string retVal = "";
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection();
            objCmd.CommandTimeout = 500000;
            var rcount = objCmd.ExecuteScalar();
            retVal = rcount == null ? null : rcount.ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public string sExecuteScalar1(string sQuery)
    {
        string retVal = "";
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection1();
            objCmd.CommandTimeout = 500000;
            var rcount = objCmd.ExecuteScalar();
            retVal = rcount == null ? null : rcount.ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public string sExecuteReader(string sQuery)
    {
        SqlCommand objCmd = new SqlCommand();
        string retval = "";
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection();

            SqlDataReader sdr;
            sdr = objCmd.ExecuteReader();
            while (sdr.Read())
            {
                retval = sdr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retval = "";
        }
        finally
        {
            objCmd.Dispose();
        }
        return retval;
    }


    public string sExecuteScalarphoto(string sQuery)
    {
        string retVal = "";
        // int retVal;
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = new SqlConnection(DbNamePH());
            if (objCmd.Connection.State == ConnectionState.Closed)
            {
                objCmd.Connection.Open();
            }
            objCmd.CommandTimeout = 500000;
            var rcount = objCmd.ExecuteScalar();
            retVal = rcount == null ? null : rcount.ToString();
            //retVal = objCmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
            retVal = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }
    public int sBindGrid(GridView sGview, string sQuery)
    {
        int retrow = 0;
        DataTable dt = new DataTable();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 500000;
            objDA.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    retrow = dt.Rows.Count;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    int TotalColumns = sGview.Rows[0].Cells.Count;
                    sGview.Rows[0].Cells.Clear();
                    sGview.Rows[0].Cells.Add(new TableCell());
                    sGview.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    sGview.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    sGview.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return retrow = -1;
        }
        finally
        {
            objCmd.Dispose();

        }
        return retrow;
    }

    public int sBindGrid1(GridView sGview, string sQuery)
    {
        int retrow = 0;
        DataTable dt = new DataTable();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnection1();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 500000;
            objDA.Fill(dt);
            // string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["acsConnectionString"].ConnectionString;
            //DataTable dt = sGetDatatable(sQuery, "", objCmd);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    retrow = dt.Rows.Count;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    int TotalColumns = sGview.Rows[0].Cells.Count;
                    sGview.Rows[0].Cells.Clear();
                    sGview.Rows[0].Cells.Add(new TableCell());
                    sGview.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    sGview.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    sGview.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return retrow = -1;
        }
        finally
        {
            objCmd.Dispose();

        }
        return retrow;
    }

    private string Decrypt1(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public void FillDDL(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

        finally
        {
            objCmd.Dispose();
        }
    }

    public void FillDDL2(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("ALL", "0"));

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

        finally
        {
            objCmd.Dispose();
        }

    }

    public void FillPayMode(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            objCmd.Dispose();
        }
    }

    public void FillListBx(ListBox lsbx, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            lsbx.DataSource = objDs;
            lsbx.DataTextField = "Name";
            lsbx.DataValueField = "id";
            lsbx.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            objCmd.Dispose();
        }
    }

    public void FillmoduleRqDDL(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl.Items.Insert(9, new ListItem("Others", "100"));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            objCmd.Dispose();
        }
    }

    public void FillDDL1(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection1();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            objCmd.Dispose();
        }
    }

    public void FillCHARGES(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "HEADDESC";
            ddl.DataValueField = "SRNUMBER";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            objCmd.Dispose();
        }
    }

    public string PCNAME()
    {
        string PACMAC = "";
        try
        {
            //var request = HttpContext.Current.Request;
            //PACMAC = "";
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                PACMAC = "No network interfaces found.";
            }
            else
            {
                //PACMAC = nics[0].Id.ToString();
                PACMAC = nics[0].GetPhysicalAddress().ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return PACMAC = "";
        }
        return PACMAC;
    }

    public string AddMonthDay(string EDT, string NO, string FL)
    {
        string sql = "";
        if (FL == "M")
        {
            sql = "select DATEADD(MONTH," + NO + ", '" + ConvertDate(EDT) + "')";

        }
        if (FL == "MR")
        {
            sql = "select DATEADD(MONTH," + Convert.ToInt32(Convert.ToInt32(NO) - 1) + ", '" + ConvertDate(EDT) + "')";

        }
        else if (FL == "D")
        {
            sql = "select  CONVERT(VARCHAR(10),DATEADD(day, datediff(day, 0,(SELECT DATEADD(DAY," + NO + ",'" + ConvertDate(EDT) + "'))), 0),121) ";
        }
        EDT = sExecuteScalar(sql);
        return Convert.ToDateTime(EDT).ToString("dd/MM/yyyy");
    }

    public string AddMonthDay7(string EDT, string NO, string FL)
    {
        string sql = "";
        if (FL == "M")
        {
            sql = "select convert(varchar(10),DATEADD(MONTH," + NO + ", '" + ConvertDate(EDT) + "'),121)";

        }
        if (FL == "MR")
        {
            sql = "select DATEADD(MONTH," + Convert.ToInt32(Convert.ToInt32(NO) - 1) + ", '" + ConvertDate(EDT) + "')";

        }
        else if (FL == "D")
        {
            sql = "select  CONVERT(VARCHAR(10),DATEADD(day, datediff(day, 0,(SELECT DATEADD(DAY," + NO + ",'" + ConvertDate(EDT) + "'))), 0),121) ";
        }
        EDT = sExecuteScalar(sql);
        return EDT;
    }

    public string GetDayDiff(string Fdate, string Tdate)
    {
        try
        {
            sql = "Select DateDiff(DAY,'" + ConvertDate(Fdate) + "','" + ConvertDate(Tdate) + "')";
            sResult = sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public string GetMonthDiff(string StartDate, string EndDate)
    {
        try
        {
            sql = "Select DateDiff(MM, '" + ConvertDate(StartDate) + "', '" + ConvertDate(EndDate) + "')";
            sResult = sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public string GetNextMonthStartDate(string strYYYYMMDD)
    {
        try
        {
            string sql = "";
            sql = "Select Convert(VarChar(10), DateAdd(MM, 1, '" + strYYYYMMDD + "'), 103)";
            strYYYYMMDD = sExecuteScalar(sql);

            char[] chrSep = { '/' };
            string[] strArr = strYYYYMMDD.Split(chrSep);
            if (strArr.Length == 3)
                return strArr[2].ToString() + "-" + strArr[1].ToString() + "-01";
            else
                return strYYYYMMDD;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return strYYYYMMDD;
    }

    public string AddMonthDay1(string EDT, string NO, string FL)
    {
        try
        {
            string sql = "";
            if (FL == "M")
            {
                sql = "select add_months('" + EDT + "'," + NO + ") from dual";
            }
            else if (FL == "D")
            {
                sql = "select '" + EDT + "'+" + NO + " from dual";
            }
            EDT = sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Convert.ToDateTime(EDT).ToString("dd/MM/yyyy");
    }

    #region Conver Date dd-mm-yyyy to yyyy-mm-dd
    public string ConvertDate(string strDDMMYYYY)
    {
        char[] chrSep = { '/' };
        string[] strArr = strDDMMYYYY.Split(chrSep);
        if (strArr.Length == 3)
            return strArr[2].ToString() + "-" + strArr[1].ToString() + "-" + strArr[0].ToString();
        else
            return strDDMMYYYY;

    }

    //  Added By Amol On 11-08-2017 for convert date
    //  Conver Date from dd/mm/yyyy to dd-mm-yyyy
    public string ConvertToDate(string strDDMMYYYY)
    {
        char[] chrSep = { '/' };
        string[] strArr = strDDMMYYYY.Split(chrSep);
        if (strArr.Length == 3)
            return strArr[0].ToString() + "-" + strArr[1].ToString() + "-" + strArr[2].ToString();
        else
            return strDDMMYYYY;

    }
    #endregion


    #region Two Date Diff In Month
    public int DiffMonths(DateTime StartDate, DateTime EndDate)
    {
        Int32 months = 0;
        DateTime tmp = StartDate;

        while (tmp < EndDate)
        {
            months++;
            tmp = tmp.AddMonths(1);
        }

        return months;
    }
    #endregion

    public string ConvertToDDSPostDate(string Date)
    {
        try
        {
            DateTime temp = DateTime.ParseExact(Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime d = DateTime.ParseExact(temp.ToString("dd/MM/yyyy").Replace(" 12:00:00", ""), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Date = d.ToString().Replace(" 12:00:00 AM", "");
            Date = d.ToString().Replace(" 12:00:00", "");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Date;
    }

    public string ConvertToDDSPostDateAKYT(string Date)
    {
        try
        {
            DateTime temp = DateTime.ParseExact(Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime d = DateTime.ParseExact(temp.ToString("dd/MM/yyyy").Replace(" 12:00:00", ""), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string YY = d.Year.ToString();
            string MM = d.Month.ToString();
            string DD = d.Day.ToString();
            Date = DD + "/" + MM + "/" + YY;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Date;
    }

    public DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = DbNamePH();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }

    public string AddYear(string EDT, string NO)
    {
        try
        {
            string sql = "";
            sql = "select DATEADD(YEAR," + NO + ", '" + ConvertDate(EDT) + "')";
            EDT = sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDateTime(EDT).ToString("dd/MM/yyyy");
    }

    public int sBindGridMob(GridView sGview, string sQuery)
    {
        int retrow = 0;
        DataTable dt = new DataTable();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnectionMob();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 500000;
            objDA.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    retrow = dt.Rows.Count;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    int TotalColumns = sGview.Rows[0].Cells.Count;
                    sGview.Rows[0].Cells.Clear();
                    sGview.Rows[0].Cells.Add(new TableCell());
                    sGview.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    sGview.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    sGview.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return retrow = -1;
        }
        finally
        {
            objCmd.Dispose();

        }
        return retrow;
    }

    public string sExecuteScalarMob(string sQuery)
    {
        string retVal = "";
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = sQuery;
            objCmd.Connection = GetDBConnectionMob();
            objCmd.CommandTimeout = 500000;
            var rcount = objCmd.ExecuteScalar();
            retVal = rcount == null ? null : rcount.ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            retVal = null;
        }
        finally
        {
            objCmd.Dispose();
        }
        return retVal;
    }

    public string AddMonth(string EDate, string Period, string sFlag)// amol 28/09/2018
    {
        try
        {
            if (sFlag.ToString() == "D")
                sql = "Select ConVert(VarChar(10), DateAdd(Day, " + Period + ", '" + ConvertDate(EDate).ToString() + "'), 103)";
            else if (sFlag.ToString() == "M")
                sql = "Select ConVert(VarChar(10), DateAdd(Month, " + Period + ", '" + ConvertDate(EDate).ToString() + "'), 103)";

            sResult = sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public bool ContainColumn(DataTable tbl1, DataTable tbl2)
    {
        bool ans = false;
        try
        {
            foreach (DataColumn columnName in tbl1.Columns)
            {
                if (!tbl2.Columns.Contains(columnName.ColumnName))
                {
                    return ans;
                }
            }
            ans = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ans;
    }

    public DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();

        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            cmd.Connection = GetDBConnection();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
        }
        return dt;
    }
    public bool fBulkCopy(System.Data.DataTable dtData, string TableName)
    {
        bool ans = false;
        try
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(GetDBConnection());
            if (dtData.Rows.Count > 0)
            {
                bulkCopy.DestinationTableName = TableName;
                bulkCopy.BulkCopyTimeout = 2000;
                bulkCopy.BatchSize = 10000;
                bulkCopy.WriteToServer(dtData);
            }
            ans = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
        }
        return ans;
    }
}