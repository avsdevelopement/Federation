using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDebitEntry : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCutBook CB = new ClsCutBook();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
     TxtFDate.Text = Session["EntryDate"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();
        }
    }
    protected void TxtPType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GL = BD.GetAccTypeGL(TxtPType.Text, Session["BRCD"].ToString()).Split('_'); ;
            TxtProName.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
         try
        {
            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                TxtPType.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtPType.Text, Session["BRCD"].ToString()).Split('_');
                //ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPType.Text + "_" + ViewState["DRGL"].ToString();

               if (TxtProName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtPType.Text = "";
                    TxtPType.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    
    protected void TXTSubmit_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DebitEntry_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&ProdCode=" + TxtPType.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptDebitEntry.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
    protected void Report_Click(object sender, EventArgs e)
    {

    }
}