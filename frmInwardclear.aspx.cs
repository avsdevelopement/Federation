using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class frmInwordclear : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsOWGCharges OWG = new ClsOWGCharges();
    ClsInwordClear OWGCL = new ClsInwordClear();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    //DbConnection conn = new DbConnection();
    scustom customcs = new scustom();
    ClsOpenClose OC = new ClsOpenClose();
    ClsOutAuthDo AD = new ClsOutAuthDo();
    ClsOutClear CLSOW = new ClsOutClear();
    ClsReturnIO RI = new ClsReturnIO();
    ClsCommon CM = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    public static string Flag;
    string sqlnewsetno = "1";
    string newscrlno = "1", STR = "";
    string newsetno = "1";
    double SumFooterValue = 0;
    double TotalValue = 0;
    int NumOfInst = 0;
    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
            if (ViewState["UN_FL"].ToString() != "AD")
            {
                ViewState["SETNO"] = Request.QueryString["setno"].ToString();
                ViewState["SCROLL"] = Request.QueryString["scrollno"].ToString();

                if (ViewState["SETNO"].ToString() != null)
                    GetUnpassDetails();
            }

            //customcs.BindInstruType(ddlinsttype);
            customcs.BindBankName(ddlBankName);
            TxtEntrydate.Text = Session["EntryDate"].ToString();
            //txtinstdate.Text = Session["EntryDate"].ToString();
            string smid = Session["MID"].ToString();
            string sBRCD = Session["BRCD"].ToString();
            ViewState["MultiEntryFlag"] = "0";
            BindGrid();
            Flag = "1";
            ViewState["Status"] = "new";
            autoglname.ContextKey = Session["BRCD"].ToString();

            //LnkVerify.Visible = false;
            rdbDebit.Checked = true;
            ViewState["Flag"] = "AD";
            btnSubmit.Text = "Submit";
            BD.BindReasons(DdlReason);
            TblDiv_MainWindow.Visible = false;
        }

        TxtProcode.Focus();

    }

    public void GetUnpassDetails()
    {
        try
        {
            DT = OWGCL.GetUnpass(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), ViewState["SCROLL"].ToString(), Session["EntryDate"].ToString(), "I");
            if (DT != null)
            {
                //ENTRYDATE	PRDUCT_CODE	ACC_NO	PARTICULARS	BANK_CODE	BRANCH_CODE	INSTRU_TYPE	INSTRUDATE	INSTRU_NO	INSTRU_AMOUNT	FUNDING_DATE	BRCD	STAGE	MID	CID	VID	PAC_MAC	CLG_FLAG	ACC_TYPE	OPRTN_TYPE	SYSTEM_DATE	SET_NO	SCROLL_NO	CD	id	REASON_CODE
                string PRDCD, ACCNO, BANKCODE, BRANCHCODE, INSTRUDATE, INSTRUNO, INSTRUAMT, BANKNAME, BRNAME, PRDNAME, ACCNAME;
                PRDCD = DT.Rows[0]["PRDUCT_CODE"].ToString();
                ACCNO = DT.Rows[0]["ACC_NO"].ToString();
                BANKCODE = DT.Rows[0]["BANK_CODE"].ToString();
                BRANCHCODE = DT.Rows[0]["BRANCH_CODE"].ToString();
                INSTRUDATE = DT.Rows[0]["INSTRUDATE"].ToString();
                INSTRUNO = DT.Rows[0]["INSTRU_NO"].ToString();
                INSTRUAMT = DT.Rows[0]["INSTRU_AMOUNT"].ToString();
                if (BANKCODE != null)
                {
                    BANKNAME = customcs.GetBankName(BANKCODE.ToString());
                    txtbankcd.Text = BANKCODE;
                    txtbnkdname.Text = BANKNAME;
                }
                if (BRANCHCODE != null)
                {
                    BRNAME = customcs.GetBranchName(BANKCODE, BRANCHCODE);
                    txtbrnchcd.Text = BRANCHCODE;
                    txtbrnchcdname.Text = BRNAME;
                }

                if (PRDCD != null)
                {
                    string ABC = BD.GetAccTypeGL(PRDCD.ToString(), Session["BRCD"].ToString());
                    string[] PRD = ABC.Split('_');
                    TxtProcode.Text = PRDCD;
                    TxtProName.Text = PRD[0].ToString();
                    ViewState["GLC"] = PRD[1].ToString();
                }
                if (ACCNO != null)
                {
                    ACCNAME = BD.GetAccName(ACCNO, PRDCD, Session["BRCD"].ToString());
                    TxtAccNo.Text = ACCNO;
                    TxtAccName.Text = ACCNAME;
                    GetBal4Unpass(PRDCD, ACCNO, ViewState["GLC"].ToString());

                }
                if (INSTRUDATE != null)
                {
                    txtinstdate.Text = INSTRUDATE.ToString();
                }
                if (INSTRUNO != null)
                {
                    txtinstno.Text = INSTRUNO.ToString();
                }
                if (INSTRUAMT != null)
                {
                    txtinstamt.Text = INSTRUAMT.ToString();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void SaveOwg(object sender, EventArgs e)
    {
        try
        {
            if (txtinstamt.Text != "0")
            {
                DateTime Test;
                if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
                {
                    double BAL, curbal, LBAL = 0;
                    bool Valid = true;
                    int res = 0;
                    BAL = curbal = 0;
                    if (rdbDebit.Checked == true && ViewState["Flag"].ToString() != "DL" && ViewState["Flag"].ToString() != "MD")
                    {
                        BAL = Convert.ToDouble(TxtBalance.Text);
                        curbal = Convert.ToDouble(txtinstamt.Text);
                    }

                    if (ViewState["UN_FL"].ToString() == "UN")
                    {
                        if (ViewState["GL"].ToString() == "3")
                        {
                            STR = OWGCL.GetLoanLimit(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                            string STR2 = OWGCL.GetLoanCatDuedate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, "DC");
                            string[] OD = STR2.Split('_');
                            LBAL = Convert.ToDouble(STR) - Math.Abs(Convert.ToDouble(TxtBalance.Text));
                            //if (STR != null && curbal > Convert.ToDouble(STR))
                            if (LBAL < curbal)
                            {
                                //WebMsgBox.Show("Loan A/C Overdrawned by  " + LBAL + ",Return Entry...!", this.Page);
                                lblMessage.Text = "";
                                lblMessage.Text = "Loan A/C Overdrawned of Rs. " + Math.Abs(LBAL) + " ,Return  Entry...!";
                                ModalPopup.Show(this.Page);
                                btnSubmit.Enabled = false;
                                BtnPostUnpass.Focus();
                                BtnPostUnpass.Enabled = false;
                                Btn_Return.Enabled = true;
                                btnSubmit.Enabled = false;
                                DIV_RETRN.Visible = true;
                                Valid = false;
                            }
                            if (LBAL > curbal && Convert.ToDateTime(OD[0].ToString()) < Convert.ToDateTime(Session["EntryDate"].ToString()))
                            {
                                // WebMsgBox.Show("Limit Date Expired.....!", this.Page);
                                lblMessage.Text = "";
                                lblMessage.Text = "Due Date " + OD[0].ToString() + "  Expired , Return Entry!";
                                ModalPopup.Show(this.Page);
                                // btnSubmit.Enabled = false;
                                // BtnPostUnpass.Focus();
                                //  BtnPostUnpass.Enabled = false;
                                //  Btn_Return.Enabled = true;
                                btnSubmit.Enabled = true;
                                //DIV_RETRN.Visible = true;
                                //  Valid = false;
                            }
                        }
                        else
                        {
                            if (BAL < curbal && Valid == true && Convert.ToInt32(TxtProcode.Text) != 177)
                            {
                                lblMessage.Text = "";
                                lblMessage.Text = "Insufficient Balance....";
                                ModalPopup.Show(this.Page);
                                return;
                            }
                            else
                            {
                                if (ViewState["Flag"].ToString() != "DL")
                                {
                                    res = OWGCL.DeleteUnpass(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["EntryDate"].ToString(), "I");
                                    if (res > 0)
                                    {
                                        DoEntry();
                                    }
                                }

                            }
                        }

                    }
                    else if (ViewState["UN_FL"].ToString() == "AD")
                    {

                        DoEntry();

                    }

                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Enter Valid date ...!";
                    ModalPopup.Show(this.Page);
                    txtinstdate.Text = "";
                    txtinstdate.Focus();

                }
            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Cheque Amount 0 not accepted,Enter Amount";
                ModalPopup.Show(this.Page);

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public void DoEntry()
    {
        try
        {

            string BRCD = Session["BRCD"].ToString();
            string PACMAC = conn.PCNAME(); ///System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
            //Get values from controls
            string Entrydate = TxtEntrydate.Text.ToString();
            string Procode = TxtProcode.Text.ToString();
            string AccNo = TxtAccNo.Text.ToString();
            string AccTypeid = txtAccTypeid.Text.ToString();
            string OpTypeId = txtOpTypeId.Text.ToString();
            string partic = txtpartic.Text.ToString();
            string bankcd = txtbankcd.Text.ToString();
            string brnchcd = txtbrnchcd.Text.ToString();
            // string insttype = ddlinsttype.SelectedValue.ToString();
            string instdate = txtinstdate.Text.ToString();
            string instno = txtinstno.Text.ToString();
            string instamt = txtinstamt.Text.ToString();
            //        

            // Get CLG_GL_NO 
            //int CLG_GL_NO = OWGCL.Get_CLG_GL_NO(BRCD);
            int CLG_GL_NO = Convert.ToInt32(ddlBankName.SelectedValue);
            // 
            if (rdbCredit.Checked == true)
            {
                if (ddlBankName.SelectedIndex == 0)
                {
                    WebMsgBox.Show("Select Bank Name!......", this.Page);
                    ddlBankName.Focus();
                }
            }
            //if (ddlBankName.SelectedIndex != 0)
            //{
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (ViewState["MultiEntryFlag"].ToString() == "0")
                {
                    //MAK 10/01/2017 CHANGES IN GetSetno
                    string setno = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString()); //OWGCL.GetNewSetNo(BRCD);
                    // int setnonew = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", setno); //OWGCL.GetcurrentSetNo(BRCD);
                    int ScrollNo = 1;
                    //MAK CHAHES insttype ="0"
                    int RC = 0;
                    if (ViewState["UN_FL"].ToString() == "AD")
                    {

                        RC = OWGCL.InsertNewSetNo(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, instamt, Session["MID"].ToString(), PACMAC, Convert.ToInt32(setno), ScrollNo, CLG_GL_NO, "D", "1001", Dll_Session.SelectedValue);
                    }
                    else if (ViewState["UN_FL"].ToString() == "UN")
                    {
                        RC = OWGCL.InsertNewSetNo(ViewState["GLC"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, instamt, Session["MID"].ToString(), PACMAC, Convert.ToInt32(setno), ScrollNo, CLG_GL_NO, "D", "1001", Dll_Session.SelectedValue);
                    }

                    if (RC > 0)
                    {

                        BindGrid();
                        Cleardata();
                        WebMsgBox.Show("Record Submitted with Inward Clearing : " + setno, this.Page);
                        //lblMessage.Text = "Record Submitted with Inward Clearing : " + setno;
                        //ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Add _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        TxtProcode.Focus();
                        SumIwOw();
                        return;
                    }
                }

                else if (ViewState["MultiEntryFlag"].ToString() == "1")
                {
                    int setno = Convert.ToInt32(txtsetno.Text);
                    int ScrollNo = OWGCL.GetNewScrollNo(setno, Session["BRCD"].ToString());
                    //MAK CHANGE insttype="0"
                    int RC = 0;
                    if (rdbDebit.Checked == true)
                    {
                        RC = OWGCL.InsertNewSetNo(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, instamt, Session["MID"].ToString(), PACMAC, setno, ScrollNo, CLG_GL_NO, "D", "1001", Dll_Session.SelectedValue);
                    }
                    else
                    {
                        RC = OWGCL.InsertNewSetNo(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, TxtCLGAmount.Text, Session["MID"].ToString(), PACMAC, setno, ScrollNo, CLG_GL_NO, "C", "1001", Dll_Session.SelectedValue);
                    }
                    int setnonew = OWGCL.GetcurrentSetNo(BRCD);
                    ViewState["ScrollNo"] = "1";
                    ViewState["SetNo"] = setnonew;
                    BindGrid();
                    Cleardata();
                    txtsetno.Text = setnonew.ToString();
                    if (RC > 0)
                    {
                        WebMsgBox.Show("Record Saved Successfully", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Add _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        TxtProcode.Focus();
                        SumIwOw();
                        //ddlinsttype.Focus();
                        return;
                    }
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {

                int RC = CLSOW.UdateValues(ViewState["ST"].ToString(), txtpartic.Text, txtinstamt.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), txtbankcd.Text, txtbrnchcd.Text, txtinstno.Text, txtinstdate.Text, Session["EntryDate"].ToString(), "I");
                if (RC > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Record Modify Successfully.......!!", this.Page);
                    SumIwOw();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Mod _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    // lblMessage.Text = "Record Modify Successfully.......!!";
                    // ModalPopup.Show(this.Page);
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                int RC = 0;
                string STR = OWGCL.GetClgFlag(Session["BRCD"].ToString(), ViewState["ST"].ToString(), Session["EntryDate"].ToString(), "I");
                if (STR == "4")
                {
                    RC = OWGCL.DeleteUnpass(Session["BRCD"].ToString(), ViewState["ST"].ToString(), Session["EntryDate"].ToString(), "I");
                }
                else
                {
                    RC = CLSOW.DeleteValues(ViewState["ST"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "I", "PASS",Session["MID"].ToString());
                }
                if (RC > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Record Delete Successfully.......!!", this.Page);
                    SumIwOw();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Del _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    // lblMessage.Text = "Record Delete Successfully.......!!";
                    //ModalPopup.Show(this.Page);
                    Cleardata();
                    ENDN(true);
                }
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
            if (TxtAccNo.Text == "")
            {
                TxtAccName.Text = "";
                txtAccTypeid.Text = "";
                TxtAccTypeName.Text = "";
                txtOpTypeId.Text = "";
                TxtOpTypeName.Text = "";
                //MAK 6/1/17 CHANGE
                //ddlinsttype.Focus(); 
                goto ext;
            }

            DataTable dt1 = new DataTable();
            if (TxtAccNo.Text != "" & TxtProcode.Text != "")
            {
                string CU1 = customcs.GetAccountName(TxtAccNo.Text.ToString(), TxtProcode.Text.ToString(), Session["BRCD"].ToString());
                if (CU1 != null)
                {
                    string[] CU;
                    CU = CU1.Split('_');
                    ViewState["CUSTNO"] = CU[0].ToString();
                    TxtAccName.Text = CU[1].ToString();
                    TxtAccNo.Text = CU[2].ToString();
                    STR = BD.GetACCSTS(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                    if (STR != null)
                    {
                        CU = STR.Split('_');
                        TxtACCStatus.Text = CU[0].ToString();
                        ViewState["ACCSTATUS"] = CU[1].ToString();
                        if (ViewState["ACCSTATUS"].ToString() == "6" || ViewState["ACCSTATUS"].ToString() == "4" || ViewState["ACCSTATUS"].ToString() == "5")
                        {
                            WebMsgBox.Show("Account No." + TxtAccNo.Text + " status is " + TxtACCStatus.Text + "...!", this.Page);
                            BtnPostUnpass.Enabled = false;
                            Btn_Return.Enabled = true;
                            btnSubmit.Enabled = false;
                            DIV_RETRN.Visible = true;
                        }
                        else
                        {

                            btnSubmit.Enabled = true;
                            BtnPostUnpass.Enabled = false;
                            Btn_Return.Enabled = false;
                            DIV_RETRN.Visible = false;


                        }
                    }


                    if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                    {
                        WebMsgBox.Show("Please enter valid Account number", this.Page);
                        TxtAccNo.Text = "";
                        TxtAccNo.Focus();
                        return;
                    }
                    // LnkVerify.Visible = true;
                }
                else
                {
                    WebMsgBox.Show("Invalid A/C Number....!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                    TxtAccName.Text = "";
                }
            }

            if (TxtAccNo.Text == "" || TxtProcode.Text == "")
            {
                TxtAccName.Text = "";
                txtAccTypeid.Text = "";
                TxtAccTypeName.Text = "";
                txtOpTypeId.Text = "";
                TxtOpTypeName.Text = "";
                //ddlinsttype.Focus();
                goto ext;
            }
            dt1 = customcs.GetAccNoAccType(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                TxtAccTypeName.Text = dt1.Rows[0]["ACCTYPE"].ToString();
                txtAccTypeid.Text = dt1.Rows[0]["ACC_TYPE"].ToString();
                txtOpTypeId.Text = dt1.Rows[0]["OPR_TYPE"].ToString();
                TxtOpTypeName.Text = dt1.Rows[0]["OPRTYPE"].ToString();
                // ddlinsttype.Focus();
                GetBalance();
                txtbankcd.Focus();
            }
            else
            {
                txtAccTypeid.Text = "";
                TxtAccTypeName.Text = "";
                txtOpTypeId.Text = "";
                TxtOpTypeName.Text = "";
                // ddlinsttype.Focus();
            }

            //Details Filling Section
            Details_Section();


        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccntTypeChanged(object sender, EventArgs e)
    {
        TxtAccTypeName.Text = customcs.GetAccountTypeName(txtAccTypeid.Text);
        txtOpTypeId.Focus();
    }

    protected void txtbankcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtbankcd.Text == "")
            {
                txtbnkdname.Text = "";
                txtbrnchcd.Text = "";
                txtbrnchcdname.Text = "";
                goto ext;
            }
            txtbnkdname.Text = customcs.GetBankName(txtbankcd.Text);
            AutoBranch.ContextKey = txtbankcd.Text;
            if (txtbnkdname.Text == "" & txtbankcd.Text != "")
            {
                WebMsgBox.Show("Please enter valid bank code", this.Page);
                txtbankcd.Text = "";
                txtbankcd.Focus();
            }
            else
            {
                txtbrnchcd.Focus();
            }
        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void txtbrnchcd_TextChanged(object sender, EventArgs e)
    {
        if (txtbankcd.Text == "" || txtbrnchcd.Text == "")
        {
            txtbrnchcdname.Text = "";
            goto ext;
        }
        txtbrnchcdname.Text = customcs.GetBranchName(txtbankcd.Text, txtbrnchcd.Text);
        if (txtbrnchcdname.Text == "" & txtbrnchcd.Text != "")
        {
            WebMsgBox.Show("Please enter valid branch number", this.Page);
            txtbrnchcd.Text = "";
            txtbrnchcd.Focus();
        }
        else
        {
            //For Focusing cursor at last character of Textbox
            txtpartic.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtpartic.Focus();
            txtpartic.Text = "By IW Clg";
        }
    ext: ;

    }

    protected void TxtOpTypeIdChanged(object sender, EventArgs e)
    {
        TxtOpTypeName.Text = customcs.GetOpTypeName(txtOpTypeId.Text);
        txtpartic.Focus();
    }

    public void BindGrid()
    {
        int RS = OWG.BindClearing(grdCharged, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
    }

    protected void grdCharged_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCharged.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void txtsetno_TextChanged(object sender, EventArgs e)
    {
        if (txtsetno.Text == "")
        {
            WebMsgBox.Show("Please enter Set No", this.Page);
            TxtProcode.Focus();
            return;
        }

        // Bind data of Set no which user inserted

        // End Bind Data 

        DataTable DT = new DataTable();
        string[] TD = TxtEntrydate.Text.Split('/');

        string sql = "SELECT OW.*,GL.GLNAME PRDNAME,M.CUSTNAME,GL1.GLNAME ACCTYPE ,GL2.GLNAME OPETYPE " +
            //,LNO.DESCRIPTION,RBI.BANK,RBI2.BRANCH,LNO1.DESCRIPTION OPRTYPE,LNO2.DESCRIPTION ACCTYPE 
                    "FROM INWORD_" + TD[2].ToString() + TD[1].ToString() + " OW " +
                    "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND GL.BRCD=OW.BRCD " +
                    "LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD  " +
                    "LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD  " +
                    "LEFT JOIN GLMAST GL1 ON GL1.SUBGLCODE=OW.ACC_TYPE AND GL1.BRCD=OW.BRCD  " +
                    "LEFT JOIN GLMAST GL2 ON GL2.SUBGLCODE=OW.OPRTN_TYPE AND GL2.BRCD=OW.BRCD " +
            //"LEFT JOIN (SELECT DESCR BANK,BANKRBICD FROM RBIBANK WHERE BRANCHRBICD='0' AND STATECD ='400') RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
            //"LEFT JOIN (SELECT DESCR BRANCH,BRANCHRBICD,BANKRBICD FROM RBIBANK WHERE STATECD ='400') RBI2 ON RBI2.BRANCHRBICD=OW.BRANCH_CODE AND RBI2.BANKRBICD=RBI.BANKRBICD " +
            // "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1017')LNO1 ON LNO1.SRNO=AC.OPR_TYPE " +
            // "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1016')LNO2 ON LNO2.SRNO=AC.ACC_TYPE " +
            // "LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE LNO=1022) LNO ON LNO.SRNO=OW.INSTRU_TYPE "+
                   " WHERE OW.SET_NO='" + txtsetno.Text + "' AND OW.STAGE<>'1004' AND OW.CD IN ('C','D') and OW.ENTRYDATE='" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'";
        DT = conn.GetDatatable(sql);
        if (DT.Rows.Count > 0)
        {
            TxtEntrydate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
            TxtProcode.Text = DT.Rows[0]["PRDUCT_CODE"].ToString();
            TxtProName.Text = DT.Rows[0]["PRDNAME"].ToString();
            TxtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
            TxtAccNo.Text = DT.Rows[0]["ACC_NO"].ToString();
            txtAccTypeid.Text = DT.Rows[0]["ACC_TYPE"].ToString();
            TxtAccTypeName.Text = DT.Rows[0]["ACCTYPE"].ToString();
            txtOpTypeId.Text = DT.Rows[0]["OPRTN_TYPE"].ToString();
            //TxtOpTypeName.Text = DT.Rows[0]["OPRTYPE"].ToString();
            txtpartic.Text = DT.Rows[0]["PARTICULARS"].ToString();
            if (ViewState["MultiEntryFlag"].ToString() == "2")
            {
                // ddlinsttype.SelectedValue = DT.Rows[0]["INSTRU_TYPE"].ToString();
                txtbankcd.Text = DT.Rows[0]["BANK_CODE"].ToString();
                txtbrnchcd.Text = DT.Rows[0]["BRANCH_CODE"].ToString();
                txtinstno.Text = DT.Rows[0]["INSTRU_NO"].ToString();
                txtinstdate.Text = Convert.ToDateTime(DT.Rows[0]["INSTRUDATE"]).ToString("dd/MM/yyyy");
                txtinstamt.Text = DT.Rows[0]["INSTRU_Amount"].ToString();
                txtbrnchcdname.Text = DT.Rows[0]["BRANCH"].ToString();
                txtbnkdname.Text = DT.Rows[0]["BANK"].ToString();
            }
            // ddlinsttype.Focus();
        }
        else
        {
            WebMsgBox.Show("No Record found for this set No", this.Page);
            Cleardata();
            txtsetno.Focus();
        }

    ext: ;
    }

    protected void UpdateOwg(object sender, EventArgs e)
    {
        string PACMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
        result = OWGCL.DeleteOwgClearingEntry(Convert.ToInt32(txtsetno.Text), Session["BRCD"].ToString());
        BindGrid();
        if (result == -1)
        {
            WebMsgBox.Show("Something went wrong.....!!!", this.Page);
        }
        else
        {
            WebMsgBox.Show("Record Deleted successfully.....!!!", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Del _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        Cleardata();
    }

    public void Cleardata()
    {
        if (ViewState["MultiEntryFlag"].ToString() == "1" || ViewState["MultiEntryFlag"].ToString() == "2")
        {
            //TxtGLCD.Text = "";
            txtbankcd.Text = "";
            txtbrnchcd.Text = "";
            txtinstamt.Text = "";
            txtbrnchcdname.Text = "";
            //TxtGLCD.Text = "";
            TxtProcode.Text = "";
            TxtAccNo.Text = "";
            txtbnkdname.Text = "";
            ddlBankName.SelectedIndex = 0;
            // txtinstdate.Text = Session["EntryDate"].ToString();
            // ddlinsttype.SelectedIndex = 3;
            TxtTotalBal.Text = "";
            TxtProcode.Focus();
            TxtLDueDt.Text = "";
            TxtLInstAmt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            TxtLInstAmt.Text = "";
            TxtLDueDt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            Img1.Src = null;
            Img2.Src = null;
        }
        else if (ViewState["MultiEntryFlag"].ToString() == "0" || ViewState["MultiEntryFlag"].ToString() == "2")
        {
            //TxtGLCD.Text = "";
            // ddlinsttype.SelectedIndex = 0;
            //   txtinstdate.Text = Session["EntryDate"].ToString();
            txtinstno.Text = "";
            TxtProcode.Text = "";
            TxtProName.Text = "";
            TxtAccName.Text = "";
            TxtAccNo.Text = "";
            txtAccTypeid.Text = "";
            TxtAccTypeName.Text = "";
            txtOpTypeId.Text = "";
            TxtOpTypeName.Text = "";
            // txtpartic.Text = "By Inward Clearing";
            txtsetno.Text = "";
            txtbankcd.Text = "";
            txtbnkdname.Text = "";
            txtbrnchcd.Text = "";
            txtbrnchcdname.Text = "";
            txtinstamt.Text = "";
            TxtBalance.Text = "";
            TxtTotalBal.Text = "";
            TxtLDueDt.Text = "";
            TxtLInstAmt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            TxtLInstAmt.Text = "";
            TxtLDueDt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            Img1.Src = null;
            Img2.Src = null;
            TxtProcode.Focus();
        }
    }

    public void ENDN(bool TF)
    {
        TxtProcode.Enabled = TF;
        TxtProName.Enabled = TF;
        TxtAccName.Enabled = TF;
        TxtAccNo.Enabled = TF;
        //txtAccTypeid.Enabled = TF;
        //TxtAccTypeName.Enabled = TF;
        //txtOpTypeId.Enabled = TF;
        //TxtOpTypeName.Enabled = TF;
        txtpartic.Enabled = TF;
        // ddlinsttype.Enabled = TF;
        txtbankcd.Enabled = TF;
        txtbrnchcd.Enabled = TF;
        txtinstno.Enabled = TF;
        txtinstdate.Enabled = TF;
        txtinstamt.Enabled = TF;
        //txtbrnchcdname.Enabled = TF;
        //txtbnkdname.Enabled = TF;        
    }

    // ---------- ********* ------- QUERY ---------- *********
    //    SELECT OW.*,GL.GLNAME PRDNAME,M.CUSTNAME,LNO.DESCRIPTION,GL1.GLNAME ACCTYPE ,GL2.GLNAME OPETYPE FROM OWG_201607 OW INNER JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND GL.BRCD=OW.BRCD INNER JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD  INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD  LEFT JOIN GLMAST GL1 ON GL1.SUBGLCODE=OW.ACC_TYPE AND GL1.BRCD=OW.BRCD  LEFT JOIN GLMAST GL2 ON GL2.SUBGLCODE=OW.OPRTN_TYPE AND GL2.BRCD=OW.BRCD  LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE LNO=1022) LNO ON LNO.SRNO=OW.INSTRU_TYPE WHERE OW.OWGID=4
    protected void AddNewBtnClicked(object sender, EventArgs e)
    {
        try
        {
            lblstatus.Text = "New Entry";
            ViewState["Status"] = "new";
            ViewState["Flag"] = "AD";
            ViewState["MultiEntryFlag"] = 0;
            txtsetno.Text = "";
            txtsetno.ReadOnly = true;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            TxtProcode.Focus();
            Flag = "1";
            TblDiv_MainWindow.Visible = true;

            Cleardata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        try
        {
            Flag = "1";
            lblstatus.Text = "Entry to selected Set";
            ViewState["Status"] = "multiple";
            ViewState["Flag"] = "AD";
            txtsetno.Focus();
            ViewState["MultiEntryFlag"] = 1;
            txtsetno.Text = "";
            btnUpdate.Visible = false;
            btnSubmit.Visible = true;
            txtsetno.ReadOnly = false;
            Cleardata();
            txtsetno.Focus();
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DeleteBtnClicked(object sender, EventArgs e)
    {
        ViewState["MultiEntryFlag"] = 2;
        Cleardata();
        lblstatus.Text = "Delete selected Set";
        txtsetno.ReadOnly = false;
        txtsetno.Focus();
        btnUpdate.Visible = true;
        btnSubmit.Visible = false;
    }

    protected void radio14_CheckedChanged(object sender, EventArgs e)
    {
        WebMsgBox.Show("hi", this.Page);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        OWGCL.DeleteData(Session["BRCD"].ToString());
        OWGCL.ReportData(Session["BRCD"].ToString());
        //Response.Redirect("FrmOutClearView.aspx");
        string url = "FrmOutClearView.aspx";
        NewWindows(url);
    }

    // Open new window
    public void NewWindows(string url)
    {
        string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=100,top=100,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=500,left=100,top=100,resizable=no');", true);
    }

    protected void grdCharged_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void GetBalance()
    {
        try
        {

            TxtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
            TxtTotalBal.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        //string bal = "";
        //string[] TD = Session["EntryDate"].ToString().Split('/');
        //bal = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
        //TxtBalance.Text = bal.ToString();
        //bal = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
        //TxtTotalBal.Text = bal.ToString();
    }

    public void GetBal4Unpass(string PRDCD, string ACCNO, string GLCODE)
    {

        try
        {

            TxtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
            TxtTotalBal.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        //string bal = "";
        //string[] TD = Session["EntryDate"].ToString().Split('/');
        //bal = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), PRDCD, ACCNO, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
        //TxtBalance.Text = bal.ToString();
        //bal = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), PRDCD, ACCNO, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
        //TxtTotalBal.Text = bal.ToString();
    }

    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        string CustName = TxtAccName.Text;
        string[] CN = CustName.Split('_');

        if (CN.Length > 1)
        {
            TxtAccName.Text = CN[0].ToString();
            TxtAccNo.Text = CN[1].ToString();
            ViewState["CUSTNO"] = CN[2].ToString();
            STR = BD.GetACCSTS(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (STR != null)
            {
                string[] CU;
                CU = STR.Split('_');
                TxtACCStatus.Text = CU[0].ToString();
                ViewState["ACCSTATUS"] = CU[1].ToString();
                if (ViewState["ACCSTATUS"].ToString() == "6" || ViewState["ACCSTATUS"].ToString() == "4" || ViewState["ACCSTATUS"].ToString() == "5")
                {
                    WebMsgBox.Show("Account No." + TxtAccNo.Text + " status is " + TxtACCStatus.Text + "...!", this.Page);
                    BtnPostUnpass.Enabled = false;
                    Btn_Return.Enabled = true;
                    btnSubmit.Enabled = false;
                    DIV_RETRN.Visible = true;
                }
                else
                {

                    btnSubmit.Enabled = true;
                    BtnPostUnpass.Enabled = false;
                    Btn_Return.Enabled = false;
                    DIV_RETRN.Visible = false;

                }
            }
            txtbankcd.Focus();

            //  ddlinsttype.Focus();
            int RC = LI.CheckAccount(TxtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
            if (RC < 0)
            {
                TxtAccNo.Focus();
                WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                return;
            }
            GetBalance();
            ViewState["CustNo"] = RC;
            //   LnkVerify.Visible = true;

            Photo_Sign();
        }
    }

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        string TD = TxtProName.Text;
        string[] TDD = TD.Split('_');
        if (TDD.Length > 1)
        {
            string STR3 = OWGCL.GetLoanCatDuedate(Session["BRCD"].ToString(), TDD[1].ToString(), "", "C");

            if (STR3 == "CCOD" || STR3 == null)
            {
                TxtProName.Text = TDD[0].ToString();
                TxtProcode.Text = TDD[1].ToString();
                // TxtGLCD.Text = TD[2].ToString();
                string[] GLD = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString()).Split('_');
                TxtProName.Text = GLD[0].ToString();
                ViewState["GL"] = GLD[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text;// +"_" + ViewState["GL"].ToString();
                TxtAccNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Loan A/C not acceepted..Enter valid Product code", this.Page);
                TxtProcode.Text = "";
                TxtProName.Text = "";
                TxtProcode.Focus();
                return;
            }

        }
    }

    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtProcode.Text == "")
            {
                TxtProName.Text = "";
                txtAccTypeid.Text = "";
                TxtAccTypeName.Text = "";
                txtOpTypeId.Text = "";
                TxtOpTypeName.Text = "";
                TxtAccNo.Focus();
            }
            //TxtProName.Text = customcs.GetProductName(TxtProcode.Text);
            //string[] GLD =BD.
            string GL1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
            if (GL1 != null)
            {
                string[] GLD = GL1.Split('_');
                if (GLD.Length > 1)
                {
                    string STR3 = OWGCL.GetLoanCatDuedate(Session["BRCD"].ToString(), TxtProcode.Text, "", "C");

                    if (STR3 == "CCOD" || STR3 == null)
                    {
                        TxtProName.Text = GLD[0].ToString();
                        ViewState["GL"] = GLD[1].ToString();
                        AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["GL"].ToString();
                        TxtAccNo.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Loan A/C not acceepted..Enter valid Product code", this.Page);
                        TxtProcode.Text = "";
                        TxtProcode.Focus();
                        return;
                    }
                }
                else
                {

                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtProcode.Text = "";
                    TxtProcode.Focus();
                }
            }
            else
            {

                WebMsgBox.Show("Please enter valid Product code", this.Page);
                TxtProcode.Text = "";
                //TxtGLCD.Text = "";
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtbnkdname_TextChanged(object sender, EventArgs e)
    {
        string[] TD = txtbnkdname.Text.Split('_');
        if (TD.Length > 1)
        {
            txtbnkdname.Text = TD[0].ToString();
            txtbankcd.Text = TD[1].ToString();
            AutoBranch.ContextKey = txtbankcd.Text;
            txtbrnchcd.Focus();
        }
    }

    protected void txtbrnchcdname_TextChanged(object sender, EventArgs e)
    {
        string[] TD = txtbrnchcdname.Text.Split('_');
        if (TD.Length > 1)
        {
            txtbrnchcdname.Text = TD[0].ToString();
            txtbrnchcd.Text = TD[1].ToString();
            //For Focusing cursor at last character of Textbox
            txtpartic.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtpartic.Focus();
            txtpartic.Text = "By IW Clg";
        }
    }

    public void NewWindowsVerify(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
    }


    protected void LnkVerify_Click(object sender, EventArgs e)
    {
        try
        {

            if (TxtAccNo.Text != "")
            {

                string custno = ViewState["CUSTNO"].ToString();
                string url = "FrmVerifySign.aspx?CUSTNO=" + custno + "";
                NewWindowsVerify(url);
            }
            else
            {
                WebMsgBox.Show("Enter Account number!....", this.Page);
                TxtAccNo.Focus();

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

    public void Calc_TotalBal()
    {
        try
        {
             double NewBal = 0;
             NewBal = (Convert.ToDouble(TxtTotalBal.Text)) - (Convert.ToDouble(txtinstamt.Text));
            TxtTotalBal.Text = Convert.ToString(NewBal);
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtinstamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double BAL, curbal;
            bool Valid = true;
            BAL = curbal = 0;
            string STR2 = "";

           BAL = Convert.ToDouble(TxtBalance.Text);
           // BAL = Convert.ToDouble(TxtTotalBal.Text);
            curbal = Convert.ToDouble(txtinstamt.Text);
            if (Convert.ToDateTime(conn.ConvertDate(txtinstdate.Text)) <= Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
            {

                if (ViewState["GL"].ToString() == "3")
                {
                    STR = OWGCL.GetLoanLimit(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                    STR2 = OWGCL.GetLoanCatDuedate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, "DC");
                    string[] OD = STR2.Split('_');
                    //double LBAL = Convert.ToDouble(STR) - Math.Abs(Convert.ToDouble(TxtBalance.Text));
                    double LBAL = Convert.ToDouble(STR) - Math.Abs(Convert.ToDouble(TxtTotalBal.Text));
                    //if (STR != null && curbal > Convert.ToDouble(STR))
                    if (LBAL < curbal)
                    {
                        //WebMsgBox.Show("Loan A/C Overdrawned by  " + LBAL + ",Return Entry...!", this.Page);
                        lblMessage.Text = "";
                        lblMessage.Text = "Loan A/C Overdrawned of Rs. " + Math.Abs(LBAL) + " ,Return Entry...!";
                        ModalPopup.Show(this.Page);
                        btnSubmit.Enabled = false;
                        BtnPostUnpass.Focus();
                        BtnPostUnpass.Enabled = false;
                        Btn_Return.Enabled = true;
                        btnSubmit.Enabled = false;
                        DIV_RETRN.Visible = true;
                        Valid = false;
                    }
                    if (LBAL > curbal && Convert.ToDateTime(conn.ConvertDate(OD[0].ToString())) < Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))// && Convert.ToDateTime(OD[0].ToString()) < Convert.ToDateTime(Session["EntryDate"].ToString()))
                    {
                        // WebMsgBox.Show("Limit Date Expired.....!", this.Page);
                        lblMessage.Text = "";
                        lblMessage.Text = "Due Date " + OD[0].ToString() + "  Expired , Return Entry!";
                        ModalPopup.Show(this.Page);
                        btnSubmit.Enabled = false;
                        BtnPostUnpass.Focus();
                        BtnPostUnpass.Enabled = false;
                        Btn_Return.Enabled = true;
                        btnSubmit.Enabled = true;
                        DIV_RETRN.Visible = true;
                        Valid = false;
                    }
                }
                if (ViewState["ACCSTATUS"].ToString() == "1")
                {
                    if (BAL < curbal && Convert.ToInt32(TxtProcode.Text) != 177)
                    {
                        string STR3 = OWGCL.GetLoanCatDuedate(Session["BRCD"].ToString(), TxtProcode.Text, "", "C");
                        if (STR3 != "CCOD")
                        {
                            // WebMsgBox.Show("Balance is Insufficient, Entry will remain in UnClear...!", this.Page);
                            lblMessage.Text = "";
                            lblMessage.Text = "Balance is Insufficient, Want to Return or Reject Entry...!";
                            ModalPopup.Show(this.Page);
                            BtnPostUnpass.Focus();
                            BtnPostUnpass.Enabled = true;
                            Btn_Return.Enabled = true;
                            btnSubmit.Enabled = false;
                            DIV_RETRN.Visible = true;
                        }
                        else
                        {
                            lblMessage.Text = "";
                            //lblMessage.Text = "Balance is Insufficient, Want to Return or Reject Entry...!";
                            //ModalPopup.Show(this.Page);
                            btnSubmit.Focus();
                            BtnPostUnpass.Enabled = false;
                            Btn_Return.Enabled = false;
                            btnSubmit.Enabled = true;
                            DIV_RETRN.Visible = false;
                            Calc_TotalBal();
                        }
                    }
                    else if (Valid == true)
                    {
                        btnSubmit.Focus();
                        BtnPostUnpass.Enabled = false;
                        btnSubmit.Enabled = true;
                        Btn_Return.Enabled = false;
                        DIV_RETRN.Visible = false;    //  Commented By amol on 07-10-2017 because reason not show
                        //Btn_Return.Enabled = true;
                        //DIV_RETRN.Visible = true;   //  Commented By Abhishek on 18-10-2017 because reason not show
                        Calc_TotalBal();
                    }
                }
                else
                {
                    DdlReason.Focus();
                    DIV_RETRN.Visible = true;
                }

            }
            else
            {

                BtnPostUnpass.Focus();
                BtnPostUnpass.Enabled = true;
                Btn_Return.Enabled = true;
                btnSubmit.Enabled = false;
                DIV_RETRN.Visible = true;
                DdlReason.Focus();
            }
            //  btnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btmClear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }
    protected void rdbCredit_CheckedChanged(object sender, EventArgs e)
    {
        double AMT = 0;
        AMT = OWGCL.GetAmount(txtsetno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        TxtCLGAmount.Text = Convert.ToString(AMT);
    }
    protected void rdbDebit_CheckedChanged(object sender, EventArgs e)
    {
        double AMT = 0;
        AMT = OWGCL.GetAmount(txtsetno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        TxtCLGAmount.Text = Convert.ToString(AMT);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string srnumId = objlink.CommandArgument;
            ViewState["Flag"] = "DL";
            ViewState["ST"] = srnumId.ToString();
            EditGrid();
            btnSubmit.Text = "Delete";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public void EditGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = AD.GetFormData(Convert.ToInt32(ViewState["ST"].ToString()), 1, Convert.ToInt32(Session["BRCD"]), Session["EntryDate"].ToString(), "I");
            if (dt.Rows.Count > 0)
            {
                TxtEntrydate.Text = Convert.ToDateTime(dt.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
                txtsetno.Text = dt.Rows[0]["SET_NO"].ToString();
                TxtProcode.Text = dt.Rows[0]["PRDUCT_CODE"].ToString();
                TxtProName.Text = dt.Rows[0]["PRDNAME"].ToString();
                TxtAccNo.Text = dt.Rows[0]["ACC_NO"].ToString();
                TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                txtAccTypeid.Text = dt.Rows[0]["ACC_TYPE"].ToString();
                TxtAccTypeName.Text = dt.Rows[0]["ACCTYPEA"].ToString();
                txtOpTypeId.Text = dt.Rows[0]["OPRTN_TYPE"].ToString();
                TxtOpTypeName.Text = dt.Rows[0]["OPRTYPE"].ToString();
                txtpartic.Text = dt.Rows[0]["PARTICULARS"].ToString();
                txtbankcd.Text = dt.Rows[0]["BANK_CODE"].ToString();
                txtbnkdname.Text = dt.Rows[0]["BANK"].ToString();
                txtbrnchcd.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
                txtbrnchcdname.Text = dt.Rows[0]["BRANCH"].ToString();
                txtinstno.Text = dt.Rows[0]["INSTRU_NO"].ToString();
                txtinstdate.Text = Convert.ToDateTime(dt.Rows[0]["INSTRUDATE"]).ToString("dd/MM/yyyy");
                txtinstamt.Text = dt.Rows[0]["INSTRU_AMOUNT"].ToString();
                if (ViewState["Flag"].ToString() == "DL")
                    ENDNED(false);
                else
                    ModENDN(true);

                TblDiv_MainWindow.Visible = true;
            }
            else
            {
                WebMsgBox.Show("Only Created Records are available to Modify/Delete....", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ModENDN(bool TF)
    {
        TxtEntrydate.Enabled = !TF;
        txtsetno.Enabled = !TF;
        TxtProcode.Enabled = !TF;
        TxtProName.Enabled = !TF;
        TxtAccNo.Enabled = !TF;
        TxtAccName.Enabled = !TF;
        txtAccTypeid.Enabled = !TF;
        TxtAccTypeName.Enabled = !TF;
        txtOpTypeId.Enabled = !TF;
        TxtOpTypeName.Enabled = !TF;
        txtpartic.Enabled = TF;
        txtbankcd.Enabled = TF;
        txtbnkdname.Enabled = TF;
        txtbrnchcd.Enabled = TF;
        txtbrnchcdname.Enabled = TF;
        txtinstno.Enabled = TF;
        txtinstdate.Enabled = TF;
        txtinstamt.Enabled = TF;

    }
    public void ENDNED(bool TF)
    {
        TxtEntrydate.Enabled = TF;
        txtsetno.Enabled = TF;
        TxtProcode.Enabled = TF;
        TxtProName.Enabled = TF;
        TxtAccNo.Enabled = TF;
        TxtAccName.Enabled = TF;
        txtAccTypeid.Enabled = TF;
        TxtAccTypeName.Enabled = TF;
        txtOpTypeId.Enabled = TF;
        TxtOpTypeName.Enabled = TF;
        txtpartic.Enabled = TF;
        txtbankcd.Enabled = !TF;
        txtbnkdname.Enabled = !TF;
        txtbrnchcd.Enabled = !TF;
        txtbrnchcdname.Enabled = !TF;
        txtinstno.Enabled = !TF;
        txtinstdate.Enabled = !TF;
        txtinstamt.Enabled = !TF;
    }
    protected void BtnPostUnpass_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime Test;
            if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            {

                string BRCD = Session["BRCD"].ToString();
                string PACMAC = conn.PCNAME(); ///System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
                //Get values from controls
                string Entrydate = TxtEntrydate.Text.ToString();
                string Procode = TxtProcode.Text.ToString();
                string AccNo = TxtAccNo.Text.ToString();
                string AccTypeid = txtAccTypeid.Text.ToString();
                string OpTypeId = txtOpTypeId.Text.ToString();
                string partic = txtpartic.Text.ToString();
                string bankcd = txtbankcd.Text.ToString();
                string brnchcd = txtbrnchcd.Text.ToString();
                // string insttype = ddlinsttype.SelectedValue.ToString();
                string instdate = txtinstdate.Text.ToString();
                string instno = txtinstno.Text.ToString();
                string instamt = txtinstamt.Text.ToString();
                //        


                int CLG_GL_NO = Convert.ToInt32(ddlBankName.SelectedValue);

                if (rdbCredit.Checked == true)
                {
                    if (ddlBankName.SelectedIndex == 0)
                    {
                        WebMsgBox.Show("Select Bank Name!......", this.Page);
                        ddlBankName.Focus();
                    }
                }

                if (ViewState["MultiEntryFlag"].ToString() == "0")
                {

                    string setno = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString());
                    int ScrollNo = 1;
                    int RC = 0;
                    RC = OWGCL.InsertUnPass(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, instamt, Session["MID"].ToString(), PACMAC, Convert.ToInt32(setno), ScrollNo, CLG_GL_NO, "D", "1001");
                    if (RC > 0)
                    {

                        BindGrid();
                        
                        //WebMsgBox.Show("Record Submitted with Inward Clearing : " + setno, this.Page);                    
                        lblMessage.Text = "Record Submitted with Inward Clearing : " + setno;
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Del _" + setno + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        TxtProcode.Focus();
                        Cleardata();
                        return;
                    }
                    else if (ViewState["MultiEntryFlag"].ToString() == "1")
                    {
                        int SetNO = Convert.ToInt32(txtsetno.Text);
                        int SCRLNO = OWGCL.GetNewScrollNo(SetNO, Session["BRCD"].ToString());
                        //MAK CHANGE insttype="0"
                        RC = 0;
                        if (rdbDebit.Checked == true)
                        {
                            RC = OWGCL.InsertUnPass(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, instamt, Session["MID"].ToString(), PACMAC, SetNO, SCRLNO, CLG_GL_NO, "D", "1001");
                        }
                        else
                        {
                            RC = OWGCL.InsertUnPass(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, "0", instdate, instno, TxtCLGAmount.Text, Session["MID"].ToString(), PACMAC, SetNO, SCRLNO, CLG_GL_NO, "C", "1001");
                        }
                        int setnonew = OWGCL.GetcurrentSetNo(BRCD);
                        ViewState["ScrollNo"] = "1";
                        ViewState["SetNo"] = setnonew;
                        BindGrid();
                        Cleardata();
                        txtsetno.Text = setnonew.ToString();
                        if (RC > 0)
                        {
                            WebMsgBox.Show("Record Saved Successfully", this.Page);
                            TxtProcode.Focus();
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Add _" + Procode + "_" + AccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            //ddlinsttype.Focus();
                            return;
                        }
                    }
                }
                else if (ViewState["Flag"].ToString() == "MD")
                {
                    int RC = CLSOW.UdateValues(ViewState["ST"].ToString(), txtpartic.Text, txtinstamt.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), txtbankcd.Text, txtbrnchcd.Text, txtinstno.Text, txtinstdate.Text, Session["EntryDate"].ToString(), "O");
                    if (RC > 0)
                    {
                        BindGrid();
                        lblMessage.Text = "Record Modify Successfully.......!!";
                        
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Md _" + Procode + "_" + AccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
                else if (ViewState["Flag"].ToString() == "DL")
                {
                    int RC = CLSOW.DeleteValues(ViewState["ST"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "O", "UNPASS",Session["MID"].ToString());
                    if (RC > 0)
                    {
                        BindGrid();
                        lblMessage.Text = "Record Delete Successfully.......!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Del _" + Procode + "_" + AccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Cleardata();
                    }
                }

            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Enter Valid date ...!";
                ModalPopup.Show(this.Page);
                txtinstdate.Text = "";
                txtinstdate.Focus();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtinstdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime temp;
            //if (DateTime.TryParse(txtinstdate.Text, out temp))
            //{
            if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out temp))
            {

                //string STR = OWGCL.InstDateDiff(Session["EntryDate"].ToString(), txtinstdate.Text);
                string STR = OWGCL.InstMonthDiff(Session["EntryDate"].ToString(), txtinstdate.Text);
                if (Convert.ToInt32(STR) >= 3)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Cheque no. " + txtinstno.Text + " date validity expired....!";
                    ModalPopup.Show(this.Page);
                    txtinstdate.Text = "";
                    return;
                }
                if (Convert.ToDateTime(conn.ConvertDate(txtinstdate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Instrument Post Dated, Want to Return or Reject Entry...!";
                    ModalPopup.Show(this.Page);
                    BtnPostUnpass.Focus();
                    BtnPostUnpass.Enabled = true;
                    Btn_Return.Enabled = true;
                    btnSubmit.Enabled = false;
                    DIV_RETRN.Visible = true;
                }
                else
                {

                    BtnPostUnpass.Enabled = false;
                    Btn_Return.Enabled = false;
                    btnSubmit.Enabled = true;
                    DIV_RETRN.Visible = false;
                    txtinstamt.Focus();

                }
            }
            else
            {
                WebMsgBox.Show("Invalid Instrument date....!", this.Page);
                txtinstdate.Text = "";
                txtinstdate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Return_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime Test;
            if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                if (DdlReason.SelectedValue != "0")
                {
                    string TRXTYPE = "", PMTMD = "", ACT = "", PAYMAST = "", CD = "", NARR = ""; ;
                    string SetN = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString()).ToString();
                    int SETNO = Convert.ToInt32(SetN);


                    TRXTYPE = "1";
                    PMTMD = "IC";
                    ACT = "31";
                    PAYMAST = "IC";
                    CD = "C";
                    NARR = txtpartic.Text + "IW R " + TxtProcode.Text + " / " + TxtAccNo.Text;



                    int RESULT = RI.InOutReturn("1", Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, NARR.ToString(), txtinstamt.Text, TRXTYPE.ToString(), ACT.ToString(), SETNO.ToString(), PMTMD.ToString(), txtinstno.Text, txtinstdate.Text, txtbankcd.Text, txtbrnchcd.Text, "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PAYMAST.ToString(), ViewState["CUSTNO"].ToString(), TxtAccName.Text, "3", "", CD.ToString(), DdlReason.SelectedValue, "ER");
                    if (RESULT > 0)
                    {
                        lblMessage.Text = " Return Voucher Number " + SETNO;
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Return _" + SETNO + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Cleardata();
                        BindGrid();

                    }
                }
                else
                {
                    WebMsgBox.Show("Select Reason For Returning the Check......!", this.Page);
                    DdlReason.Focus();
                }
            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Enter Valid date ...!";
                ModalPopup.Show(this.Page);
                txtinstdate.Text = "";
                txtinstdate.Focus();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void grdCharged_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Amount")).Text) ? "0" : ((Label)e.Row.FindControl("Amount")).Text;
                if (Amt != "0")
                    NumOfInst++;
                TotalValue = Convert.ToDouble(Amt);
                SumFooterValue += TotalValue;
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("Lbl_total");
                lbl.Text = SumFooterValue.ToString();
                Label Lbl_NumofI = (Label)e.Row.FindControl("Lbl_Numofnst");
                Lbl_NumofI.Text = "No. of Inst = " + NumOfInst;


            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtinstno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int ChqLen = 0;
            string Chqno_Text = "";
            Chqno_Text = txtinstno.Text;
            ChqLen = Chqno_Text.Length;
            if (ChqLen > 6 || ChqLen < 6)
            {
                WebMsgBox.Show("Enter 6 digit Instrument number ....!!", this.Page);
                txtinstno.Text = "";
                txtinstno.Focus();
            }
            else
            {
                // txtinstdate.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                //txtinstdate.Focus();
                txtinstdate.Focus();
                if (txtinstdate.Text == "")
                    txtinstdate.Text = Session["EntryDate"].ToString();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string srnumId = objlink.CommandArgument;
            ViewState["Flag"] = "MD";
            ViewState["ST"] = srnumId.ToString();
            btnSubmit.Text = "Modify";
            EditGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Photo_Sign()
    {
        try
        {
            string FileName = "";
            DataTable dt = CM.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = dt.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = dt.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["PhotoImg"];
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

    public void Details_Section()
    {
        try
        {
            DataTable DT = null;


            //Loan Details
            if (ViewState["GL"].ToString() == "3")
            {
                DT = CM.LoanDetails("DETAILS", Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    //Id	Limit	SancDate	LIntDate	DueDate	Intrate	InstAmt	NInstDate	SumIWC	SumIWCR	SumIWCU	SumOWC	SumOWCR	SumOWCU
                    TxtLLimit.Text = DT.Rows[0]["Limit"].ToString();
                    TxtLSancDt.Text = DT.Rows[0]["SancDate"].ToString();
                    TxtLLastIntDate.Text = DT.Rows[0]["LIntDate"].ToString();
                    TxtLDueDt.Text = DT.Rows[0]["DueDate"].ToString();
                    TxtLIntRate.Text = DT.Rows[0]["Intrate"].ToString();
                    TxtLInstAmt.Text = DT.Rows[0]["InstAmt"].ToString();

                }
            }
            // DT.Clear();

            //Details filling section

            SumIwOw();
            Photo_Sign();



        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void SumIwOw()
    {
        try
        {
            DataTable DT;
            DT = CM.IWOWSum("DETAILS", "CDETAILS", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtIwcTotal.Text = DT.Rows[0]["SumIWC"].ToString();
                TxtIwcRTotal.Text = DT.Rows[0]["SumIWCR"].ToString();
                TxtIWCUTotal.Text = DT.Rows[0]["SumIWCU"].ToString();

                TxtOwcTotal.Text = DT.Rows[0]["SumOWC"].ToString();
                TxtOwcRTotal.Text = DT.Rows[0]["SumOWCR"].ToString();
                TxtOWCUTotal.Text = DT.Rows[0]["SumOWCU"].ToString();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


}