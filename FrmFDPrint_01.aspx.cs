using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmFDPrint_01 : System.Web.UI.Page
{
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsDocRegister DR = new ClsDocRegister();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string bankcd, FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string YEAR = "";
                TextReport.Visible = false;
                //DateTime DT = Convert.ToDateTime(Convert.ToDateTime(Session["EntryDate"].ToString()).ToString("dd/MM/yyyy"));
                YEAR = DateTime.Now.Date.Year.ToString();//DT.Year.ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
                TxtAccType.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnBacktPrint_Click(object sender, EventArgs e)
    {
        bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
        string SS = DR.GetPrintStatus(ViewState["GL"].ToString(), TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
        string FL = "";
        if (Rdb_TypePrint.SelectedValue == "2")
        {
            FL = "DUP";
        }
        else
        {
            FL = "ORG";
        }
        if (bankcd == "1008")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SubGlCode=" + TxtAccType.Text + "&Accno=" + TxtAccno.Text + "&FL=" + FL + "&rptname=RptFDBackPrint_Palghar.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        if (bankcd == "1009")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SubGlCode=" + TxtAccType.Text + "&Accno=" + TxtAccno.Text + "&FL=" + FL + "&rptname=RptFDBackPrint_MSEB.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
    }
    protected void btnFrntPrint_Click(object sender, EventArgs e)
    {
        bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
        string SS = DR.GetPrintStatus(ViewState["GL"].ToString(), TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
        string FL = "";
        if (Rdb_TypePrint.SelectedValue == "2")
        {
            FL = "DUP";
        }
        else
        {
            FL = "ORG";
        }
        if (bankcd == "1008")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SubGlCode=" + TxtAccType.Text + "&Accno=" + TxtAccno.Text + "&FL=" + FL + "&rptname=RptFDPrint_Palghar.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        if (bankcd == "1009")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SubGlCode=" + TxtAccType.Text + "&Accno=" + TxtAccno.Text + "&FL=" + FL + "&rptname=RptFDPrint_MSEB.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
    }
    protected void TxtAccHName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccHName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {

                TxtAccHName.Text = custnob[0].ToString();
                TxtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                //--Abhishek
                string RES = AST.GetAccStatus(TxtBRCD.Text, TxtAccno.Text, TxtAccType.Text);
                Btn_View.Focus();

            }
            else
            {
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string AT = "";
                // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
                AT = BD.Getstage1(TxtAccno.Text, TxtBRCD.Text, TxtAccType.Text);
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccHName.Text = "";
                    TxtAccType.Text = "";
                    TxtATName.Text = "";
                    TxtAccno.Text = "";
                    TxtBRCD.Text = "";
                    TxtAccType.Focus();
                }
                else
                {
                    string checkprintstatus = AST.checkstatus(TxtBRCD.Text, TxtAccno.Text, TxtAccType.Text);
                    if (checkprintstatus != "1")
                    {
                        DataTable DT = new DataTable();
                        DT = AST.GetCustName(TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
                        if (DT.Rows.Count > 0)
                        {
                            TxtAccHName.Text = DT.Rows[0]["CustName"].ToString();
                        }
                    }
                    else
                    {
                        DataTable DT = new DataTable();
                        DT = AST.GetCustName(TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
                        if (DT.Rows.Count > 0)
                        {
                            TxtAccHName.Text = DT.Rows[0]["CustName"].ToString();
                        }
                        btnFrntPrint.Visible = false;
                        TextReport.Visible = true;

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAccno.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string custno = TxtATName.Text;
                string[] CT = custno.Split('_');
                if (CT.Length > 0)
                {
                    TxtATName.Text = CT[0].ToString();
                    TxtAccType.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = TxtBRCD.Text + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

                }
                TxtAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtATName.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                TxtATName.Text = AST.GetAccType(TxtAccType.Text, TxtBRCD.Text);

                string[] GL = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
                TxtATName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                AutoAccname.ContextKey = TxtBRCD.Text + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

                TxtAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAccType.Text = "";
                TxtBRCD.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBrname.Text = bname;
                    TxtAccType.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCD.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TextReport_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_View_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = DR.FDReceiptGrid(Grid_ViewFD, Session["BRCD"].ToString(), TxtAccType.Text, TxtAccno.Text);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void ClearData()
    {
        TxtBRCD.Text = "";
        TxtAccType.Text = "";
        TxtBrname.Text = "";
        TxtATName.Text = "";
        TxtAccno.Text = "";
        TxtAccHName.Text = "";
        TxtBRCD.Focus();

    }
}