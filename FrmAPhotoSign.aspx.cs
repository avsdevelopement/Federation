using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;

public partial class FrmAPhotoSign : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CT"] = Request.QueryString["CT"].ToString();
            GetData();
        }
    }
    public void GetData()
    {
        SqlCommand cmd = new SqlCommand();
        string sql = "select photo from avs1011 where custno='" + ViewState["CT"].ToString() + "'";
        cmd = new SqlCommand(sql, conn.GetDBConnection());
        SqlDataReader DR = cmd.ExecuteReader();
        if (DR.HasRows)
        {
            while (DR.Read())
            {
                byte[] buf = new byte[5000];
                long bytesRead = DR.GetBytes(DR.GetOrdinal("photo"), 0, buf, 0, 5000);
                FileStream fs = new FileStream("C:\\test\\test_long" + bytesRead + ".jpg", FileMode.Create);
                fs.Write(buf, 0, (int)bytesRead);
            }
        } 
    }
    private static Stream RetrievePhotoNoProfile()
    {
        string noprofileimgPath = HttpContext.Current.Server.MapPath("~/images/noprofile.png");
        System.IO.FileStream fs = new System.IO.FileStream(noprofileimgPath, System.IO.FileMode.Open, FileAccess.Read);
        byte[] ba = new byte[fs.Length];
        fs.Read(ba, 0, (int)fs.Length);
        Stream myImgStream = new MemoryStream(ba);
        fs.Close();
        return myImgStream;
    }
}