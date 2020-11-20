using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class FrmInvStatement : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsInvData SV = new ClsInvData();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AutoProdName.ContextKey = Session["BRCD"].ToString();
                BindBranch(ddlBrName);
                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtBrCode.Text = Session["BRCD"].ToString();

                txtProdCode.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        } 
    }
    public void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = SV.GetProductName(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtProdName.Text = CT[0].ToString();
                    txtProdCode.Text = CT[2].ToString();
                    ViewState["GlCode"] = CT[1].ToString();
                    txtAccNo.Text = "";
                    txtAccName.Text = "";

                    AutoCustName.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdCode.Text.ToString();
                    sResult = SV.GetAccYN(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

                    if (ViewState["GlCode"].ToString() == "3")
                    {
                        DivAmount.Visible = true;
                        DivPeriod.Visible = true;
                        DivIntRate.Visible = true;
                        DivMaturity.Visible = true;
                        DivOverDue.Visible = true;
                        DivReceipt.Visible = false;
                        LblName1.InnerText = "Loan Amt";
                        LblName4.InnerText = "Inst Amt";
                    }
                    else if (ViewState["GlCode"].ToString() == "5")
                    {
                        DivAmount.Visible = true;
                        DivPeriod.Visible = true;
                        DivIntRate.Visible = true;
                        DivMaturity.Visible = true;
                        DivOverDue.Visible = false;
                        DivReceipt.Visible = true;
                        LblName1.InnerText = "DepositAmt";
                        LblName4.InnerText = "MaturityAmt";
                    }
                    else
                    {
                        DivAmount.Visible = false;
                        DivPeriod.Visible = false;
                        DivIntRate.Visible = false;
                        DivMaturity.Visible = false;
                        DivOverDue.Visible = false;
                        DivReceipt.Visible = false;
                    }

                    //  Show clear and unclear blance
                    txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                    txtUnClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                    if (Convert.ToDouble(ViewState["GlCode"].ToString()) >= 100 && sResult != "Y")
                    {
                        txtAccNo.Focus();
                    }
                    else
                    {
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    txtProdCode.Text = "";
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                    txtProdCode.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtAccNo.Text = "";
                txtAccName.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
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
            Clear();
            sResult = txtProdName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[2].ToString();
                ViewState["GlCode"] = CT[1].ToString();
                txtAccNo.Text = "";
                txtAccName.Text = "";
                Clear();

                AutoCustName.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdCode.Text.ToString();
                sResult = SV.GetAccYN(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

                if (ViewState["GlCode"].ToString() == "3")
                {
                    DivAmount.Visible = true;
                    DivPeriod.Visible = true;
                    DivIntRate.Visible = true;
                    DivMaturity.Visible = true;
                    DivOverDue.Visible = true;
                    DivReceipt.Visible = false;
                    LblName1.InnerText = "Loan Amt";
                    LblName4.InnerText = "Inst Amt";
                }
                else if (ViewState["GlCode"].ToString() == "5")
                {
                    DivAmount.Visible = true;
                    DivPeriod.Visible = true;
                    DivIntRate.Visible = true;
                    DivMaturity.Visible = true;
                    DivOverDue.Visible = false;
                    DivReceipt.Visible = true;
                    LblName1.InnerText = "DepositAmt";
                    LblName4.InnerText = "MaturityAmt";
                }
                else
                {
                    DivAmount.Visible = false;
                    DivPeriod.Visible = false;
                    DivIntRate.Visible = false;
                    DivMaturity.Visible = false;
                    DivOverDue.Visible = false;
                    DivReceipt.Visible = false;
                }

                //  Show clear and unclear blance
                txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                txtUnClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                if (Convert.ToDouble(ViewState["GlCode"].ToString()) >= 100 && sResult != "Y")
                {
                    txtAccNo.Focus();
                }
                else
                {
                    txtAccNo.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtAccNo.Text = "";
                txtAccName.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtProdCode.Focus();
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
            Clear();
            sResult = SV.GetAccountName(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtAccName.Text = CT[0].ToString();
                    txtAccNo.Text = CT[1].ToString();

                    sResult = SV.GetAccDetails(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
                    string[] CT1 = sResult.Split('_');

                    if (CT1.Length > 0)
                    {
                        txtAccOpenDate.Text = CT1[0].ToString();
                        txtAccStatus.Text = CT1[1].ToString();
                        txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        txtUnClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();

                        if (ViewState["GlCode"].ToString() == "3")
                        {
                            DT = SV.AccountInfo(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (DT.Rows.Count > 0)
                            {
                                txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString();
                                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                                txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                                txtInstallmentAmt.Text = DT.Rows[0]["InstallMent"].ToString();
                                //txtOverDue.Text = SV.OVerdueAmt(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Convert.ToDouble(txtClearBal.Text.ToString()), Session["EntryDate"].ToString()).ToString();
                            }
                        }
                        else
                        {
                            DT = SV.AccountInfo(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (DT.Rows.Count > 0)
                            {
                                txtLimitAmt.Text = DT.Rows[0]["PrnAmt"].ToString();
                                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                                txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                                txtInstallmentAmt.Text = DT.Rows[0]["MaturityAmt"].ToString();
                            }
                        }
                    }
                    txtFromDate.Focus();
                }
                else
                {
                    WebMsgBox.Show("Sorry customer not exists ...!!", this.Page);
                    txtAccName.Text = "";
                    txtAccNo.Text = "";
                    txtAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Sorry account not exists ...!!", this.Page);
                txtAccName.Text = "";
                txtAccNo.Text = "";
                txtAccNo.Focus();
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
            Clear();
            sResult = txtAccName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtAccName.Text = CT[0].ToString();
                txtAccNo.Text = CT[1].ToString();

                sResult = SV.GetAccDetails(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
                string[] CT1 = sResult.Split('_');

                if (CT1.Length > 0)
                {
                    txtAccOpenDate.Text = CT1[0].ToString();
                    txtAccStatus.Text = CT1[1].ToString();
                    txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                    txtUnClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();

                    if (ViewState["GlCode"].ToString() == "3")
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "3", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                            txtInstallmentAmt.Text = DT.Rows[0]["InstallMent"].ToString();
                        }
                    }
                    else 
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "5", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["PrnAmt"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                            txtInstallmentAmt.Text = DT.Rows[0]["MaturityAmt"].ToString();
                        }
                    }
                }
                txtFromDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Sorry customer not exists ...!!", this.Page);
                txtAccName.Text = "";
                txtAccNo.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            {
                    DT = new DataTable();
                    DT = SV.GetStatementView(txtBrCode.Text.Trim().ToString(), ViewState["GlCode"].ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtRecNo.Text.ToString() == "" ? "0" : txtRecNo.Text.ToString(), txtFromDate.Text.ToString(), txtToDate.Text.ToString());
                    if (DT.Rows.Count > 0)
                    {
                        grdStmtGrd.DataSource = DT;
                        grdStmtGrd.DataBind();

                        divStmtGrd.Visible = true;
                        divSavingStmt.Visible = false;


                        //Calculate Sum and display in Footer Row
                        decimal PCreditTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("PCredit"));
                        decimal PDebitTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("PDebit"));
                        decimal ICreditTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("ICredit"));
                        decimal IDebitTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("IDebit"));
                        grdStmtGrd.FooterRow.Cells[1].Text = "Total";
                        grdStmtGrd.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        grdStmtGrd.FooterRow.Cells[4].Text = PCreditTotal.ToString("N2");
                        grdStmtGrd.FooterRow.Cells[5].Text = PDebitTotal.ToString("N2");
                        grdStmtGrd.FooterRow.Cells[8].Text = ICreditTotal.ToString("N2");
                        grdStmtGrd.FooterRow.Cells[9].Text = IDebitTotal.ToString("N2");
                    }
                    else
                    {

                        grdStmtGrd.DataSource = null;
                        grdStmtGrd.DataBind();

                        divStmtGrd.Visible = true;
                        divSavingStmt.Visible = false;
                        GRdMonthly.Visible = false;
                    }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnLazerReport_Click(object sender, EventArgs e)
    {
        string redirectURL = "FrmReportViewer.aspx?BC=" + txtBrCode.Text.Trim().ToString() + "&GC=" + ViewState["GlCode"].ToString() + "&PC=" + txtProdCode.Text.Trim().ToString() + "&AN=" + txtAccNo.Text.Trim().ToString() + "&FD=" + txtFromDate.Text.ToString() + "&TD=" + txtToDate.Text.ToString() + "&LAMT=" + txtLimitAmt.Text.ToString() + "&InstAmt=" + txtInstallmentAmt.Text.ToString() + "&IRate=" + txtIntRate.Text.ToString() + "&rptname=RptInvStatementView.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearAll()
    {
        try
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtClearBal.Text = "";
            txtUnClearBal.Text = "";
            txtAccOpenDate.Text = "";
            txtAccStatus.Text = "";
            txtLimitAmt.Text = "";
            txtPeriod.Text = "";
            txtIntRate.Text = "";
            txtInstallmentAmt.Text = "";
            txtOverDue.Text = "";
            txtFromDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            txtToDate.Text = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkSelect1_Click(object sender, EventArgs e)
    {

    }
    protected void lnkSelect2_Click(object sender, EventArgs e)
    {

    }
    protected void grdStmtGrd_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridDataBound();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridDataBound()
    {
        try
        {
            GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 4;
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Principle";
            cell.ColumnSpan = 4;
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 4;
            cell.Text = "Interest";
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Total";
            row1.Controls.Add(cell);

            row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            grdStmtGrd.HeaderRow.Parent.Controls.AddAt(0, row1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {
        try
        {
            //  Clear Grid
            grdSaving.DataSource = null;
            grdSaving.DataBind();

            //  Clear Grid
            grdStmtGrd.DataSource = null;
            grdStmtGrd.DataBind();

            //  Clear all fields
            txtFromDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            txtToDate.Text = Session["EntryDate"].ToString();
            txtClearBal.Text = "";
            txtUnClearBal.Text = "";
            txtAccOpenDate.Text = "";
            txtAccStatus.Text = "";
            txtLimitAmt.Text = "";
            txtPeriod.Text = "";
            txtIntRate.Text = "";
            txtInstallmentAmt.Text = "";
            txtOverDue.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GRdMonthly_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            //GridDataBound();
            LinkButton objlnk = (LinkButton)sender;
            string[] AT = objlnk.CommandArgument.ToString().Split('_');
            ViewState["BrCode"] = AT[0].ToString();
            ViewState["EntryDate"] = AT[1].ToString();
            ViewState["SetNo"] = AT[2].ToString();

            if ((Convert.ToDouble(ViewState["BrCode"].ToString()) > 0) && (Convert.ToDouble(ViewState["SetNo"].ToString()) > 0) && (ViewState["EntryDate"].ToString() != ""))
            {
                DT = new DataTable();
                DT = SV.GetVoucherDetails(ViewState["BrCode"].ToString(), ViewState["EntryDate"].ToString(), ViewState["SetNo"].ToString());

                if (DT.Rows.Count > 0)
                {
                    grdVoucherDetails.DataSource = DT;
                    grdVoucherDetails.DataBind();

                    //Calculate Sum and display in Footer Row
                    decimal CrTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("Credit"));
                    decimal DrTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("Debit"));
                    grdVoucherDetails.FooterRow.Cells[0].Text = "Total";
                    grdVoucherDetails.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    grdVoucherDetails.FooterRow.Cells[8].Text = CrTotal.ToString("N2");
                    grdVoucherDetails.FooterRow.Cells[9].Text = DrTotal.ToString("N2");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                }
                else
                {
                    grdVoucherDetails.DataSource = null;
                    grdVoucherDetails.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}