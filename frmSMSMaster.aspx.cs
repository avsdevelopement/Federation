using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class frmSMSMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

               

            }
            if (Rdb_type.SelectedValue == "1")
            {
                div_mobile.Visible = true;

            }
            if (Rdb_type.SelectedValue == "2")
            {
                div_mobile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
       
    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClearAll_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
    //public void clear()

}