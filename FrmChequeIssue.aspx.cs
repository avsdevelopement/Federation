using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmChequeIssue : System.Web.UI.Page
{
    ClsChequeIssue ci = new ClsChequeIssue();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Flag"] = Request.QueryString["Flag"].ToString();
        BindGrid();
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["BRCD"].ToString();
            if (ViewState["Flag"].ToString() == "AD")
            {
                btnSubmit.Visible = true;
                btnModify.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                btnModify.Visible = true;
                btnSubmit.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnAuthorize.Visible = true;
                btnDelete.Visible = false;
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnAuthorize.Visible = false;
                btnDelete.Visible = true;
            }
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = ci.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), "");

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
                string[] AC = ci.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
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
                DT = ci.GetCustName(txtProdCode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtAccName.Text = DT.Rows[0]["CustName"].ToString();
                }
                txtNoLeaves.Focus();
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

    protected void txtInstFrom_TextChanged(object sender, EventArgs e)
    {
        //int RC = ci.CHECKALLOCATED();
        txtInstTo.Text = (Convert.ToInt32(txtInstFrom.Text) + (Convert.ToInt32(txtNoLeaves.Text) - 1)).ToString();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = ci.ChequeIssueInst(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtAccName.Text.ToString(), txtNoLeaves.Text.ToString(), txtInstFrom.Text, txtInstTo.Text, Session["MID"].ToString(), conn.PCNAME().ToString(), Session["EntryDate"].ToString());

            if (cnt > 0)
            {
                BindGrid();
                
                lblMessage.Text = "Successfully Inserted..!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeBook_Add _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = ci.ChequeIssueAth(ViewState["id"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());

            if (cnt > 0)
            {
                BindGrid();
              
                lblMessage.Text = "Successfully Authorised..!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeBook_Autho _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
            }
            else
            {
                lblMessage.Text = "Sorry Same User Not Authorised..!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = ci.ChequeIssueModfy(ViewState["id"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtProdName.Text.ToString(), txtAccNo.Text.ToString(), txtAccName.Text.ToString(), txtNoLeaves.Text.ToString(), txtInstFrom.Text, txtInstTo.Text);

            if (cnt > 0)
            {
                BindGrid();
                
                lblMessage.Text = "Successfully Modified..!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeBook_Mod _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CheckStage();
            int cnt = ci.ChequeIssueDel(ViewState["id"].ToString(), Session["BRCD"].ToString());

            if (cnt > 0)
            {
                BindGrid();
               
                lblMessage.Text = "Successfully Deleted..!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ChequeBook_Del _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CheckStage()
    {
        try
        {
            string ST = ci.GetStage1(Session["BRCD"].ToString(), txtProdCode.Text, txtAccNo.Text, ViewState["id"].ToString());
            if (ST == "1003")
            {
                WebMsgBox.Show("Record Already Authorize......!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        int Result;
        Result = ci.BindGrid(grdChequeIssue, Session["BRCD"].ToString());
    }

    protected void grdChequeIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeIssue.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData()
    {
        txtProdCode.Text = "";
        txtProdName.Text = "";
        txtAccNo.Text = "";
        txtAccName.Text = "";
        txtNoLeaves.Text = "";
        txtInstFrom.Text = "";
        txtInstTo.Text = "";
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                LinkButton objlink = (LinkButton)sender;
                string id = objlink.CommandArgument;
                ViewState["id"] = id;
                callData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void callData()
    {
        try
        {
            CheckStage();
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = ci.GetInfo(Session["BRCD"].ToString(), ViewState["id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtProdCode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    txtProdName.Text = DT.Rows[0]["SUBGLNAME"].ToString();
                    txtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                    txtAccName.Text = DT.Rows[0]["ACCNAME"].ToString();
                    txtNoLeaves.Text = DT.Rows[0]["NOOFLEAVES"].ToString();
                    txtInstFrom.Text = DT.Rows[0]["FSERIES"].ToString();
                    txtInstTo.Text = DT.Rows[0]["TSERIES"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtNoLeaves_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["Flag"].ToString() == "MD")
        {
            txtInstTo.Text = (Convert.ToInt32(txtInstFrom.Text) + (Convert.ToInt32(txtNoLeaves.Text) - 1)).ToString();
        }
    }
}