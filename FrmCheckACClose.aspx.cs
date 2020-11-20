using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmCheckACClose : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    ClsCheckACClose CACC = new ClsCheckACClose();
    ClsAuthorized AT = new ClsAuthorized();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SUBGLCODE"].ToString()))
            {
                TxtProcode.Text = Request.QueryString["SUBGLCODE"].ToString();
                productcd();
                TxtAccNo.Text = Request.QueryString["ACCNO"].ToString();
                accno();
            }
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            TxtProcode.Focus();
            autoglname.ContextKey = Session["BRCD"].ToString();
            AutoTGlName.ContextKey = Session["BRCD"].ToString();
            DIV_TPRDACC.Visible = false;
            Lbl_IntApp.InnerText = CACC.GetIA_LBL(Session["BRCD"].ToString());
            Lbl_UCC.InnerText = CACC.GetLabel("UCC", Session["BRCD"].ToString());
            Lbl_SC.InnerText = CACC.GetLabel("SC", Session["BRCD"].ToString());
            Lbl_ECC.InnerText = CACC.GetLabel("ECC", Session["BRCD"].ToString());
            Lbl_ST.InnerText = CACC.GetLabel("ST", Session["BRCD"].ToString());
            Lbl_CA.InnerText = CACC.GetLabel("CA", Session["BRCD"].ToString());
            Lbl_BC.InnerText = CACC.GetLabel("BC", Session["BRCD"].ToString());
            ENDN(false);
        }
    }
    protected void ENDN(bool TF)
    {
        TxtCustNo.Enabled = TF;
        TxtOpenDate.Enabled = TF;
        TxtUnsedChqChrges.Enabled = TF;
        // TxtTotalTax.Enabled = TF;
        TxtFinal.Enabled = TF;
        TxtClearBal.Enabled = TF;
        TxtTotalBal.Enabled = TF;
        TxtChrgsCheque.Enabled = TF;
        TxtChrgsCheque.Enabled = TF;
        TxtACStatus.Enabled = TF;
        TxtLastIntDate.Enabled = TF;
        TxtIntApp.Enabled = TF;
    }

    protected void Clear()
    {

        TxtProcode.Text = "";
        TxtProName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        TxtACStatus.Text = "";
        TxtCustNo.Text = "";
        TxtOpenDate.Text = "";
        TxtClearBal.Text = "";
        TxtTotalBal.Text = "";
        TxtChrgsCheque.Text = "";
        TxtUnusedCheque.Text = "";
        TxtMaxC.Text = "0";
        TxtOtherChrgs.Text = "0";
        TxtStartNo.Text = "";
        TxtEndNo.Text = "";
        TxtBoooksize.Text = "";
        TxtBookUnused.Text = "";
        TxtIntApp.Text = "0";
        TxtLastIntDate.Text = "0";
        TxtUnsedChqChrges.Text = "0";
        TxtServChrgs.Text = "0";
        TxtEarlyClose.Text = "0";
        TxtTotalCease.Text = "0";
        TxtTotalTax.Text = "0";
        TxtBCCT.Text = "0";
        TxtFinal.Text = "0";
        TxtCrInt.Text = "0";
        TxtSI.Text = "";
        TxtReason.Text = "";
        TxtTProcode.Text = "";
        TxtTPName.Text = "";
        TxtTAccno.Text = "";
        TxtTAccName.Text = "";
        Ddl_type.SelectedValue = "0";
        TxtInstNo.Text = "";
        TxtInstruDate.Text = "";
        TxtProcode.Focus();
        DIV_TPRDACC.Visible = false;
    }


    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Btn_btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GetOpening()
    {
        try
        {
            DT = OC.GetOpeningDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GetBal()
    {
        try
        {
            DT = OC.GetBalOC(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (DT.Rows.Count > 0)
            {
                TxtClearBal.Text = DT.Rows[0]["CLEARBAL"].ToString();
                TxtTotalBal.Text = DT.Rows[0]["TOTALBAL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GetIDetails()
    {
        try
        {
            DT = OC.GetInstruDetail(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (DT.Rows.Count > 0)
            {
                TxtStartNo.Text = DT.Rows[0]["STARTNO"].ToString();
                TxtEndNo.Text = DT.Rows[0]["ENDNO"].ToString();
                TxtBoooksize.Text = DT.Rows[0]["BOOKSIZE"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string getGlCode(string BRCD, string subglcode)
    {
        string glcode = "";
        try
        {
            string sql = "SELECT GLCODE FROM GLMAST WHERE  SUBGLCODE='" + subglcode + "' and BRCD='" + BRCD + "'";
            glcode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return glcode;
    }

    protected void GetLID()
    {
        try
        {
            string RES, GL, DiffMon;
            double Closebal;
            RES = OC.GetLastIntDate(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtAccNo.Text, TxtProcode.Text);
            string TD = Session["EntryDate"].ToString();
            string[] MM = TD.Split('/');
            GL = getGlCode(Session["BRCD"].ToString(), TxtProcode.Text);
            // DiffMon = conn.GetMonthDiff(RES, Session["EntryDate"].ToString());
            if (RES != "")
            {
                TxtLastIntDate.Text = RES;
                Closebal = OC.GetOpenClose("CLOSING", MM[2].ToString(), MM[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GL);
                if (Closebal != 0.00)
                {
                    double INTR;
                    INTR = ((Closebal / 100) * 4);// * Convert.ToInt32(DiffMon);
                    TxtIntApp.Text = INTR.ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GetChrgs(string FL)
    {
        string RES;
        try
        {
            if (FL == "UCC")
            {
                RES = OC.GetCharges(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
                if (RES != null)
                {
                    TxtChrgsCheque.Text = RES;
                }
            }

            if (FL == "MCM")
            {
                DateTime CDT;
                string MINMON = "";
                MINMON = OC.GetMinMonth(Session["BRCD"].ToString());
                string chkdt = conn.AddMonthDay(TxtOpenDate.Text, MINMON, "M").Replace("12:00:00", "");
                // OPD = Convert.ToDateTime(TxtOpenDate.Text);
                CDT = Convert.ToDateTime(chkdt);
                if (Convert.ToDateTime(Session["EntryDate"].ToString()) <= CDT)
                {
                    RES = OC.GetCharges(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
                    if (RES != null)
                        TxtEarlyClose.Text = RES;

                }
                else
                    TxtEarlyClose.Text = "0";

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
            productcd();
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), CT[1].ToString()).ToString() != "3")
                {
                    TxtProName.Text = CT[0].ToString();
                    TxtProcode.Text = CT[1].ToString();
                    //TxtGLCD.Text = CT[2].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                    TxtAccNo.Focus();
                    if (TxtProName.Text == "")
                    {
                        WebMsgBox.Show("Please enter valid Product code", this.Page);
                        TxtProcode.Text = "";
                        TxtProcode.Focus();

                    }
                }
                else
                {
                    TxtProName.Text = "";
                    TxtProcode.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }


    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {

        try
        {
            accno();
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {

        try
        {
            string ACST = "";
            ACST = OC.GETACCStatus(Session["BRCD"].ToString(), TxtAccNo.Text, TxtProcode.Text);
            if (ACST != "3")
            {

                TxtACStatus.Text = "Normal/Operative";
                string CUNAME = TxtAccName.Text;
                string[] custnob = CUNAME.Split('_');
                if (custnob.Length > 1)
                {
                    TxtAccName.Text = custnob[0].ToString();
                    TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    TxtCustNo.Text = custnob[2].ToString();
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    GetOpening();
                    GetBal();
                    GetIDetails();
                    GetLID();
                    GetChrgs("UCC");
                    GetChrgs("MCM");

                    if (TxtAccNo.Text == "")
                    {
                        TxtAccName.Text = "";
                        return;
                    }
                    TxtBookUnused.Focus();
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Invalid Account Number.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Focus();
                    return;
                }
            }

            else
            {
                WebMsgBox.Show("Account already closed!...", this.Page);
                Clear();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }

    protected void TxtUnusedCheque_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtUnsedChqChrges.Text = (Convert.ToInt32(TxtUnusedCheque.Text) * Convert.ToInt32(TxtChrgsCheque.Text)).ToString();
            TxtMaxC.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Ddl_type.SelectedValue == "1")
            {
                //Fetch 99 gl to prdcode and Cash in hand glname
                string NM = BD.GetGlDetails(Session["BRCD"].ToString(), "", "", "CASH");
                string[] GLNM = NM.Split('_');

                TxtTPName.Text = GLNM[1].ToString();
                TxtTProcode.Text = GLNM[0].ToString();
                TxtTProcode.Enabled = false;
                TxtTPName.Enabled = false;
                DIV_TPRDACC.Visible = true;
                DIV1.Visible = false;
                DIV_TACC.Visible = false;

            }
            else if (Ddl_type.SelectedValue == "2")
            {
                //Fecth only CBB glgroup Account 
                TxtTProcode.Enabled = true;
                TxtTPName.Enabled = true;
                DIV_TPRDACC.Visible = true;
                DIV_TACC.Visible = false;
                TxtTPName.Text = "";
                TxtTProcode.Text = "";
                TxtTProcode.Focus();

            }
            else if (Ddl_type.SelectedValue == "3")
            {
                TxtTProcode.Enabled = true;
                TxtTPName.Enabled = true;
                DIV_TPRDACC.Visible = true;
                DIV_TACC.Visible = true;
                TxtTPName.Text = "";
                TxtTProcode.Text = "";
                TxtTProcode.Focus();
            }
            else
            {
                DIV_TPRDACC.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTProcode.Text == "")
            {
                TxtTPName.Text = "";
                TxtTAccno.Focus();
                goto ext;
            }
            int result = 0;
            string GlS1;
            string CHK = BD.GetGlDetails(Session["BRCD"].ToString(), TxtTProcode.Text, TxtTAccno.Text, "CHEQUE");
            if (CHK == null && Ddl_type.SelectedIndex == 2)
            {
                WebMsgBox.Show("Product Code is Not in CBB Group!.....", this.Page);
                TxtTProcode.Text = "";
                TxtTPName.Text = "";
                TxtTProcode.Focus();
                return;
            }

            else
            {

                int.TryParse(TxtTProcode.Text, out result);
                TxtTPName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
                GlS1 = BD.GetAccTypeGL(TxtTProcode.Text, Session["BRCD"].ToString());
                if (GlS1 != null)
                {
                    string[] GLS = GlS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    AutoTAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtTProcode.Text + "_" + ViewState["DRGL"].ToString();
                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                    TxtTAccno.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                    TxtTProcode.Text = "";
                    TxtTPName.Text = "";
                    TxtTProcode.Focus();
                }
            }
        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtTPName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtTPName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtTPName.Text = CT[0].ToString();
                TxtTProcode.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtTProcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoTAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtTProcode.Text + "_" + ViewState["DRGL"].ToString();

                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                TxtTAccno.Focus();
                if (TxtTPName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtTProcode.Text = "";
                    TxtTProcode.Focus();

                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }


    }
    protected void TxtTAccno_TextChanged(object sender, EventArgs e)
    {

        try
        {
            string AT = "";
            string ACST = "";
            ACST = OC.GETACCStatus(Session["BRCD"].ToString(), TxtTAccno.Text, TxtTProcode.Text);
            if (ACST != "3")
            {

                AT = BD.Getstage1(TxtTAccno.Text, Session["BRCD"].ToString(), TxtTProcode.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        //  Clear();
                    }
                    else
                    {

                        string[] TD = Session["EntryDate"].ToString().Split('/');

                        if (TxtTAccno.Text == "")
                        {
                            TxtTAccName.Text = "";
                            goto ext;
                        }

                        DataTable dt1 = new DataTable();
                        if (TxtTAccno.Text != "" & TxtTProcode.Text != "")
                        {
                            string PRD = "";
                            string[] CN;
                            PRD = TxtTProcode.Text;
                            CN = customcs.GetAccountName(TxtTAccno.Text.ToString(), PRD, Session["BRCD"].ToString()).Split('_');
                            ViewState["CUSTNO"] = CN[0].ToString();

                            TxtTAccName.Text = CN[1].ToString();

                            if (TxtTAccName.Text == "" & TxtTAccno.Text != "")
                            {
                                WebMsgBox.Show("Please enter valid Account number", this.Page);
                                TxtTAccno.Text = "";
                                TxtTAccno.Focus();
                                return;
                            }
                            Btn_Submit.Focus();
                        }

                        if (TxtTAccno.Text == "" || TxtTProcode.Text == "")
                        {
                            TxtTAccName.Text = "";
                            goto ext;
                        }
                        dt1 = customcs.GetAccNoAccType(TxtTProcode.Text, TxtTAccno.Text, Session["BRCD"].ToString());

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                        }
                        else
                        {

                        }
                    ext: ;
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    TxtTAccno.Text = "";
                    TxtTAccno.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Account already closed!...", this.Page);
                Clear();

            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtTAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtTAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtTAccName.Text = custnob[0].ToString();
                TxtTAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] TD = Session["EntryDate"].ToString().Split('/');

                if (TxtTAccno.Text == "")
                {
                    TxtTAccName.Text = "";
                    return;
                }
                Btn_Submit.Focus();
            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                TxtTAccno.Text = "";
                TxtTAccName.Text = "";
                TxtTAccno.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {

        try
        {
            string GLCODE = "", TGLCODE = "";
            GLCODE = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
            string[] GL = GLCODE.Split('_');
            string TGL = BD.GetAccTypeGL(TxtTProcode.Text, Session["BRCD"].ToString());
            string[] TG = TGL.Split('_');
            double TotalDR = Convert.ToDouble(TxtTotalBal.Text) + Convert.ToDouble(TxtCrInt.Text);
            string ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
            // int resultout = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", ST);
            ViewState["ST"] = ST.ToString();
            if (Ddl_type.SelectedIndex == 1)
            {



                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtProcode.Text,
                                    TxtAccNo.Text, "BY CASH", "BY CASH", TxtFinal.Text, "2", "4", "CASH", ST, "0", "1900/01/01", "0", "0", "1001", Session["EntryDate"].ToString(),
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CP", TxtCustNo.Text, TxtAccName.Text, "1", "0");
                if (Result > 0)
                {
                    Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TG[1].ToString(), TxtTProcode.Text,
                                        "", "BY CASH", "BY CASH", TxtFinal.Text, "1", "4", "CASH", ST, "0", "1900/01/01", "0", "0", "1001", Session["EntryDate"].ToString(),
                                        Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CP", TxtCustNo.Text, TxtAccName.Text, "1", "0");
                }
            }
            else if (Ddl_type.SelectedIndex == 2)
            {
                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtProcode.Text,
                                    TxtAccNo.Text, "TO CHEQUE", "TO CHEQUE", TxtFinal.Text, "2", "4", "CHEQUE", ST, "0", "1900/01/01", "0", "0", "1001", Session["EntryDate"].ToString(),
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CP", TxtCustNo.Text, TxtAccName.Text, "1", "0");
                if (Result > 0)
                {
                    Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TG[1].ToString(), TxtTProcode.Text,
                                      "", "BY CHEQUE", "BY CHEQUE", TxtFinal.Text, "1", "4", "CHEQUE", ST, TxtInstNo.Text, TxtInstruDate.Text, "0", "0", "1001", Session["EntryDate"].ToString(),
                                      Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CP", TxtCustNo.Text, TxtAccName.Text, "1", "0");
                }
            }
            else if (Ddl_type.SelectedIndex == 3)
            {
                // GLCODE = BD.GetAccTypeGL(TxtTProcode.Text, Session["BRCD"].ToString());

                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtProcode.Text,
                                   TxtAccNo.Text, "BY TRF", "BY TRF", TxtFinal.Text, "2", "7", "TRF", ST, "0", "1900/01/01", "0", "0", "1001", Session["EntryDate"].ToString(),
                                   Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TR", TxtCustNo.Text, TxtAccName.Text, "1", "0");
                if (Result > 0)
                {
                    Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TG[1].ToString(), TxtTProcode.Text,
                                       TxtTAccno.Text, "BY TRF", "BY TRF", TxtFinal.Text, "1", "7", "TRF", ST, "0", "1900/01/01", "0", "0", "1001", Session["EntryDate"].ToString(),
                                       Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TR", TxtCustNo.Text, TxtAccName.Text, "1", "0");

                }

            }

            Result = CACC.Post_Charges(TxtMaxC.Text, TxtOtherChrgs.Text, TxtIntApp.Text, TxtEarlyClose.Text, TxtUnsedChqChrges.Text, TxtServChrgs.Text, TxtTotalTax.Text, TxtTotalCease.Text, TxtBCCT.Text, TxtCrInt.Text,
                                        Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), TxtCustNo.Text, TxtAccName.Text, ST.ToString(), TxtAccNo.Text, GL[1].ToString(), TxtProcode.Text);
            if (Result > 0)
            {


                Result = CACC.Operate_Closure(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                if (Result > 0)
                {
                    Btn_Receipt.Visible = true;
                    WebMsgBox.Show("Vocuher No Generated is:" + ST + " and Account Closed Successfully", this.Page);
                    ViewState["SETNO"] = ST;
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Check_acc_close _" + ST + "_" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ViewState["CNO"] = TxtCustNo.Text;
                    ViewState["NAME"] = TxtAccName.Text;
                    Clear();
                }
            }
            else
            {
                WebMsgBox.Show("Operation Failed!.......", this.Page);
                Clear();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Receipt_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Check_acc_close _Rpt_" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?SETNO=" + ViewState["SETNO"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&CNO=" + ViewState["CNO"].ToString() + "&NAME=" + ViewState["NAME"].ToString() + "&FN=V&rptname=RptReceiptPrint.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            Btn_Receipt.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #region common function(subgl,accno)
    public void productcd()
    {
        try
        {
            if (TxtProcode.Text == "")
            {
                TxtProName.Text = "";
                TxtAccNo.Focus();
                goto ext;
            }

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString()).ToString() != "3")
            {
                int result = 0;
                string GlS1;
                int.TryParse(TxtProcode.Text, out result);
                TxtProName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
                GlS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GlS1 != null)
                {
                    string[] GLS = GlS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                    TxtAccNo.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                    TxtProcode.Text = "";
                    TxtProName.Text = "";
                    TxtProcode.Focus();
                }
            }
            else
            {
                TxtProcode.Text = "";
                TxtProName.Text = "";
                lblMessage.Text = "Product is not operating...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void accno()
    {
        try
        {
            string AT = "";
            string ACST = "";
            ACST = OC.GETACCStatus(Session["BRCD"].ToString(), TxtAccNo.Text, TxtProcode.Text);
            if (ACST != "3")
            {

                AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {

                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        //  Clear();

                    }
                    else
                    {
                        TxtACStatus.Text = "Normal/Operative";
                        string[] TD = Session["EntryDate"].ToString().Split('/');

                        if (TxtAccNo.Text == "")
                        {
                            TxtAccName.Text = "";
                            goto ext;
                        }

                        DataTable dt1 = new DataTable();
                        if (TxtAccNo.Text != "" & TxtProcode.Text != "")
                        {
                            string PRD = "";
                            string[] CN;
                            PRD = TxtProcode.Text;
                            CN = customcs.GetAccountName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString()).Split('_');
                            ViewState["CUSTNO"] = CN[0].ToString();
                            TxtCustNo.Text = CN[0].ToString();
                            TxtAccName.Text = CN[1].ToString();

                            GetOpening();
                            GetBal();
                            GetIDetails();
                            GetLID();
                            GetChrgs("UCC");
                            GetChrgs("MCM");

                            if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                            {
                                WebMsgBox.Show("Please enter valid Account number", this.Page);
                                TxtAccNo.Text = "";
                                TxtAccNo.Focus();
                                return;
                            }
                            TxtBookUnused.Focus();
                        }

                        if (TxtAccNo.Text == "" || TxtProcode.Text == "")
                        {
                            TxtAccName.Text = "";
                            goto ext;
                        }
                        dt1 = customcs.GetAccNoAccType(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                        }
                        else
                        {

                        }
                    ext: ;
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Account No. " + TxtAccNo.Text + " already closed!...", this.Page);
                Clear();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    #endregion
}