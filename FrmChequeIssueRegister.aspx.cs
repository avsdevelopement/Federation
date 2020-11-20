using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmChequeIssueRegister : System.Web.UI.Page
{
    ClsChequeIssueRegister cir = new ClsChequeIssueRegister();
    ClsBindDropdown BD = new ClsBindDropdown();
    string RButton;

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
            autoglname.ContextKey = Session["BRCD"].ToString();
            rbtnSpecificProd.Checked = true;
            rbtnAllProd.Checked = false;
        }
    }

    protected void SpecificProd_CheckedChanged(object sender, EventArgs e)
    {
        rbtnSpecificProd.Checked = true;
        rbtnAllProd.Checked = false;
        divProd.Visible = true;
        divAcc.Visible = true;
    }

    protected void AllProd_CheckedChanged(object sender, EventArgs e)
    {
        rbtnAllProd.Checked = true;
        rbtnSpecificProd.Checked = false;
        divProd.Visible = false;
        divAcc.Visible = false;
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = cir.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                txtProdName.Text = AC[2].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text + "_" + ViewState["GLCODE"].ToString();
                txtAccNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                txtProdCode.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName.Text = custnob[0].ToString();
                txtProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = cir.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ProdName"] = AC[0].ToString();
                ViewState["ProdCode"] = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), txtProdCode.Text);
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise.........!!";
                ModalPopup.Show(this.Page);
                txtAccNo.Text = "";
                txtAccName.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
            }
            else
            {
                DataTable DT = new DataTable();
                DT = cir.GetCustName(txtProdCode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtAccName.Text = DT.Rows[0]["CustName"].ToString();
                }
                txtAsOnDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtAccName.Text = custnob[0].ToString();
                txtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FL = "Insert";//Dhanya Shetty
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Chq_Issu_reg _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        BindGrid();
    }

    protected void grdChequeStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeStockReport.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        int Result;

        if (rbtnSpecificProd.Checked)
        {
            RButton = "S";
        }
        else if (rbtnAllProd.Checked)
        {
            RButton = "A";
        }

        Result = cir.BindGrid(grdChequeStockReport, Session["BRCD"].ToString(), txtProdCode.Text,txtAccNo.Text, txtAsOnDate.Text, RButton);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnSpecificProd.Checked)
            {
                RButton = "S";
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Chq_Issu_rpt _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?BCode=" + Session["BRCD"].ToString() + "&ProdCode=" + txtProdCode.Text + "&AccNo=" + txtAccNo.Text + "&AsOnDate=" + txtAsOnDate.Text + "&Flag=" + RButton + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptChequeIssueRegister.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbtnAllProd.Checked)
            {
                RButton = "A";
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Chq_Issu_reg _" + txtAsOnDate.Text +  "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?BCode=" + Session["BRCD"].ToString() + "&AsOnDate=" + txtAsOnDate.Text + "&Flag=" + RButton + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptChequeIssueRegister.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}