using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInwardUpload : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsInwardUpload IU = new ClsInwardUpload();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TxtTDate.Text = Session["EntryDate"].ToString();
            customcs.BindBankName(ddlBankName);
            TxtINWDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Process_Click(object sender, EventArgs e)
    {
        try
        {
            if (RdbAutho.Checked == true)
            {

                BindToClear();
            }
            else if (RdbNonAutho.Checked == true)
            {
                BindToReturn();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
           
        }
    }
    protected void BindToClear()
    {
        try
        {
            IU.GetInwardProcess(GrdAN, Session["EntryDate"].ToString(), TxtINWDate.Text, Session["BRCD"].ToString(), "AUTHO");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BindToReturn()
    {
        try
        {
            IU.GetInwardProcess(GrdAN, Session["EntryDate"].ToString(), TxtINWDate.Text, Session["BRCD"].ToString(), "NONAUTHO");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}