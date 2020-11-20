using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMigTransaction : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsMigTransaction MT = new ClsMigTransaction();
    string SQuery = "";
    int IntResult = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBranch();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindBranch()
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            IntResult = MT.MoveTransactions(txtBrCode.Text.Trim().ToString(), txtUptoDate.Text.ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (IntResult > 0)
            {
                lblMessage.Text = "Successfully moved data...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}