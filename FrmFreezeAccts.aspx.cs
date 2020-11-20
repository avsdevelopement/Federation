using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmFreezeAccts : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFreezeAccts FA = new ClsFreezeAccts();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            txtProdCode.Focus();
            ViewState["Flag"] = Request.QueryString["Flag"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();

            if (Request.QueryString["Flag"].ToString() == "FZ")
            {
                btnFreez.Visible = true;
                btnAuthorize.Visible = false;
                btnUnfreez.Visible = false;
            }
            else if (Request.QueryString["Flag"].ToString() == "AT")
            {
                BindGrid();
                btnFreez.Visible = false;
                btnAuthorize.Visible = true;
                btnUnfreez.Visible = false;
            }
            else if (Request.QueryString["Flag"].ToString() == "UF")
            {
                BindGrid();
                btnFreez.Visible = false;
                btnAuthorize.Visible = false;
                btnUnfreez.Visible = true;
            }
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = FA.GetAccType(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString());

            string[] GL = BD.GetAccTypeGL(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString()).Split('_');
            txtProdName.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();

            TxtAccno.Focus();
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
                string[] GLS = BD.GetAccTypeGL(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text.Trim().ToString() + "_" + ViewState["DRGL"].ToString();

                if (txtProdName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtProdCode.Text = "";
                    txtProdCode.Focus();
                }
                else
                    TxtAccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(TxtAccno.Text.Trim().ToString(), Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString());

            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise...!!";
                ModalPopup.Show(this.Page);
                TxtAccName.Text = "";
                txtProdCode.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
                return;
            }
            else
            {
                if (ViewState["Flag"].ToString() == "FZ")
                {
                    AT = FA.ChkExists(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString());

                    if (Convert.ToInt32(AT) > 0)
                    {
                        TxtAccno.Text = "";
                        TxtAccName.Text = "";
                        lblMessage.Text = "Account Allready Freeze...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else
                    {
                        DT = new DataTable();
                        DT = FA.GetCustName(txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString(), Session["BRCD"].ToString());

                        if (DT.Rows.Count > 0)
                        {
                            TxtAccName.Text = DT.Rows[0]["CustName"].ToString();
                        }
                        rbtnType.Focus();
                    }
                }
                else
                {
                    DT = new DataTable();
                    DT = FA.GetCustName(txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString(), Session["BRCD"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        TxtAccName.Text = DT.Rows[0]["CustName"].ToString();
                    }
                    rbtnType.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            string CUNAME = TxtAccName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                if (ViewState["Flag"].ToString() == "FZ")
                {
                    AT = FA.ChkExists(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                    if (Convert.ToInt32(AT) > 0)
                    {
                        TxtAccno.Text = "";
                        TxtAccName.Text = "";
                        lblMessage.Text = "Account Allready Freeze...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else
                    {
                        TxtAccName.Text = custnob[0].ToString();
                        TxtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                        rbtnType.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Invalid Account Number...!!";
                ModalPopup.Show(this.Page);
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
        if (Request.QueryString["Flag"].ToString() == "UF")
            FA.BindGrid1(grdFreezeAcclst, Session["BRCD"].ToString());
        else
            FA.BindGrid(grdFreezeAcclst, Session["BRCD"].ToString());
    }

    protected void btnFreez_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue.ToString() == "1")
            {
                if (txtAmount.Text == "")
                {
                    lblMessage.Text = "Enter credit amount ...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }

                Result = FA.FreezeAccInst(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString(), rbtnType.SelectedValue.ToString(), Convert.ToDouble(txtAmount.Text.Trim().ToString()), txtReason.Text.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
            }
            else if (rbtnType.SelectedValue.ToString() == "2")
            {
                if (txtAmount.Text == "")
                {
                    lblMessage.Text = "Enter debit amount ...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }

                Result = FA.FreezeAccInst(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString(), rbtnType.SelectedValue.ToString(), Convert.ToDouble(txtAmount.Text.Trim().ToString()), txtReason.Text.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
            }
            else
            {
                Result = FA.FreezeAccInst(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString(), rbtnType.SelectedValue.ToString(), 0, txtReason.Text.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
            }

            if (Result > 0)
            {
                BindGrid();
                ClearData();
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Freeze _" + txtProdCode.Text + "_" + TxtAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                WebMsgBox.Show("Accounts Freeze Successfully ...!!", this.Page);
                return;
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
            int cnt = FA.FreezeAccAth(Session["BRCD"].ToString(), txtProdCode.Text, TxtAccno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());

            if (cnt > 0)
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Freeze _Autho_" + txtProdCode.Text + "_" + TxtAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                BindGrid();
                lblMessage.Text = "Successfully Authorised..!";
                ModalPopup.Show(this.Page);
                return;
            }
            else
            {
                lblMessage.Text = "Sorry Same User Not Authorised..!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnUnfreez_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt = FA.FreezeAccDel(Session["BRCD"].ToString(), txtProdCode.Text, TxtAccno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());

            if (cnt > 0)
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Freeze _Unfreeze_" + txtProdCode.Text + "_" + TxtAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                BindGrid();
                lblMessage.Text = "Accounts Un-Freeze Successfully..!";
                ModalPopup.Show(this.Page);
                return;
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
            if (Request.QueryString["Flag"].ToString() == "AT")
            {
                string ST = FA.GetStage(Session["BRCD"].ToString(), ViewState["PrCode"].ToString().Trim(), ViewState["PrCode"].ToString().Trim());
                if (ST == "1003")
                {
                    lblMessage.Text = "Record Already Authorize...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                string ST = FA.GetStage(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), TxtAccno.Text.Trim().ToString());
                if (ST == "1004")
                {
                    lblMessage.Text = "Account Already Un-Freezed...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
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
        TxtAccno.Text = "";
        TxtAccName.Text = "";
        txtReason.Text = "";
        txtAmount.Text = "";
    }

    protected void grdFreezeAcclst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFreezeAcclst.PageIndex = e.NewPageIndex;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue.ToString() != "3")
                DivAmount.Visible = true;
            else
                DivAmount.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "FZ")
            {
                LinkButton objlink = (LinkButton)sender;
                string id = objlink.CommandArgument;

                string[] PrAcc = id.ToString().Split('_');
                ViewState["PrCode"] = PrAcc[0].ToString();
                ViewState["AccNo"] = PrAcc[1].ToString();

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
                DT = new DataTable();
                DT = FA.GetInfo(Session["BRCD"].ToString(), ViewState["PrCode"].ToString(), ViewState["AccNo"].ToString());

                if (DT.Rows.Count > 0)
                {
                    txtProdCode.Text = DT.Rows[0]["SubGlCode"].ToString();
                    txtProdName.Text = DT.Rows[0]["GlName"].ToString();

                    TxtAccno.Text = DT.Rows[0]["AccNo"].ToString();
                    TxtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();

                    if (DT.Rows[0]["TrxType"].ToString() == "1")
                    {
                        rbtnType.SelectedIndex = 1;
                        DivAmount.Visible = true;
                        txtAmount.Text = DT.Rows[0]["Amount"].ToString();
                    }
                    else if (DT.Rows[0]["TrxType"].ToString() == "2")
                    {
                        rbtnType.SelectedIndex = 2;
                        DivAmount.Visible = false;
                    }
                    else if (DT.Rows[0]["TrxType"].ToString() == "3")
                    {
                        rbtnType.SelectedIndex = 3;
                        DivAmount.Visible = false;
                    }

                    txtReason.Text = DT.Rows[0]["Description"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}