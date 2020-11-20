using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient; 

public partial class DBBACK : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand sqlcmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBackup_Click(object sender, EventArgs e)
    {

        con.ConnectionString = @"Server=AVS-SERVER\AVS_SERVER;database=YSPMCore;uid=sa;pwd=avs@123;";  
    
           
        string backupDIR = "E:\\DBBackup";
   
        if (!System.IO.Directory.Exists(backupDIR))
        {
            System.IO.Directory.CreateDirectory(backupDIR);
        }
        try
        {
            con.Open();
            sqlcmd = new SqlCommand("backup database YSPMCore to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
            sqlcmd.ExecuteNonQuery();
            con.Close();
            lblError.Text = "Backup database successfully";
        }
        catch (Exception ex)
        {
            lblError.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
        }
    }
}