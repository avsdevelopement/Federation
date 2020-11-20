using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmNPAReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    int Result;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            autoglname.ContextKey = Session["BRCD"].ToString();
            TxtFPRD.Focus();
            TxtDate.Text = Session["EntryDate"].ToString();
        }
    }

   
    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TxtFPRDName.Text = TD[0].ToString();
            TxtTPRD.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TXtTPRDName.Text = TD[0].ToString();
            TxtDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            TxtDate.Focus();
        }
    }

    protected void TxtFPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtFPRDName.Text = CT[0].ToString();
                TxtFPRD.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TxtFPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtFPRD.Text = "";
                    TxtFPRD.Focus();

                }
                else
                {
                    TxtTPRD.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TXtTPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TXtTPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TXtTPRDName.Text = CT[0].ToString();
                TxtTPRD.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TXtTPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtTPRD.Text = "";
                    TxtTPRD.Focus();

                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
   

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SFlag1 = "ALL", SFlag2 = "REG1";
            

            string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + TxtFPRD.Text + "&TSUBGL=" + TxtTPRD.Text + "&SFlag1=" + SFlag1 + "&SFlag2=" + SFlag2 + "&rptname=RptNPAReg1.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);





            //Commented Beacuse Only Specific is in work --Abhishek 16-06-2017
            //if (RdbSelect1.SelectedValue == "A")
            //{
            //    string Flag = "A";
            //    string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&GLNAME='All Loan Account'&Flag=" + Flag + "&rptname=RptODNpaReport.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
            //else if (RdbSelect1.SelectedValue == "S")
            //{
            //    string Flag = "S";

                //string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&SUBGL=" + TxtProcode.Text + "&GLNAME=" + TxtProName.Text + "&Flag=" + Flag + "&SFlag1=" + SFlag1 + "&SFlag2=" + SFlag2 + "&rptname=RptNPAReg1.rdlc";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);


                //Commented on 16-06-2017 --Abhishek
                //string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&SUBGL=" + TxtProcode.Text + "&GLNAME=" + TxtProName.Text + "&Flag=" + Flag + "&rptname=RptODNpaReport.rdlc";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                //string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&SUBGL=" + TxtProcode.Text + "&GLNAME=" + TxtProName.Text + "&Flag='S'&rptname=RptLoanNPAReport.rdlc";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
}