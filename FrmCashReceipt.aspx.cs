using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class FrmCashReceipt : System.Web.UI.Page
{
    int ShareSuspGl = 0, ShareAccNo = 0;
    int resultout, resultint = 0;
    string AC_Status = "", FL = "";
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsVoucherActInfo VA = new ClsVoucherActInfo();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    ClsShareApp SA = new ClsShareApp();
    scustom customcs = new scustom();
    ClsAccopen accop = new ClsAccopen();
    ClsCommon CMN = new ClsCommon();
    ClsNewTDA NewTDA = new ClsNewTDA();
    DbConnection conn = new DbConnection();
    DataTable Datatbl = new DataTable();
    DataTable DT = new DataTable();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsGlMast GlMaster = new ClsGlMast();
    string GlCode = "", sResult = "", Param = "";

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string FL = Request.QueryString["FL"].ToString();
                ViewState["FL"] = FL;
                if (ViewState["FL"].ToString() == "ACO")
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("FrmLogin.aspx");
                    }
                    string P = Request.QueryString["P"].ToString();
                    string A = Request.QueryString["A"].ToString();
                    string R = Request.QueryString["R"].ToString();

                    ViewState["PCode"] = string.IsNullOrEmpty(P.ToString()) ? "0" : P.ToString();
                    ViewState["ACode"] = string.IsNullOrEmpty(A.ToString()) ? "0" : A.ToString();
                    ViewState["RecSrno"] = string.IsNullOrEmpty(R.ToString()) ? "0" : R.ToString();
                    TxtProcode.Text = ViewState["PCode"].ToString();
                    TxtAccNo.Text = ViewState["ACode"].ToString();
                    TxtRecNo.Text = ViewState["RecSrno"].ToString();

                    Procode_Change();
                    Accno_Change();
                    GetTotalAmt();
                    SetFDAmt(TxtProcode.Text, TxtAccNo.Text);

                }
                else
                {
                    TxtProcode.Focus();

                }

                BindGrid();
                TxtEntrydate.Text = Session["EntryDate"].ToString();
                TxtEntrydate.Enabled = false;
                autoglname.ContextKey = Session["BRCD"].ToString();



            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

    }

    #endregion

    #region Text Changed Event

    protected void rbtnNewSet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DivExistSet.Visible = false;
            txtExistSetNo.Text = "";
            txtExistSetNo.Enabled = true;
            TxtProcode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnExistSet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DivExistSet.Visible = true;
            txtExistSetNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtExistSetNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtExistSetNo.Text.Trim().ToString() == "" ? "0" : txtExistSetNo.Text.Trim().ToString()) > 0)
            {
                DT = new DataTable();
                DT = CurrentCls.CheckCashSet(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtExistSetNo.Text.Trim().ToString());
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() != "1003")
                    {
                        //if (DT.Rows[0]["Activity"].ToString() == "3")
                        //{
                        //    txtExistSetNo.Text = txtExistSetNo.Text.Trim().ToString();
                        //    txtExistSetNo.Enabled = false;
                        //    TxtProcode.Focus();
                        //}
                        //else
                        //{
                        //    txtExistSetNo.Text = "";
                        //    txtExistSetNo.Focus();
                        //    lblMessage.Text = "Voucher is not of type-cash...!!";
                        //    ModalPopup.Show(this.Page);
                        //    return;
                        //}

                        //  Commited and changes by amol on 2018-07-04 (As per instruction by ambika mam for take all types of set)
                        txtExistSetNo.Text = txtExistSetNo.Text.Trim().ToString();
                        txtExistSetNo.Enabled = false;
                        TxtProcode.Focus();
                    }
                    else
                    {
                        txtExistSetNo.Text = "";
                        txtExistSetNo.Focus();
                        WebMsgBox.Show("Set No " + txtExistSetNo.Text.ToString() + " is already authorized ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtExistSetNo.Text = "";
                    txtExistSetNo.Focus();
                    WebMsgBox.Show("Set No " + txtExistSetNo.Text.ToString() + " is either deleted or not exists....!", this.Page);
                    return;
                }
            }
            else
            {
                txtExistSetNo.Text = "";
                txtExistSetNo.Focus();
                WebMsgBox.Show("Enter existing set no first....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Procode_Change();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CT = TxtProName.Text.ToString().Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                TxtProcode.Text = CT[1].ToString();
                Txtcustno.Text = "0";

                Procode_Change();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["DRGL"].ToString() == "5")
            {
                int RC = NewTDA.CheckAccount(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                if (RC == 2)
                {
                    string ACNM = NewTDA.GetAccName(TxtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                    string[] AC = ACNM.Split('-');
                    TxtAccName.Text = AC[0].ToString();

                    TxtRecNo.Text = "";
                    TxtRecNo.Focus();
                }
                else
                {
                    Accno_Change();
                    getMobile();
                    if (ViewState["DRGL"].ToString() == "5")
                    {
                        SetFDAmt(TxtProcode.Text, TxtAccNo.Text);
                    }
                }
            }
            else
            {
                TxtRecNo.Text = "0";
                TxtRecNo.Enabled = false;

                Accno_Change();
                getMobile();
                if (ViewState["DRGL"].ToString() == "5")
                {
                    SetFDAmt(TxtProcode.Text, TxtAccNo.Text);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    public void getMobile()
    {
        try
        {
            DT = CMN.getMobiles(Txtcustno.Text);
            if (DT.Rows.Count > 0)
            {
                TxtMobile1.Text = DT.Rows[0]["Mobile1"].ToString();
                TxtMobile2.Text = DT.Rows[0]["Mobile2"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtAccName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                //  Added By Amol On 2018-02-05 For take additional share
                if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                {
                    Param = CMN.getShrParam();
                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                        AC_Status = CMN.GetAccStatus("1", TxtProcode.Text.ToString(), custnob[1].ToString());
                    else
                        AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), custnob[1].ToString());
                }
                else
                    AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), custnob[1].ToString());

                if (AC_Status == "1" || AC_Status == "4" || AC_Status == "2" || AC_Status == "6") // Debit frezzed allowed
                {
                    TxtAccName.Text = custnob[0].ToString();
                    TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());


                    if (ViewState["DRGL"].ToString() == "5")
                    {
                        int RC = NewTDA.CheckAccount(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                        if (RC == 2)
                        {
                            string ACNM = NewTDA.GetAccName(TxtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                            string[] AC = ACNM.Split('-');
                            TxtAccName.Text = AC[0].ToString();

                            TxtRecNo.Text = "";
                            TxtRecNo.Focus();
                            return;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        TxtRecNo.Text = "0";
                        TxtRecNo.Enabled = false;

                        Accno_Change();
                        getMobile();
                        if (ViewState["DRGL"].ToString() == "5")
                        {
                            SetFDAmt(TxtProcode.Text, TxtAccNo.Text);
                        }
                    }



                    ViewState["CUSTNO"] = custnob[2].ToString();
                    Txtcustno.Text = ViewState["CUSTNO"].ToString();
                    getMobile();
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    //  Added By Amol On 2018-02-05 For take additional share
                    if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                    {
                        Param = CMN.getShrParam();
                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                        {
                            txtPan.Text = PanCard("1", Txtcustno.Text.ToString());
                            TxtAadharNo.Text = AadharNo("1", Txtcustno.Text.ToString());
                            txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), "1", Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                            TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), "1", Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                        }
                        else
                        {
                            txtPan.Text = PanCard("1", Txtcustno.Text.ToString());
                            TxtAadharNo.Text = AadharNo("1", Txtcustno.Text.ToString());
                            txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                            TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                        }
                    }
                    else
                    {
                        txtPan.Text = PanCard(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                        TxtAadharNo.Text = AadharNo(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                        txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                        TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                    }

                    if (TxtAccNo.Text == "")
                    {
                        TxtAccName.Text = "";
                    }
                    if (TxtAccNo.Text != "" & TxtProcode.Text != "")
                    {
                        string PRD = "", acctype, sql = "", acctypeno, jointname = ""; ;
                        string[] accn;
                        PRD = TxtProcode.Text;
                        ////Added account type,account joint name and special instruction by ankita 03/06/2017
                        DataTable dt = new DataTable();

                        //  Added By Amol On 2018-02-05 For take additional share
                        if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                        {
                            Param = CMN.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                                dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, "1");
                            else
                                dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());
                        }
                        else
                            dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());
                        accn = dt.Rows[0]["CUSTNAME"].ToString().Split('_');
                        ViewState["CUSTNO"] = accn[0].ToString();
                        Txtcustno.Text = ViewState["CUSTNO"].ToString();
                        getMobile();
                        TxtAccName.Text = accn[1].ToString();
                        TxtSplInst.Text = dt.Rows[0]["SPL_INSTRUCTION"].ToString();
                        acctypeno = dt.Rows[0]["OPR_TYPE"].ToString();

                        acctype = CurrentCls.GetAcctype(acctypeno);
                        txtAccTypeName.Text = acctype.ToString();
                        if (txtAccTypeName.Text == "JOINT")
                        {
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    jointname = CurrentCls.Getjointname("1", TxtAccNo.Text.ToString(), PRD);
                                else
                                    jointname = CurrentCls.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);
                            }
                            else
                                jointname = CurrentCls.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);

                            lbjoint.Visible = true;
                            TxtJointName.Visible = true;
                            TxtJointName.Text = jointname.ToString();
                        }
                        else
                        {
                            lbjoint.Visible = false;
                            TxtJointName.Visible = false;
                        }
                        if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                        {
                            grdAccDetails.DataSource = null;
                            grdAccDetails.DataBind();
                            WebMsgBox.Show("Please enter valid Account number", this.Page);
                            TxtAccNo.Text = "";
                            TxtAccNo.Focus();
                            return;
                        }
                        if (TxtAccName.Text != "")
                        {
                            txtnaration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                        }

                        // Added By Amol as per darade sir instruction on 19/06/2017 bcoz display all account details related to customer in grid
                        if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                        {
                            Param = CMN.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                                resultout = CurrentCls.GetAccDetails(grdAccDetails, "1", Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                            else
                                resultout = CurrentCls.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                        }
                        else
                            resultout = CurrentCls.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                        if (resultout < 0)
                        {
                            grdAccDetails.DataSource = null;
                            grdAccDetails.DataBind();
                        }

                        ////Displayed modal popup of voucher info by ankita 20/05/2017
                        DataTable dtmodal = new DataTable();
                        //  Added By Amol On 2018-02-05 For take additional share
                        if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                        {
                            Param = CMN.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                                dtmodal = CurrentCls.GetInfoTbl("1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                            else
                                dtmodal = CurrentCls.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                        }
                        else
                            dtmodal = CurrentCls.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());

                        if (dtmodal.Rows.Count > 0)
                        {
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    resultout = CurrentCls.GetInfo(GrdView, "1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                                else
                                    resultout = CurrentCls.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                            }
                            else
                                resultout = CurrentCls.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());

                            if (resultout > 0)
                            {
                                string Modal_Flag = "VOUCHERVIEW";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }

                        //  Added by amol on 27/12/2017 for show message only if customer loan acc is in overdue
                        if (Txtcustno.Text.ToString() != "0")
                        {
                            Photo_Sign();
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    DT = CMN.CheckCustODAcc("1", Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                                else
                                    DT = CMN.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                            }
                            else
                                DT = CMN.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());

                            if (DT.Rows.Count > 0)
                                WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                        }
                        if (ViewState["DRGL"].ToString() == "5")
                        {
                            SetFDAmt(TxtProcode.Text, TxtAccNo.Text);
                        }
                    }
                }
                else if (AC_Status == "3")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "5")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Credit Freezed...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "7")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Total Freezed...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "8")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Dormant / In Operative...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "9")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Suit File...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "10")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Call back Ac...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "11")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is NPA Ac...!!", this.Page);
                    Clear();
                }
                else if (AC_Status == "12")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " have Interest Suspended...!!", this.Page);
                    Clear();
                }
                else
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }
            }
            else
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
                return;
            }
            if (TxtAccName.Text != "")
            {
                txtnaration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtamountt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int NewBal = 0;
            NewBal = (Convert.ToInt32(txtBalance.Text)) + (Convert.ToInt32(txtamountt.Text));
            TxtNewBalance.Text = Convert.ToString(NewBal);
            btnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function
    public void SetFDAmt(string Prdcode, string Acode)
    {
        try
        {
            string AMTVAL = CMN.GetAMTVAL(Session["BRCD"].ToString(), TxtProcode.Text);
            if (AMTVAL != null && AMTVAL == "Y")
            {
                ViewState["AMTVAL"] = "Y";
                string FDAMT = CMN.GetFDAMT(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);
                if (FDAMT != null)
                {
                    ViewState["FDAMT"] = FDAMT.ToString();
                }
                else
                {
                    ViewState["FDAMT"] = "0";
                }
            }
            else
            {
                ViewState["AMTVAL"] = "N";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "CASHR");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Procode_Change()
    {
        try
        {
            if (TxtProcode.Text == "")
            {
                TxtProName.Text = "";
                TxtAccNo.Focus();
                goto ext;
            }
            DataTable DTGlDetails = new DataTable();
            DTGlDetails = GlMaster.GetGLDetails(TxtProcode.Text, Session["BRCD"].ToString());

            string[] GLS = DTGlDetails.Rows[0]["GL"].ToString().Split('_');
            ViewState["DRGL"] = GLS[1].ToString();

            //  Added By Amol on 02/05/2018 as per darade sir instruction
            if (ViewState["DRGL"].ToString() != "3")
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (DTGlDetails.Rows[0]["UnOperate"].ToString() != "3")
                {
                    int result = 0;
                    string GlS1;
                    int.TryParse(TxtProcode.Text, out result);
                    TxtProName.Text = DTGlDetails.Rows[0]["GLNAME"].ToString();
                    Txtcustno.Text = "0";
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    //GlS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                    if (DTGlDetails.Rows[0]["GL"].ToString() != null)
                    {


                        //  Added By amol On 2018-02-05 for share account name search
                        if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                        {
                            Param = CMN.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                                AutoAccname.ContextKey = "1" + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                            else
                                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                        }
                        else
                            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                        string YN = DTGlDetails.Rows[0]["INTACCYN"].ToString();

                        ViewState["ACCYN"] = string.IsNullOrEmpty(YN) ? "0" : YN.ToString(); // Added by Abhishek as per Bug stated Accepting Entry without accno 19-02-2018

                        if (YN == "Y")
                        {
                            txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                            TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                        }
                        else
                        {
                            txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), YN).ToString();
                            TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), YN).ToString();
                        }

                        int GL = 0;
                        int.TryParse(ViewState["DRGL"].ToString(), out GL);
                        if (GL >= 100 && YN != "Y")
                        {
                            TxtAccNo.Text = TxtProcode.Text;
                            TxtAccName.Text = TxtProName.Text;
                            txtnaration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                            txtnaration.Focus();
                            txtnaration.Text = "By Cash";
                        }
                        else
                        {
                            TxtAccNo.Focus();
                        }

                    }
                    else
                    {
                        TxtProcode.Text = "";
                        TxtProName.Text = "";
                        TxtProcode.Focus();
                        WebMsgBox.Show("Enter Valid Product code...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    TxtProcode.Text = "";
                    TxtProName.Text = "";
                    TxtProcode.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtProcode.Text = "";
                TxtProName.Text = "";
                TxtProcode.Focus();
                WebMsgBox.Show("Use loan installment screen...!!", this.Page);
                return;
            }
        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Accno_Change()
    {
        try
        {
            //  Added By Amol On 2018-02-05 For take additional share
            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
            {
                Param = CMN.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                    AC_Status = CMN.GetAccStatus("1", TxtProcode.Text, TxtAccNo.Text.ToString());
                else
                    AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text.ToString());
            }
            else
                AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text.ToString());

            if (AC_Status == "1" || AC_Status == "4" || AC_Status == "2" || AC_Status == "6") // Debit frezzed allowed
            {
                //  Added By Amol On 2018-02-05 For take additional share
                if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                {
                    Param = CMN.getShrParam();
                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                        sResult = BD.Getstage1(TxtAccNo.Text, "1", TxtProcode.Text.ToString());
                    else
                        sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text.ToString());
                }
                else
                    sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text.ToString());

                if (sResult != null)
                {
                    if (sResult != "1003" && ViewState["FL"].ToString() != "ACO")
                    {
                        grdAccDetails.DataSource = null;
                        grdAccDetails.DataBind();
                        WebMsgBox.Show("Sorry Customer not Authorise...!!", this.Page);
                        Clear();
                    }
                    else
                    {
                        //  Added By Amol On 2018-02-05 For take additional share
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                        {
                            Param = CMN.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                            {
                                txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, "1", Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                                TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, "1", Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                            }
                            else
                            {
                                txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                                TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                            }
                        }
                        else
                        {
                            txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                            TxtNewBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), "OPT", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text).ToString();
                        }

                        if (TxtAccNo.Text == "")
                        {
                            TxtAccName.Text = "";
                            goto ext;
                        }

                        DataTable dt1 = new DataTable();
                        if (TxtAccNo.Text.ToString() != "" & TxtProcode.Text.ToString() != "")
                        {
                            string PRD = "", acctype, sql = "", acctypeno, jointname = "";
                            string[] accn;
                            DataTable dt = new DataTable();
                            PRD = TxtProcode.Text.ToString();
                            ////Added account type,account joint name and special instruction by ankita 03/06/2017
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")

                                    dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, "1");

                                else
                                    dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());
                            }
                            else
                                dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());

                            accn = dt.Rows[0]["CUSTNAME"].ToString().Split('_');
                            ViewState["CUSTNO"] = accn[0].ToString();
                            Txtcustno.Text = ViewState["CUSTNO"].ToString();
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                {
                                    TxtAadharNo.Text = AadharNo("1", Txtcustno.Text.ToString());
                                    txtPan.Text = PanCard("1", Txtcustno.Text.ToString());
                                }
                                else
                                {
                                    TxtAadharNo.Text = AadharNo(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                                    txtPan.Text = PanCard(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                                }
                            }
                            else
                            {
                                txtPan.Text = PanCard(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                                TxtAadharNo.Text = AadharNo(Session["BRCD"].ToString(), Txtcustno.Text.ToString());
                            }

                            TxtAccName.Text = accn[1].ToString();
                            TxtSplInst.Text = dt.Rows[0]["SPL_INSTRUCTION"].ToString();
                            acctypeno = dt.Rows[0]["OPR_TYPE"].ToString();

                            acctype = CurrentCls.GetAcctype(acctypeno);
                            txtAccTypeName.Text = acctype.ToString();
                            if (txtAccTypeName.Text == "JOINT")
                            {
                                //  Added By Amol On 2018-02-05 For take additional share
                                if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                                {
                                    Param = CMN.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        jointname = CurrentCls.Getjointname("1", TxtAccNo.Text.ToString(), PRD);
                                    else
                                        jointname = CurrentCls.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);
                                }
                                else
                                    jointname = CurrentCls.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);

                                lbjoint.Visible = true;
                                TxtJointName.Visible = true;
                                TxtJointName.Text = jointname.ToString();
                            }
                            else
                            {
                                lbjoint.Visible = false;
                                TxtJointName.Visible = false;
                            }
                            if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                            {
                                grdAccDetails.DataSource = null;
                                grdAccDetails.DataBind();
                                WebMsgBox.Show("Please enter valid Account number", this.Page);
                                TxtAccNo.Text = "";
                                TxtAccNo.Focus();
                                return;
                            }
                            if (TxtAccName.Text != "")
                            {
                                txtnaration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                                txtnaration.Focus();
                                txtnaration.Text = "By Cash";
                            }

                            // Added By Amol as per darade sir instruction on 19/06/2017 bcoz display all account details related to customer in grid
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    resultout = CurrentCls.GetAccDetails(grdAccDetails, "1", Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                                else
                                    resultout = CurrentCls.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                            }
                            else
                                resultout = CurrentCls.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());

                            if (resultout < 0)
                            {
                                grdAccDetails.DataSource = null;
                                grdAccDetails.DataBind();
                            }

                            ////Displayed modal popup of voucher info by ankita 20/05/2017
                            DataTable dtmodal = new DataTable();
                            //  Added By Amol On 2018-02-05 For take additional share
                            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                            {
                                Param = CMN.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    dtmodal = CurrentCls.GetInfoTbl("1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                                else
                                    dtmodal = CurrentCls.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                            }
                            else
                                dtmodal = CurrentCls.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());

                            if (dtmodal.Rows.Count > 0)
                            {
                                //  Added By Amol On 2018-02-05 For take additional share
                                if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                                {
                                    Param = CMN.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        resultout = CurrentCls.GetInfo(GrdView, "1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                                    else
                                        resultout = CurrentCls.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                                }
                                else
                                    resultout = CurrentCls.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());

                                if (resultout > 0)
                                {
                                    string Modal_Flag = "VOUCHERVIEW";
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                    sb.Append(@"</script>");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                                }
                            }

                            //  Added by amol on 27/12/2017 for show message only if customer loan acc is in overdue
                            if (Txtcustno.Text.ToString() != "0")
                            {
                                Photo_Sign();
                                //  Added By Amol On 2018-02-05 For take additional share
                                if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
                                {
                                    Param = CMN.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        DT = CMN.CheckCustODAcc("1", Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                                    else
                                        DT = CMN.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                                }
                                else
                                    DT = CMN.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                                if (DT.Rows.Count > 0)
                                    WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                            }
                        }
                    ext: ;
                    }
                }
                else
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    WebMsgBox.Show("Enter Valid Account number...!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }
            }
            else if (AC_Status == "3")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "5")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Credit Freezed...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "7")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Total Freezed...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "8")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Dormant / In Operative...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "9")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Suit File...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "10")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Call back Ac...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "11")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is NPA Ac...!!", this.Page);
                Clear();
            }
            else if (AC_Status == "12")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " have Interest Suspended...!!", this.Page);
                Clear();
            }
            else
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                WebMsgBox.Show("Invalid Account Number...!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = CurrentCls.GetPanNo(BRCD, CustNo);
        return PanNo;
    }
    public string AadharNo(string BRCD, string CustNo) // added by Abhishek as per req on 14-03-2018
    {
        string AadharNo = "";
        try
        {
            AadharNo = CurrentCls.GetAadharNo(BRCD, CustNo);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AadharNo;
    }

    protected void CallUpdate()
    {
        try
        {
            string RC = CheckStage();
            string sql;
            int result;

            if (RC != "1003")
            {
                result = CurrentCls.CancelCashreceipt(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Entry Canceled for Voucher No-" + ViewState["SetNo"].ToString() + "....", this.Page);
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Receipt _Entrycancel_" + ViewState["SetNo"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                    TxtProcode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("The Voucher is already Authorized, Cannot delete!.....", this.Page);
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckStage()
    {
        string RC = "";
        string ed = Session["EntryDate"].ToString();
        try
        {
            string sql = "select Stage from ALLVCR where SETNO='" + ViewState["SetNo"].ToString() + "' and EntryDate='" + conn.ConvertDate(ed) + "' and STAGE<>1004";
            RC = conn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
    }

    public DataTable GetAccStatDetails(string FinDate)
    {
        try
        {
            DT = new DataTable();
            string[] DTF, DTT;

            DTF = FinDate.ToString().Split('/');
            DTT = Session["EntryDate"].ToString().Split('/');

            DT = CurrentCls.GetAccStatDetails(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), TxtAccNo.Text.Trim().ToString(), TxtProcode.Text.Trim().ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public void Photo_Sign()
    {
        try
        {
            //  Added By Amol On 2018-02-05 For take additional share
            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
            {
                Param = CMN.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                    Datatbl = CMN.ShowIMAGE(ViewState["CUSTNO"].ToString(), "1", TxtAccNo.Text.ToString());
                else
                    Datatbl = CMN.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text.ToString());
            }
            else
                Datatbl = CMN.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text.ToString());

            if (Datatbl.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = Datatbl.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Datatbl.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = Datatbl.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Datatbl.Rows[i]["PhotoImg"];
                    }
                    if (FilePath != "")
                    {

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);


                        if (y == 0)
                        {

                            Img1.Src = "data:image/tif;base64," + base64String;
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "data:image/tif;base64," + base64String;
                        }
                    }
                    else
                    {
                        if (y == 0)
                        {

                            Img1.Src = "";
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "";
                        }
                    }
                }
            }
            else
            {
                Img1.Src = "";
                Img2.Src = "";
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetTotalAmt()
    {
        try
        {
            TxtTotalCP.Text = CMN.CRCPSUM("CR", "TOTAL", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotalUnCp.Text = CMN.CRCPSUM("CR", "UNAUTH", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotCR.Text = CMN.CRCPSUM("CP", "TOTAL", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotalUnCr.Text = CMN.CRCPSUM("CP", "UNAUTH", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Clear()
    {
        //TxtGLCD.Text = "";
        TxtProcode.Text = "";
        TxtProName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        txtBalance.Text = "";
        txtamountt.Text = "";
        TxtNewBalance.Text = "";
        //txtnaration1.Text = "";
        TxtProcode.Focus();
        txtAccTypeName.Text = "";
        TxtJointName.Text = "";
        TxtSplInst.Text = "";
        TxtRecNo.Text = "";

    }

    #endregion

    #region Button Click Event

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string BRCD = Session["BRCD"].ToString();
            string MID = Session["MID"].ToString();
            string EntryDate = "";
            string custno = "";
            string SetNo = "", settype = "";
            EntryDate = TxtEntrydate.Text.ToString();
            string PCMAC = conn.PCNAME().ToString();
            string PAYMAST = "CASHR";
            string ScrollNo = "1";
            string REFERENCEID = "";
            if (TxtProcode.Text.ToString() == "4")// ankita 15/03/2018
            {
                Param = CMN.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                {
                    if (Session["BRCD"].ToString() == "1")
                        settype = "DaySetNo";
                    else
                        settype = "IBTSetNo";
                }
                else
                    settype = "DaySetNo";
            }
            else
            { settype = "DaySetNo"; }
            ViewState["ACCYN"] = ViewState["ACCYN"] == null ? "0" : ViewState["ACCYN"].ToString();
            if (ViewState["ACCYN"].ToString() == "N") // Added by Abhishek for bug that accepting entry without acc no 19-02-2018
            {
                if (TxtAccNo.Text == "")
                {
                    WebMsgBox.Show("Invalid account number, Enter account number first...!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                    return;
                }
            }
            ViewState["AMTVAL"] = ViewState["AMTVAL"] == null ? "0" : ViewState["AMTVAL"].ToString();

            if (ViewState["AMTVAL"].ToString() == "Y")
            {
                if (Convert.ToDouble(ViewState["FDAMT"].ToString()) != Convert.ToDouble(txtamountt.Text))
                {
                    WebMsgBox.Show("Deposit Amount not matched....!", this.Page);
                    txtamountt.Text = "";
                    TxtNewBalance.Text = "0";
                    txtamountt.Focus();
                    return;
                }

            }


            //Added  by amol on 26-09-2017 as per darade sir instruction
            if (rbtnExistSet.Checked == true && Convert.ToInt32(txtExistSetNo.Text.Trim().ToString() == "" ? "0" : txtExistSetNo.Text.Trim().ToString()) > 0 || rbtnNewSet.Checked == true)
            {
                if (rbtnExistSet.Checked == true && Convert.ToDouble(TxtProcode.Text.ToString()) != 4 || rbtnNewSet.Checked == true)
                {
                    if (rbtnNewSet.Checked == true)
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), settype.ToString(), Session["BRCD"].ToString()).ToString();
                    else if (rbtnExistSet.Checked == true)
                        SetNo = txtExistSetNo.Text.Trim().ToString();

                    REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                    ViewState["RID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();

                    string PRNAMT = CMN.GETDEPOSITAMT(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), TxtProcode.Text.ToString());
                    string Depositstatus = CMN.Checkapara(Session["BRCD"].ToString(), TxtProcode.Text.ToString());

                    if (Convert.ToSingle(PRNAMT) != Convert.ToSingle(txtamountt.Text) && Convert.ToInt32(ViewState["DRGL"].ToString()) == 5 && Convert.ToInt32(Depositstatus == "" ? "0" : Depositstatus).ToString() != "1")
                    {
                        txtamountt.Focus();
                        WebMsgBox.Show("Entered Amount Should Be Equal To Term Deposit Amount ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        if (TxtAccNo.Text != "")
                        {
                            custno = Txtcustno.Text.ToString();
                        }
                        else
                        {
                            custno = "0";
                            TxtAccNo.Text = "0";
                        }

                        //  Added By Amol On 2018-02-05 For take additional share
                        if (Convert.ToDouble(TxtProcode.Text.ToString()) != 4)
                        {
                            // POST VOUCHER
                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.ToString(),
                                TxtAccNo.Text.ToString(), txtnaration.Text, "", txtamountt.Text.ToString(), "1", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001",
                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);

                            if (resultint > 0)
                            {
                                //Added  by amol on 26-09-2017 as per darade sir instruction
                                if (rbtnNewSet.Checked == true)
                                {
                                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl, TxtAccNo.Text,
                                              txtnaration.Text, "", txtamountt.Text.ToString(), "2", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(),
                                              "0", "0", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);

                                }
                                else if (rbtnExistSet.Checked == true)
                                {
                                    resultint = PVOUCHER.UpdateCashSet(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtExistSetNo.Text.Trim().ToString(), txtamountt.Text.ToString(), Session["MID"].ToString());
                                }

                                if (resultint > 0)
                                {
                                    grdAccDetails.DataSource = null;
                                    grdAccDetails.DataBind();
                                    TxtProcode.Focus();
                                    WebMsgBox.Show("Record Submitted Successfully With Receipt No :" + SetNo, this.Page);
                                    BindGrid();
                                    Clear();
                                    TxtProcode.Focus();
                                }
                            }
                        }
                        else if (Convert.ToDouble(TxtProcode.Text.ToString()) == 4)
                        {
                            PAYMAST = "ABB-CASHR";

                            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl, TxtAccNo.Text,
                                      "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + TxtProcode.Text.ToString() + "/" + TxtAccNo.Text.ToString() + "", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "2", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", Session["BRCD"].ToString(),
                                      Session["MID"].ToString(), Session["BRCD"].ToString(), "1", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);

                            //ShareSuspGl = CurrentCls.GetShareSuspGl(Session["BRCD"].ToString());
                            //// Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                            //if (ShareSuspGl > 0)
                            //    ShareAccNo = accop.insert(Session["BRCD"].ToString(), "4", ShareSuspGl.ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0");

                            //if (ShareAccNo > 0)
                            //{
                            //    // credit and debit through share suspense account
                            //    GlCode = BD.GetCashSubglcode(ShareSuspGl.ToString(), Session["BRCD"].ToString());

                            //    if (Convert.ToDouble(GlCode) > 0)
                            //    {
                            //        resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), ShareSuspGl.ToString(), ShareAccNo.ToString(),
                            //              "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + TxtProcode.Text.ToString() + "/" + TxtAccNo.Text.ToString() + "", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "1", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(),
                            //              Session["BRCD"].ToString(), "1", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                            //        resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), ShareSuspGl.ToString(), ShareAccNo.ToString(),
                            //              "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + TxtProcode.Text.ToString() + "/" + TxtAccNo.Text.ToString() + "", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "2", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(),
                            //              Session["BRCD"].ToString(), "1", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                            //    }
                            //}

                            if ((Convert.ToDouble(Session["BRCD"].ToString()) != 1) && (resultint > 0))
                            {
                                //Credit to Selected Branch
                                DT = CurrentCls.GetADMSubGl("1");
                                resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                      "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + TxtProcode.Text.ToString() + "/" + TxtAccNo.Text.ToString() + "", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "1", "3", "CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(),
                                      Session["BRCD"].ToString(), "1", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);

                                if (resultint > 0)
                                {
                                    //Debit to Selected Branch
                                    DT = CurrentCls.GetADMSubGl(Session["BRCD"].ToString());
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                      "ABB- From " + Session["BRCD"].ToString() + " To 1 - 99/0", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "2", "41", "ABB-CR", SetNo, "0", "01/01/1990", "0", "0", "1001", "", "1", Session["MID"].ToString(),
                                      Session["BRCD"].ToString(), "1", PAYMAST, Txtcustno.Text.ToString() == "" ? "0" : Txtcustno.Text.ToString(), TxtAccName.Text.ToString() == "" ? "" : TxtAccName.Text.ToString(), ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);
                                }
                            }

                            if (resultint > 0)
                            {
                                //  Added By Amol On 2018-02-05 for additional share allotment
                                if (Convert.ToDouble(Session["BRCD"].ToString()) == 1)
                                {
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.ToString(),
                                        TxtAccNo.Text.ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - 99/0", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "1", "3", "CR", SetNo, "0", "01/01/1990",
                                        "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "1", PAYMAST, "0", "", ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);
                                }
                                else
                                {
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.ToString(),
                                        TxtAccNo.Text.ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - 99/0", txtnaration.Text.ToString(), txtamountt.Text.ToString(), "1", "41", "ABB-CR", SetNo, "0", "01/01/1990",
                                        "0", "0", "1001", "", "1", Session["MID"].ToString(), Session["BRCD"].ToString(), "1", PAYMAST, "0", "", ViewState["RID"].ToString(), "0", TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);
                                }

                                if (resultint > 0)
                                {
                                    sResult = CurrentCls.InsertData(Session["BRCD"].ToString(), "2", TxtAccNo.Text.Trim().ToString(), Txtcustno.Text.Trim().ToString(), "1", "100", txtamountt.Text.Trim().ToString(), SetNo, "1", "Share Application", "Share Application", Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    string[] Res = sResult.ToString().Split('_');
                                    if (Convert.ToDouble(Res[0].ToString()) > 0)
                                    {
                                        grdAccDetails.DataSource = null;
                                        grdAccDetails.DataBind();
                                        TxtProcode.Focus();
                                        WebMsgBox.Show("Record Submitted Successfully With Receipt No :" + SetNo + " And Appl No :" + Res[1].ToString(), this.Page);
                                        BindGrid();
                                        Clear();
                                        TxtProcode.Focus();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    WebMsgBox.Show("Not possible on existing voucher no....!", this.Page);
                    return;
                }
            }
            else
            {
                txtExistSetNo.Text = "";
                txtExistSetNo.Focus();
                WebMsgBox.Show("Enter existing voucher no first....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string setno = objlink.CommandArgument;
            ViewState["SetNo"] = setno.ToString();
            CallUpdate();
            Clear();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "PRINT";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDens_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "DENS";
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            string[] dens = id.ToString().Split('_');
            Session["densset"] = dens[0].ToString();
            Session["densamt"] = dens[1].ToString();
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            string i = CurrentCls.CheckDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            if (i == null)
            {
                string redirectURL = "FrmCashDenom.aspx";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                WebMsgBox.Show("Already Cash Denominations..!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear(); ;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

            DT = GetAccStatDetails(OpDate);
            if (DT.Rows.Count > 0)
            {
                grdAccStatement.DataSource = DT;
                grdAccStatement.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                WebMsgBox.Show("Details Not Found For This Account Number...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changes Events

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCashRct.PageIndex = e.NewPageIndex;
            BindGrid();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void GrdView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void grdCashRct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
            {
                DataTable dt = new DataTable();
                string Setno = (grdCashRct.SelectedRow.FindControl("SET_NO") as Label).Text;
                ViewState["SUBGLCODE"] = (grdCashRct.SelectedRow.FindControl("AT") as Label).Text;
                ViewState["ACCNO"] = (grdCashRct.SelectedRow.FindControl("ACNO") as Label).Text;
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Receipt _Print_" + Setno + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string FL1 = ASM.GetFlag("RECEIPTTXT");
                if (FL1 == "Y")
                {

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Runbat1()", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Runbat3()", true);
                    btnprint.Visible = true;
                    // BindDetails(Setno);
                    DataTable DTRec = new DataTable();
                    dt = CurrentCls.RECYSPM(Setno, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Txtcustno.Text, TxtAccName.Text, FL);
                    if (dt.Rows.Count > 0)
                    {
                        DTRec.Columns.Add("Row");
                        DTRec.Columns.Add("C2");
                        DTRec.Columns.Add("C3");
                        DTRec.Columns.Add("C4");
                        DTRec.Columns.Add("C5");
                        DTRec.Columns.Add("BRCD");
                        DTRec.Columns.Add("MID");
                        DTRec.Rows.Add("R1", Setno, Setno, Setno, Setno, Session["BRCD"].ToString(), Session["MID"].ToString());
                        DTRec.Rows.Add("R2", conn.ConvertDate(Session["EntryDate"].ToString()), conn.ConvertDate(Session["EntryDate"].ToString()), conn.ConvertDate(Session["EntryDate"].ToString()), conn.ConvertDate(Session["EntryDate"].ToString()), Session["BRCD"].ToString(), Session["MID"].ToString());
                        DTRec.Rows.Add("R3", dt.Rows[0]["CustName"].ToString(), "", dt.Rows[0]["CustName"].ToString(), "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        if (dt.Rows[0]["Rank"].ToString() == "1")
                        {
                            DTRec.Rows.Add("R4", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "2")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "3")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "4")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "5")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "6")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "7")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else if (dt.Rows[0]["Rank"].ToString() == "8")
                        {
                            DTRec.Rows.Add("R4", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R5", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R6", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R7", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R8", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R9", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R10", "", "", "", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R11", dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), dt.Rows[0]["PARTICULARS"].ToString(), dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        DTRec.Rows.Add("R12", "Balance", dt.Rows[0]["ClBal"].ToString(), "Balance", dt.Rows[0]["ClBal"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                        DTRec.Rows.Add("R13", "Interest", dt.Rows[0]["IntBal"].ToString(), "Interest", dt.Rows[0]["IntBal"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                        DTRec.Rows.Add("R14", "Total", dt.Rows[0]["Amount"].ToString(), "Total", dt.Rows[0]["Amount"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                        string RWord = rupees(Convert.ToInt64(dt.Rows[0]["AMOUNT"]));
                        int Length = RWord.Length;
                        if (Length > 30)
                        {
                            string modified = RWord.Insert(30, "_");
                            string[] words = modified.ToString().Split('_');
                            string Word1 = words[0].ToString();
                            string Word2 = words[1].ToString();
                            DTRec.Rows.Add("R15", "Inword", Word1.ToString(), "Inword", Word1.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R16", "Inword1", Word2.ToString(), "Inword1", Word2.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        else
                        {
                            DTRec.Rows.Add("R15", "Inword", RWord.ToString(), "Inword", RWord.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                            DTRec.Rows.Add("R16", "Inword1", "", "Inword1", "", Session["BRCD"].ToString(), Session["MID"].ToString());
                        }
                        int Res1 = 0;
                        //Res1 = CurrentCls.InsertData(DTRec, Session["BRCD"].ToString(), Session["MID"].ToString());
                        //if (Res1 > 0)
                        //{
                        BindDetails(DTRec);
                        //}
                    }
                }
                else
                {

                    btnprint.Visible = false;
                    string Para = VA.GetParameter();
                    if (Para == "Y")//Dhanya Shetty
                    {
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + "R" + "&Subg=" + ViewState["SUBGLCODE"].ToString() + "&Acc=" + ViewState["ACCNO"].ToString() + "&rptname=RptReceiptPrintPal.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    else
                    {
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrint.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindDetails(DataTable dtRes)
    {
        string path1 = "RecPrint.txt";
        string[] EDT1;
        string EDT2 = "";
        EDT1 = Session["EntryDate"].ToString().Split('/');
        EDT2 = EDT1[0].ToString() + "." + EDT1[1].ToString() + "." + EDT1[2].ToString();

        // string DPath = @"D:\Balaji";
        string pp = Request.PhysicalApplicationPath;
        string path = pp + path1;

        string DateF = "";
        string SFilename = "";
        Response.Clear();
        Response.Buffer = true;
        DateF = DateTime.Now.ToShortDateString();
        SFilename = DateF;

        Response.AddHeader("content-disposition", "attachment;filename=" + path1);
        Response.Charset = "";
        Response.ContentType = "application/octet-stream";
        string PRD = "";
        string[] CN;
        //PRD = TxtAccType.Text;
        //CN = customcs.GetAccountName(TxtAccno.Text.ToString(), PRD, Session["BRCD"].ToString()).Split('_');
        //ViewState["CUSTNO"] = CN[0].ToString();

        DataTable DtPara = new DataTable();
        DtPara = CurrentCls.GetFDPara("1");
        DataView DV = new DataView(DtPara);
        DV.RowFilter = "columnStatus=49";
        int nbr = Convert.ToInt32(DV[0]["PRows"]);
        string txt = "";
        if (nbr == 1 || nbr > 1)
        {
            for (int i = 0; i < nbr; i++)
            {
                if (i == 0)
                    txt = "";
                else
                    txt += "";
            }
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(txt);

                writer.Close();
                Response.Output.Write(writer);
            }
        }
        txt = "";
        DtPara = CurrentCls.GetFDPara("2");
        for (int s = 0; s < DtPara.Rows.Count; s++)
        {
            if (s == 0)
            {
                int line = Convert.ToInt32(DtPara.Rows[s]["PRows"]);
                line = line - 1;
                for (int a = 0; a < line; a++)
                {
                    txt += "";
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(txt);

                        writer.Close();
                        Response.Output.Write(writer);
                    }
                }
            }
            else
            {
                int BLine = Convert.ToInt32(DtPara.Rows[s]["PRows"]) - Convert.ToInt32(DtPara.Rows[s - 1]["PRows"]);
                if (BLine > 0)
                {
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(txt);

                        writer.Close();
                        Response.Output.Write(writer);
                    }
                    txt = "";
                    for (int k = 0; k < BLine - 1; k++)
                    {
                        txt = "";
                        using (StringWriter writer = new StringWriter())
                        {
                            writer.WriteLine(txt);

                            writer.Close();
                            Response.Output.Write(writer);
                        }
                    }
                }
            }
            for (int j = 0; j < Convert.ToInt32(DtPara.Rows[s]["PCOlumn"]); j++)
            {
                txt += " ";
            }

            if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 1)
            {
                txt += "  " + dtRes.Rows[0]["C2"].ToString();//setno
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 2)
            {
                txt += "  " + Session["Entrydate"].ToString();
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 3)
            {
                txt += "   " + dtRes.Rows[0]["C2"].ToString();//setno
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 4)
            {
                txt += "    " + Session["Entrydate"].ToString();
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 5)
            {
                txt += "    " + dtRes.Rows[2]["C2"].ToString();//CustNo
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 6)
            {
                txt += "   " + dtRes.Rows[2]["C2"].ToString();//CustNo
            }

            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 7)
            {
                txt += "   " + dtRes.Rows[3]["C2"].ToString();//PRD1
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 8)
            {
                txt += "   " + dtRes.Rows[3]["C3"].ToString();//PRD1
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 9)
            {
                txt += "   " + dtRes.Rows[3]["C2"].ToString();//PRD1
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 10)
            {
                txt += "   " + dtRes.Rows[3]["C3"].ToString();//PRD1
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 11)
            {
                txt += "   " + dtRes.Rows[4]["C2"].ToString();//PRD2
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 12)
            {
                txt += "   " + dtRes.Rows[4]["C3"].ToString();//PRD2
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 13)
            {
                txt += "   " + dtRes.Rows[4]["C2"].ToString();//PRD2
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 14)
            {
                txt += "   " + dtRes.Rows[4]["C3"].ToString();//PRD2
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 15)
            {
                txt += "   " + dtRes.Rows[5]["C2"].ToString();//PRD3
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 16)
            {
                txt += "   " + dtRes.Rows[5]["C3"].ToString();//PRD3
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 17)
            {
                txt += "   " + dtRes.Rows[5]["C2"].ToString();//PRD3
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 18)
            {
                txt += "   " + dtRes.Rows[5]["C3"].ToString();//PRD3
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 19)
            {
                txt += "   " + dtRes.Rows[6]["C2"].ToString();//PRD4
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 20)
            {
                txt += "   " + dtRes.Rows[6]["C3"].ToString();//PRD4
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 21)
            {
                txt += "   " + dtRes.Rows[6]["C2"].ToString();//PRD4
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 22)
            {
                txt += "   " + dtRes.Rows[6]["C3"].ToString();//PRD4
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 23)
            {
                txt += "   " + dtRes.Rows[7]["C2"].ToString();//PRD5
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 24)
            {
                txt += "   " + dtRes.Rows[7]["C3"].ToString();//PRD5
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 25)
            {
                txt += "   " + dtRes.Rows[7]["C2"].ToString();//PRD5
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 26)
            {
                txt += "   " + dtRes.Rows[7]["C3"].ToString();//PRD5
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 27)
            {
                txt += "   " + dtRes.Rows[8]["C2"].ToString();//PRD6
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 28)
            {
                txt += "   " + dtRes.Rows[8]["C3"].ToString();//PRD6
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 29)
            {
                txt += "   " + dtRes.Rows[8]["C2"].ToString();//PRD6
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 30)
            {
                txt += "   " + dtRes.Rows[8]["C3"].ToString();//PRD6
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 31)
            {
                txt += "   " + dtRes.Rows[9]["C2"].ToString();//PRD7
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 32)
            {
                txt += "   " + dtRes.Rows[9]["C3"].ToString();//PRD7
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 33)
            {
                txt += "   " + dtRes.Rows[9]["C2"].ToString();//PRD7
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 34)
            {
                txt += "   " + dtRes.Rows[9]["C3"].ToString();//PRD7
            }

            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 35)
            {
                txt += "   " + dtRes.Rows[10]["C2"].ToString();//PRD8
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 36)
            {
                txt += "   " + dtRes.Rows[10]["C3"].ToString();//PRD8
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 37)
            {
                txt += "   " + dtRes.Rows[10]["C2"].ToString();//PRD8
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 38)
            {
                txt += "   " + dtRes.Rows[10]["C3"].ToString();//PRD8
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 39)
            {
                txt += "   Balance" + dtRes.Rows[11]["C3"].ToString();//Balance
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 40)
            {
                txt += "   Balance" + dtRes.Rows[11]["C3"].ToString();//Balance
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 41)
            {
                txt += "   Interest";//Interest
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 42)
            {
                txt += "   " + dtRes.Rows[12]["C3"].ToString();//Interest
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 43)
            {
                txt += "   Interest";//Interest
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 44)
            {
                txt += "   " + dtRes.Rows[12]["C3"].ToString();//Interest
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 45)
            {
                txt += "  " + dtRes.Rows[13]["C3"].ToString();//Amount
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 46)
            {
                txt += "  " + dtRes.Rows[13]["C3"].ToString();//Amount

            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 47)
            {
                txt += "   " + dtRes.Rows[14]["C3"].ToString();//Inword
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 48)
            {
                txt += "   " + dtRes.Rows[14]["C3"].ToString();//Inword
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 51)
            {
                txt += "   " + dtRes.Rows[15]["C3"].ToString();//Inword1
            }
            else if (Convert.ToInt32(DtPara.Rows[s]["columnStatus"]) == 52)
            {
                txt += "   " + dtRes.Rows[15]["C3"].ToString();//Inword1
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine(txt);

                    writer.Close();
                    Response.Output.Write(writer);
                }
            }
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    public string rupees(Int64 rupees)
    {
        string result = "";
        Int64 res;
        if ((rupees / 10000000) > 0)
        {
            res = rupees / 10000000;
            rupees = rupees % 10000000;
            result = result + ' ' + rupeestowords(res) + " Crore";
        }
        if ((rupees / 100000) > 0)
        {
            res = rupees / 100000;
            rupees = rupees % 100000;
            result = result + ' ' + rupeestowords(res) + " Lack";
        }
        if ((rupees / 1000) > 0)
        {
            res = rupees / 1000;
            rupees = rupees % 1000;
            result = result + ' ' + rupeestowords(res) + " Thousand";
        }
        if ((rupees / 100) > 0)
        {
            res = rupees / 100;
            rupees = rupees % 100;
            result = result + ' ' + rupeestowords(res) + " Hundred";
        }
        if ((rupees % 10) > 0)
        {
            res = rupees % 100;
            result = result + " " + rupeestowords(res);
        }
        result = result + ' ' + " Rupees only";
        return result;
    }
    public string rupeestowords(Int64 rupees)
    {
        string result = "";
        if ((rupees >= 1) && (rupees <= 10))
        {
            if ((rupees % 10) == 1) result = "One";
            if ((rupees % 10) == 2) result = "Two";
            if ((rupees % 10) == 3) result = "Three";
            if ((rupees % 10) == 4) result = "Four";
            if ((rupees % 10) == 5) result = "Five";
            if ((rupees % 10) == 6) result = "Six";
            if ((rupees % 10) == 7) result = "Seven";
            if ((rupees % 10) == 8) result = "Eight";
            if ((rupees % 10) == 9) result = "Nine";
            if ((rupees % 10) == 0) result = "Ten";
        }
        if (rupees > 9 && rupees < 20)
        {
            if (rupees == 11) result = "Eleven";
            if (rupees == 12) result = "Twelve";
            if (rupees == 13) result = "Thirteen";
            if (rupees == 14) result = "Forteen";
            if (rupees == 15) result = "Fifteen";
            if (rupees == 16) result = "Sixteen";
            if (rupees == 17) result = "Seventeen";
            if (rupees == 18) result = "Eighteen";
            if (rupees == 19) result = "Nineteen";
        }
        if (rupees > 20 && (rupees / 10) == 2 && (rupees % 10) == 0) result = "Twenty";
        if (rupees > 20 && (rupees / 10) == 3 && (rupees % 10) == 0) result = "Thirty";
        if (rupees > 20 && (rupees / 10) == 4 && (rupees % 10) == 0) result = "Forty";
        if (rupees > 20 && (rupees / 10) == 5 && (rupees % 10) == 0) result = "Fifty";
        if (rupees > 20 && (rupees / 10) == 6 && (rupees % 10) == 0) result = "Sixty";
        if (rupees > 20 && (rupees / 10) == 7 && (rupees % 10) == 0) result = "Seventy";
        if (rupees > 20 && (rupees / 10) == 8 && (rupees % 10) == 0) result = "Eighty";
        if (rupees > 20 && (rupees / 10) == 9 && (rupees % 10) == 0) result = "Ninty";

        if (rupees > 20 && (rupees / 10) == 2 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Twenty One";
            if ((rupees % 10) == 2) result = "Twenty Two";
            if ((rupees % 10) == 3) result = "Twenty Three";
            if ((rupees % 10) == 4) result = "Twenty Four";
            if ((rupees % 10) == 5) result = "Twenty Five";
            if ((rupees % 10) == 6) result = "Twenty Six";
            if ((rupees % 10) == 7) result = "Twenty Seven";
            if ((rupees % 10) == 8) result = "Twenty Eight";
            if ((rupees % 10) == 9) result = "Twenty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 3 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Thirty One";
            if ((rupees % 10) == 2) result = "Thirty Two";
            if ((rupees % 10) == 3) result = "Thirty Three";
            if ((rupees % 10) == 4) result = "Thirty Four";
            if ((rupees % 10) == 5) result = "Thirty Five";
            if ((rupees % 10) == 6) result = "Thirty Six";
            if ((rupees % 10) == 7) result = "Thirty Seven";
            if ((rupees % 10) == 8) result = "Thirty Eight";
            if ((rupees % 10) == 9) result = "Thirty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 4 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Forty One";
            if ((rupees % 10) == 2) result = "Forty Two";
            if ((rupees % 10) == 3) result = "Forty Three";
            if ((rupees % 10) == 4) result = "Forty Four";
            if ((rupees % 10) == 5) result = "Forty Five";
            if ((rupees % 10) == 6) result = "Forty Six";
            if ((rupees % 10) == 7) result = "Forty Seven";
            if ((rupees % 10) == 8) result = "Forty Eight";
            if ((rupees % 10) == 9) result = "Forty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 5 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Fifty One";
            if ((rupees % 10) == 2) result = "Fifty Two";
            if ((rupees % 10) == 3) result = "Fifty Three";
            if ((rupees % 10) == 4) result = "Fifty Four";
            if ((rupees % 10) == 5) result = "Fifty Five";
            if ((rupees % 10) == 6) result = "Fifty Six";
            if ((rupees % 10) == 7) result = "Fifty Seven";
            if ((rupees % 10) == 8) result = "Fifty Eight";
            if ((rupees % 10) == 9) result = "Fifty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 6 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Sixty One";
            if ((rupees % 10) == 2) result = "Sixty Two";
            if ((rupees % 10) == 3) result = "Sixty Three";
            if ((rupees % 10) == 4) result = "Sixty Four";
            if ((rupees % 10) == 5) result = "Sixty Five";
            if ((rupees % 10) == 6) result = "Sixty Six";
            if ((rupees % 10) == 7) result = "Sixty Seven";
            if ((rupees % 10) == 8) result = "Sixty Eight";
            if ((rupees % 10) == 9) result = "Sixty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 7 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Seventy One";
            if ((rupees % 10) == 2) result = "Seventy Two";
            if ((rupees % 10) == 3) result = "Seventy Three";
            if ((rupees % 10) == 4) result = "Seventy Four";
            if ((rupees % 10) == 5) result = "Seventy Five";
            if ((rupees % 10) == 6) result = "Seventy Six";
            if ((rupees % 10) == 7) result = "Seventy Seven";
            if ((rupees % 10) == 8) result = "Seventy Eight";
            if ((rupees % 10) == 9) result = "Seventy Nine";
        }
        if (rupees > 20 && (rupees / 10) == 8 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Eighty One";
            if ((rupees % 10) == 2) result = "Eighty Two";
            if ((rupees % 10) == 3) result = "Eighty Three";
            if ((rupees % 10) == 4) result = "Eighty Four";
            if ((rupees % 10) == 5) result = "Eighty Five";
            if ((rupees % 10) == 6) result = "Eighty Six";
            if ((rupees % 10) == 7) result = "Eighty Seven";
            if ((rupees % 10) == 8) result = "Eighty Eight";
            if ((rupees % 10) == 9) result = "Eighty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 9 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Ninty One";
            if ((rupees % 10) == 2) result = "Ninty Two";
            if ((rupees % 10) == 3) result = "Ninty Three";
            if ((rupees % 10) == 4) result = "Ninty Four";
            if ((rupees % 10) == 5) result = "Ninty Five";
            if ((rupees % 10) == 6) result = "Ninty Six";
            if ((rupees % 10) == 7) result = "Ninty Seven";
            if ((rupees % 10) == 8) result = "Ninty Eight";
            if ((rupees % 10) == 9) result = "Ninty Nine";
        }
        return result;
    }
    #endregion
    //ADDED BY ANKITA 08/02/2018 FOR UPDATE MOBILE NUMBER WHILE TRANSACTION
    public void BtnMobUpld_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = CMN.getContct(Txtcustno.Text);
            if (dt.Rows.Count > 0)
            {
                TxtCustno1.Text = Txtcustno.Text;
                TxtBrcd1.Text = dt.Rows[0]["brcd"].ToString();
            }
            TxtMob1.Text = TxtMobile1.Text;
            TxtMob2.Text = TxtMobile2.Text;

            string Modal_Flag = "CNTCT";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#" + Modal_Flag + "').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModlUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtMob1.Text != "")
            {
                if (TxtMob1.Text.Length < 10)
                {
                    WebMsgBox.Show("Enter 10 digit contact number..!!", this.Page);
                    return;
                }
            }
            if (TxtMob2.Text != "" && TxtMob2.Text != "0")
            {
                if (TxtMob2.Text.Length < 10)
                {
                    WebMsgBox.Show("Enter 10 digit contact number..!!", this.Page);
                    return;
                }
            }
            resultout = CMN.insertContct(TxtCustno1.Text, TxtBrcd1.Text, TxtMob1.Text == "" ? "0" : TxtMob1.Text, TxtMob2.Text == "" ? "0" : TxtMob2.Text, Session["MID"].ToString());
            if (resultout > 0)
            {
                WebMsgBox.Show("Contact Number changed Successfully..!!", this.Page);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                //lblMessage.Text = "Contact Added Successfully..!!";
                //ModalPopup.Show(this.Page);
                //BtnModlUpdate.Attributes.Add("data-dismiss", "modal");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Runbat3()", true);
    }
    protected void Btn_KycUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txtcustno.Text == "" || Txtcustno.Text == "0")
            {
                WebMsgBox.Show("Cutomer number cannot be empty...!", this.Page);
                return;
            }
            HttpContext.Current.Response.Redirect("FrmIAddress.aspx?Flag=" + Txtcustno.Text + "_MD_RDR", false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtRecNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string RR = CMN.CheckRec_Exists("RECSRNO_EX", Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtRecNo.Text == "" ? "0" : TxtRecNo.Text);
            if (RR != null)
            {
                if (RR == "EXISTS")
                {
                    Accno_Change();
                    getMobile();
                    if (ViewState["DRGL"].ToString() == "5")
                    {
                        SetFDAmt(TxtProcode.Text, TxtAccNo.Text);
                    }
                }
                else
                {
                    WebMsgBox.Show("Invalid Receipt Number....!", this.Page);
                    TxtRecNo.Text = "";
                    TxtRecNo.Focus();
                }

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}