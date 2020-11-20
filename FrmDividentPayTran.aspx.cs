using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmDividentPayTran : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    Cls_RecoBindDropdown RBD = new Cls_RecoBindDropdown();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();
    ClsDividentPayTran DP = new ClsDividentPayTran();
    ClsBindDropdown BD = new ClsBindDropdown();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();
    DataTable DT = new DataTable();
    string sResult = "";
    double Result1 = 0;
    int Result = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnSubmit.Visible = true;
                btnPost.Visible = false;
                txtBrCode.Text = Session["BRCD"].ToString();
                AutoGlName.ContextKey = txtBrCode.Text.ToString();
                txtBrName.Text = DP.GetBranchName(txtBrCode.Text.ToString());
                txtAsOnDate.Text = Session["EntryDate"].ToString();
                BindRecDiv();
                txtBrCode.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change Events

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() != "")
            {
                string BrName = DP.GetBranchName(txtBrCode.Text.ToString());
                if (BrName != null || BrName != "")
                {
                    AutoGlName.ContextKey = txtBrCode.Text.ToString();
                    txtBrName.Text = BrName;

                    txtProdCode.Focus();
                    return;
                }
                else
                {
                    txtBrName.Text = "";
                    txtBrCode.Text = "";
                    txtBrCode.Focus();
                    WebMsgBox.Show("Enter valid branch code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtBrName.Text = "";
                txtBrCode.Text = "";
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = DP.GetProduct(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), txtProdCode.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();

                    txtAsOnDate.Focus();
                }
                else
                {
                    txtProdCode.Text = "";
                    txtProdName.Text = "";
                    txtProdCode.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
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
            string[] custnob = txtProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtProdName.Text = custnob[0].ToString();
                    ViewState["GlCode"] = custnob[1].ToString();
                    txtProdCode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                    txtAsOnDate.Focus();
                }
                else
                {
                    txtProdCode.Text = "";
                    txtProdName.Text = "";
                    txtProdCode.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changed

    protected void DdlRecDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRecDept();
            DdlRecDept.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter procudt code first", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter as on date first", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else
            {
                DT = DP.BindData(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAsOnDate.Text.ToString(), "Trial", DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString(), txtProdCode2.Text.ToString(), Session["MID"].ToString());
                if (DT.Rows.Count > 0)
                {
                    btnSubmit.Visible = false;
                    btnPost.Visible = true;
                    btnPost.Focus();

                    grdDevident.DataSource = DT;
                    grdDevident.DataBind();
                }
                else
                {
                    btnSubmit.Visible = true;
                    btnPost.Visible = false;

                    DdlRecDept.Focus();
                    grdDevident.DataSource = null;
                    grdDevident.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int tmp = 0;
            string SetNo = "", RefId = "";
            double DrAmount = 0;

            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter procudt code first", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter as on date first", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else
            {
                //DT = CreateFirst(DT);
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RefId"] = (Convert.ToInt32(RefId) + 1).ToString();

                foreach (GridViewRow gvRow in grdDevident.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("chk")).Checked)
                    {
                        Result = DP.InsertData(txtBrCode.Text.ToString(), txtProdCode2.Text.ToString(), txtAsOnDate.Text.ToString(), SetNo, ((Label)gvRow.FindControl("lblSrNum")).Text, Session["MID"].ToString());
                        DT = DP.DrInsertData(txtBrCode.Text.ToString(), txtProdCode2.Text.ToString(), txtAsOnDate.Text.ToString(), SetNo, ((Label)gvRow.FindControl("lblSrNum")).Text, Session["MID"].ToString());

                        DrAmount = Convert.ToDouble(DrAmount.ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Total"].ToString()) < 0 ? "0" : DT.Rows[0]["Total"].ToString());

                        //DT.Rows.Add(txtBrCode.Text.ToString(), Session["EntryDate"].ToString(), ((Label)gvRow.FindControl("lblGlcode")).Text, ((Label)gvRow.FindControl("lblSubglcode")).Text, 
                        //    ((Label)gvRow.FindControl("lblAccNo")).Text, ((Label)gvRow.FindControl("lblCustNo")).Text, ((Label)gvRow.FindControl("lblCustName")).Text,
                        //    ((Label)gvRow.FindControl("lblBalance")).Text, ((Label)gvRow.FindControl("lblIMSBalance")).Text, ((Label)gvRow.FindControl("lblAGMBalance")).Text, 
                        //    txtProdCode2.Text.ToString());

                        //if (Result > 0)
                        //{
                        //    Result = DP.Authorized(DT.Rows[tmp]["BrCode"].ToString(), DT.Rows[tmp]["EntryDate"].ToString(), DT.Rows[tmp]["SubGlCode2"].ToString(), DT.Rows[tmp]["SubGlCode2"].ToString(), "0",
                        //        "Divident Acc BrCode " + txtBrCode.Text.ToString() + "/" + DT.Rows[tmp]["SubGlCode"].ToString() + "", "", DT.Rows[tmp]["Balance"].ToString(), "1", "7", "TRF", SetNo, "1003",
                        //        Session["MID"].ToString(), "0", Session["MID"].ToString(), "PAYDIV", DT.Rows[tmp]["CustNo"].ToString(), DT.Rows[tmp]["CustName"].ToString(), ViewState["RefId"].ToString());
                        //}
                        tmp++;
                    }
                }
                if (Result > 0)
                {
                    Result = DP.InsertData_DR(txtBrCode.Text.ToString(), txtProdCode2.Text.ToString(), txtAsOnDate.Text.ToString(), SetNo, "0", Session["MID"].ToString(), DrAmount.ToString());

                    ClearAllData();
                    txtProdCode.Focus();
                    WebMsgBox.Show("Successfully Posted with set no : " + SetNo, this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Divident_Transfer_SetNo" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        DT = new DataTable();
        try
        {
            int tmp = 0;
            string SetNo = "", RefId = "";

            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter procudt code first", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter as on date first", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else
            {
                System.Text.StringBuilder Sb = new System.Text.StringBuilder();
                Label Str_Id;
                foreach (GridViewRow gvRow in grdDevident.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("chk")).Checked)
                    {
                        Str_Id = (Label)gvRow.FindControl("lblSrNum");
                        if (Sb.Length == 0)
                            Sb.Append(Str_Id.Text.ToString());
                        else
                            Sb.Append("," + Str_Id.Text.ToString());
                    }
                }

                string redirectURL = "FrmRView.aspx?FLAG=0&ID=" + Sb.ToString() + "&rptname=RptDivPayTrans.rdlc" + "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //  Added by amol ON 20/09/2018 (as per ambika mam requirement)
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DT = new DataTable();
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter procudt code first", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter as on date first", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else
            {
                System.Text.StringBuilder Sb = new System.Text.StringBuilder();
                Label Str_Id;
                foreach (GridViewRow gvRow in grdDevident.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("chk")).Checked)
                    {
                        Str_Id = (Label)gvRow.FindControl("lblSrNum");
                        if (Sb.Length == 0)
                            Sb.Append(Str_Id.Text.ToString());
                        else
                            Sb.Append("," + Str_Id.Text.ToString());
                    }
                }

                string redirectURL = "FrmRView.aspx?FLAG=1&ID=" + Sb.ToString() + "&rptname=RptDivPayTrans.rdlc" + "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAllData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Function

    public void BindRecDept()
    {
        try
        {
            BD1.BRCD = txtBrCode.Text;
            BD1.Ddl = DdlRecDept;
            BD1.RECDIV = DdlRecDiv.SelectedValue.ToString();
            BD1.FnBL_BindRecDept(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindRecDiv()
    {
        try
        {
            BD1.BRCD = txtBrCode.Text;
            BD1.Ddl = DdlRecDiv;
            BD1.FnBL_BindRecDiv(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private DataTable CreateFirst(DataTable dt)
    {
        dt.Columns.Add("BrCode");
        dt.Columns.Add("EntryDate");
        dt.Columns.Add("GlCode");
        dt.Columns.Add("SubGlCode");
        dt.Columns.Add("AccNo");
        dt.Columns.Add("CustNo");
        dt.Columns.Add("CustName");
        dt.Columns.Add("Balance");
        dt.Columns.Add("IMSBalance");
        dt.Columns.Add("AGMBalance");
        dt.Columns.Add("SubGlCode2");
        dt.AcceptChanges();
        return dt;
    }

    public void ClearAllData()
    {
        try
        {
            btnSubmit.Visible = true;
            btnPost.Visible = false;

            txtBrCode.Text = Session["BRCD"].ToString();
            AutoGlName.ContextKey = txtBrCode.Text.ToString();
            txtBrName.Text = DP.GetBranchName(txtBrCode.Text.ToString());
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAsOnDate.Text = Session["EntryDate"].ToString();
            DdlRecDiv.SelectedValue = "0";
            DdlRecDept.SelectedValue = "0";
            txtProdCode2.Text = "";

            grdDevident.DataSource = null;
            grdDevident.DataBind();

            txtBrCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}

