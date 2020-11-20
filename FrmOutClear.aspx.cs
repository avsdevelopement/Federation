using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class FrmOutClear : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    scustom customcs = new scustom();
    ClsInwordClear IWCL = new ClsInwordClear();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOWGCharges OWG = new ClsOWGCharges();
    ClsOutClear OWGCL = new ClsOutClear();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsOpenClose OC = new ClsOpenClose();
    ClsOutAuthDo AD = new ClsOutAuthDo();
    DataTable DT = new DataTable();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsCommon CMM = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsEncryptValue Encry = new ClsEncryptValue();
    string FL = "";
    int result = 0;
    public static string Flag;
    string sqlnewsetno = "1";
    string newscrlno = "1";
    string newsetno = "1";
    string Res = "";
    double SumFooterValue = 0;
    double TotalValue = 0;
    int NumOfInst = 0;
    string AC_Status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            customcs.BindInstruType(ddlinsttype);
            //mak 22/01/17 changed 
            //customcs.BindBankName(ddlBankName);
            TxtEntrydate.Text = Session["EntryDate"].ToString();
            txtinstdate.Text = Session["EntryDate"].ToString();
            string smid = Session["MID"].ToString();
            string sBRCD = Session["BRCD"].ToString();
            ViewState["MultiEntryFlag"] = "0";
            BindGrid();
            Flag = "1";
            ViewState["Status"] = "new";
            //mak 22/01/17 changed
            //ddlBankName.Focus();
            autoglname.ContextKey = Session["BRCD"].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString();
            ddlinsttype.SelectedIndex = 3;
            //LnkVerify.Visible = false;
            ViewState["Flag"] = "AD";
            btnSubmit.Text = "Submit";
            TxtProcode.Focus();




            int Ip = GetMultipleInfo();
            if (Ip == 1)
            {
                CallMultiple();
                BindMultipleGrid();
                Div_Grd_Single.Visible = false;
                Div_Grd_Mul.Visible = true;
            }


            if (TxtDrAmt.Text != "" && TxtDrAmt.Text != null)
            {
                TxtDrAmt.Enabled = false;
                Div_Multi.Visible = true;
                Div_Grd_Single.Visible = false;
                BindMultipleGrid();
            }
            else
            {
                Div_Multi.Visible = false;
                Btn_AddMultiple.Visible = false;
                Btn_PostMultiple.Visible = false;
            }
            TblDiv_MainWindow.Visible = false;

        }
        // ddlBankName.Focus();
    }

    public void CallMultiple()
    {
        try
        {
            TxtDrAmt.Focus();
            txtsetno.ReadOnly = false;
            btnSubmit.Visible = false;
            Btn_AddDebit.Visible = true;
            Div_Multi.Visible = true;
            Div_Grd_Single.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public int GetMultipleInfo()
    {
        int Out = 0;
        try
        {
            DT = AD.GetCRDR("DRCR", Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
            if (Convert.ToDouble(DT.Rows[0]["Debit"].ToString()) != 0)
            {
                TxtDrAmt.Text = DT.Rows[0]["Debit"].ToString();
                TxtCrAmt.Text = DT.Rows[0]["Credit"].ToString();
                TxtDifference.Text = (Convert.ToInt32(DT.Rows[0]["Debit"]) - Convert.ToInt32(DT.Rows[0]["Credit"])).ToString();
                TxtDrAmt.Enabled = false;
                Out = 1;

                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(TxtCrAmt.Text);
                DR = Convert.ToDouble(TxtDrAmt.Text);

                TxtDifference.Text = (CR - DR).ToString();

                if (CR == DR)
                {
                    Btn_PostMultiple.Enabled = true;
                    Btn_PostMultiple.Focus();
                }
                else
                {
                    Btn_PostMultiple.Enabled = false;
                    TxtProcode.Focus();
                }
            }
            else
                Out = 0;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Out;
    }

    public void BindMultipleGrid()
    {
        try
        {
            int RR = AD.GetMultipleGrid(GrdMul, "BINDMUL", Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
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
            DateTime Test;
            if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                string BRCD = Session["BRCD"].ToString();
                string PACMAC = conn.PCNAME();
                //Get values from controls
                string Entrydate = TxtEntrydate.Text.ToString();
                string Procode = TxtProcode.Text.ToString();
                string AccNo = TxtAccNo.Text.ToString();
                string AccTypeid = txtAccTypeid.Text.ToString();
                string OpTypeId = txtOpTypeId.Text.ToString();
                string partic = txtpartic.Text.ToString();
                string bankcd = txtbankcd.Text.ToString();
                string brnchcd = txtbrnchcd.Text.ToString();
                string insttype = ddlinsttype.SelectedValue.ToString();
                string instdate = txtinstdate.Text.ToString();
                string instno = txtinstno.Text.ToString();
                string instamt = txtinstamt.Text.ToString();
                //        

                // Get CLG_GL_NO 
                //int CLG_GL_NO =OWGCL.Get_CLG_GL_NO(BRCD);
                int CLG_GL_NO = 0;// Convert.ToInt32(ddlBankName.SelectedValue); //mak 22/01/17 changed


                int RC = 0;
                string SetNo = "";

                if (ViewState["Flag"].ToString() == "AD")
                {
                    if (ViewState["MultiEntryFlag"].ToString() == "0")
                    {
                        if (ViewState["GL"].ToString() != "3")
                        {
                            int ScrollNo = 1;
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString()).ToString();
                            int setnonew = Convert.ToInt32(SetNo);
                            RC = OWGCL.InsertNewSetNo(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, insttype, instdate, instno, instamt, Session["MID"].ToString(), PACMAC, Convert.ToInt32(SetNo), ScrollNo, "0", "C", "O", "1001", Dll_Session.SelectedValue);

                        }
                        else
                        {

                            string OwRes = OW_LoanEntries();
                            if (OwRes != null)
                            {
                                RC = Convert.ToInt32(OwRes);
                                SetNo = OwRes;
                            }
                            else
                            {
                                RC = 0;
                            }

                        }
                        if (RC > 0)
                        {

                            BindGrid();

                            WebMsgBox.Show("Record Submitted with Outward Clearing : " + SetNo + "", this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Add _" + SetNo + "_" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            SumIwOw();
                            Cleardata();
                            //lblMessage.Text = "Record Submitted with Outward Clearing : " + SetNo;
                            // ModalPopup.Show(this.Page);
                            TxtProcode.Focus();
                            return;
                        }
                    }

                    else if (ViewState["MultiEntryFlag"].ToString() == "1")
                    {
                        int setno = Convert.ToInt32(txtsetno.Text);
                        int ScrollNo = OWGCL.GetNewScrollNo(setno, Session["BRCD"].ToString(), Session["Entrydate"].ToString());
                        OWGCL.InsertNewSetNo(ViewState["GL"].ToString(), Entrydate, BRCD, Procode, AccNo, AccTypeid, OpTypeId, partic, bankcd, brnchcd, insttype, instdate, instno, instamt, Session["MID"].ToString(), PACMAC, setno, ScrollNo, CLG_GL_NO.ToString(), "C", "O", "1001", Dll_Session.SelectedValue);
                        int setnonew = OWGCL.GetcurrentSetNo(BRCD, Session["Entrydate"].ToString());
                        ViewState["ScrollNo"] = "1";
                        ViewState["SetNo"] = setnonew;
                        BindGrid();

                        txtsetno.Text = setnonew.ToString();
                        WebMsgBox.Show("Record Saved Successfully", this.Page);
                        SumIwOw();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Add _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        //lblMessage.Text = "Record Saved Successfully ";
                        // ModalPopup.Show(this.Page);
                        ddlinsttype.Focus();
                        Cleardata();
                        return;
                    }
                }
                else if (ViewState["Flag"].ToString() == "MD")
                {
                    RC = OWGCL.UdateValues(ViewState["ST"].ToString(), txtpartic.Text, txtinstamt.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), txtbankcd.Text, txtbrnchcd.Text, txtinstno.Text, txtinstdate.Text, Session["EntryDate"].ToString(), "O");
                    if (RC > 0)
                    {
                        BindGrid();

                        WebMsgBox.Show("Record Modify Successfully.......!!", this.Page);
                        SumIwOw();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Mod _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        //lblMessage.Text = "Record Modify Successfully.......!!";
                        //ModalPopup.Show(this.Page);
                        Cleardata();
                    }
                }
                else if (ViewState["Flag"].ToString() == "DL")
                {
                    RC = OWGCL.DeleteValues(ViewState["ST"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "O", "PASS", Session["MID"].ToString());
                    if (RC > 0)
                    {


                        WebMsgBox.Show("Record Delete Successfully.......!!", this.Page);
                        SumIwOw();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Del _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        //lblMessage.Text = "Record Delete Successfully.......!!";
                        //ModalPopup.Show(this.Page);
                        BindGrid();
                        Cleardata();
                    }
                }
                //}
                //else
                //{
                //    lblMessage.Text = "";
                //    lblMessage.Text = "Invalid date ...!";
                //    ModalPopup.Show(this.Page);
                //    txtinstdate.Text = "";
                //    txtinstdate.Focus();

                //}

            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Enter Valid Date ...!";
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
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
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
        string GL1;
        GL1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
        if (GL1 != null)
        {
            string[] GLD = GL1.Split('_');
            if (GLD.Length > 1)
            {
                TxtProName.Text = GLD[0].ToString();
                ViewState["GL"] = GLD[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["GL"].ToString();
                TxtAccNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Please enter valid Product code", this.Page);
                TxtProcode.Text = "";
                //TxtGLCD.Text = "";
                TxtProcode.Focus();
            }
        }
        else
        {
            WebMsgBox.Show("Enter Valid Product code!....", this.Page);
            // TxtGLCD.Text = "";
            TxtProcode.Text = "";
            TxtProcode.Focus();
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
                ddlinsttype.Focus();
                goto ext;
            }

            string AT = "";
            AC_Status = CMM.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (AC_Status == "1")
            {
                DataTable dt1 = new DataTable();
                string[] CU;
                string CA = "", STR = "";
                CA = customcs.GetAccountName(TxtAccNo.Text.ToString(), TxtProcode.Text.ToString(), Session["BRCD"].ToString());
                if (CA != null)
                {

                    if (TxtAccNo.Text != "" & TxtProcode.Text != "")
                    {



                        CU = CA.Split('_');
                        ViewState["CUSTNO"] = CU[0].ToString();
                        TxtAccName.Text = CU[1].ToString();
                        TxtAccNo.Text = CU[2].ToString();
                        STR = BD.GetACCSTS(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                        if (STR != null)
                        {

                            CU = STR.Split('_');
                            TxtACCStatus.Text = CU[0].ToString();


                        }
                        else
                        {
                            TxtACCStatus.Text = "";
                        }
                        if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                        {
                            WebMsgBox.Show("Please enter valid Account number", this.Page);
                            TxtAccNo.Text = "";
                            TxtAccNo.Focus();
                            return;
                        }
                        //LnkVerify.Visible = true;
                    }

                    if (TxtAccNo.Text == "" || TxtProcode.Text == "")
                    {
                        TxtAccName.Text = "";
                        txtAccTypeid.Text = "";
                        TxtAccTypeName.Text = "";
                        txtOpTypeId.Text = "";
                        TxtOpTypeName.Text = "";
                        ddlinsttype.Focus();
                        goto ext;
                    }
                    dt1 = customcs.GetAccNoAccType(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!....", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }


                if (dt1 != null && dt1.Rows.Count != 0)
                {
                    TxtAccTypeName.Text = dt1.Rows[0]["ACCTYPE"].ToString();
                    txtAccTypeid.Text = dt1.Rows[0]["ACC_TYPE"].ToString();
                    txtOpTypeId.Text = dt1.Rows[0]["OPR_TYPE"].ToString();
                    TxtOpTypeName.Text = dt1.Rows[0]["OPRTYPE"].ToString();
                    ddlinsttype.Focus();
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                    TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                    txtbankcd.Focus();
                }
                else
                {
                    txtAccTypeid.Text = "";
                    TxtAccTypeName.Text = "";
                    txtOpTypeId.Text = "";
                    TxtOpTypeName.Text = "";
                    ddlinsttype.Focus();
                }

                //Fill Details Section
                Details_Section();
            }
            else if (AC_Status == "2")
            {

                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is In-operative.........!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();


            }
            else if (AC_Status == "3")
            {

                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed.........!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();


            }
            else if (AC_Status == "4")
            {

                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();

            }
            else if (AC_Status == "6")
            {

                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed.........!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();

            }
            else if (AC_Status == "7")
            {

                WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Total Freezed.........!!", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();

            }
            else
            {
                WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
            }
        ext: ;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
            txtpartic.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtpartic.Focus();
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
        int RS = OWGCL.BindClearing(grdCharged, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
    }

    protected void grdCharged_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCharged.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void txtsetno_TextChanged(object sender, EventArgs e)
    {

        //if (ViewState["MultiEntryFlag"].ToString() == "1" || txtsetno.Text == "")
        //{
        //    WebMsgBox.Show("Please enter Set No", this.Page);
        //    TxtProcode.Focus();
        //    return;
        //}
        if (txtsetno.Text == "")
        {
            WebMsgBox.Show("Please enter Set No", this.Page);
            TxtProcode.Focus();
            return;
        }

        // Bind data of Set no which user inserted

        // End Bind Data 

        DataTable DT = new DataTable();
        DT = AD.GetSetDetails(Session["EntryDate"].ToString(), txtsetno.Text);


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
            TxtOpTypeName.Text = DT.Rows[0]["OPRTYPE"].ToString();
            txtpartic.Text = DT.Rows[0]["PARTICULARS"].ToString();
            if (ViewState["MultiEntryFlag"].ToString() == "2")
            {
                ddlinsttype.SelectedValue = DT.Rows[0]["INSTRU_TYPE"].ToString();
                txtbankcd.Text = DT.Rows[0]["BANK_CODE"].ToString();
                txtbrnchcd.Text = DT.Rows[0]["BRANCH_CODE"].ToString();
                txtinstno.Text = DT.Rows[0]["INSTRU_NO"].ToString();
                txtinstdate.Text = Convert.ToDateTime(DT.Rows[0]["INSTRUDATE"]).ToString("dd/MM/yyyy");
                txtinstamt.Text = DT.Rows[0]["INSTRU_Amount"].ToString();
                txtbrnchcdname.Text = DT.Rows[0]["BRANCH"].ToString();
                txtbnkdname.Text = DT.Rows[0]["BANK"].ToString();
            }
            ddlinsttype.Focus();
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
        result = OWGCL.DeleteOwgClearingEntry(Convert.ToInt32(txtsetno.Text), Session["BRCD"].ToString(), Session["Entrydate"].ToString());
        BindGrid();
        if (result == -1)
        {
            WebMsgBox.Show("Something went wrong.....!!!", this.Page);
        }
        else
        {
            WebMsgBox.Show("Record Deleted successfully.....!!!", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Del _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        Cleardata();
    }

    public void Cleardata()
    {
        if (ViewState["MultiEntryFlag"].ToString() == "1" || ViewState["MultiEntryFlag"].ToString() == "2")
        {
            txtbankcd.Text = "";
            txtbrnchcd.Text = "";
            txtinstno.Text = "";
            txtinstamt.Text = "";
            txtbrnchcdname.Text = "";
            txtbankcd.Text = "";
            txtbrnchcd.Text = "";
            txtbnkdname.Text = "";
            txtinstamt.Text = "";
            // TxtGLCD.Text = "";
            txtBalance.Text = "";
            TxtTotalBal.Text = "";
            //ddlBankName.Focus(); //mak 22/01/2017
            TxtLDueDt.Text = "";
            TxtLInstAmt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            TxtProcode.Focus();
            TxtLInstAmt.Text = "";
            TxtLDueDt.Text = "";
            TxtLIntRate.Text = "";
            TxtLLastIntDate.Text = "";
            TxtLLimit.Text = "";
            TxtLSancDt.Text = "";
            Img1.Src = null;
            Img2.Src = null;

        }
        if (ViewState["MultiEntryFlag"].ToString() == "0" || ViewState["MultiEntryFlag"].ToString() == "2")
        {
            //ddlinsttype.SelectedIndex = 0;
            txtBalance.Text = "";
            //TxtGLCD.Text = "";
            txtinstamt.Text = "";
            TxtProcode.Text = "";
            TxtProName.Text = "";
            TxtAccName.Text = "";
            TxtAccNo.Text = "";
            txtAccTypeid.Text = "";
            TxtAccTypeName.Text = "";
            txtOpTypeId.Text = "";
            TxtOpTypeName.Text = "";
            txtpartic.Text = "By OWC Clg";
            txtsetno.Text = "";
            txtbankcd.Text = "";
            txtbrnchcd.Text = "";
            txtinstno.Text = "";
            txtinstdate.Text = Session["EntryDate"].ToString();
            txtbnkdname.Text = "";
            txtbrnchcdname.Text = "";
            ddlinsttype.SelectedIndex = 3;
            //ddlBankName.SelectedIndex = 0;
            TxtTotalBal.Text = "";
            //ddlBankName.Focus();
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
            TblDiv_MainWindow.Visible = true;
            lblstatus.Text = "New Entry";
            ViewState["Status"] = "new";
            ViewState["Flag"] = "AD";
            ViewState["MultiEntryFlag"] = 0;
            btnSubmit.Text = "Submit";
            ENDN(true);
            txtsetno.Text = "";
            txtsetno.ReadOnly = true;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            Btn_AddDebit.Visible = false;
            Btn_AddMultiple.Visible = false;
            Btn_PostMultiple.Visible = false;
            BindGrid();
            //ddlBankName.Focus(); //mak 22/01/17 
            Flag = "1";
            Cleardata();
            Div_Grd_Single.Visible = true;
            Div_Multi.Visible = false;
            Div_Grd_Mul.Visible = false;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        //Flag = "1";
        //lblstatus.Text = "Entry to selected Set";
        //ViewState["Status"] = "multiple";
        //ViewState["Flag"] = "AD";
        //txtsetno.Focus();

        //ViewState["MultiEntryFlag"] = 1;
        //txtsetno.Text = "";
        //btnUpdate.Visible = false;
        //btnSubmit.Visible = true;
        //txtsetno.ReadOnly = false;
        //Cleardata();
        //txtsetno.Focus();

        try
        {
            TblDiv_MainWindow.Visible = true;
            CallMultiple();
            TxtDrAmt.Enabled = true;
            Btn_AddMultiple.Visible = true;
            Btn_PostMultiple.Visible = true;


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
        OWGCL.DeleteData(Session["Entrydate"].ToString(), Session["BRCD"].ToString());
        OWGCL.ReportData(Session["Entrydate"].ToString(), Session["Entrydate"].ToString());
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
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CustName = TxtAccName.Text;
            string[] CN = CustName.Split('_');

            if (CN.Length > 1)
            {
                TxtAccName.Text = CN[0].ToString();
                TxtAccNo.Text = CN[1].ToString();
                ViewState["CUSTNO"] = CN[2].ToString();
                string AT = "";
                AC_Status = CMM.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                if (AC_Status == "1")
                {

                    int RC = LI.CheckAccount(TxtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                    if (RC < 0)
                    {
                        TxtAccNo.Focus();
                        WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                        return;
                    }
                    ViewState["CustNo"] = RC;
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                    TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                    ddlinsttype.Focus();
                    //LnkVerify.Visible = true;
                }
                else if (AC_Status == "2")
                {

                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is In-operative.........!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();


                }
                else if (AC_Status == "3")
                {

                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed.........!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();


                }
                else if (AC_Status == "4")
                {

                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();

                }
                else if (AC_Status == "6")
                {

                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Closed.........!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();

                }
                else if (AC_Status == "7")
                {

                    WebMsgBox.Show("Acc number " + TxtAccNo.Text + " is Total Freezed.........!!", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();

                }

            }

            //Fill Details Section
            Details_Section();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        string TD = TxtProName.Text;
        string[] TDD = TD.Split('_');
        if (TDD.Length > 1)
        {
            TxtProName.Text = TDD[0].ToString();
            TxtProcode.Text = TDD[1].ToString();
            string[] GLD = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString()).Split('_');
            TxtProName.Text = GLD[0].ToString();
            ViewState["GL"] = GLD[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text;// +"_" + ViewState["GL"].ToString();
            TxtAccNo.Focus();
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
            txtpartic.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtpartic.Focus();
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
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }
    //protected void lnkEdit_Click(object sender, EventArgs e)
    //{
    //    LinkButton objlink = (LinkButton)sender;
    //    string srnumId = objlink.CommandArgument;
    //    ViewState["Flag"] = "MD";
    //    ViewState["ST"] = srnumId.ToString();
    //    btnSubmit.Text = "Modify";
    //    EditGrid();
    //}
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string srnumId = objlink.CommandArgument;
            ViewState["Flag"] = "DL";
            ViewState["ST"] = srnumId.ToString();
            btnSubmit.Text = "Delete";
            EditGrid();
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
            dt = AD.GetFormData(Convert.ToInt32(ViewState["ST"].ToString()), 1, Convert.ToInt32(Session["BRCD"]), Session["EntryDate"].ToString(), "O");
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
                ddlinsttype.SelectedValue = dt.Rows[0]["INSTRU_TYPE"].ToString();
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
        ddlinsttype.Enabled = TF;
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
        //  ddlinsttype.Enabled = TF;
        txtbankcd.Enabled = !TF;
        txtbnkdname.Enabled = !TF;
        txtbrnchcd.Enabled = !TF;
        txtbrnchcdname.Enabled = !TF;
        txtinstno.Enabled = !TF;
        txtinstdate.Enabled = !TF;
        txtinstamt.Enabled = !TF;
    }
    protected void txtinstdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime temp;
            //if (DateTime.TryParse(txtinstdate.Text, out temp))
            if (DateTime.TryParseExact(txtinstdate.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out temp))
            {
                string STR = IWCL.InstMonthDiff(Session["EntryDate"].ToString(), txtinstdate.Text);
                if (Convert.ToInt32(STR) >= 3)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Cheque no. " + txtinstno.Text + " date validity expired....!";
                    ModalPopup.Show(this.Page);
                    txtinstdate.Text = "";
                    return;
                }
                else
                    txtinstamt.Focus();

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
    protected void Btn_AddMultiple_Click(object sender, EventArgs e) //Down Add button
    {
        try
        {
            //ADDM Flag for Multiple outward entry
            int Res = AD.InsertOwMultiple("ADDM", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["CUSTNO"].ToString(), TxtAccName.Text, ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, txtpartic.Text, "OWC-M", txtinstamt.Text, "1", "1", ddlinsttype.SelectedValue.ToString(), txtbankcd.Text, txtbrnchcd.Text, txtinstno.Text, txtinstdate.Text, Session["MID"].ToString());
            if (Res > 0)
            {
                WebMsgBox.Show("Outward Multiple entry submitted successfully...", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Multiple_Add _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                DT = AD.GetCRDR("DRCR", Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                if (DT.Rows.Count > 0)
                {
                    GetMultipleInfo();
                    Cleardata();
                    CallMultiple();
                    BindMultipleGrid();
                    TxtProcode.Focus();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_AddDebit_Click(object sender, EventArgs e) //Uppper add debit button
    {
        try
        {
            double Dramt = Convert.ToDouble(TxtDrAmt.Text);
            double DiffAmt = Convert.ToDouble(string.IsNullOrEmpty(TxtDifference.Text) ? "-1" : TxtDifference.Text);
            if (Dramt != 0 && DiffAmt == -1)
            {
                int Res = AD.InsertOwMultiple("ADDM", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "0", "N/A", "504", "504", "0", "OWC-M", "OWC-M", TxtDrAmt.Text, "2", "1", "0", "0", "0", "0", "01-01-1990", Session["MID"].ToString());
                if (Res > 0)
                {
                    WebMsgBox.Show("Outward Multiple entry submitted successfully...", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Multiple_Add _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    DT = AD.GetCRDR("DRCR", Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        GetMultipleInfo();

                        Cleardata();
                        CallMultiple();
                        BindMultipleGrid();
                        TxtProcode.Focus();
                    }

                }
            }
            else
            {

                WebMsgBox.Show("Multiple Entry already debited...", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_PostMultiple_Click(object sender, EventArgs e)
    {
        try
        {
            string Setno = AD.PostOw_Multiple("POST", Session["EntryDate"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString());
            if (Setno != null)
            {
                lblMessage.Text = "Record Submitted with Outward Clearing : " + Setno;
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Multiple_Post _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                BindMultipleGrid();
                TxtProcode.Focus();
                Btn_AddDebit.Visible = false;
                Btn_AddMultiple.Visible = false;
                Btn_PostMultiple.Visible = false;
                TxtDrAmt.Text = "";
                TxtCrAmt.Text = "";
                TxtDifference.Text = "";
                TxtDrAmt.Focus();
                //HttpContext.Current.Response.Redirect("FrmOutClear.aspx", true);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDrAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Btn_AddDebit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string Iden = objlink.CommandArgument;

            int Outp = AD.Delete_Multiple("DEL", Session["BRCD"].ToString(), Iden, Session["EntryDate"].ToString());
            if (Outp > 0)
            {
                WebMsgBox.Show("Entry canceled successfully....", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Outward_Entry_Cancel_" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                TxtDrAmt.Text = "";
                TxtCrAmt.Text = "";
                TxtDifference.Text = "";
                TxtDrAmt.Focus();
                BindMultipleGrid();
                GetMultipleInfo();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string OW_LoanEntries()
    {
        try
        {
            DataTable DT = new DataTable();

            DT = MV.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "UNC");

            if (DT.Rows.Count > 0)
            {

                // string IntAmt = (Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())).ToString(); Commented as per Pen Reuquiremetn 04/09/2017
                string IntAmt = DT.Rows[0]["Interest"].ToString();
                //string PenAmt = (Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(); Commented as per Pen Reuquiremetn 04/09/2017
                string PenAmt = "0";    // DT.Rows[0]["PInterest"].ToString();
                string RecAmt = "0";   // DT.Rows[0]["InterestRec"].ToString();
                string NotAmt = "0";   // DT.Rows[0]["NoticeChrg"].ToString();
                string SerAmt = "0";   // DT.Rows[0]["ServiceChrg"].ToString();
                string CouAmt = "0";   // DT.Rows[0]["CourtChrg"].ToString();
                string SurAmt = "0";   // DT.Rows[0]["SurChrg"].ToString();
                string OthAmt = "0";   // DT.Rows[0]["OtherChrg"].ToString();
                string BanAmt = "0";   // DT.Rows[0]["BankChrg"].ToString();
                string InsAmt = "0";   // DT.Rows[0]["InsChrg"].ToString();
                string IPriAmt = "0", IIntAmt = "0", IPenAmt = "0", IRecAmt = "0", INotAmt = "0", ISerAmt = "0", ICouAmt = "0", ISurAmt = "0", IOthAmt = "0", IBanAmt = "0", IInsAmt = "0";
                string PriAmt = "";
                if (Convert.ToDouble(DT.Rows[0]["Principle"]) < 0) //If Balance is Excess 05-12-2017
                {
                    PriAmt = Math.Abs(Convert.ToDouble(DT.Rows[0]["Principle"])).ToString();
                    IPriAmt = txtinstamt.Text;
                }
                else
                {
                    PriAmt = DT.Rows[0]["Principle"].ToString();

                    double TotalDr = Convert.ToDouble(txtinstamt.Text);
                    if (TotalDr > Convert.ToDouble(InsAmt))
                    {
                        TotalDr = TotalDr - Convert.ToDouble(InsAmt);
                        IInsAmt = InsAmt;
                        if (TotalDr > Convert.ToDouble(BanAmt))
                        {
                            TotalDr = TotalDr - Convert.ToDouble(BanAmt);

                            IBanAmt = BanAmt;
                            if (TotalDr > Convert.ToDouble(OthAmt))
                            {
                                TotalDr = TotalDr - Convert.ToDouble(OthAmt);
                                IOthAmt = OthAmt;
                                if (TotalDr > Convert.ToDouble(SurAmt))
                                {
                                    TotalDr = TotalDr - Convert.ToDouble(SurAmt);
                                    ISurAmt = SurAmt;
                                    if (TotalDr > Convert.ToDouble(CouAmt))
                                    {
                                        TotalDr = TotalDr - Convert.ToDouble(CouAmt);
                                        ICouAmt = CouAmt;
                                        if (TotalDr > Convert.ToDouble(SerAmt))
                                        {
                                            TotalDr = TotalDr - Convert.ToDouble(SerAmt);
                                            ISerAmt = SerAmt;
                                            if (TotalDr > Convert.ToDouble(NotAmt))
                                            {
                                                TotalDr = TotalDr - Convert.ToDouble(NotAmt);
                                                INotAmt = NotAmt;
                                                if (TotalDr > Convert.ToDouble(RecAmt))
                                                {
                                                    TotalDr = TotalDr - Convert.ToDouble(RecAmt);
                                                    IRecAmt = RecAmt;
                                                    if (TotalDr > Convert.ToDouble(PenAmt))
                                                    {
                                                        TotalDr = TotalDr - Convert.ToDouble(PenAmt);
                                                        IPenAmt = PenAmt;
                                                        if (TotalDr > Convert.ToDouble(IntAmt))
                                                        {
                                                            TotalDr = TotalDr - Convert.ToDouble(IntAmt);
                                                            IIntAmt = IntAmt;
                                                            if (TotalDr > Convert.ToDouble(PriAmt))
                                                            {
                                                                TotalDr = TotalDr - Convert.ToDouble(PriAmt);
                                                            }
                                                            else
                                                            {
                                                                IPriAmt = TotalDr.ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            IIntAmt = TotalDr.ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IPenAmt = TotalDr.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    IRecAmt = TotalDr.ToString();
                                                }
                                            }
                                            else
                                            {
                                                INotAmt = TotalDr.ToString();
                                            }
                                        }
                                        else
                                        {
                                            ISerAmt = TotalDr.ToString();
                                        }
                                    }
                                    else
                                    {
                                        ICouAmt = TotalDr.ToString();
                                    }
                                }
                                else
                                {
                                    ISurAmt = TotalDr.ToString();
                                }
                            }
                            else
                            {
                                IOthAmt = TotalDr.ToString();
                            }
                        }
                        else
                        {
                            IBanAmt = TotalDr.ToString();
                        }
                    }
                    else
                    {
                        IInsAmt = TotalDr.ToString();
                    }
                }


                string EntryMid = Encry.GetMK(Session["MID"].ToString());
                string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");

                Res = CMM.Uni_LoanEntries("OWC", Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, Session["BRCD"].ToString(),
                                        TxtAccNo.Text, txtAccTypeid.Text, ddlinsttype.SelectedValue, txtOpTypeId.Text, "1", txtinstamt.Text, txtpartic.Text, "Loan", "32",
                                        "TR", txtinstno.Text, txtinstdate.Text, txtbankcd.Text, txtbrnchcd.Text, "1001", Session["MID"].ToString(), "0", "0", "OC",
                                        ViewState["CUSTNO"].ToString(), TxtAccName.Text, IPriAmt, IIntAmt, IPenAmt, IRecAmt, INotAmt, ISerAmt, ICouAmt, ISurAmt, IOthAmt, IBanAmt, IInsAmt,
                                        EntryMid, REFERENCEID);


            }
            else
            {
                WebMsgBox.Show("Loan Account Balances Details not found...!", this.Page);
                Cleardata();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
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
                //txtinstdate.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
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
    protected void GrdMul_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("AMOUNT")).Text) ? "0" : ((Label)e.Row.FindControl("AMOUNT")).Text;
                if (Amt != "0")
                    NumOfInst++;

                TotalValue = Convert.ToDouble(Amt);
                SumFooterValue += TotalValue;
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("Lbl_TotalM");
                lbl.Text = SumFooterValue.ToString();
                Label Lbl_NumofI = (Label)e.Row.FindControl("Lbl_NumofnstM");
                Lbl_NumofI.Text = "No. of Inst = " + NumOfInst;
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
                string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("AMOUNT")).Text) ? "0" : ((Label)e.Row.FindControl("AMOUNT")).Text;
                if (Amt != "0")
                    NumOfInst++;
                TotalValue = Convert.ToDouble(Amt);
                SumFooterValue += TotalValue;
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("Lbl_TotalS");
                lbl.Text = SumFooterValue.ToString();
                Label Lbl_NumofI = (Label)e.Row.FindControl("Lbl_NumofnstS");
                Lbl_NumofI.Text = "No. of Inst = " + NumOfInst;
            }

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
            DataTable dt = CMM.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text);
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
                DT = CMM.LoanDetails("DETAILS", Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
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
            DT = CMM.IWOWSum("DETAILS", "CDETAILS", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
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