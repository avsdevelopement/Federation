using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTransPrint : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsRptGL GL = new ClsRptGL();
    scustom customcs = new scustom();
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
            txtfromdate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
               
            txttodate.Text = Session["EntryDate"].ToString();
            //BD.BindTRX(ddlTRx);
            AutoCompleteExtender1.ContextKey = Session["BRCD"].ToString();
            AutoCompleteExtender2.ContextKey = Session["BRCD"].ToString()+"_0";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
            txtAmount.Text = "0";
        }
    }
    #region Text Change Event
    protected void txtBranchCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GL.GetBranch(txtBranchCode.Text);
            if (dt.Rows.Count > 0)
            {
                txtBranchName.Text = dt.Rows[0]["MIDNAME"].ToString();

                AutoCompleteExtender1.ContextKey = txtBranchCode.Text;
                AutoCompleteExtender2.ContextKey = txtBranchCode.Text+"_0";
                Flag.Value = "1";
                BRCD.Value = txtBranchCode.Text;
            }
            else
            {
                WebMsgBox.Show("Sorry, No Record Fount!!!", this.Page);
                txtBranchCode.Text = "";
                txtBranchCode.Focus();
                Flag.Value = "0";
                BRCD.Value = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtBranchName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtBranchName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtBranchName.Text = CT[0].ToString();
                txtBranchCode.Text = CT[1].ToString();
                AutoCompleteExtender1.ContextKey = txtBranchCode.Text;
                AutoCompleteExtender2.ContextKey = txtBranchCode.Text + "_0";
                Flag.Value = "1";
                BRCD.Value = txtBranchCode.Text;
                //TxtGLCD.Text = CT[2].ToString();

                if (txtBranchName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtBranchCode.Text = "";
                    txtBranchCode.Focus();
                    Flag.Value = "0";
                    BRCD.Value = Session["BRCD"].ToString();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtProdCode.Text == "")
            {
                txtProdCode.Text = "";
                goto ext;
            }
            int result = 0;
            string GlS1;
            int.TryParse(txtProdCode.Text, out result);
            txtProdName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(txtProdCode.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                if (txtBranchCode.Text == "")
                {
                    AutoCompleteExtender2.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
                    Flag.Value = "0";
                    BRCD.Value = Session["BRCD"].ToString();
                }
                else
                {
                    AutoCompleteExtender2.ContextKey = txtBranchCode.Text + "_" + txtProdCode.Text;
                    Flag.Value = "1";
                    BRCD.Value = txtBranchCode.Text;
                }

            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                txtProdCode.Text = "";
                txtProdCode.Focus();
                Flag.Value = "0";
                BRCD.Value = Session["BRCD"].ToString();
            }


        ext: ;
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
            string custno = txtProdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[1].ToString();

                if (txtBranchCode.Text == "")
                {
                    AutoCompleteExtender2.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
                    Flag.Value = "0";
                    BRCD.Value = Session["BRCD"].ToString();
                }
                else
                {
                    AutoCompleteExtender2.ContextKey = txtBranchCode.Text + "_" + txtProdCode.Text;
                    Flag.Value = "1";
                    BRCD.Value = txtBranchCode.Text;
                }
                //TxtGLCD.Text = CT[2].ToString();

                if (txtProdName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtProdCode.Text = "";
                    txtProdCode.Focus();
                    Flag.Value = "0";
                    BRCD.Value = Session["BRCD"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtAccno_TextChanged(object sender, EventArgs e)
    {

        try
        {
           DataTable AT=new DataTable ();
          
               AT = BD.GetUserCode(txtAccno.Text);
            if (AT != null)
            {
                    txtAccName.Text = AT.Rows[0]["LOGINCODE"].ToString();
            }
            else
            {
                lblMessage.Text = "Enter valid user code...!!";
                ModalPopup.Show(this.Page);
                txtAccno.Focus();
                return;
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
                txtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion 

    #region Button Click
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Scroll" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + txtfromdate.Text.ToString() + "&TDate=" + txttodate.Text.ToString() + "&BRCD=" + BRCD.Value + "&Flag=" + Flag.Value + "&ProdCode=" + txtProdCode.Text + "&AccNo=" + txtAccno.Text + "&Amount=" + txtAmount.Text + "&TRXType=" + ddlTRXTYPE.SelectedValue + "&Activity=" + ddlTRx.SelectedValue + "&UNAME=" + Session["UserName"].ToString() + "&rptname=RptTransfer.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    #endregion

    #region SelectedIndexChange
    protected void rbtBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtBranch.SelectedValue == "1")
        {
            txtBranchCode.Visible = true;
            txtBranchName.Visible = true;
            CustList1.Visible = true;
        }
        else
        {
            txtBranchCode.Visible = false;
            txtBranchName.Visible = false;
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
            CustList1.Visible = false;
        }
    }
    protected void rbtProd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtProd.SelectedValue == "1")
        {
            txtProdCode.Visible = true;
            txtProdName.Visible = true;
            Div1.Visible = true;
        }
        else
        {
            txtProdCode.Visible = false;
            txtProdName.Visible = false;
            txtProdCode.Text = "";
            txtProdName.Text = "";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
            Div1.Visible = false;
        }
    }
    protected void rbtTRX_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtTRX.SelectedValue == "1")
        {
            ddlTRx.Visible = true;
            DivDDS.Visible = true;
            DivInterest.Visible = true;
        }
        else
        {
            ddlTRx.Visible = false;
            DivDDS.Visible = false;
            DivInterest.Visible = false;
            ddlTRx.SelectedValue = "0";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
        }
    }
    protected void rbtAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtAmount.SelectedValue == "1")
        {
            txtAmount.Visible = true;
        }
        else
        {
            txtAmount.Visible = false;
            txtAmount.Text = "0";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
        }
    }
    protected void rbtUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtUser.SelectedValue == "1")
        {
            txtAccno.Visible = true;
            txtAccName.Visible = true;
            Div2.Visible = true;
        }
        else
        {
            txtAccno.Visible = false;
            txtAccName.Visible = false;
            txtAccno.Text = "";
            txtAccName.Text = "";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
            Div2.Visible = false;
        }
    }
    protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtType.SelectedValue == "1")
        {
            ddlTRXTYPE.Visible = true;
        }
        else
        {
            ddlTRXTYPE.Visible = false;
            ddlTRXTYPE.SelectedValue = "0";
            BRCD.Value = Session["BRCD"].ToString();
            Flag.Value = "0";
        }
    }
    #endregion
   
}