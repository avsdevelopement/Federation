using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Services;
using System.Web.Services.Protocols;


using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


public partial class FrmVerifySign : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        DbConnection conn = new DbConnection();
        ViewState["CUSTNO"]= Request.QueryString["CUSTNO"].ToString();
        DataTable pic = new DataTable();
        try
        {
            string id = ViewState["CUSTNO"].ToString();
            
           // Image1.Visible = id != "0";
            if (id != "0")
            {
                string sql = "";
                sql = "SELECT photo Data FROM avs1011 WHERE CUSTNO = " + id + " brcd='" + Session["BRCD"] + "'";
               pic = conn.GetPhotoTable(sql);
                if (pic.Rows.Count > 0)
                {
                    for (int i = 0; i <= pic.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            byte[] bytes = (byte[])pic.Rows[0]["Data"];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            // Image1.ImageUrl = "data:image/tif;base64," + base64String;

                            imgPopup.ImageUrl = "data:image/tif;base64," + base64String;
                        }

                        if (i == 1)
                        {

                            byte[] bytes = (byte[])pic.Rows[1]["Data"];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgSignPopup.ImageUrl = "data:image/tif;base64," + base64String;
                        }
                    }
                  
                }
                else
                {
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //lblMessage.Text = "No Image for that customer !!!!";
                    //ModalPopup.Show(this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
}