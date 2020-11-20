using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMultiVoucherAutho : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Rdb_PassType.SelectedValue == "2")
            {
                Div_LotPassing.Visible = true;
                Div_SinglePassing.Visible = false;
            }
            else
            {
                Div_LotPassing.Visible = false;
                Div_SinglePassing.Visible = true;
            }

            ViewState["SETNO"] = Request.QueryString["SETNO"].ToString();
            ViewState["ENTRYDATE"] = Request.QueryString["ENTRYDATE"].ToString();
            ViewState["MID"] = Request.QueryString["MID"].ToString();
        }
    }
    protected void Rdb_PassType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_PassType.SelectedValue == "2")
            {
                Div_LotPassing.Visible = true;
                Div_SinglePassing.Visible = false;
            }
            else
            {
                Div_LotPassing.Visible = false;
                Div_SinglePassing.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
}