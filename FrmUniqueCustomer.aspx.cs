using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class FrmUniqueCustomer : System.Web.UI.Page
{
    ClsUniqueCustomer UC = new ClsUniqueCustomer();
    ClsBindDropdown BD = new ClsBindDropdown();
    string sResult = "";
    int Result = 0;

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBranch(ddlBrName);
                txtBrCode.Text = Session["BRCD"].ToString();
                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtAsOnDate.Text = Session["EntryDate"].ToString();

                ddlBrName.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function
    
    public void BindBranch(DropDownList ddlBrName)
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
    
    #endregion

    #region Index Changed
    
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();

            txtAsOnDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Events
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Result = UC.AssignUniqueCustNo(txtBrCode.Text.Trim().ToString(), txtAsOnDate.Text.ToString());

            if (Result > 0)
            {
                lblMessage.Text = "Unique customer no assign successfully...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}