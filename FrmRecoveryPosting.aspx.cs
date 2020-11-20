using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmRecoveryPosting : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsBindDropdown CBS_BD = new ClsBindDropdown();
    Cls_RecoCommon CM = new Cls_RecoCommon();
    ClsRecoveryPosting RP = new ClsRecoveryPosting();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();
    ClsRecoveryOperation RO = new ClsRecoveryOperation();
    ClsRecoveryOperation NRO = new ClsRecoveryOperation();
    DataTable dt = new DataTable();
    ClsBindDropdown CBSBD = new ClsBindDropdown();
    DbConnection Conn = new DbConnection();
    DbConnection conn = new DbConnection();
    string MM, YY, EDT;
    string STR = "";
    int ResultInt = 0;
    double SumFooterValue = 0, TotalValue = 0;
    int Res = 0;
    string STRRes,bankcd="";

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            AutoCustName.ContextKey = Session["BRCD"].ToString();
            BindBr();
            ddlBrCode.Focus();
            hdnCustNo.Value = "0";

            DIV1.Visible = false;
            DIV2.Visible = true;
            string BKCD = RS.FnBl_GetBANKCode(RS);

            if (BKCD != null)
            {
                ViewState["BANKCODE"] = BKCD;
            }
            else
            {
                ViewState["BANKCODE"] = "0";
            }
            autoglname.ContextKey = Session["BRCD"].ToString();
            EnText(false);
        }

    }

    #endregion

    #region User functions
    public void EnText(bool TF)
    {
        TxtChequeNo.Text = "";
        TxtChequeDate.Text = "";
        TxtChequeNo.Enabled = TF;
        TxtChequeDate.Enabled = TF;
    }
    public void ClearData()
    {
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        DdlRecDiv.SelectedValue = "0";
        DdlRecDept.SelectedValue = "0";
        TxtDebitCode.Text = "0";
        TxtDebitCodeName.Text = "0";
        ddlBrCode.Focus();
    }
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();
        DdlRecDiv.SelectedValue = "1";
        BindRecDiv();
        BindRecDept();
        TxtMM.Focus();
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

            //EDT = D[2].ToString() + "/" + MM + "/" + YY;
            EDT = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return EDT;
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

    public void GetPostAll()
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RS.GD = GridView1009;
            }
            else
            {
                RS.GD = Grd_SpecificPost;
            }

            RS.BRCD = ddlBrCode.SelectedValue.ToString();
            string EDT = DateSet();
            RO.BANKCODE = bankcd;
           // RS.BANKCODE = ViewState["BANKCODE"].ToString();
            RS.FL = "SHOWPOST";
            RS.ASONDT = EDT;
            RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
            RS.RECCODE = DdlRecDept.SelectedValue.ToString();
            RS.MM = TxtMM.Text;
            RS.YY = TxtYYYY.Text;
            RS.BANKCODE = bankcd;

            ResultInt = RS.FnBl_GetPostings(RS);
            GetRecTotal();
            if (Convert.ToString(ViewState["BtnType"]) == "Update")
            {
                if (ResultInt > 0)
                {
                    WebMsgBox.Show("Data Modified Sucessfully.......!", this.Page);
                    return;
                }
            }
            GetRecTotal();
            BtnUpdateAll.Visible = true;


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    public void GetPostSpecific()
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                GridView1009.Visible = true;
                Grd_SpecificPost.Visible = false;
                RS.GD = GridView1009;

            }
            else
            {
                GridView1009.Visible = false;
                Grd_SpecificPost.Visible = true;
                RS.GD = Grd_SpecificPost;
            }
            if (txtCustno.Text == "")
            {
                RS.BRCD = ddlBrCode.SelectedValue.ToString();
                string EDT = DateSet();
                RS.FL = "SHOWPOST";
                RS.ASONDT = EDT;
                RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
                RS.RECCODE = DdlRecDept.SelectedValue.ToString();
                RS.MM = TxtMM.Text;
                RS.YY = TxtYYYY.Text;
                RS.BANKCODE = bankcd;
               // RS.BANKCODE = ViewState["BANKCODE"].ToString();

                ResultInt = RS.FnBl_GetPostings(RS);
                GetRecTotal();
                if (ResultInt > 0)
                {
                    if (Convert.ToString(ViewState["BtnType"]) == "Update")
                    {
                        WebMsgBox.Show("Data Modified Sucessfully.......!", this.Page);
                        return;
                    }
                }
                GetRecTotal();
            }
            else
            {

                RS.BRCD = ddlBrCode.SelectedValue.ToString();
                string EDT = DateSet();
                RS.FL = "SHOWPOST";
                RS.ASONDT = EDT;
                RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
                RS.BANKCODE = bankcd;
               // RS.BANKCODE = ViewState["BANKCODE"].ToString();

                if (hdnRec.Value == "0")
                {
                    RS.RECCODE = DdlRecDept.SelectedValue.ToString();
                    hdnRec.Value = DdlRecDept.SelectedValue.ToString();
                }

                else
                {
                    RS.RECCODE = hdnRec.Value + "," + DdlRecDept.SelectedValue.ToString();
                    hdnRec.Value = hdnRec.Value + "," + DdlRecDept.SelectedValue.ToString();
                }

                if (hdnCustNo.Value == "0")
                {
                    RS.CustNo = txtCustno.Text;
                    hdnCustNo.Value = txtCustno.Text;
                }
                else
                {
                    RS.CustNo = hdnCustNo.Value + "," + txtCustno.Text;
                    hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                }

                ResultInt = RS.FnBl_GetCustPostings(RS);

                GetRecTotalSpe();
            }
            BtnUpdateAll.Visible = true;
            BtnSelectAll.Visible = true;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetPostSpecifictTotalOnly()
    {
        try
        {
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RS.GD = GridView1009;

            }
            else
            {
                RS.GD = Grd_SpecificPost;
            }

            RS.BANKCODE = ViewState["BANKCODE"].ToString();

            if (txtCustno.Text == "")
            {
                RS.BRCD = ddlBrCode.SelectedValue.ToString();
                string EDT = DateSet();

                RS.FL = "SHOWPOST";
                RS.ASONDT = EDT;
                RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
                RS.RECCODE = DdlRecDept.SelectedValue.ToString();
                RS.MM = TxtMM.Text;
                RS.YY = TxtYYYY.Text;

                ResultInt = RS.FnBl_GetPostings(RS);
                if (ResultInt > 0)
                {
                    GetRecTotal();
                }
            }
            else
            {


                RS.BRCD = ddlBrCode.SelectedValue.ToString();
                string EDT = DateSet();
                RS.FL = "SHOWPOST";
                RS.ASONDT = EDT;
                RS.RECDIV = DdlRecDiv.SelectedValue.ToString();

                if (hdnCustNo.Value == "0")
                {
                    RS.CustNo = txtCustno.Text;
                    hdnCustNo.Value = txtCustno.Text;
                }
                else
                {
                    RS.CustNo = hdnCustNo.Value + "," + txtCustno.Text;
                    hdnCustNo.Value = hdnCustNo.Value + "," + txtCustno.Text;
                }

                ResultInt = RS.FnBl_GetCustPostings(RS);
                if (ResultInt > 0)
                {
                    GetRecTotalSpe();
                }
            }
            BtnUpdateAll.Visible = true;
            BtnSelectAll.Visible = true;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text change

    protected void Rdb_PostType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_PostType.SelectedValue == "A")
            {
                BtnPost.Visible = true;
                BtnPostSpecific.Visible = false;
                DivType.Visible = false;
                DIV1.Visible = false;
                DIV2.Visible = true;
                txtCustno.Text = "";
                txtCustName.Text = "";
            }
            else
            {

                DIV1.Visible = false;
                DIV2.Visible = true;
                BtnPost.Visible = false;
                BtnPostSpecific.Visible = true;
                DivType.Visible = true;
                txtCustno.Text = "";
                txtCustName.Text = "";
                Rdb_Type.SelectedValue = "D";
            }
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
            BindRecDept();
            DdlRecDept.Focus();
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
                TxtMM.Focus();
            }
            else
            {
                DdlRecDiv.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
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
    protected void TxtDebitCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CM.BRCD = ddlBrCode.SelectedValue.ToString();
            CM.SUBGLCODE = TxtDebitCode.Text;
            string res = CBS_BD.GetGLGroup(TxtDebitCode.Text, Session["BRCD"].ToString(), "0");
            if (res != null)
            {
                ViewState["GLGroup"] = res.ToString();
            }

            string NA = CM.FnBL_GetGlName(CM);
            if (NA != null)
            {
                string[] NAME = NA.Split('_');
                TxtDebitCodeName.Text = NAME[0].ToString();
                ViewState["DGL"] = NAME[1].ToString();
                if (ViewState["GLGroup"].ToString() == "CBB" && TxtDebitCode.Text != "99")
                {
                    EnText(true);
                    TxtChequeNo.Focus();
                }
                else
                {
                    EnText(false);
                }

            }
            else
            {
                WebMsgBox.Show("Invalid Code...!", this.Page);
                TxtDebitCode.Text = "";
                TxtDebitCode.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    #endregion

    #region On click post

    protected void BtnPost_Click(object sender, EventArgs e)
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());

            RP.BRCD = ddlBrCode.SelectedValue.ToString();
            string EDT = DateSet();
            RP.FL = "ALLPOST";
            RP.ASONDT = EDT;
            RP.DEBITCD = TxtDebitCode.Text;
            RP.RECDIV = DdlRecDiv.SelectedValue.ToString();
            RP.RECCODE = DdlRecDept.SelectedValue.ToString();
            RP.MID = Session["MID"].ToString();
            RP.MM = TxtMM.Text;
            RP.YY = TxtYYYY.Text;
            RP.BANKCODE = bankcd;
           // RP.BANKCODE = ViewState["BANKCODE"].ToString();
            RP.ChequeNo = TxtChequeNo.Text == "" ? "0" : TxtChequeNo.Text;
            RP.ChequeDate = TxtChequeDate.Text == "" ? "01/01/1990" : TxtChequeDate.Text;

            STR = RP.FnBl_RecoveryPost(RP);
            if (STR != null)
            {
                WebMsgBox.Show("Recovery for " + DdlRecDept.SelectedItem.Text + " is Posted successfully with Set No- " + STR + "...!", this.Page);
                AfterPost();
            }
            else
            {
                WebMsgBox.Show("Recovery Posting already done for authorized data...!", this.Page);
            }
            ClearData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void BtnPostSpecific_Click(object sender, EventArgs e)
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());

            if (TxtDebitCode.Text != "" && TxtDebitCode.Text != null)
            {
                string MergeCustno = "", MergeMM = "", MergeYYYY = "";
                string[] DDate = new string[10];
                string MergeId = "";

                if (ViewState["BANKCODE"].ToString() == "1009")
                {
                    foreach (GridViewRow Grow in GridView1009.Rows)
                    {
                        if (((CheckBox)Grow.FindControl("Chk_Specific1009")).Checked)
                        {
                            if (MergeMM.ToString() == "" && MergeCustno.ToString() == "" && MergeYYYY.ToString() == "")
                            {
                                TextBox TxtCustno = (TextBox)Grow.FindControl("TxtCustNo");
                                Label LblMM = (Label)Grow.FindControl("Lbl_MM");
                                Label LblYY = (Label)Grow.FindControl("Lbl_YYYY");
                                MergeCustno = TxtCustno.Text.Trim();
                                MergeMM = LblMM.Text.Trim();
                                MergeYYYY = LblYY.Text.Trim();
                            }
                            else
                            {
                                TextBox TxtCustno = (TextBox)Grow.FindControl("TxtCustNo");
                                Label LblMM = (Label)Grow.FindControl("Lbl_MM");
                                Label LblYY = (Label)Grow.FindControl("Lbl_YYYY");
                                MergeCustno = MergeCustno + "," + TxtCustno.Text.Trim();
                                MergeMM = MergeMM + "," + LblMM.Text.Trim();
                                MergeYYYY = MergeYYYY + "," + LblYY.Text.Trim();
                            }
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow Grow in Grd_SpecificPost.Rows)
                    {
                        if (((CheckBox)Grow.FindControl("Chk_Specific")).Checked)
                        {
                            if (MergeMM.ToString() == "" && MergeCustno.ToString() == "" && MergeYYYY.ToString() == "")
                            {
                                TextBox TxtCustno = (TextBox)Grow.FindControl("TxtCustNo");
                                Label LblMM = (Label)Grow.FindControl("Lbl_MM");
                                Label LblYY = (Label)Grow.FindControl("Lbl_YYYY");
                                MergeCustno = TxtCustno.Text.Trim();
                                MergeMM = LblMM.Text.Trim();
                                MergeYYYY = LblYY.Text.Trim();
                            }
                            else
                            {
                                TextBox TxtCustno = (TextBox)Grow.FindControl("TxtCustNo");
                                Label LblMM = (Label)Grow.FindControl("Lbl_MM");
                                Label LblYY = (Label)Grow.FindControl("Lbl_YYYY");
                                MergeCustno = MergeCustno + "," + TxtCustno.Text.Trim();
                                MergeMM = MergeMM + "," + LblMM.Text.Trim();
                                MergeYYYY = MergeYYYY + "," + LblYY.Text.Trim();
                            }
                        }
                    }
                }
               

                if (MergeMM.ToString() != "" && MergeCustno.ToString() != "" && MergeYYYY.ToString() != "")
                {
                    if (hdnCustNo.Value == "0")
                    {
                        RP.ChequeNo = TxtChequeNo.Text == "" ? "0" : TxtChequeNo.Text;
                        RP.ChequeDate = TxtChequeDate.Text == "" ? "01/01/1990" : TxtChequeDate.Text;
                        RP.BRCD = ddlBrCode.SelectedValue.ToString();
                        string EDT = DateSet();
                        RP.FL = "SPEPOST";
                        RP.ASONDT = EDT;
                        RP.DEBITCD = TxtDebitCode.Text;
                        RP.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RP.RECCODE = DdlRecDept.SelectedValue.ToString();
                        RP.MID = Session["MID"].ToString();
                        RP.MM = TxtMM.Text;
                        RP.YY = TxtYYYY.Text;
                        RP.MergeCustno = MergeCustno.ToString();
                        RP.MergeMM = MergeMM.ToString();
                        RP.MergeYYYY = MergeYYYY.ToString();
                        RP.BANKCODE = bankcd;
                        //RP.BANKCODE = ViewState["BANKCODE"].ToString();

                        STR = RP.FnBl_RecoveryPostSpecific(RP);
                    }
                    else
                    {
                        RP.ChequeNo = TxtChequeNo.Text == "" ? "0" : TxtChequeNo.Text;
                        RP.ChequeDate = TxtChequeDate.Text == "" ? "01/01/1990" : TxtChequeDate.Text;
                        RP.BRCD = ddlBrCode.SelectedValue.ToString();
                        string EDT = DateSet();
                        RP.FL = "SPEPOST";
                        RP.ASONDT = EDT;
                        RP.DEBITCD = TxtDebitCode.Text;
                        RP.RECDIV = DdlRecDiv.SelectedValue.ToString();
                        RP.RECCODE = hdnRec.Value;
                        RP.MID = Session["MID"].ToString();
                        RP.CustNo = hdnCustNo.Value;

                        RP.MergeCustno = MergeCustno.ToString();
                        RP.MergeMM = MergeMM.ToString();
                        RP.MergeYYYY = MergeYYYY.ToString();
                        RP.BANKCODE = bankcd;
                      //  RP.BANKCODE = ViewState["BANKCODE"].ToString();
                        STR = RP.FnBl_RecoveryCustPostSpecific(RP);
                    }
                    if (STR != "" && STR != null)
                    {
                        GetPostSpecific();
                        GetPostSpecifictTotalOnly();
                        WebMsgBox.Show("Recovery for " + DdlRecDept.SelectedItem.Text + " is Posted successfully with Set No- " + STR + "...!", this.Page);
                    }
                    else if (STR == "")
                    {
                        BtnPostSpecific.Focus();
                        WebMsgBox.Show("Specific Recovery failed....!", this.Page);
                        return; 
                    }
                    else
                    {
                        BtnPostSpecific.Focus();
                        WebMsgBox.Show("Recovery Posting already done for authorized data...!", this.Page);
                        return; 
                    }
                    ClearData();
                }
                else
                {
                    BtnPostSpecific.Focus(); 
                    WebMsgBox.Show("Recovery for posting not selected....!", this.Page);
                    return;
                }
            }
            else
            {
                TxtDebitCode.Focus();
                WebMsgBox.Show("Enter debit code first ...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    #endregion

    protected void BtnUpdateAll_Click(object sender, EventArgs e)
    {
        try
        {
     
            ViewState["BtnType"] = "Update";
            if (Rdb_PostType.SelectedValue == "S")
            {
                GetPostSpecific();
                TxtDebitCode.Focus();
            }
            else
            {
                GetPostAll();
                TxtDebitCode.Focus();
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
            if (Rdb_PostType.SelectedValue == "S")
            {
                GetPostSpecific();
                TxtDebitCode.Focus();
            }
            else
            {
                GetPostAll();
                TxtDebitCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void AfterPost()
    {
        try
        {
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RS.GD = GridView1009;
            }
            else
            {
                RS.GD = Grd_SpecificPost;
            }

            RS.BRCD = ddlBrCode.SelectedValue.ToString();
            string EDT = DateSet();
            RS.BANKCODE = ViewState["BANKCODE"].ToString();
            RS.FL = "SHOWPOST";
            RS.ASONDT = EDT;
            RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
            RS.RECCODE = DdlRecDept.SelectedValue.ToString();
            RS.MM = TxtMM.Text;
            RS.YY = TxtYYYY.Text;

            ResultInt = RS.FnBl_GetPostings(RS);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetRecTotal()
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());

            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RS.GD = GridView1009;
            }
            else
            {
                RS.GD = Grd_SpecificPost;
            }
            RS.BANKCODE = bankcd;
           // RS.BANKCODE = ViewState["BANKCODE"].ToString();
            RS.BRCD = ddlBrCode.SelectedValue.ToString();
            string EDT = DateSet();
            RS.FL = "ShowTotal";
            RS.ASONDT = EDT;
            RS.RECDIV = DdlRecDiv.SelectedValue.ToString();
            RS.RECCODE = DdlRecDept.SelectedValue.ToString();
            RS.MM = TxtMM.Text;
            RS.YY = TxtYYYY.Text;
            string AMT = RS.FnBl_GetPostingTotal(RS);
            double val;

            if (AMT != null)
            {
                if (double.TryParse(AMT, out val))
                {
                    TxtAmountSpe.Text = AMT;
                }
                else
                {
                    WebMsgBox.Show("" + AMT, this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
   
    public void GetRecTotalSpe()
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RS.GD = GridView1009;
            }
            else
            {
                RS.GD = Grd_SpecificPost;
            }
            RS.BANKCODE = bankcd;
           // RS.BANKCODE = ViewState["BANKCODE"].ToString();

            RS.BRCD = ddlBrCode.SelectedValue.ToString();
            string EDT = DateSet();

            RS.FL = "ShowTotalSpe";
            RS.ASONDT = EDT;
            RS.RECDIV = DdlRecDiv.SelectedValue.ToString();

            if (hdnRec.Value != "0")
                RS.RECCODE = hdnRec.Value;
            else
                RS.RECCODE = DdlRecDept.SelectedValue.ToString();

            RS.MM = TxtMM.Text;
            RS.YY = TxtYYYY.Text;
            if (hdnCustNo.Value != "0")
                RS.CustNo = hdnCustNo.Value;
            else
                RS.CustNo = txtCustno.Text;

            TxtAmountSpe.Text = RS.FnBl_GetPostingTotal(RS);
            if (TxtAmountSpe.Text == "")
            {
                WebMsgBox.Show("Authorized First...!", this.Page);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Chk_Specific_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            double Value = 0;

            foreach (GridViewRow Grow in Grd_SpecificPost.Rows)
            {
                if (((CheckBox)Grow.FindControl("Chk_Specific")).Checked)
                {
                    TextBox TS1Bal = (TextBox)Grow.FindControl("TxtS1Bal") as TextBox;
                    TextBox TS1Inst = (TextBox)Grow.FindControl("TxtS1Inst") as TextBox;
                    TextBox TS1Intr = (TextBox)Grow.FindControl("TxtS1Intr") as TextBox;
                    TextBox TS2Bal = (TextBox)Grow.FindControl("TxtS2Bal") as TextBox;
                    TextBox TS2Inst = (TextBox)Grow.FindControl("TxtS2Inst") as TextBox;
                    TextBox TS2Intr = (TextBox)Grow.FindControl("TxtS2Intr") as TextBox;
                    TextBox TS3Bal = (TextBox)Grow.FindControl("TxtS3Bal") as TextBox;
                    TextBox TS4Bal = (TextBox)Grow.FindControl("TxtS4Bal") as TextBox;
                    TextBox TS5Bal = (TextBox)Grow.FindControl("TxtS5Bal") as TextBox;
                    TextBox TS6Bal = (TextBox)Grow.FindControl("TxtS6Bal") as TextBox;
                    TextBox TS7Bal = (TextBox)Grow.FindControl("TxtS7Bal") as TextBox;
                    TextBox TS8Bal = (TextBox)Grow.FindControl("TxtS8Bal") as TextBox;
                    TextBox TS9Bal = (TextBox)Grow.FindControl("TxtS9Bal") as TextBox;
                    TextBox TSurityAmt = (TextBox)Grow.FindControl("TxtSurityAmt") as TextBox;

                    if (Value == 0)
                    {
                        Value = Convert.ToDouble(TS1Inst.Text) + Convert.ToDouble(TS1Intr.Text) + Convert.ToDouble(TS2Inst.Text)
                            + Convert.ToDouble(TS2Intr.Text) + Convert.ToDouble(TS3Bal.Text) + Convert.ToDouble(TS4Bal.Text) + Convert.ToDouble(TS5Bal.Text)
                            + Convert.ToDouble(TS6Bal.Text) + Convert.ToDouble(TS7Bal.Text) + Convert.ToDouble(TS8Bal.Text) + Convert.ToDouble(TS9Bal.Text) + Convert.ToDouble(TSurityAmt.Text);
                    }
                    else
                    {
                        Value = Value + Convert.ToDouble(TS1Inst.Text) + Convert.ToDouble(TS1Intr.Text) + Convert.ToDouble(TS2Inst.Text)
                           + Convert.ToDouble(TS2Intr.Text) + Convert.ToDouble(TS3Bal.Text) + Convert.ToDouble(TS4Bal.Text) + Convert.ToDouble(TS5Bal.Text)
                           + Convert.ToDouble(TS6Bal.Text) + Convert.ToDouble(TS7Bal.Text) + Convert.ToDouble(TS8Bal.Text) + Convert.ToDouble(TS9Bal.Text) + Convert.ToDouble(TSurityAmt.Text); 
                    }
                }
                TxtAmountSpe.Text = Value.ToString();
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
 
    protected void Rdb_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rdb_Type.SelectedValue == "D")
        {
            DIV1.Visible = false;
            DIV2.Visible = true;
            txtCustno.Text = "";
            txtCustName.Text = "";
            btnAdd.Visible = false;
        }
        else
        {
            DIV1.Visible = true;
            DIV2.Visible = false;
            txtCustno.Text = "";
            txtCustName.Text = "";
            btnAdd.Visible = true;
        }
    }

    protected void txtCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CustName = "";
            RP.BRCD = ddlBrCode.SelectedValue;
            RP.CustNo = txtCustno.Text;

            CustName = RP.GetCustName(RP);

            if (CustName == null || CustName == "")
            {
                WebMsgBox.Show("Custno is Invalid!!!", this.Page);
                txtCustno.Focus();
            }
            else
            {
                txtCustName.Text = CustName;
                DataTable dt = new DataTable();
                dt = RP.GetCustDeatails(RP);
                if (dt.Rows.Count > 0)
                {
                    DdlRecDiv.SelectedValue = dt.Rows[0]["DIVNO"].ToString();
                    BindRecDept();
                    DdlRecDept.SelectedValue = dt.Rows[0]["OFFNO"].ToString();
                }
                if (Rdb_PostType.SelectedValue == "S")
                {
                    GetPostSpecific();
                    TxtDebitCode.Focus();
                }
                else
                {
                    TxtDebitCode.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtCustno.Text = "";
        txtCustName.Text = "";
        DdlRecDiv.SelectedValue = "1";
        DdlRecDept.SelectedValue = "0";
    }
 
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        //AutoRNames.ContextKey = ddlBrCode.SelectedValue.ToString() + "_" + txtCustno.Text.ToString();
        string CustNo = txtCustName.Text;
        string[] Cust = CustNo.ToString().Split('_');
        if (Cust.Length > 1)
        {
            txtCustno.Text = Cust[1].ToString();
            txtCustName.Text = Cust[0].ToString();
            RP.BRCD = ddlBrCode.SelectedValue;
            RP.CustNo = txtCustno.Text;
            DataTable dt = new DataTable();
            dt = RP.GetCustDeatails(RP);
            if (dt.Rows.Count > 0)
            {
                DdlRecDiv.SelectedValue = dt.Rows[0]["DIVNO"].ToString();
                BindRecDept();
                DdlRecDept.SelectedValue = dt.Rows[0]["OFFNO"].ToString();
            }
            if (Rdb_PostType.SelectedValue == "S")
            {
                GetPostSpecific();
                TxtDebitCode.Focus();
            }
            else
            {
                TxtDebitCode.Focus();
            }

        }
    }
  
    protected void Grd_SpecificPost_RowUpdated(object sender, GridViewUpdateEventArgs e)
    {
        Label lb = (Label)Grd_SpecificPost.Rows[e.RowIndex].FindControl("Lbl_ID");
        Label lbmm = (Label)Grd_SpecificPost.Rows[e.RowIndex].FindControl("Lbl_MM");
        Label lbyy = (Label)Grd_SpecificPost.Rows[e.RowIndex].FindControl("Lbl_YYYY");
        TextBox TxtCustnoGrd = (TextBox)Grd_SpecificPost.Rows[e.RowIndex].FindControl("TxtCustno") as TextBox;

        string redirectURL = "FrmModifyData.aspx?FL=POSTMOD&BKCD=" + ViewState["BANKCODE"].ToString() + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&MM=" + lbmm.Text + "&YY=" + lbyy.Text + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&CNO=" + TxtCustnoGrd.Text + "&ID=" + lb.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + redirectURL + "', 'popup_window', 'width=1100,height=400,left=150,top=150,resizable=no');", true);
    }
  
    protected void Grd_SpecificPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = new DataTable();
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

    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            EDT = Session["EntryDate"].ToString();
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                string redirectURL = "FrmRecRView.aspx?FL=P&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + DdlRecDept.SelectedItem.Text + "&rptname=RptRecoveryStatement_1009.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {

                string redirectURL = "FrmRecRView.aspx?FL=P&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + DdlRecDept.SelectedItem.Text + "&rptname=RptRecoveryStatement.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnSelectAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                foreach (GridViewRow Gr in GridView1009.Rows)
                {
                    CheckBox chk1 = (CheckBox)Gr.FindControl("Chk_Specific1009") as CheckBox;
                    chk1.Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow Gr in Grd_SpecificPost.Rows)
                {
                    CheckBox chk2 = (CheckBox)Gr.FindControl("Chk_Specific") as CheckBox;
                    chk2.Checked = true;
                }
            }
            GetRecTotalSpe();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GridView1009_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //bankcd = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='RecoveryCD'"));
            bankcd = RP.GetRecTypeCode(ddlBrCode.SelectedValue.ToString(), DdlRecDiv.SelectedValue.ToString(), DdlRecDept.SelectedValue.ToString());

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

           // RO.BANKCODE = ViewState["BANKCODE"].ToString();
            RO.BANKCODE = bankcd;

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
                    else if (dt.Rows[i]["REC_PRD"].ToString() == "4")
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

            if (Rdb_PostType.SelectedValue == "A")
            {
                e.Row.Cells[0].Enabled = false;
            }
            else
            {
                e.Row.Cells[0].Enabled = true;
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
            Label lb = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_ID");
            Label lbmm = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_MM");
            Label lbyy = (Label)GridView1009.Rows[e.RowIndex].FindControl("Lbl_YYYY");
            TextBox TxtCustnoGrd = (TextBox)GridView1009.Rows[e.RowIndex].FindControl("TxtCustno") as TextBox;

            string redirectURL = "FrmModifyData.aspx?FL=POSTMOD&BKCD=" + ViewState["BANKCODE"].ToString() + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&MM=" + lbmm.Text + "&YY=" + lbyy.Text + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&CNO=" + TxtCustnoGrd.Text + "&ID=" + lb.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + redirectURL + "', 'popup_window', 'width=1100,height=400,left=150,top=150,resizable=no');", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void Chk_Specific1009_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            double Value = 0;

            foreach (GridViewRow Grow in GridView1009.Rows)
            {
                if (((CheckBox)Grow.FindControl("Chk_Specific1009")).Checked)
                {
                    TextBox TS1Bal = (TextBox)Grow.FindControl("TxtS1Bal") as TextBox;
                    TextBox TS1Inst = (TextBox)Grow.FindControl("TxtS1Inst") as TextBox;
                    TextBox TS1Intr = (TextBox)Grow.FindControl("TxtS1Intr") as TextBox;
                    TextBox TS2Bal = (TextBox)Grow.FindControl("TxtS2Bal") as TextBox;
                    TextBox TS2Inst = (TextBox)Grow.FindControl("TxtS2Inst") as TextBox;
                    TextBox TS2Intr = (TextBox)Grow.FindControl("TxtS2Intr") as TextBox;

                    TextBox TS4Bal = (TextBox)Grow.FindControl("TxtS4Bal") as TextBox;
                    TextBox TS5Bal = (TextBox)Grow.FindControl("TxtS5Bal") as TextBox;
                    TextBox TS6Bal = (TextBox)Grow.FindControl("TxtS6Bal") as TextBox;
                    TextBox TS7Bal = (TextBox)Grow.FindControl("TxtS7Bal") as TextBox;
                    TextBox TS8Bal = (TextBox)Grow.FindControl("TxtS8Bal") as TextBox;
                    TextBox TS9Bal = (TextBox)Grow.FindControl("TxtS9Bal") as TextBox;
                    TextBox TS10Bal = (TextBox)Grow.FindControl("TxtS10Bal") as TextBox;

                    TextBox TS3Bal = (TextBox)Grow.FindControl("TxtS3Bal") as TextBox;
                    TextBox TS3Inst = (TextBox)Grow.FindControl("TxtS3Inst") as TextBox;
                    TextBox TS3Intr = (TextBox)Grow.FindControl("TxtS3Intr") as TextBox;

                    //TextBox TSurityAmt = (TextBox)Grow.FindControl("TxtSurityAmt") as TextBox;

                    if (Value == 0)
                    {
                        Value = Convert.ToDouble(TS1Inst.Text) + Convert.ToDouble(TS1Intr.Text) + Convert.ToDouble(TS2Inst.Text) + Convert.ToDouble(TS3Inst.Text) + Convert.ToDouble(TS3Intr.Text)
                            + Convert.ToDouble(TS2Intr.Text) + Convert.ToDouble(TS4Bal.Text) + Convert.ToDouble(TS5Bal.Text)
                            + Convert.ToDouble(TS6Bal.Text) + Convert.ToDouble(TS7Bal.Text) + Convert.ToDouble(TS8Bal.Text) + Convert.ToDouble(TS9Bal.Text) + Convert.ToDouble(TS10Bal.Text);
                            //+Convert.ToDouble(TSurityAmt.Text);
                    }
                    else
                    {
                        Value = Value + Convert.ToDouble(TS1Inst.Text) + Convert.ToDouble(TS1Intr.Text) + Convert.ToDouble(TS2Inst.Text) + Convert.ToDouble(TS3Inst.Text) + Convert.ToDouble(TS3Intr.Text)
                           + Convert.ToDouble(TS2Intr.Text) + Convert.ToDouble(TS4Bal.Text) + Convert.ToDouble(TS5Bal.Text)
                           + Convert.ToDouble(TS6Bal.Text) + Convert.ToDouble(TS7Bal.Text) + Convert.ToDouble(TS8Bal.Text) + Convert.ToDouble(TS9Bal.Text) + Convert.ToDouble(TS10Bal.Text);
                           //+Convert.ToDouble(TSurityAmt.Text);
                    }
                }
                TxtAmountSpe.Text = Value.ToString();
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
 
    protected void Btn_ExReport_Click(object sender, EventArgs e)
    {
        try
        {
            EDT = DateSet();
            string redirectURL = "FrmRecRView.aspx?REPTYPE=" + Rdb_PostType.SelectedValue.ToString() + "&BKCD=" + ViewState["BANKCODE"].ToString() + "&FL=ALLPOST&SFL=EXREPORT&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + DdlRecDept.SelectedItem.Text + "&rptname=RptExRecBeforePost.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void TxtDebitCodeName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtDebitCodeName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtDebitCodeName.Text = CT[0].ToString();
                TxtDebitCode.Text = CT[1].ToString();
                string res = CBS_BD.GetGLGroup(TxtDebitCode.Text, Session["BRCD"].ToString(), "0");
                if (res != null)
                {
                    ViewState["GLGroup"] = res.ToString();
                }

                string[] GLS = CBSBD.GetAccTypeGL(TxtDebitCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DGL"] = GLS[1].ToString();

                if (ViewState["GLGroup"].ToString() == "CBB" && TxtDebitCode.Text != "99")
                {
                    EnText(true);
                    TxtChequeNo.Focus();
                }
                else
                {
                    EnText(false);
                }
            }
            else
            {
                TxtDebitCodeName.Text = "";
                TxtDebitCode.Text = "";
                TxtDebitCodeName.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    protected void TxtChequeDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(Conn.ConvertDate(TxtChequeDate.Text)) <= Convert.ToDateTime(Conn.ConvertDate(Session["EntryDate"].ToString())))
            {
                string STR = RP.InstMonthDiff(Session["EntryDate"].ToString(), TxtChequeDate.Text);
                if (Convert.ToInt32(STR) <= 3)
                {
                    if (Rdb_PostType.SelectedValue == "S")
                        BtnPostSpecific.Focus();
                    else
                        BtnPost.Focus();
                }
                else
                {
                    WebMsgBox.Show("Instrument date invalid...!", this.Page);
                    TxtChequeDate.Text = "";
                    TxtChequeDate.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Instrument date must be less than equal to working date...!", this.Page);
                TxtChequeDate.Text = "";
                TxtChequeDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}