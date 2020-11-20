using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCommonPatch : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    scustom cc = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsTableCommPatch RO = new ClsTableCommPatch();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    DataTable dtFirst = new DataTable();
    ClsBindDropdown DD = new ClsBindDropdown();

    string sResult = "";
    int Result = 0;
    string FL = "";
    string STR = "";
    int Res = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                DD.BindtableActivity(DdlAccActivity);
                string YEAR = "";
                YEAR = DateTime.Now.Date.Year.ToString();//DT.Year.ToString();

                DivLoan.Visible = false;
                DivCalculatedData.Visible = false;

                GridView1009.Visible = false;
                GridViewLoan.Visible = false;
                GridViewAcc.Visible = false;
                GridViewGL.Visible = false;

                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
                autoglname.ContextKey = Session["BRCD"].ToString();
                // TxtBRCD.Focus();
                TxtAccType.Focus();
                //added by ankita 07/10/2017 to make user frndly
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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
                    TxtAccno.Text = "";
                    TxtAccno.Focus();
                }
                else
                {

                    DataTable DT = new DataTable();
                    DT = AST.GetCustName(TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
                    if (DT.Rows.Count > 0)
                    {
                        TxtAccHName.Text = DT.Rows[0]["CustName"].ToString();
                        //--Abhishek
                        string RES = AST.GetAccStatus(TxtBRCD.Text, TxtAccno.Text, TxtAccType.Text);
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
            }
            else
            {
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                return;
            }
            if (TxtAccHName.Text != "")
            {
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
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
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlAccActivity.SelectedValue == "Deposit_Info")
            {
                int Res = 0;
                DivCalculatedData.Visible = true;

                GridView1009.Visible = true;
                GridViewLoan.Visible = false;
                GridViewAcc.Visible = false;
                GridViewGL.Visible = false;

                FL = "Dep";
                Res = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            }
            if (DdlAccActivity.SelectedValue == "Loan_Info")
            {
                int Res = 0;
                DivLoan.Visible = true;

                GridView1009.Visible = false;
                GridViewLoan.Visible = true;
                GridViewAcc.Visible = false;
                GridViewGL.Visible = false;

                FL = "Loan";
                Res = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            }
            if (DdlAccActivity.SelectedValue == "Acc_Master")
            {
                int Res = 0;
                DivAcc.Visible = true;

                GridView1009.Visible = false;
                GridViewLoan.Visible = false;
                GridViewAcc.Visible = true;
                GridViewGL.Visible  = false;

                FL = "Acc";
                Res = RO.GetAccInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            }
            if (DdlAccActivity.SelectedValue == "GLMast")
            {
                int Res = 0;
                DivGlmast.Visible = true;

                GridView1009.Visible = false;
                GridViewLoan.Visible = false;
                GridViewAcc.Visible = false;
                GridViewGL.Visible = true;

                FL = "GLMast";
                Res = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            }
            DivVisibility();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void DivVisibility()
    {
        DivCalculatedData.Visible = false;
        DivLoan.Visible = false;
        DivAcc.Visible = false;
        DivGlmast.Visible = false;
        switch (DdlAccActivity.SelectedValue)
        {
            case "Loan_Info":
                DivLoan.Visible = true;
                break;
            case "Deposit_Info":
                DivCalculatedData.Visible = true;
                break;
            case "Acc_Master":
                DivAcc.Visible = true;
                break;
            case "GLMast":
                DivGlmast.Visible = true;
                break;
        }

    }

    protected void DdlAccActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (DdlAccActivity.SelectedValue == "Deposit_Info")
            //{
            //    int Res = 0;
            //    DivCalculatedData.Visible = true;

            //    GridView1009.Visible = true;
            //    GridViewLoan.Visible = false;
            //    GridViewAcc.Visible = false;
            //    GridViewGL.Visible = false;

            //    FL = "Dep";
            //    Res = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            //}
            //if (DdlAccActivity.SelectedValue == "Loan_Info")
            //{
            //    int Res = 0;
            //    DivLoan.Visible = true;

            //    GridView1009.Visible = false;
            //    GridViewLoan.Visible = true;
            //    GridViewAcc.Visible = false;
            //    GridViewGL.Visible = false;

            //    FL = "Loan";
            //    Res = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            //}
            //if (DdlAccActivity.SelectedValue == "Acc_Master")
            //{
            //    int Res = 0;
            //    DivAcc.Visible = true;

            //    GridView1009.Visible = false;
            //    GridViewLoan.Visible = false;
            //    GridViewAcc.Visible = true;
            //    GridViewGL.Visible = false;

            //    FL = "Acc";
            //    Res = RO.GetAccInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            //}
            //if (DdlAccActivity.SelectedValue == "GLMast")
            //{
            //    int Res = 0;
            //    DivGlmast.Visible = true;

            //    GridView1009.Visible = false;
            //    GridViewLoan.Visible = false;
            //    GridViewAcc.Visible = false;
            //    GridViewGL.Visible = true;

            //    FL = "GLMast";
            //    Res = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnUpdateAll_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            int STRRes = 0;
            ViewState["BtnType"] = "Update";
            if (ViewState["Flag"].ToString() == "MD")
            {
                InsertData("RC");
                //ClearData(); 23-03-2019
            }
            else
            {
                if (Chk_AllAutho.Checked == false)
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Dep";

                        STRRes = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (STRRes.ToString() != Session["MID"].ToString())
                        {
                            Res = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                ClearData();

                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                ClearData();
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                            ClearData();
                            return;
                        }
                    }
                    ClearData();
                }
                else if (Chk_AllAutho.Checked == true) // For All Auhtorize at a time 
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Dep";
                        Res = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (Res > 0)
                        {
                            WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                            ClearData();
                        }
                        else
                        {
                            WebMsgBox.Show("Already authorized......!", this.Page);
                            ClearData();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private void InsertData(string sFlag)
    {
        try
        {
            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);

            int tmp = 0;

            foreach (GridViewRow gvRow in GridView1009.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk")).Checked == true)
                {
                    dtFirst.Rows.Add(GridView1009.DataKeys[gvRow.DataItemIndex].Value, ((Label)gvRow.FindControl("lblBrCd")).Text, ((Label)gvRow.FindControl("lblProduct")).Text, ((Label)gvRow.FindControl("lblAccno")).Text);

                    string[] AT = dtFirst.Rows[tmp]["ID"].ToString().Split('_');
                    ViewState["Custno"] = AT[0].ToString();

                    Result = RO.InsertTrans((((Label)gvRow.FindControl("lblBrCd")).Text), (((Label)gvRow.FindControl("lblProduct")).Text), (((Label)gvRow.FindControl("lblAccno")).Text), ((TextBox)gvRow.FindControl("TxtCustno")).Text, ((TextBox)gvRow.FindControl("TxtLimit")).Text, ((TextBox)gvRow.FindControl("TxtROI")).Text, ((TextBox)gvRow.FindControl("TxtOpDate")).Text, ((TextBox)gvRow.FindControl("TxtDueDate")).Text, ((TextBox)gvRow.FindControl("TxtPeriod")).Text, ((TextBox)gvRow.FindControl("TxtIntAmt")).Text, ((TextBox)gvRow.FindControl("TxtMatAmt")).Text, ((TextBox)gvRow.FindControl("TxtLien")).Text, ((TextBox)gvRow.FindControl("TxtLienAmt")).Text, ((TextBox)gvRow.FindControl("TxtTrfAcType")).Text, ((TextBox)gvRow.FindControl("TxtTrfAcc")).Text, ((TextBox)gvRow.FindControl("TxtStatus")).Text, ((TextBox)gvRow.FindControl("TxtIntPay")).Text, ((TextBox)gvRow.FindControl("TxtlastIntDT")).Text, ((TextBox)gvRow.FindControl("TxtRemark")).Text, ((TextBox)gvRow.FindControl("TxtRecSrno")).Text, ((TextBox)gvRow.FindControl("TxtRECEIPT_NO")).Text, ((TextBox)gvRow.FindControl("TxtPTD")).Text, ((TextBox)gvRow.FindControl("TxtPTM")).Text, ((TextBox)gvRow.FindControl("TxtPTY")).Text, Session["MID"].ToString(), ((TextBox)gvRow.FindControl("TxtSTAGE")).Text);

                    tmp++;
                }
            }

            if (Result > 0)
            {
                WebMsgBox.Show("Sucessfully Modify...!!", this.Page);
                GridView1009.Visible = false;
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private DataTable CreateFirst(DataTable dt)
    {
        dt.Columns.Add("ID");
        dt.Columns.Add("Brcd");
        dt.Columns.Add("Product");
        dt.Columns.Add("AccNo");
        dt.AcceptChanges();
        return dt;
    }
    protected void GridView1009_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Chk_AllAutho_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (Chk_AllAutho.Checked == true)
            {
                int STRRes = 0;

                if (DdlAccActivity.SelectedValue == "Deposit_Info")
                {
                    BtnUpdateAll.Visible = true;
                    BtnUpdateAll.Text = "Authorize All";

                    FL = "Dep";

                    STRRes = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
                    ViewState["Flag"] = "AT";
                }
                if (DdlAccActivity.SelectedValue == "Loan_Info")
                {
                    UpdateAll_LN.Visible = true;
                    UpdateAll_LN.Text = "Authorize All";

                    FL = "Loan";

                    STRRes = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
                    ViewState["Flag"] = "AT";
                }
                if (DdlAccActivity.SelectedValue == "Acc_Master")
                {
                    BtnUpdateAll_Acc.Visible = true;
                    BtnUpdateAll_Acc.Text = "Authorize All";

                    FL = "Acc";

                    STRRes = RO.GetLoanInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
                    ViewState["Flag"] = "AT";
                }
                if (DdlAccActivity.SelectedValue == "GLMast")
                {
                    BtnUpdateAll_GL.Visible = true;
                    BtnUpdateAll_GL.Text = "Authorize All";

                    FL = "GLMast";

                    STRRes = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
                    ViewState["Flag"] = "AT";
                }
            }
            else
            {
                BtnUpdateAll.Visible = false;
                BtnUpdateAll.Text = "Update All";
            }
            DivVisibility();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["BtnType"] = "Modify";

            if (DdlAccActivity.SelectedValue == "Deposit_Info")
            {
                GridView1009.Columns[0].Visible = true;
                GridView1009.Columns[1].Visible = true;

                string test = hdnCustNo.Value;
                BindNew_Calculations();
                BtnUpdateAll.Text = "Update All";
                ViewState["Flag"] = "MD";
            }
            if (DdlAccActivity.SelectedValue == "Loan_Info")
            {
                GridViewLoan.Columns[0].Visible = true;
                GridViewLoan.Columns[1].Visible = true;

                string test = hdnCustNo.Value;
                BindNew_Calculations_1();
                UpdateAll_LN.Text = "Update All";
                ViewState["Flag"] = "MD";
            }
            if (DdlAccActivity.SelectedValue == "Acc_Master")
            {
                GridViewAcc.Columns[0].Visible = true;
                GridViewAcc.Columns[1].Visible = true;

                string test = hdnCustNo.Value;
                BindNew_Calculations_2();
                UpdateAll_LN.Text = "Update All";
                ViewState["Flag"] = "MD";
            }
            if (DdlAccActivity.SelectedValue == "GLMast")
            {
                GridViewGL.Columns[0].Visible = true;
                GridViewGL.Columns[1].Visible = true;

                string test = hdnCustNo.Value;
                BindNew_Calculations_3();
                BtnUpdateAll_GL.Text = "Update All";
                ViewState["Flag"] = "MD";
            }
            DivVisibility();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindNew_Calculations()
    {
        try
        {
            int Res = 0;
            GridView1009.Visible = true;

            FL = "Dep";
            Lbl_GridName.Text = "DepositInfo Data";

            Res = RO.GetDepositInfo_1(GridView1009, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            BtnUpdateAll.Visible = true;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindNew_Calculations_1()
    {
        try
        {
            int Res = 0;
            GridViewLoan.Visible = true;

            FL = "Loan";
            Lbl_GridName1.Text = "LoanInfo Data";

            Res = RO.GetDepositInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            UpdateAll_LN.Visible = true;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindNew_Calculations_2()
    {
        try
        {
            int Res = 0;
            GridViewAcc.Visible = true;

            FL = "Acc";
            Lbl_GridName2.Text = "AccMaster Data";

            Res = RO.GetDepositInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            BtnUpdateAll_Acc.Visible = true;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindNew_Calculations_3()
    {
        try
        {
            int Res = 0;
            GridViewGL.Visible = true;

            FL = "GLMast";
            Lbl_GridName3.Text = "GlMaster Data";

            Res = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);
            BtnUpdateAll_GL.Visible = true;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void UpdateAll_LN_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            int STRRes = 0;
            ViewState["BtnType"] = "Update";
            if (ViewState["Flag"].ToString() == "MD")
            {
                InsertData_loan("RC");
                //ClearData(); 23-03-2019
            }
            else
            {
                if (Chk_AllAutho.Checked == false)
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Loan";

                        STRRes = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (STRRes.ToString() != Session["MID"].ToString())
                        {
                            Res = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                ClearData();

                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                ClearData();
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                            ClearData();
                            return;
                        }
                    }
                    ClearData();
                }
                else if (Chk_AllAutho.Checked == true) // For All Auhtorize at a time 
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Loan";
                        Res = RO.GetLoanInfo_1(GridViewLoan, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (Res > 0)
                        {
                            WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                            ClearData();
                        }
                        else
                        {
                            WebMsgBox.Show("Already authorized......!", this.Page);
                            ClearData();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

 
    private void InsertData_loan(string sFlag)
    {
        try
        {
            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);

            int tmp = 0;

            foreach (GridViewRow gvRow in GridViewLoan.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk_1")).Checked == true)
                {
                    dtFirst.Rows.Add(GridViewLoan.DataKeys[gvRow.DataItemIndex].Value, ((Label)gvRow.FindControl("lblBrCd_1")).Text, ((Label)gvRow.FindControl("lblProduct_1")).Text, ((Label)gvRow.FindControl("lblAccno_1")).Text);

                    string[] AT = dtFirst.Rows[tmp]["ID"].ToString().Split('_');
                    ViewState["Custno"] = AT[0].ToString();

                    Result = RO.InsertTrans_loan((((Label)gvRow.FindControl("lblBrCd_1")).Text), (((Label)gvRow.FindControl("lblProduct_1")).Text), (((Label)gvRow.FindControl("lblAccno_1")).Text), ((TextBox)gvRow.FindControl("TxtCustno_1")).Text, ((TextBox)gvRow.FindControl("TxtLimit_1")).Text, ((TextBox)gvRow.FindControl("TxtInst_1")).Text, ((TextBox)gvRow.FindControl("TxtROI_1")).Text, ((TextBox)gvRow.FindControl("TxtPenal_1")).Text, ((TextBox)gvRow.FindControl("TxtOpDate_1")).Text, ((TextBox)gvRow.FindControl("TxtDueDt_1")).Text, ((TextBox)gvRow.FindControl("TxtStatus_1")).Text, ((TextBox)gvRow.FindControl("TxtInstDT_1")).Text, ((TextBox)gvRow.FindControl("TxtPERIOD_1")).Text, ((TextBox)gvRow.FindControl("TxtBONDNO_1")).Text, ((TextBox)gvRow.FindControl("TxtlastIntDt_1")).Text, ((TextBox)gvRow.FindControl("TxtDISYN_1")).Text, ((TextBox)gvRow.FindControl("TxtEMI_1")).Text, ((TextBox)gvRow.FindControl("TxtRecommAutho_1")).Text, ((TextBox)gvRow.FindControl("TxtEquated_1")).Text, ((TextBox)gvRow.FindControl("TxtIntFund_1")).Text, ((TextBox)gvRow.FindControl("TxtPLRLink_1")).Text, ((TextBox)gvRow.FindControl("TxtLoanPurpose_1")).Text, ((TextBox)gvRow.FindControl("TxtRemark_1")).Text, Session["MID"].ToString(), ((TextBox)gvRow.FindControl("TxtStage_1")).Text);
                    
                    tmp++;
                }
            }

            if (Result > 0)
            {
                WebMsgBox.Show("Sucessfully Modify...!!", this.Page);
                GridViewLoan.Visible = false;
                ClearData();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridViewLoan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void BtnUpdateAll_Acc_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            int STRRes = 0;
            ViewState["BtnType"] = "Update";
            if (ViewState["Flag"].ToString() == "MD")
            {
                InsertData_Acc("RC");
                //ClearData(); 23-03-2019
            }
            else
            {
                if (Chk_AllAutho.Checked == false)
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Acc";

                        STRRes = RO.GetLoanInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (STRRes.ToString() != Session["MID"].ToString())
                        {
                            Res = RO.GetLoanInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                ClearData();

                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                ClearData();
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                            ClearData();
                            return;
                        }
                    }
                    ClearData();
                }
                else if (Chk_AllAutho.Checked == true) // For All Auhtorize at a time 
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Acc";
                        Res = RO.GetLoanInfo_1(GridViewAcc, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (Res > 0)
                        {
                            WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                            ClearData();
                        }
                        else
                        {
                            WebMsgBox.Show("Already authorized......!", this.Page);
                            ClearData();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private void InsertData_Acc(string sFlag)
    {
        try
        {
            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);

            int tmp = 0;

            foreach (GridViewRow gvRow in GridViewAcc.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk_2")).Checked == true)
                {
                    dtFirst.Rows.Add(GridViewAcc.DataKeys[gvRow.DataItemIndex].Value, ((Label)gvRow.FindControl("lblBrCd_2")).Text, ((Label)gvRow.FindControl("lblProduct_2")).Text, ((Label)gvRow.FindControl("lblAccno_2")).Text);

                    string[] AT = dtFirst.Rows[tmp]["ID"].ToString().Split('_');
                    ViewState["Custno"] = AT[0].ToString();

                    if (((TextBox)gvRow.FindControl("TxtDAmt_2")).Text == "")
                    {
                        ((TextBox)gvRow.FindControl("TxtDAmt_2")).Text = "0";
                    }

                    Result = RO.InsertTrans_Acc((((Label)gvRow.FindControl("lblBrCd_2")).Text), (((Label)gvRow.FindControl("lblProduct_2")).Text), (((Label)gvRow.FindControl("lblAccno_2")).Text), ((TextBox)gvRow.FindControl("TxtGLCODE_2")).Text, ((TextBox)gvRow.FindControl("TxtCustno_2")).Text, ((TextBox)gvRow.FindControl("TxtOpDate_2")).Text, ((TextBox)gvRow.FindControl("TxtCloseDate_2")).Text, ((TextBox)gvRow.FindControl("TxtAcStatus_2")).Text, ((TextBox)gvRow.FindControl("TxtAcType_2")).Text, ((TextBox)gvRow.FindControl("TxtOprType_2")).Text, ((TextBox)gvRow.FindControl("TxtDPeriod_2")).Text, ((TextBox)gvRow.FindControl("TxtDAmt_2")).Text, ((TextBox)gvRow.FindControl("TxtLastIntDT_2")).Text, ((TextBox)gvRow.FindControl("TxtRegAgt_2")).Text, ((TextBox)gvRow.FindControl("TxtRefCust_2")).Text, ((TextBox)gvRow.FindControl("TxtSHRBr_2")).Text, ((TextBox)gvRow.FindControl("TxtRemark1_2")).Text, Session["MID"].ToString());

                    tmp++;
                }
            }

            if (Result > 0)
            {
                WebMsgBox.Show("Sucessfully Modify...!!", this.Page);
                GridViewAcc.Visible = false;
                ClearData();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridViewAcc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void BtnUpdateAll_GL_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            int STRRes = 0;
            ViewState["BtnType"] = "Update";
            if (ViewState["Flag"].ToString() == "MD")
            {
                InsertData_GL("RC");
            }
            else
            {
                if (Chk_AllAutho.Checked == false)
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "GL";

                        STRRes = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (STRRes.ToString() != Session["MID"].ToString())
                        {
                            Res = RO.GetGLInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                ClearData();

                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                ClearData();
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                            ClearData();
                            return;
                        }
                    }
                    ClearData();
                }
                else if (Chk_AllAutho.Checked == true) // For All Auhtorize at a time 
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        string FL = "Acc";
                        Res = RO.GetLoanInfo_1(GridViewGL, TxtBRCD.Text, TxtAccType.Text, TxtAccno.Text, FL);

                        if (Res > 0)
                        {
                            WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                            ClearData();
                        }
                        else
                        {
                            WebMsgBox.Show("Already authorized......!", this.Page);
                            ClearData();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private void InsertData_GL(string sFlag)
    {
        try
        {
            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);

            int tmp = 0;

            foreach (GridViewRow gvRow in GridViewGL.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk_3")).Checked == true)
                {
                    dtFirst.Rows.Add(GridViewGL.DataKeys[gvRow.DataItemIndex].Value, ((Label)gvRow.FindControl("lblBrCd_3")).Text, ((Label)gvRow.FindControl("lblProduct_3")).Text);

                    string[] AT = dtFirst.Rows[tmp]["ID"].ToString().Split('_');
                    ViewState["SUBGLCODE"] = AT[0].ToString();

                    Result = RO.InsertTrans_GL((((Label)gvRow.FindControl("lblBrCd_3")).Text), (((Label)gvRow.FindControl("lblProduct_3")).Text), ((TextBox)gvRow.FindControl("TxtGLCODE_3")).Text, ((TextBox)gvRow.FindControl("TxtGLName_3")).Text, ((TextBox)gvRow.FindControl("TxtGLGrp_3")).Text, ((TextBox)gvRow.FindControl("TxtCategory_3")).Text, ((TextBox)gvRow.FindControl("TxtROI_3")).Text, ((TextBox)gvRow.FindControl("TxtIntCalType_3")).Text, ((TextBox)gvRow.FindControl("TxtIntApp_3")).Text, ((TextBox)gvRow.FindControl("TxtINTPAY_3")).Text, ((TextBox)gvRow.FindControl("TxtGLBal_3")).Text, ((TextBox)gvRow.FindControl("TxtPLAcc_3")).Text, ((TextBox)gvRow.FindControl("TxtAccYN_3")).Text, ((TextBox)gvRow.FindControl("TxtLastNo_3")).Text, ((TextBox)gvRow.FindControl("TxtIR_3")).Text, ((TextBox)gvRow.FindControl("TxtIOR_3")).Text, ((TextBox)gvRow.FindControl("TxtIntAccYN_3")).Text, ((TextBox)gvRow.FindControl("TxtShortGlName_3")).Text, ((TextBox)gvRow.FindControl("TxtPLGrp_3")).Text, ((TextBox)gvRow.FindControl("TxtCashDR_3")).Text, ((TextBox)gvRow.FindControl("TxtCashCR_3")).Text, ((TextBox)gvRow.FindControl("TxtTrfDR_3")).Text, ((TextBox)gvRow.FindControl("TxtTrfCR_3")).Text, ((TextBox)gvRow.FindControl("TxtClgDR_3")).Text, ((TextBox)gvRow.FindControl("TxtClgCR_3")).Text, ((TextBox)gvRow.FindControl("TxtGLMarathi_3")).Text, ((TextBox)gvRow.FindControl("TxtImplimentDT_3")).Text, ((TextBox)gvRow.FindControl("TxtOpenBal_3")).Text);

                    tmp++;
                }
            }

            if (Result > 0)
            {
                WebMsgBox.Show("Sucessfully Modify...!!", this.Page);
                GridViewGL.Visible = false;
                ClearData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridViewGL_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}