using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmIntRateWiseDP : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                //added by ankita 07/10/2017 to make user frndly
                TxtAsonDate.Text = Session["EntryDate"].ToString();
               TxtFBrID.Text = Session["BRCD"].ToString();
              TxtTBrID.Text = Session["BRCD"].ToString();
                TxtFBrID.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "IntRateWiseDP_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string ProdCode;

            if (TxtFSubgl.Text.Trim().ToString() == "")
                ProdCode = "0000";
            else
                ProdCode = TxtFSubgl.Text.Trim().ToString();

            if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + ProdCode + "&AsOnDate=" + TxtAsonDate.Text + "&Flag=" + "TDSU" + "&rptname=RptIntRateWiseDPDT.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + ProdCode + "&AsOnDate=" + TxtAsonDate.Text + "&Flag=" + "TD" + "&rptname=RptIntRateWiseDPSumry.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}