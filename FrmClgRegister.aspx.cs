using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmClgRegister : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
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
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = Conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + Conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
            TxtBankCD.Text = "0000";
            TxtBrID.Text = Session["BRCD"].ToString();
        }
    }
    protected void ClgRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnInward.Checked)
            {
                if (RbtnBank.Checked)
                {
                    if (rbtnDetails.Checked)
                    {
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Rpt_Detail _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FL=IW&rptname=RptIWClearngDetails.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    
                }
                else if (RbnAccount.Checked)//Dhanya Shetty/12/07/2017
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Rpt_Accwise _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptInwRegAccWise.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                else
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Rpt_Summary _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FL=IW&rptname=RptIWClearngSummary.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
               
            }
            else if (rbtnOutward.Checked)
            {
                if (RbtnBank.Checked)
                {
                    if (rbtnDetails.Checked)
                    {
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Rpt_Detail _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FL=OW&rptname=RptIWClearngDetails.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                }
                    else if (RbnAccount.Checked)//Dhanya Shetty/12/07/2017
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Rpt_Accwise _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptOutwardRegAccWise.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                else
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Rpt_Summary _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FL=OW&rptname=RptIWClearngSummary.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnInward_CheckedChanged(object sender, EventArgs e)
    {
        rbtnInward.Checked = true;
        rbtnOutward.Checked = false;
    }

    protected void rbtnOutward_CheckedChanged(object sender, EventArgs e)
    {
        rbtnOutward.Checked = true;
        rbtnInward.Checked = false;
    }

    protected void rbtnDetails_CheckedChanged(object sender, EventArgs e)
    {
        rbtnDetails.Checked = true;
        rbtnSummary.Checked = false;
        if (rbtnDetails.Checked == true)
        {
            div_AccBank.Visible = true;
        }
        else
        {
            div_AccBank.Visible = false;
        }
    }

    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {
        rbtnSummary.Checked = true;
        rbtnDetails.Checked = false;
        if (rbtnSummary.Checked == true)
        {
            div_AccBank.Visible = false;
        }
    }
    protected void RbtnBank_CheckedChanged(object sender, EventArgs e)//Dhanya Shetty/12/07/2017
    {
        RbtnBank.Checked = true;
        RbnAccount.Checked = false;
    }
    protected void RbnAccount_CheckedChanged(object sender, EventArgs e)//Dhanya Shetty/12/07/2017
    {
        RbtnBank.Checked = false;
        RbnAccount.Checked = true;
    }
}