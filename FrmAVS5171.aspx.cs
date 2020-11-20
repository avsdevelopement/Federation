using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class FrmAVS5171 : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsSMSRecovery SMSR = new ClsSMSRecovery();
    ClsAVS5171 RO = new ClsAVS5171();
    ClsAVS5171_1 RS = new ClsAVS5171_1();
    DbConnection Conn = new DbConnection();
    Mobile_Service MS = new Mobile_Service();
    ClsCommon CMN = new ClsCommon();
    DataTable dt = new DataTable();

    int Res = 0;
    string MM, YY, EDT, STRRes;
    double SumFooterValue = 0, TotalValue = 0;
    string Message = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
            hdnCustNo.Value = "0";
            TxtDeathFund.Text = "0";

            GrdInsert.Columns[0].Visible = true;
            GrdInsert.Columns[1].Visible = false;
            string BKCD = RS.FnBl_GetBANKCode(RS);

            if (BKCD != null)
            {
                ViewState["BANKCODE"] = BKCD;
            }
            else
            {
                ViewState["BANKCODE"] = "0";
            }

            TxtMM.Focus();
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }

    public void ClearData()
    {
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        DdlRecDept.SelectedValue = "0";
        TxtDeathFund.Text = "";
        ddlBrCode.Focus();
    }

    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.RECDIV = DdlRecDiv.SelectedValue.ToString();
        BD.RECCODE = DdlRecDiv.SelectedValue.ToString();
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();// Amruta 
        BindRecDiv();// Amruta 
        BindRecDept();// Amruta 
    }

    public void BindRecDept()
    {
        BD.BRCD = ddlBrCode.SelectedValue.ToString();
        BD.Ddl = DdlRecDept;
        BD.RECDIV = DdlRecDiv.SelectedValue.ToString();
        BD.FnBL_BindRecDept(BD);
    }

    public void BindRecDiv()
    {
        BD.BRCD = ddlBrCode.SelectedValue.ToString();
        BD.Ddl = DdlRecDiv;
        BD.FnBL_BindRecDiv(BD);
    }

    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            string RptName = "", RecFor = "";
            EDT = DateSet();

            string BKCD = RS.FnBl_GetBANKCode(RS);
            if (BKCD == "1009")
            {
                RptName = "RptRecoveryStatement_1009.rdlc";
            }
            else if (BKCD == "1010")
            {
                RptName = "RptRecoveryStatement_1010.rdlc";
            }
            else
            {
                RptName = "RptRecoveryStatement.rdlc";
            }

            RecFor = DdlRecDiv.SelectedItem.Text + " ( " + DdlRecDept.SelectedItem.Text + " ) ";

            string redirectURL = "FrmRecRView.aspx?FL=S&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + RecFor + "&rptname=" + RptName + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #region Old Code
    public void BindCalculations()
    {
        try
        {
            if (ViewState["BANKCODE"].ToString() == "1009") //MSEB
            {
                GridView1009.Visible = true;
                GrdInsert.Visible = false;

                if (txtCustno.Text == "")
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";
                    EDT = DateSet();

                    RO.GRD = GridView1009;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    RO.FnBl_RecOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
                else
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";

                    EDT = DateSet();
                    RO.GRD = GridView1009;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }

                    RO.FnBl_RecCustOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
            }


            else //TZSSPM
            {
                GridView1009.Visible = false;
                GrdInsert.Visible = true;

                if (txtCustno.Text == "")
                {
                    string FL = "";
                    //if (Rdb_PostType.SelectedValue == "S")
                    //{
                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";
                    //}
                    //else
                    //{
                    //    FL = "SELPOST";
                    //    Lbl_GridName.Text = "Post Recovery Data";
                    //}

                    EDT = DateSet();

                    RO.GRD = GrdInsert;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    RO.FnBl_RecOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
                else
                {
                    string FL = "";
                    //if (Rdb_PostType.SelectedValue == "S")
                    //{
                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";
                    //}
                    //else
                    //{
                    //    FL = "SELPOST";
                    //    Lbl_GridName.Text = "Post Recovery Data";
                    //}

                    EDT = DateSet();
                    RO.GRD = GrdInsert;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }

                    RO.FnBl_RecCustOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindSumGrid()
    {
        try
        {
            RO.FL = "DETGRID";
            RO.BRCD = ddlBrCode.SelectedValue.ToString();
            RO.MM = TxtMM.Text;
            RO.YY = TxtYYYY.Text;
            RO.GRD = GrdSumDetails;
            RO.BANKCODE = ViewState["BANKCODE"].ToString();

            Res = RO.FnBL_GetSumGrid(RO);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public string DateSet()
    {
        try
        {
            string ED = Conn.ConvertToDate(Session["EntryDate"].ToString());
            string[] D = ED.Split('-');

            MM = TxtMM.Text;
            YY = TxtYYYY.Text;
            if (TxtMM.Text.Length == 1)
            {
                if (Convert.ToInt32(MM) < 10)
                    MM = "0" + MM;
            }

            //  EDT = D[2].ToString() + "/" + MM + "/" + YY;
            EDT = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return EDT;
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";

            #region Specific Create
            if (Rdb_CreateType.SelectedValue == "S")
            {
                if (txtCustno.Text == "")
                {
                    RO.FL = "CHMASTER";
                    RO.BRCD = ddlBrCode.SelectedValue;
                    RO.RECCODE = DdlRecDept.SelectedValue;
                    RO.RECDIV = DdlRecDiv.SelectedValue;

                    string MasterCount = RO.FnBL_CheckMaster(RO);
                    if (Convert.ToInt32(MasterCount) > 0)
                    {
                        RO.FL = "EXI";
                        RO.BRCD = ddlBrCode.SelectedValue.ToString();
                        EDT = DateSet();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();

                        STRRes = RO.FnBL_GetExistsRecovery(RO);
                        if (Convert.ToInt32(STRRes) > 0)
                        {
                            Msg = "Last recovery deleted for - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!";
                            //WebMsgBox.Show("Recovery is already created for Month - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!", this.Page);
                        }
                        //else
                        //{

                        //EDT = DateSet();
                        EDT = Session["EntryDate"].ToString();

                        RO.BRCD = ddlBrCode.SelectedValue.ToString();
                        RO.FL = "CRE";
                        RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.DeathFund = TxtDeathFund.Text;
                        RO.Narration = txtNarration.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();
                        if (chkMW.Checked == true)
                            RO.MW = "1";
                        else
                            RO.MW = "0";
                        if (chkUS.Checked == true)
                            RO.US = "1";
                        else
                            RO.US = "0";

                        Res = RO.FnBl_CreateCalc(RO);
                        if (Res > 0)
                        {
                            Msg = Msg + " Recovery for " + DdlRecDept.SelectedItem.Text + " is created successfully....!";
                            WebMsgBox.Show("" + Msg, this.Page);
                        }
                        else
                        {
                            WebMsgBox.Show("Recovery creation failed....!", this.Page);
                        }
                        //}
                    }
                    else
                    {
                        WebMsgBox.Show("Customers not available for " + DdlRecDept.SelectedItem.Text + " ,cannot create Recovery", this.Page);
                    }
                }
                else//amruta
                {
                    RO.FL = "CHMASTER";
                    RO.BRCD = ddlBrCode.SelectedValue;
                    RO.RECCODE = DdlRecDept.SelectedValue;
                    RO.RECDIV = DdlRecDiv.SelectedValue;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }


                    string MasterCount = RO.FnBL_CheckCustMaster(RO);
                    if (Convert.ToInt32(MasterCount) > 0)
                    {


                        RO.FL = "EXI";
                        RO.BRCD = ddlBrCode.SelectedValue.ToString();
                        EDT = DateSet();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        if (hdnCustNo.Value == "0")
                        {
                            RO.CustNO = txtCustno.Text;
                            hdnCustNo.Value = txtCustno.Text;
                        }
                        else
                        {
                            RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                            hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                        }
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();

                        STRRes = RO.FnBL_GetCustExistsRecovery(RO);
                        if (Convert.ToInt32(STRRes) > 0)
                        {
                            Msg = "Last recovery deleted for - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!";
                            //WebMsgBox.Show("Recovery for " + DdlRecDept.SelectedItem.Text + " is already craeted for Month - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!", this.Page);
                        }
                        else
                        {

                            //EDT = DateSet();
                            EDT = Session["EntryDate"].ToString();

                            RO.BRCD = ddlBrCode.SelectedValue.ToString();
                            RO.FL = "CRE";
                            RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                            RO.ASONDT = EDT;
                            RO.MID = Session["MID"].ToString();
                            RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                            RO.MM = TxtMM.Text;
                            RO.YY = TxtYYYY.Text;
                            RO.DeathFund = TxtDeathFund.Text;
                            RO.BANKCODE = ViewState["BANKCODE"].ToString();
                            if (hdnCustNo.Value == "0")
                            {
                                RO.CustNO = txtCustno.Text;
                                hdnCustNo.Value = txtCustno.Text;
                            }
                            else
                            {
                                RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                                hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                            }

                            RO.BANKCODE = ViewState["BANKCODE"].ToString();

                            Res = RO.FnBl_CreateCustCalc(RO);
                            if (Res > 0)
                            {
                                Msg = Msg + " Recovery for " + DdlRecDept.SelectedItem.Text + " is created successfully....!";
                                WebMsgBox.Show("" + Msg, this.Page);
                            }
                            else
                            {
                                WebMsgBox.Show("Recovery creation failed....!", this.Page);
                            }
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Customers not available for " + DdlRecDept.SelectedItem.Text + " ,cannot create Recovery", this.Page);
                    }
                }
            }
            #endregion

            #region All Create
            else if (Rdb_CreateType.SelectedValue == "A")
            {

                DataTable DT1 = RS.GetCodes(Session["BRCD"].ToString(), DdlRecDiv.SelectedValue.ToString());
                if (DT1.Rows.Count > 0)
                {
                    for (int i = 0; i <= DT1.Rows.Count - 1; i++)
                    {
                        RO.FL = "EXI";
                        RO.BRCD = DT1.Rows[i]["BRCD"].ToString();
                        RO.ASONDT = Session["EntryDate"].ToString();
                        RO.MID = Session["MID"].ToString();
                        RO.RECCODE = DT1.Rows[i]["RECCODE"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();

                        STRRes = RO.FnBL_GetExistsRecovery(RO);
                        if (Convert.ToInt32(STRRes) > 0)
                        {
                            Msg = Msg + "Last recovery deleted for - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!";
                        }

                        EDT = Session["EntryDate"].ToString();

                        RO.BRCD = DT1.Rows[i]["BRCD"].ToString();
                        RO.FL = "CRE";
                        RO.RECCODE = DT1.Rows[i]["RECCODE"].ToString();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.DeathFund = TxtDeathFund.Text;
                        RO.Narration = txtNarration.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();
                        if (chkMW.Checked == true)
                            RO.MW = "1";
                        else
                            RO.MW = "0";
                        if (chkUS.Checked == true)
                            RO.US = "1";
                        else
                            RO.US = "0";

                        Res = RO.FnBl_CreateCalc(RO);
                    }

                }
                if (Res > 0)
                {
                    Msg = " Recovery created successfully for all department....!";
                    WebMsgBox.Show("" + Msg, this.Page);
                }
                else
                {
                    WebMsgBox.Show("Recovery creation failed....!", this.Page);
                }

            }
            #endregion

            ClearData();
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
            string test = hdnCustNo.Value;
            //BindCalculations();
            BindNew_Calculations();
            BtnUpdateAll.Text = "Update All";
            GrdSumDetails.Visible = false;
            ViewState["Flag"] = "MD";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            //BindCalculations();
            BindNew_Calculations();
            BtnUpdateAll.Text = "Auhtorize All";
            ViewState["Flag"] = "AT";

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
    public void SMSFunction(string MM, string YY, string ToTalAmount, string DeductionAmt, string CNO)
    {
        try
        {
            string MonName = new DateTime(Convert.ToInt32(YY), Convert.ToInt32(MM), 1).ToString("MMM", CultureInfo.InvariantCulture);
            Message = "Dear Member,Your recovery statement for the month " + MonName + " - " + YY + " total of Rs. " + ToTalAmount + " /- including regular monthly deduction of Rs." + DeductionAmt + " /- Regards, TZMSSSPM";
            SMSR.InsertSMSRecAutho(Session["BRCD"].ToString(), CNO, Message.ToString(), Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "RecoAuthorize");
            Message = "";
        }
        catch (Exception Ex)
        {

            throw;
        }
    }

    protected void GrdInsert_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lb = (Label)GrdInsert.Rows[e.RowIndex].FindControl("Lbl_ID");
            TextBox TxtCustnoGrd = (TextBox)GrdInsert.Rows[e.RowIndex].FindControl("TxtCustno") as TextBox;

            string redirectURL = "FrmModifyData.aspx?FL=SENDMOD&BKCD=" + ViewState["BANKCODE"].ToString() + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&CNO=" + TxtCustnoGrd.Text + "&ID=" + lb.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + redirectURL + "', 'popup_window', 'width=1100,height=400,left=150,top=150,resizable=no');", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void ddlBrCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DdlRecDiv.SelectedValue = "1";
            BindRecDiv();
            BindRecDept();
            TxtMM.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlRecDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  AutoRecDeptName.ContextKey = ddlBrCode.SelectedValue.ToString() + "_" + DdlRecDiv.SelectedValue.ToString();
            BindRecDept();
            if (Rdb_CreateType.SelectedValue == "A")
            {
                TxtDeathFund.Focus();
            }
            else
            {
                DdlRecDept.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtMM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtMM.Text.Length;
            if (Len > 2 || Convert.ToInt32(TxtMM.Text) > 12)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtMM.Text = "";
                TxtMM.Focus();
            }
            else
            {
                TxtYYYY.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtYYYY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtYYYY.Text.Length;
            if (Len != 4)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtYYYY.Text = "";
                TxtYYYY.Focus();
            }
            else
            {
                BindSumGrid();
                DdlRecDiv.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDeathFund_TextChanged(object sender, EventArgs e)
    {
        try
        {
            WebMsgBox.Show("Death fund is Calculated from " + TxtDeathFund.Text + " amount", this.Page);
            txtNarration.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Sms_New()
    {
        try
        {
            RS.MID = Session["MID"].ToString();
            RS.ASONDT = EDT;
            RS.BRCD = ddlBrCode.SelectedValue.ToString();
            RS.FL = "Insert_Sms";
            RS.RECCODE = DdlRecDept.SelectedValue.ToString();
            RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
            RS.MM = TxtMM.Text;
            RS.YY = TxtYYYY.Text;
            RS.BANKCODE = ViewState["BANKCODE"].ToString();

            int RR = RS.Fn_InsertSMS(RS);
            if (RR > 0)
            {
                RS.MID = Session["MID"].ToString();
                RS.ASONDT = EDT;
                RS.BRCD = ddlBrCode.SelectedValue.ToString();
                RS.FL = "Get_Cust";
                RS.RECCODE = DdlRecDept.SelectedValue.ToString();
                RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
                RS.MM = TxtMM.Text;
                RS.YY = TxtYYYY.Text;
                RS.BANKCODE = ViewState["BANKCODE"].ToString();

                DataTable DT1 = RS.Fn_GetCustno(RS);
                if (DT1.Rows.Count > 0)
                {
                    for (int i = 0; i < DT1.Rows.Count; i++)
                    {
                        string SMS = MS.Send_SMS(DT1.Rows[i]["Custno"].ToString(), Session["EntryDate"].ToString());
                    }
                }
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
            if (ViewState["Flag"].ToString() == "MD")
            {
                BindNew_Calculations();
            }
            else
            {
                if (Chk_AllAutho.Checked == false)
                {
                    if (ViewState["BANKCODE"].ToString() == "1009")
                    {
                        if (ViewState["Flag"].ToString() == "AT")
                        {
                            EDT = Session["EntryDate"].ToString();

                            string FL = "";

                            RO.FL = "CMID";
                            RO.MID = Session["MID"].ToString();
                            RO.ASONDT = EDT;
                            RO.BRCD = ddlBrCode.SelectedValue.ToString();
                            RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                            RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                            RO.MM = TxtMM.Text;
                            RO.YY = TxtYYYY.Text;
                            RO.BANKCODE = ViewState["BANKCODE"].ToString();

                            STRRes = RO.FnBl_CheckMid(RO);
                            if (STRRes.ToString() != Session["MID"].ToString())
                            {


                                RO.MID = Session["MID"].ToString();
                                RO.ASONDT = EDT;
                                RO.BRCD = ddlBrCode.SelectedValue.ToString();
                                RO.FL = "SENDAUTHO";
                                RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                                RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                                RO.MM = TxtMM.Text;
                                RO.YY = TxtYYYY.Text;
                                RO.BANKCODE = ViewState["BANKCODE"].ToString();
                                Res = RO.FnBl_AuthoCalcNew(RO);


                                if (Res > 0)
                                {
                                    WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                    BindNew_Calculations();

                                }
                                else
                                {
                                    WebMsgBox.Show("Already authorized......!", this.Page);
                                    BindNew_Calculations();
                                }


                            }
                            else
                            {
                                WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                                BindNew_Calculations();
                                return;
                            }


                        }
                    }
                    else
                    {

                        if (ViewState["Flag"].ToString() == "AT")
                        {
                            EDT = Session["EntryDate"].ToString();

                            RO.FL = "CMID";
                            RO.MID = Session["MID"].ToString();
                            RO.ASONDT = EDT;
                            RO.BRCD = ddlBrCode.SelectedValue.ToString();
                            RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                            RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                            RO.MM = TxtMM.Text;
                            RO.YY = TxtYYYY.Text;
                            RO.BANKCODE = ViewState["BANKCODE"].ToString();

                            STRRes = RO.FnBl_CheckMid(RO);
                            if (STRRes.ToString() != Session["MID"].ToString())
                            {


                                RO.MID = Session["MID"].ToString();
                                RO.ASONDT = EDT;
                                RO.BRCD = ddlBrCode.SelectedValue.ToString();
                                RO.FL = "SENDAUTHO";
                                RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                                RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                                RO.MM = TxtMM.Text;
                                RO.YY = TxtYYYY.Text;
                                RO.BANKCODE = ViewState["BANKCODE"].ToString();

                                Res = RO.FnBl_AuthoCalcNew(RO);

                                if (Res > 0)
                                {
                                    WebMsgBox.Show("Authorized Succesfully......!", this.Page);

                                    string Str = RS.Fn_GetSMSPara();
                                    if (Str != null && Str == "Y")
                                    {
                                        Sms_New();
                                    }
                                    BindNew_Calculations();

                                }
                                else
                                {
                                    WebMsgBox.Show("Already authorized......!", this.Page);
                                    BindNew_Calculations();
                                }


                            }
                            else
                            {
                                WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
                                BindNew_Calculations();
                                return;
                            }




                        }
                    }
                }
                else if (Chk_AllAutho.Checked == true) // For All Auhtorize at a time 
                {
                    if (ViewState["BANKCODE"].ToString() == "1009")
                    {
                        if (ViewState["Flag"].ToString() == "AT")
                        {

                            RO.MID = Session["MID"].ToString();
                            RO.ASONDT = Session["EntryDate"].ToString();
                            RO.BRCD = ddlBrCode.SelectedValue.ToString();
                            RO.FL = "ALL_SENDAUTHO";
                            RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                            RO.MM = TxtMM.Text;
                            RO.YY = TxtYYYY.Text;
                            RO.BANKCODE = ViewState["BANKCODE"].ToString();
                            Res = RO.FnBl_ALL_AuthoCalcNew(RO);


                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                BindNew_Calculations();
                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                BindNew_Calculations();
                            }




                        }
                    }
                    else
                    {

                        if (ViewState["Flag"].ToString() == "AT")
                        {


                            RO.MID = Session["MID"].ToString();
                            RO.ASONDT = Session["EntryDate"].ToString();
                            RO.BRCD = ddlBrCode.SelectedValue.ToString();
                            RO.FL = "ALL_SENDAUTHO";
                            RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                            RO.MM = TxtMM.Text;
                            RO.YY = TxtYYYY.Text;
                            RO.BANKCODE = ViewState["BANKCODE"].ToString();

                            Res = RO.FnBl_ALL_AuthoCalcNew(RO);

                            if (Res > 0)
                            {
                                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
                                BindNew_Calculations();
                            }
                            else
                            {
                                WebMsgBox.Show("Already authorized......!", this.Page);
                                BindNew_Calculations();
                            }


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
    protected void GrdInsert_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string OrdLoan = "";
        string EmgLoan = "";
        string MON = "";
        string MA = "";
        string KNT = "";
        string RD = "";
        string DF = "";
        string MW = "";
        string US = "";

        RO.BRCD = ddlBrCode.SelectedValue.ToString();
        dt = RO.GetLableName(RO);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["REC_PRD"].ToString() == "201")
                    OrdLoan = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "202")
                    EmgLoan = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "44")
                    MON = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "198")
                    MA = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "1")
                    KNT = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "306")
                    RD = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "176")
                    DF = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "199")
                    MW = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "425")
                    US = dt.Rows[i]["SHORTNAME"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = OrdLoan + "- Bal";
            e.Row.Cells[8].Text = OrdLoan + "- Inst";
            e.Row.Cells[9].Text = OrdLoan + "- Intr";

            e.Row.Cells[10].Text = EmgLoan + "- Bal";
            e.Row.Cells[11].Text = EmgLoan + "- Inst";
            e.Row.Cells[12].Text = EmgLoan + "- Intr";

            e.Row.Cells[13].Text = MON;
            e.Row.Cells[14].Text = MA;
            e.Row.Cells[15].Text = KNT;
            e.Row.Cells[16].Text = RD;
            e.Row.Cells[17].Text = DF;
            e.Row.Cells[18].Text = MW;
            e.Row.Cells[19].Text = US;
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_RowTotal")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_RowTotal")).Text;
            TotalValue = Convert.ToDouble(Amt);
            SumFooterValue += TotalValue;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("Lbl_SumTotal");
            lbl.Text = SumFooterValue.ToString();

        }
    }

    protected void txtCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CustName = "";
            RO.BRCD = ddlBrCode.SelectedValue;
            RO.CustNO = txtCustno.Text;
            CustName = RO.GetCustName(RO);
            if (CustName == null || CustName == "")
                WebMsgBox.Show("Custno is Invalid!!!", this.Page);
            else
                txtCustName.Text = CustName;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    protected void Rdb_CreateType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_CreateType.SelectedValue == "A")
            {
                Div_Reccode.Visible = false;
            }
            else
            {
                Div_Reccode.Visible = true;
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
                Label lb = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_ID");
                TextBox TxtCustnoGrd = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtCustno") as TextBox;

                string redirectURL = "FrmModifyData.aspx?FL=SENDMOD&BKCD=" + ViewState["BANKCODE"].ToString() + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&CNO=" + TxtCustnoGrd.Text + "&ID=" + lb.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + redirectURL + "', 'popup_window', 'width=1100,height=400,left=150,top=150,resizable=no');", true);

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }

            //if (txtCustno.Text == "")
            //{
            //    Label lb = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_ID");
            //    EDT = DateSet();
            //    if (ViewState["Flag"].ToString() == "MD")
            //    {
            //        string FL = "";
            //        //if (Rdb_PostType.SelectedValue == "S")
            //        //{
            //        FL = "MOD";
            //        //}
            //        //else
            //        //{
            //        //    FL = "MODPOST";
            //        //}

            //        TextBox TS1Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Bal") as TextBox;
            //        TextBox TS1Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Inst") as TextBox;
            //        TextBox TS1Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Intr") as TextBox;
            //        TextBox TS2Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Bal") as TextBox;
            //        TextBox TS2Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Inst") as TextBox;
            //        TextBox TS2Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Intr") as TextBox;
            //        TextBox TS10Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Bal") as TextBox;
            //        TextBox TS10Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Inst") as TextBox;
            //        TextBox TS10Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Intr") as TextBox;
            //        TextBox TS3Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS3Bal") as TextBox;
            //        TextBox TS4Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS4Bal") as TextBox;
            //        TextBox TS5Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS5Bal") as TextBox;
            //        TextBox TS6Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS6Bal") as TextBox;
            //        TextBox TS7Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS7Bal") as TextBox;
            //        TextBox TS8Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS8Bal") as TextBox;
            //        TextBox TS9Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS9Bal") as TextBox;


            //        RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //        RO.MID = Session["MID"].ToString();
            //        RO.ASONDT = EDT;
            //        RO.FL = FL;
            //        RO.S1Bal = TS1Bal.Text;
            //        RO.S1Inst = TS1Inst.Text;
            //        RO.S1Intr = TS1Intr.Text;
            //        RO.S2Bal = TS2Bal.Text;
            //        RO.S2Inst = TS2Inst.Text;
            //        RO.S2Intr = TS2Intr.Text;
            //        RO.S3Bal = TS3Bal.Text;
            //        RO.S4Bal = TS4Bal.Text;
            //        RO.S5Bal = TS5Bal.Text;
            //        RO.S6Bal = TS6Bal.Text;
            //        RO.S7Bal = TS7Bal.Text;
            //        RO.S8Bal = TS8Bal.Text;
            //        RO.S9Bal = TS9Bal.Text;

            //        RO.S10Bal = TS10Bal.Text;
            //        RO.S10Inst = TS10Bal.Text;
            //        RO.S10Intr = TS10Intr.Text;

            //        RO.BANKCODE = ViewState["BANKCODE"].ToString();

            //        RO.ID = lb.Text;

            //        Res = RO.FnBl_ModifyCalc(RO);
            //        if (Res > 0)
            //        {
            //            WebMsgBox.Show("Updated Succesfully......!", this.Page);
            //            BindCalculations();
            //        }
            //        else
            //        {
            //            WebMsgBox.Show("Error occured while updating (already authorized)......!", this.Page);
            //            BindCalculations();
            //        }

            //    }
            //    else if (ViewState["Flag"].ToString() == "AT")
            //    {
            //        RO.FL = "CMID";
            //        RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //        RO.ID = lb.Text;

            //        STRRes = RO.FnBL_GetMID(RO);
            //        if (STRRes.ToString() != Session["MID"].ToString())
            //        {

            //            RO.ID = lb.Text;
            //            RO.MID = Session["MID"].ToString();
            //            RO.ASONDT = EDT;
            //            RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //            RO.FL = "AUT";
            //            RO.BANKCODE = ViewState["BANKCODE"].ToString();

            //            Res = RO.FnBl_AuthoCalc(RO);
            //            if (Res > 0)
            //            {
            //                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
            //                BindCalculations();
            //            }
            //            else
            //            {
            //                WebMsgBox.Show("Already authorized......!", this.Page);
            //                BindCalculations();
            //            }
            //        }
            //        else
            //        {
            //            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
            //            BindCalculations();
            //        }


            //    }

            //}
            //else //txtCustno.Text != ""
            //{
            //    Label lb = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_ID");
            //    EDT = DateSet();
            //    if (ViewState["Flag"].ToString() == "MD")
            //    {
            //        string FL = "";
            //        //if (Rdb_PostType.SelectedValue == "S")
            //        //{
            //        FL = "MOD";
            //        //}
            //        //else
            //        //{
            //        //    FL = "MODPOST";
            //        //}

            //        TextBox TS1Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Bal") as TextBox;
            //        TextBox TS1Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Inst") as TextBox;
            //        TextBox TS1Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS1Intr") as TextBox;
            //        TextBox TS2Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Bal") as TextBox;
            //        TextBox TS2Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Inst") as TextBox;
            //        TextBox TS2Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS2Intr") as TextBox;

            //        TextBox TS10Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Bal") as TextBox;
            //        TextBox TS10Inst = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Inst") as TextBox;
            //        TextBox TS10Intr = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS10Intr") as TextBox;

            //        TextBox TS3Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS3Bal") as TextBox;
            //        TextBox TS4Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS4Bal") as TextBox;
            //        TextBox TS5Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS5Bal") as TextBox;
            //        TextBox TS6Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS6Bal") as TextBox;
            //        TextBox TS7Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS7Bal") as TextBox;
            //        TextBox TS8Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS8Bal") as TextBox;
            //        TextBox TS9Bal = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtS9Bal") as TextBox;


            //        RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //        RO.MID = Session["MID"].ToString();
            //        RO.ASONDT = EDT;
            //        RO.FL = FL;
            //        RO.S1Bal = TS1Bal.Text;
            //        RO.S1Inst = TS1Inst.Text;
            //        RO.S1Intr = TS1Intr.Text;
            //        RO.S2Bal = TS2Bal.Text;
            //        RO.S2Inst = TS2Inst.Text;
            //        RO.S2Intr = TS2Intr.Text;
            //        RO.S10Bal = TS10Bal.Text;
            //        RO.S10Inst = TS10Inst.Text;
            //        RO.S10Intr = TS10Intr.Text;
            //        RO.S3Bal = TS3Bal.Text;
            //        RO.S4Bal = TS4Bal.Text;
            //        RO.S5Bal = TS5Bal.Text;
            //        RO.S6Bal = TS6Bal.Text;
            //        RO.S7Bal = TS7Bal.Text;
            //        RO.S8Bal = TS8Bal.Text;
            //        RO.S9Bal = TS9Bal.Text;
            //        RO.ID = lb.Text;
            //        RO.BANKCODE = ViewState["BANKCODE"].ToString();

            //        Res = RO.FnBl_ModifyCalc(RO);
            //        if (Res > 0)
            //        {
            //            WebMsgBox.Show("Updated Succesfully......!", this.Page);
            //            BindCalculations();
            //        }
            //        else
            //        {
            //            WebMsgBox.Show("Error occured while updating (already authorized)......!", this.Page);
            //            BindCalculations();
            //        }

            //    }
            //    else if (ViewState["Flag"].ToString() == "AT")
            //    {
            //        RO.FL = "CMID";
            //        RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //        RO.ID = lb.Text;
            //        RO.BANKCODE = ViewState["BANKCODE"].ToString();

            //        STRRes = RO.FnBL_GetMID(RO);
            //        if (STRRes.ToString() != Session["MID"].ToString())
            //        {

            //            RO.ID = lb.Text;
            //            RO.MID = Session["MID"].ToString();
            //            RO.ASONDT = EDT;
            //            RO.BRCD = ddlBrCode.SelectedValue.ToString();
            //            RO.FL = "AUT";
            //            RO.BANKCODE = ViewState["BANKCODE"].ToString();
            //            Res = RO.FnBl_AuthoCalc(RO);
            //            if (Res > 0)
            //            {
            //                WebMsgBox.Show("Authorized Succesfully......!", this.Page);
            //                BindCalculations();
            //            }
            //            else
            //            {
            //                WebMsgBox.Show("Already authorized......!", this.Page);
            //                BindCalculations();
            //            }
            //        }
            //        else
            //        {
            //            WebMsgBox.Show("Warning :User is restricted to authorize, Change user......!", this.Page);
            //            BindCalculations();
            //        }


            //    }
            //}

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridView1009_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string L1 = "";
        string L2 = "";
        string L3 = "";
        string MON = "";
        string MA = "";
        string KNT = "";
        string RD = "";
        string DF = "";
        string MW = "";
        string US = "";

        RO.BRCD = ddlBrCode.SelectedValue.ToString();
        RO.BANKCODE = ViewState["BANKCODE"].ToString();

        dt = RO.GetLableName(RO);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["REC_PRD"].ToString() == "201")
                    L1 = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "202")
                    L2 = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "203")
                    L3 = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "44" || dt.Rows[i]["REC_PRD"].ToString() == "4")
                    MON = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "312")
                    MA = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "408")
                    KNT = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "409")
                    RD = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "406")
                    DF = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "405")
                    MW = dt.Rows[i]["SHORTNAME"].ToString();
                else if (dt.Rows[i]["REC_PRD"].ToString() == "411")
                    US = dt.Rows[i]["SHORTNAME"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = L1 + "- Bal";
            e.Row.Cells[8].Text = L1 + "- Inst";
            e.Row.Cells[9].Text = L1 + "- Intr";

            e.Row.Cells[10].Text = L2 + "- Bal";
            e.Row.Cells[11].Text = L2 + "- Inst";
            e.Row.Cells[12].Text = L2 + "- Intr";

            e.Row.Cells[13].Text = L3 + "- Bal";
            e.Row.Cells[14].Text = L3 + "- Inst";
            e.Row.Cells[15].Text = L3 + "- Intr";


            e.Row.Cells[16].Text = MON;
            e.Row.Cells[17].Text = MA;
            e.Row.Cells[18].Text = KNT;
            e.Row.Cells[19].Text = RD;
            e.Row.Cells[20].Text = DF;
            e.Row.Cells[21].Text = MW;
            e.Row.Cells[22].Text = US;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_RowTotal")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_RowTotal")).Text;
            TotalValue = Convert.ToDouble(Amt);
            SumFooterValue += TotalValue;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("Lbl_SumTotal");
            lbl.Text = SumFooterValue.ToString();

        }
    }

    #endregion

    public void BindNew_Calculations()
    {
        try
        {
            if (ViewState["BANKCODE"].ToString() == "1009") //MSEB
            {
                GridView1009.Visible = true;
                GrdInsert.Visible = false;

                if (txtCustno.Text == "")
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";
                    EDT = DateSet();

                    RO.GRD = GridView1009;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    RO.FnBl_RecOperationsNew(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
                else
                {
                    string FL = "";

                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";

                    EDT = DateSet();
                    RO.GRD = GridView1009;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }

                    RO.FnBl_RecCustOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
            }


            else //TZSSPM
            {
                GridView1009.Visible = false;
                GrdInsert.Visible = true;

                if (txtCustno.Text == "")
                {
                    string FL = "";
                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";

                    EDT = DateSet();
                    RO.GRD = GrdInsert;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    RO.FnBl_RecOperationsNew(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
                else
                {
                    string FL = "";
                    FL = "SEL";
                    Lbl_GridName.Text = "Send Recovery Data";

                    EDT = DateSet();
                    RO.GRD = GrdInsert;
                    RO.BRCD = ddlBrCode.SelectedValue.ToString();
                    RO.ASONDT = EDT;
                    RO.MID = Session["MID"].ToString();
                    RO.FL = FL;
                    RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                    RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                    RO.MM = TxtMM.Text;
                    RO.YY = TxtYYYY.Text;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }

                    RO.FnBl_RecCustOperations(RO);
                    GrdSumDetails.Visible = false;
                    BtnUpdateAll.Visible = true;
                }
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_CreateAccwise_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";

            #region Specific Create
            if (Rdb_CreateType.SelectedValue == "S")
            {
                if (txtCustno.Text == "")
                {
                    RO.FL = "CHMASTER";
                    RO.BRCD = ddlBrCode.SelectedValue;
                    RO.RECCODE = DdlRecDept.SelectedValue;
                    RO.RECDIV = DdlRecDiv.SelectedValue;

                    string MasterCount = RO.FnBL_CheckMaster(RO);
                    if (Convert.ToInt32(MasterCount) > 0)
                    {

                        EDT = Session["EntryDate"].ToString();

                        RO.BRCD = ddlBrCode.SelectedValue.ToString();
                        RO.FL = "CRE";
                        RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.DeathFund = TxtDeathFund.Text;
                        RO.Narration = txtNarration.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();
                        if (chkMW.Checked == true)
                            RO.MW = "1";
                        else
                            RO.MW = "0";
                        if (chkUS.Checked == true)
                            RO.US = "1";
                        else
                            RO.US = "0";

                        Res = RO.FnBl_CreateCalcAcc(RO);
                        if (Res > 0)
                        {
                            Msg = Msg + " Recovery for " + DdlRecDept.SelectedItem.Text + " is created successfully....!";
                            WebMsgBox.Show("" + Msg, this.Page);
                        }
                        else
                        {
                            WebMsgBox.Show("Recovery creation failed....!", this.Page);
                        }
                        //}
                    }
                    else
                    {
                        WebMsgBox.Show("Customers not available for " + DdlRecDept.SelectedItem.Text + " ,cannot create Recovery", this.Page);
                    }
                }
                else//amruta
                {
                    RO.FL = "CHMASTER";
                    RO.BRCD = ddlBrCode.SelectedValue;
                    RO.RECCODE = DdlRecDept.SelectedValue;
                    RO.RECDIV = DdlRecDiv.SelectedValue;
                    RO.BANKCODE = ViewState["BANKCODE"].ToString();
                    if (hdnCustNo.Value == "0")
                    {
                        RO.CustNO = txtCustno.Text;
                        hdnCustNo.Value = txtCustno.Text;
                    }
                    else
                    {
                        RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                        hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                    }

                    string MasterCount = RO.FnBL_CheckCustMaster(RO);
                    if (Convert.ToInt32(MasterCount) > 0)
                    {
                        EDT = Session["EntryDate"].ToString();
                        RO.BRCD = ddlBrCode.SelectedValue.ToString();
                        RO.FL = "CRE";
                        RO.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.DeathFund = TxtDeathFund.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();
                        if (hdnCustNo.Value == "0")
                        {
                            RO.CustNO = txtCustno.Text;
                            hdnCustNo.Value = txtCustno.Text;
                        }
                        else
                        {
                            RO.CustNO = hdnCustNo.Value + "," + txtCustno.Text;
                            hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                        }

                        RO.BANKCODE = ViewState["BANKCODE"].ToString();

                        Res = RO.FnBl_CreateCustCalc(RO);
                        if (Res > 0)
                        {
                            Msg = Msg + " Recovery for " + DdlRecDept.SelectedItem.Text + " is created successfully....!";
                            WebMsgBox.Show("" + Msg, this.Page);
                        }
                        else
                        {
                            WebMsgBox.Show("Recovery creation failed....!", this.Page);
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Customers not available for " + DdlRecDept.SelectedItem.Text + " ,cannot create Recovery", this.Page);
                    }
                }
            }
            #endregion

            #region All Create
            else if (Rdb_CreateType.SelectedValue == "A")
            {
                DataTable DT1 = RS.GetCodes(Session["BRCD"].ToString(), DdlRecDiv.SelectedValue.ToString());
                if (DT1.Rows.Count > 0)
                {
                    for (int i = 0; i <= DT1.Rows.Count - 1; i++)
                    {
                        RO.FL = "EXI";
                        RO.BRCD = DT1.Rows[i]["BRCD"].ToString();
                        RO.ASONDT = Session["EntryDate"].ToString();
                        RO.MID = Session["MID"].ToString();
                        RO.RECCODE = DT1.Rows[i]["RECCODE"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();

                        STRRes = RO.FnBL_GetExistsRecovery(RO);
                        if (Convert.ToInt32(STRRes) > 0)
                        {
                            Msg = Msg + "Last recovery deleted for - " + TxtMM.Text + " , Year - " + TxtYYYY.Text + " ...!";
                        }

                        EDT = Session["EntryDate"].ToString();

                        RO.BRCD = DT1.Rows[i]["BRCD"].ToString();
                        RO.FL = "CRE";
                        RO.RECCODE = DT1.Rows[i]["RECCODE"].ToString();
                        RO.ASONDT = EDT;
                        RO.MID = Session["MID"].ToString();
                        RO.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RO.MM = TxtMM.Text;
                        RO.YY = TxtYYYY.Text;
                        RO.DeathFund = TxtDeathFund.Text;
                        RO.Narration = txtNarration.Text;
                        RO.BANKCODE = ViewState["BANKCODE"].ToString();
                        if (chkMW.Checked == true)
                            RO.MW = "1";
                        else
                            RO.MW = "0";
                        if (chkUS.Checked == true)
                            RO.US = "1";
                        else
                            RO.US = "0";

                        Res = RO.FnBl_CreateCalc(RO);
                    }

                }
                if (Res > 0)
                {
                    Msg = " Recovery created successfully for all department....!";
                    WebMsgBox.Show("" + Msg, this.Page);
                }
                else
                {
                    WebMsgBox.Show("Recovery creation failed....!", this.Page);
                }

            }
            #endregion

            //ClearData();
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
                Div_Reccode.Visible = false;
                BtnUpdateAll.Visible = true;
                BtnUpdateAll.Text = "Authorize All";
                ViewState["Flag"] = "AT";
                GrdSumDetails.Visible = false;
            }
            else
            {
                Div_Reccode.Visible = true;
                BtnUpdateAll.Visible = false;
                BtnUpdateAll.Text = "Update All";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void TxtRecDivNo_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string DivNo = CMN.GetDiviName(TxtRecDivNo.Text);
    //        if (DivNo != null)
    //        {
    //            TxtRecDivName.Text = DivNo;
    //            TxtRecDeptNo.Focus();
    //            AutoRecDeptName.ContextKey=TxtRecDivNo.Text+"_"+Session["BRCD"].ToString();
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Invalid Division code,Enter valid division code..!", this.Page);
    //            TxtRecDivNo.Text = "";
    //            TxtRecDivNo.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void TxtRecDivName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string RecDivNo = TxtRecDivName.Text;
    //        string[] CT = RecDivNo.Split('_');
    //        if (CT != null)
    //        {
    //            TxtRecDivNo.Text = CT[1].ToString();
    //            TxtRecDivName.Text = CT[0].ToString();
    //            TxtRecDeptNo.Focus();
    //            AutoRecDeptName.ContextKey = TxtRecDivNo.Text + "_" + Session["BRCD"].ToString();
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Invalid Division Name,Enter valid division Name..!", this.Page);
    //            TxtRecDivNo.Text = "";
    //            TxtRecDivNo.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void TxtRecDeptNo_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string DivNo = CMN.GetDeptName(TxtRecDivNo.Text,TxtRecDeptNo.Text);
    //        if (DivNo != null)
    //        {
    //            TxtRecDeptName.Text = DivNo;
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Invalid Department code,Enter valid Department code..!", this.Page);
    //            TxtRecDeptNo.Text = "";
    //            TxtRecDeptNo.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void TxtRecDeptName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string RecDeptNo = TxtRecDeptName.Text;
    //        string[] CT = RecDeptNo.Split('_');
    //        if (CT != null)
    //        {
    //            TxtRecDeptNo.Text = CT[1].ToString();
    //            TxtRecDeptName.Text = CT[0].ToString();
    //            TxtRecDeptNo.Focus();
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Invalid Department Name,Enter valid Department Name..!", this.Page);
    //            TxtRecDeptName.Text = "";
    //            TxtRecDeptName.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
}