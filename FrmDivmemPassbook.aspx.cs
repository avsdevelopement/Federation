using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDivmemPassbook : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DbConnection conn = new DbConnection();
    scustom cc = new scustom();
    ClsOpenClose OC = new ClsOpenClose();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsGetNPAList RO = new ClsGetNPAList ();
    ClsRecoveryOperation REC = new ClsRecoveryOperation();
    DataTable dtFirst = new DataTable();
    scustom customcs = new scustom();
    
    string sResult = "";
    int Result = 0;
    string FL = "";
    string STR = "";
    int Res = 0;
    string MM, YY, EDT, STRRes;
    double SumFooterValue = 0, SumFooterValue1 = 0, SumFooterValue2 = 0, SumFooterValue3 = 0, SumFooterValue4 = 0, SumFooterValue5 = 0, SumFooterValue6 = 0, SumFooterValue7 = 0,
       SumFooterValue8 = 0, SumFooterValue9 = 0, SumFooterValue10 = 0, SumFooterValue11 = 0, SumFooterValue12 = 0, SumFooterValue13 = 0, SumFooterValue14 = 0, SumFooterValue15 = 0, SumFooterValue16 = 0;
    double TotalValue = 0, TotalValue1 = 0, TotalValue2 = 0, TotalValue3 = 0, TotalValue4 = 0, TotalValue5 = 0, TotalValue6 = 0, TotalValue7 = 0, TotalValue8 = 0
        , TotalValue9 = 0, TotalValue10 = 0, TotalValue11 = 0, TotalValue12 = 0, TotalValue13 = 0, TotalValue14 = 0, TotalValue15 = 0, TotalValue16 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRecDiv();
            DD.BindFinancialActivity(DdlAccActivity);
            DIV1.Visible = false;
            DIV2.Visible = false;
            BtnCreate.Visible = false;
            BtnModify.Visible = false;
            BtnPost.Visible = false;
            DIV5.Visible = false;
            DIV3.Visible = false;

            Txtfrmbrcd.Text = Session["BRCD"].ToString();
            Txttobrcd.Text = Session["BRCD"].ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["CUSTNO"]))
            {
                txtCustNo.Text = Request.QueryString["CUSTNO"].ToString();
                DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
            }
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlRecDept.SelectedValue.ToString() == "")
            {
                DdlRecDept.SelectedValue = "0";
            }       
            if (rbtnRptType.SelectedValue == "1")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + txtCustNo.Text + "&TPRCD=" + TxtToCustno.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptDivdentMemWiseList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "2")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + txtCustNo.Text + "&TPRCD=" + TxtToCustno.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptDivdentSHRDPList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "3")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + txtCustNo.Text + "&TPRCD=" + TxtToCustno.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FL=" + DdlAccActivity.SelectedValue.ToString() + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptDivPendingList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        Txttobrcd.Text = "";
        DdlRecDiv.SelectedValue = "0";
        DdlRecDept.SelectedValue = "0";
        DdlAccActivity.SelectedValue = "0";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        txtCustName.Text = "";
        TxtToCustName.Text = "";
        TxtToCustno.Text = "";
        txtCustNo.Text = "";
        TxtAmountSpe.Text = "0";
        TxtFprdcode.Text = "";
        TxtFprdname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetStage(txtCustNo.Text);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
                TxtToCustno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CUNAME = txtCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                txtCustName.Text = custnob[0].ToString();
                txtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(txtCustNo.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else
                {
                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
                    TxtToCustno.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtToCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetStage(TxtToCustno.Text);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(TxtToCustno.Text.ToString());
                TxtToCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
                TxtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtToCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CUNAME = TxtToCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                TxtToCustName.Text = custnob[0].ToString();
                TxtToCustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(TxtToCustno.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else
                {
                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        Txttobrcd.Focus();
    }
    protected void Txttobrcd_TextChanged(object sender, EventArgs e)
    {
        DdlRecDiv.Focus();
    }
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
    public void BindRecDept()
    {
        try
        {
            BD1.BRCD = Txtfrmbrcd.Text;
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
            BD1.BRCD = Txtfrmbrcd.Text;
            BD1.Ddl = DdlRecDiv;
            BD1.FnBL_BindRecDiv(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rbtnRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnRptType.SelectedValue == "3")
            {
                DIV1.Visible = true;
                BtnCreate.Visible = true;
                BtnModify.Visible = true;
                DIV2.Visible = true;
                BtnPost.Visible = true;
                DIV5.Visible = true;
                DIV3.Visible = true;
                Div_Cust.Visible = false;
            }
            else  
            {
                DIV1.Visible = false;
                BtnCreate.Visible = false;
                BtnModify.Visible = false;
                DIV2.Visible = false;
                BtnPost.Visible = false;
                DIV5.Visible = false;
                DIV3.Visible = false;
                Div_Cust.Visible = true;
            }
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
            
            GridView1009.Columns[0].Visible = true;
            GridView1009.Columns[1].Visible = true;

            string test = hdnCustNo.Value;
            BindNew_Calculations();
            BtnUpdateAll.Text = "Update All";
            ViewState["Flag"] = "MD";
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
            
            FL = "SEL";
            Lbl_GridName.Text = "Divident Data";

            Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
            GetTotal();
            BtnUpdateAll.Visible = true;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindNew_Calculations1()
    {
    try
        {
            int Res = 0;
           
                GridView1009.Visible = true;

                if (txtCustNo.Text == "")
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";

                    Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
                    BtnUpdateAll.Visible = true;
                    if (Convert.ToString(ViewState["BtnType"]) == "Update")
                    {
                        if (Res > 0)
                        {
                            WebMsgBox.Show("Data Modified Sucessfully.......!", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Divident Data";

                    if (hdnCustNo.Value == "0")
                    {
                        REC.CustNO = txtCustNo.Text;
                        hdnCustNo.Value = txtCustNo.Text;
                    }
                    else
                    {
                        REC.CustNO = hdnCustNo.Value + "," + txtCustNo.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustNo.Text;
                    }

                    Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text,FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
                    BtnUpdateAll.Visible = true;
                }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
}
    protected void Chk_AllAutho_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (Chk_AllAutho.Checked == true)
            {
                int STRRes = 0;

                BtnUpdateAll.Visible = true;
                BtnUpdateAll.Text = "Authorize All";
                STRRes = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, "SEL", DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
                ViewState["Flag"] = "AT";
            }
            else
            {
                BtnUpdateAll.Visible = false;
                BtnUpdateAll.Text = "Update All";
            }
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
                            string FL = "";

                            //RO.FL = "CMID";
                            STRRes = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);

                            if (STRRes.ToString() != Session["MID"].ToString())
                            {
                                //RO.MID = Session["MID"].ToString();
                                //RO.FL = "SENDAUTHO";
                                Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);

                                if (Res > 0)
                                {
                                    WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                    GetTotal();
                                    ClearData(); 
                                }
                                else
                                {
                                    WebMsgBox.Show("Already authorized......!", this.Page);
                                    GetTotal();
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
                            string FL = "AUTH";
                            Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);

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
                    dtFirst.Rows.Add(GridView1009.DataKeys[gvRow.DataItemIndex].Value, ((Label)gvRow.FindControl("TxtCustNo")).Text, ((Label)gvRow.FindControl("lbls1Bal")).Text, ((Label)gvRow.FindControl("lbls1Inst")).Text);

                    string[] AT = dtFirst.Rows[tmp]["ID"].ToString().Split('_');
                    ViewState["Custno"] = AT[0].ToString();

                    Result = RO.InsertTrans((((Label)gvRow.FindControl("TxtCustNo")).Text), ((TextBox)gvRow.FindControl("lbls1Bal_R")).Text, ((TextBox)gvRow.FindControl("lbls1Inst_R")).Text, ((TextBox)gvRow.FindControl("lblChequeNo")).Text, ((TextBox)gvRow.FindControl("lblRecDiv")).Text, ((TextBox)gvRow.FindControl("lblRecDept")).Text);

                    tmp++;
                }
            }

            if (Result > 0)
            {
                WebMsgBox.Show("Sucessfully Modify...!!", this.Page);
                //GridView1009.Visible = false;
                //ClearData();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridView1009_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            try
            {
               
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private DataTable CreateFirst(DataTable dt)
    {
        dt.Columns.Add("id");
        dt.Columns.Add("Custno");
        dt.Columns.Add("INT_CR");
        dt.Columns.Add("DIV_CR");
        dt.AcceptChanges();
        return dt;
    }

    protected void GridView1009_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Princ = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbls1Bal")).Text) ? "0" : ((Label)e.Row.FindControl("lbls1Bal")).Text;
                TotalValue1 = Convert.ToDouble(Princ);
                SumFooterValue1 += TotalValue1;
                string Inttr = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbls1Inst")).Text) ? "0" : ((Label)e.Row.FindControl("lbls1Inst")).Text;
                TotalValue3 = Convert.ToDouble(Inttr);
                SumFooterValue3 += TotalValue3;
                string PriCrDr = string.IsNullOrEmpty(((TextBox)e.Row.FindControl("lbls1Bal_R")).Text) ? "0" : ((TextBox)e.Row.FindControl("lbls1Bal_R")).Text;
                TotalValue2 = Convert.ToDouble(PriCrDr);
                SumFooterValue2 += TotalValue2;
                string PInst = string.IsNullOrEmpty(((TextBox)e.Row.FindControl("lbls1Inst_R")).Text) ? "0" : ((TextBox)e.Row.FindControl("lbls1Inst_R")).Text;
                TotalValue4 = Convert.ToDouble(PInst);
                SumFooterValue4 += TotalValue4;
                string PInstR = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblTotal")).Text) ? "0" : ((Label)e.Row.FindControl("lblTotal")).Text;
                TotalValue5 = Convert.ToDouble(PInstR);
                SumFooterValue5 += TotalValue5;

                TextBox txtInt = (TextBox)e.Row.FindControl("lbls1Bal_R");
                TextBox txtDiv = (TextBox)e.Row.FindControl("lbls1Inst_R");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Attributes.Add("onkeyup", "CalcSellPrice2(" + txtDiv.ClientID + ", '" + txtInt.ClientID + "','" + lblTotal.ClientID + "')");        
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl1 = (Label)e.Row.FindControl("lbls1Bal_tot");
                lbl1.Text = SumFooterValue1.ToString() + "";
                Label lbl3 = (Label)e.Row.FindControl("lbls1Inst_tot");
                lbl3.Text = SumFooterValue3.ToString() + "";
                TextBox lbl2 = (TextBox)e.Row.FindControl("lbls1Bal_R_tot");
                lbl2.Text = SumFooterValue2.ToString() + "";
                TextBox lbl4 = (TextBox)e.Row.FindControl("lbls1Inst_R_tot");
                lbl4.Text = SumFooterValue4.ToString() + "";
                Label lbl5 = (Label)e.Row.FindControl("lblTotal_tot");
                lbl5.Text = SumFooterValue5.ToString() + "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        string FL = "";

        FL = "CRE";

        Res = RO.GetDivMemPendingListDT_CRE(Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
        BtnUpdateAll.Visible = false;

        if (Res > 0)
        {
            WebMsgBox.Show("Created Sucessfully.......!", this.Page);
            return;
        }
    }
    protected void BtnPost_Click(object sender, EventArgs e)
    {
        FL = "POST";

        STR = RO.GetDivMemPendingListDT_Post(Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue, TxtFprdcode.Text);

        if (STR != null)
        {
            WebMsgBox.Show("Voucher Post successfully with Set No - " + STR, this.Page);
            FL = "Insert"; 
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
        }
        else
        {
            WebMsgBox.Show("Voucher Is Already Posted...", this.Page);
        }
        BtnPost.Enabled = false;
    }
    protected void TxtFprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = DD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            string PDName = customcs.GetProductName(TxtFprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtFprdname.Text = PDName;
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtFprdcode.Text = "";
                TxtFprdcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtFprdname.Text = CT[0].ToString();
                TxtFprdcode.Text = CT[1].ToString();
                TxtFDate.Focus();
                string[] GLS = DD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlRecDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetTotal ()
    {
        try
        {
            FL = "ShowTotal";

            string AMT;
            AMT = RO.GetDivMemPendingListDT_Total (Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);
            double val;

            if (AMT != null)
            {
                if (double.TryParse(AMT, out val))
                {
                    TxtAmountSpe.Text = AMT;
                }
                else
                {
                    TxtAmountSpe.Text = "0";
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlAccActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            GridView1009.Visible = true;

            FL = "SEL";
            Res = RO.GetDivMemPendingListDT_1(GridView1009, Txtfrmbrcd.Text, Txttobrcd.Text, TxtFDate.Text, TxtTDate.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue);

            GetTotal();
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPostInd_Click(object sender, EventArgs e)
    {
        FL = "POST";

        STR = RO.GetDivMemListDT_Post(Txtfrmbrcd.Text, Txttobrcd.Text, TxtPostDt.Text, FL, DdlAccActivity.SelectedValue, DdlRecDiv.SelectedValue, DdlRecDept.SelectedValue, TxtFprdcode.Text);

        if (STR != null)
        {
            WebMsgBox.Show("Voucher Post successfully with Set No - " + STR, this.Page);
            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            BtnPostInd.Enabled = false;
            ClearData();
        }
        else
        {
            WebMsgBox.Show("Voucher Is Already Posted...", this.Page);
        }
    }
}