using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class FrmTDARenewNCBS2 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsOpenClose OC = new ClsOpenClose();
    scustom SC = new scustom();
    ClsFDARenew FDR = new ClsFDARenew();
    DbConnection conn = new DbConnection();
    ClsCommon CMN = new ClsCommon();
    ClsNewTDA CurrentCls = new ClsNewTDA();


    ClsTDARenewN RN = new ClsTDARenewN();
    int Result = 0;
    bool ValidCash = false;
    bool ValidTrf = false;

    bool Valid = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Res;
            ViewState["MS"] = Request.QueryString["MS"].ToString();
            BD.BindRelation(ddlRelation_1);
            BD.BindRelation(ddlRelation_2);

            if (ViewState["MS"].ToString() == "N")
            {
                ViewState["RecSrno"] = Request.QueryString["RS"].ToString();
                ViewState["IPostType"] = Request.QueryString["IPostType"].ToString();
                ViewState["PRD"] = Request.QueryString["PRD"].ToString();
                ViewState["ACC"] = Request.QueryString["FD"].ToString();
                ViewState["FDTP"] = Request.QueryString["FDP"].ToString();
                ViewState["TPAY"] = Request.QueryString["TPAMT"].ToString();
                ViewState["CT"] = Request.QueryString["CT"].ToString();
                ViewState["DAMT"] = Request.QueryString["DAMT"].ToString();
                ViewState["ACCTYPE"] = Request.QueryString["ACCT"].ToString();
                ViewState["INTPAY"] = Request.QueryString["INTPAY"].ToString();
                ViewState["IR"] = Request.QueryString["IR"].ToString();
                ViewState["OPRTYPE"] = Request.QueryString["OPRTYPE"].ToString();
                ViewState["REFID"] = Request.QueryString["REFID"].ToString();

                //Admin Fee
                ViewState["AFEE"] = Request.QueryString["ADFEE"].ToString();
                ViewState["ADSBGL"] = Request.QueryString["ADSBGL"].ToString();

                //Commision
                ViewState["COMMCHG"] = Request.QueryString["COMMCHG"].ToString();
                ViewState["COMMSBGL"] = Request.QueryString["COMMSBGL"].ToString();

                ViewState["GLC"] = Request.QueryString["GLC"].ToString();
                TxtAcPayment.Text = ViewState["TPAY"].ToString();
                BD.BindIntrstPayout(Ddl_IntPayout);
                BD.BindWithReceipt(DdlPayType, "1");
                AutoWRPRD.ContextKey = Session["BRCD"].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                AutoPrdName.ContextKey = Session["BRCD"].ToString();

                Res = RN.DeleteData("DELETE", "ALL", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());

                //-----Nominee details
                DataTable DT = RN.GetNomDetail(Session["BRCD"].ToString(), ViewState["CT"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString());
                if (DT != null && DT.Rows.Count > 0)
                {
                    TxtNominee_1.Text = DT.Rows[0]["NOMINEENAME"].ToString();
                    ddlRelation_1.SelectedValue = DT.Rows[0]["RELATION"].ToString();
                    RdbGender_1.SelectedValue = DT.Rows[0]["RELATION"].ToString() == "0" ? "M" : DT.Rows[0]["RELATION"].ToString();

                    if (DT.Rows.Count > 1)
                    {
                        TxtNominee_2.Text = DT.Rows[1]["NOMINEENAME"].ToString();
                        ddlRelation_2.SelectedValue = DT.Rows[1]["RELATION"].ToString();
                        RdbGender_2.SelectedValue = DT.Rows[1]["RELATION"].ToString() == "0" ? "M" : DT.Rows[0]["RELATION"].ToString();

                    }
                }

            }
            else
            {
                ViewState["RecSrno"] = Request.QueryString["RS"].ToString();
                ViewState["IPostType"] = Request.QueryString["IPostType"].ToString();
                ViewState["TPAY"] = Request.QueryString["TPAMT"].ToString();
                ViewState["ACCTYPE"] = Request.QueryString["ACCT"].ToString();
                ViewState["CT"] = Request.QueryString["CT"].ToString();
                ViewState["MSETNO"] = Request.QueryString["SNO"].ToString();
                ViewState["OPRTYPE"] = Request.QueryString["OPR"].ToString();
                ViewState["INTPAY"] = Request.QueryString["INTPAY"].ToString();
                ViewState["REFID"] = Request.QueryString["REFID"].ToString();
                TxtAcPayment.Text = ViewState["TPAY"].ToString();

                BD.BindIntrstPayout(Ddl_IntPayout);
                BD.BindWithReceipt(DdlPayType, "1");
                // dtDeposDate.Text = Session["EntryDate"].ToString();
                AutoWRPRD.ContextKey = Session["BRCD"].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                AutoPrdName.ContextKey = Session["BRCD"].ToString();
                // BD.BindDepositGL(Ddl_Prdname, "5", Session["BRCD"].ToString());
                Res = RN.DeleteData("DELETE", "ALL", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());

                string NP = CMN.GetNETPAID("1");
                if (NP != null)
                {
                    ViewState["NetPaid"] = NP.ToString();
                }
                else
                {
                    ViewState["NetPaid"] = "0";
                }
                string NPGL = BD.GetAccTypeGL(ViewState["NetPaid"].ToString(), Session["BRCD"].ToString());
                if (NPGL != null)
                {
                    string[] NG = NPGL.Split('_');
                    ViewState["GLNetPaid"] = NG[1].ToString();
                }



            }
            BindGrid();
            TxtInstNumber.Enabled = false;
            NomineeOperate();
            Show_Details();

        }
    }

    #region Checked and Change Events

    protected void DdlPayType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlPayType.SelectedValue == "2")
            {
                AMT.Visible = true;
                DIVWR.Visible = true;
                AMTDetail.Visible = true;
                TxtWrPrdcode.Focus();
            }
            else if (DdlPayType.SelectedValue == "4")
            {
                AMT.Visible = true;
                DIVWR.Visible = true;
                AMTDetail.Visible = true;
                TxtWrPrdcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_PrinWithInt_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DIVWR.Visible = false;
            AMT.Visible = false;
            WRPAYTP.Visible = false;
            AMTDetail.Visible = false;
            TxtWrAmount.Text = "";
            int Res = RN.DeleteData("DELETE", "ALL", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_OnlyPrin_CheckedChanged(object sender, EventArgs e)
    {

        if (ViewState["MS"].ToString() == "N")
        {
            DIVWR.Visible = true;
            AMT.Visible = true;
            WRPAYTP.Visible = false;
            AMTDetail.Visible = false;
            double DAMT, MAMT, INTAMT;
            DAMT = MAMT = INTAMT = 0;
            DAMT = Convert.ToDouble(ViewState["DAMT"].ToString());
            MAMT = Convert.ToDouble(ViewState["TPAY"].ToString());


            if (Convert.ToDouble(ViewState["INTPAY"].ToString()) + (MAMT - DAMT) != 0)
                INTAMT = (MAMT - DAMT);
            //Changes done for RD 11-01-2018
            else if (Convert.ToDouble(ViewState["INTPAY"].ToString()) < (MAMT - DAMT))
                INTAMT = Convert.ToDouble(ViewState["INTPAY"].ToString());
            else
                INTAMT = (MAMT - DAMT);

            ViewState["TINTAMT"] = INTAMT.ToString();
            TxtWrAmount.Text = INTAMT.ToString();
        }
        else
        {

            InsertNetPaid(TxtAcPayment.Text);
            DIVWR.Visible = true;
            AMT.Visible = true;
            WRPAYTP.Visible = false;
            AMTDetail.Visible = false;
            double DAMT, MAMT, INTAMT;
            DAMT = MAMT = INTAMT = 0;

            INTAMT = Convert.ToDouble(ViewState["INTPAY"].ToString());
            // ViewState["TINTAMT"] = INTAMT.ToString();
            TxtWrAmount.Text = INTAMT.ToString();

        }
        TxtWrPrdcode.Focus();
    }

    protected void Rdb_WithReceipt_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DIVWR.Visible = false;
            AMT.Visible = false;
            WRPAYTP.Visible = true;
            DdlPayType.Focus();
            int Res = RN.DeleteData("DELETE", "ALL", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            InsertNetPaid(ViewState["TPAY"].ToString());

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtWrPrdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GG = BD.GetTDAAccTypeGL(TxtWrPrdcode.Text, Session["BRCD"].ToString(), "0");
            if (GG != null)
            {
                string[] GLT = GG.Split('_');
                ViewState["CRGL"] = GLT[1].ToString();
                TxtWrPrdName.Text = GLT[0].ToString();
                AutoWRAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtWrPrdcode.Text + "_" + ViewState["CRGL"].ToString();
                if (GLT[1] != null || GLT[1] != "")
                {
                    string res = BD.GetGLGroup(TxtWrPrdcode.Text, Session["BRCD"].ToString(), "0");
                    if (res != null)
                    {

                        ViewState["GLGroup"] = res.ToString();
                    }
                    if (res == "CBB" && TxtWrPrdcode.Text != "99")
                    {

                        TxtWrAccno.Text = TxtWrPrdcode.Text;
                        TxtWrAccName.Text = TxtWrPrdName.Text;

                        if (res == "CBB")
                        {
                            TxtInstNumber.Enabled = true;
                            TxtInstNumber.Focus();
                        }
                        else
                        {
                            Btn_SubmitWr.Focus();
                        }

                    }
                    else if (TxtWrPrdcode.Text == "99")
                    {
                        TxtWrAccno.Text = TxtWrPrdcode.Text;
                        TxtWrAccName.Text = TxtWrPrdName.Text;
                        TxtWrAmount.Text = "0";
                        TxtWrAmount.Focus();
                    }
                    else
                    {
                        TxtWrAccno.Focus();
                        TxtWrAccno.Text = "";
                        TxtWrAccName.Text = "";
                    }
                }
                if (TxtWrPrdName.Text == "")
                {
                    WebMsgBox.Show("Invalid product code", this.Page);
                    ClearData();
                    TxtProcode.Focus();
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Product code Invalid.....!", this.Page);
                TxtWrPrdcode.Text = "";
                TxtWrPrdName.Text = "";
                TxtWrPrdcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtWrPrdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = TxtWrPrdName.Text;
            string[] GLS = GL.Split('_');
            if (GLS.Length > 1)
            {
                TxtWrPrdName.Text = GLS[0].ToString();
                TxtWrPrdcode.Text = GLS[1].ToString();
                ViewState["CRGL"] = GLS[2].ToString();
                AutoWRAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtWrPrdcode.Text + "_" + ViewState["CRGL"].ToString();
                if (GLS[2] != null || GLS[2] != "")
                {
                    string res = BD.GetGLGroup(TxtWrPrdcode.Text, Session["BRCD"].ToString(), "0");
                    if (res != null)
                    {
                        ViewState["GLGroup"] = res.ToString();
                    }

                    if (res == "CBB" || res == "CAS")
                    {
                        TxtWrAccno.Text = TxtWrPrdcode.Text;
                        TxtWrAccName.Text = TxtWrPrdName.Text;

                        if (res == "CBB")
                        {
                            TxtInstNumber.Enabled = true;
                            TxtInstNumber.Focus();
                        }
                        else
                        {
                            Btn_SubmitWr.Focus();
                        }


                    }
                    else
                    {
                        TxtWrAccno.Focus();
                        TxtWrAccno.Text = "";
                        // TxtWrPrdName.Text = "";
                    }
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtWrAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = Session["EntryDate"].ToString().Split('/');
            TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtWrPrdcode.Text, TxtWrAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["CRGL"].ToString()).ToString();
            if (TxtWrAccno.Text == "")
            {
                TxtWrAccno.Focus();
            }
            DataTable dt1 = new DataTable();
            if (TxtWrAccno.Text != "" & TxtWrPrdcode.Text != "")
            {
                string PRD = TxtWrPrdcode.Text;
                string[] namecust;
                string NN = SC.GetAccountName(TxtWrAccno.Text.ToString(), PRD, Session["BRCD"].ToString());

                if (NN != null)
                {
                    namecust = NN.Split('_');
                    if (namecust.Length > 0)
                    {
                        ViewState["CUSTNO"] = namecust[0].ToString();
                        TxtWrAccName.Text = namecust[1].ToString();

                        if (TxtWrAccName.Text == "" & TxtWrAccno.Text != "")
                        {
                            WebMsgBox.Show("Please enter valid Account number", this.Page);
                            TxtWrAccno.Text = "";
                            TxtWrAccno.Focus();
                            return;
                        }
                        TxtWrAmount.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Account number not found....!", this.Page);
                    TxtWrAccno.Text = "";
                    TxtWrAccName.Text = "";
                    TxtWrAccno.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtWrAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtWrAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtWrAccName.Text = custnob[0].ToString();
                TxtWrAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                ViewState["CUSTNO"] = custnob[2].ToString();
                string[] TD = Session["EntryDate"].ToString().Split('/');
                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtWrPrdcode.Text, TxtWrAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["CRGL"].ToString()).ToString();
                //txtnaration1.Focus();
            }
            else
            {
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void TxtBalance_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TxtWrAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CURR, PRIN, TAMT;

            if (Rdb_WithReceipt.Checked == false)
            {
                if (((Convert.ToDouble(TxtWrAmount.Text) <= Convert.ToDouble(ViewState["TPAY"].ToString())) || DdlPayType.SelectedValue == "4"))
                {

                    CURR = PRIN = TAMT = 0;
                    CURR = Convert.ToDouble(TxtWrAmount.Text);
                    PRIN = Convert.ToDouble(ViewState["TPAY"].ToString());
                    TAMT = CURR + PRIN;
                    //TxtDepoAmt.Text = TAMT.ToString();
                }
                else
                {
                    WebMsgBox.Show("Invalid Amount....!", this.Page);
                    TxtWrAmount.Text = "";
                    TxtWrAmount.Focus();

                }
            }
            else if (TxtWrPrdcode.Text == "99")
            {
                Btn_SubmitWr.Focus();
            }
            else
            {
                if ((Convert.ToDouble(TxtWrAmount.Text) <= Convert.ToDouble(TxtBalance.Text) || DdlPayType.SelectedValue == "4"))
                {

                    CURR = PRIN = TAMT = 0;
                    CURR = Convert.ToDouble(TxtWrAmount.Text);
                    PRIN = Convert.ToDouble(ViewState["TPAY"].ToString());
                    TAMT = CURR - PRIN;
                    Btn_SubmitWr.Focus();
                    //TxtDepoAmt.Text = TAMT.ToString();
                }
                else
                {
                    WebMsgBox.Show("Invalid Amount....!", this.Page);
                    TxtWrAmount.Text = "";
                    TxtWrAmount.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void Rdb_Single_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["PRD"] != null)
            {
                TxtProcode.Text = ViewState["PRD"].ToString();
                ProdChange();
            }
            if (Rdb_OnlyPrin.Checked == false && Rdb_PrinWithInt.Checked == false && Rdb_WithReceipt.Checked == false)
            {
                WebMsgBox.Show("Select Renewal type first......!", this.Page);
                Rdb_Single.Checked = false;
                Rdb_Multiple.Checked = false;
                Rdb_PrinWithInt.Focus();
            }
            else
            {
                #region Single Closure
                if (ViewState["MS"].ToString() == "N")
                {
                    if (Rdb_WithReceipt.Checked != true)
                    {
                        TxtDepoAmt.Enabled = false;
                        TxtProcode.Focus();
                        if (Rdb_OnlyPrin.Checked == true)
                        {
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDepoAmt.Text = TxtAmtColl.Text;
                        }
                        else
                        {
                            TxtDepoAmt.Text = TxtAcPayment.Text;
                            TxtAmtColl.Text = TxtAcPayment.Text;
                            TxtDiff.Text = TxtAcPayment.Text;
                        }


                        //ClearFD();
                        string DD;
                        DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString(), ViewState["RecSrno"].ToString());
                        if (DD != null)
                        {
                            TxtDepositDate.Text = DD.ToString();
                        }
                    }
                    else
                    {
                        TxtDepoAmt.Enabled = false;
                        TxtProcode.Focus();
                        if (Rdb_OnlyPrin.Checked == true)
                        {
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDepoAmt.Text = TxtAmtColl.Text;
                        }
                        else
                        {
                            TxtDepoAmt.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                        }

                        //ClearFD();
                        string DD;
                        DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString(), ViewState["RecSrno"].ToString());
                        if (DD != null)
                        {
                            TxtDepositDate.Text = DD.ToString();
                        }
                    }
                }
                #endregion
                #region Multiple Closure
                if (ViewState["MS"].ToString() == "Y")
                {
                    if (Rdb_WithReceipt.Checked != true)
                    {
                        TxtDepoAmt.Enabled = false;
                        TxtProcode.Focus();
                        if (Rdb_OnlyPrin.Checked == true)
                        {
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDepoAmt.Text = TxtAmtColl.Text;
                        }
                        else
                        {
                            TxtDepoAmt.Text = TxtAcPayment.Text;
                            TxtAmtColl.Text = TxtAcPayment.Text;
                            TxtDiff.Text = TxtAcPayment.Text;
                        }

                        //ClearFD();

                    }
                    else
                    {
                        TxtDepoAmt.Enabled = false;
                        TxtProcode.Focus();
                        if (Rdb_OnlyPrin.Checked == true)
                        {
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDepoAmt.Text = TxtAmtColl.Text;
                        }
                        else
                        {
                            TxtDepoAmt.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtAmtColl.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                            TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                        }

                        //ClearFD();

                    }
                }
                #endregion
            }
            CheckDepoistdate();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_Multiple_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_OnlyPrin.Checked == false && Rdb_PrinWithInt.Checked == false && Rdb_WithReceipt.Checked == false)
            {
                WebMsgBox.Show("Select Renewal type first......!", this.Page);
                Rdb_Single.Checked = false;
                Rdb_Multiple.Checked = false;
                Rdb_PrinWithInt.Focus();
            }
            else
            {
                CheckDepoistdate();
                CheckMultiple();
                TxtProcode.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void SetIntPayout()
    {
        try
        {
            string Ip = CurrentCls.GetIntPayout(Session["BRCD"].ToString(), TxtProcode.Text);

            if (Ip != null)
            {
                if (Ip == "M" || Ip == "m")
                {
                    Ddl_IntPayout.SelectedValue = "1";
                }
                else if (Ip == "Q" || Ip == "q")
                {
                    Ddl_IntPayout.SelectedValue = "2";
                }
                else if (Ip == "H" || Ip == "h")
                {
                    Ddl_IntPayout.SelectedValue = "3";
                }
                else if (Ip == "Y" || Ip == "y")
                {
                    Ddl_IntPayout.SelectedValue = "5";
                }
                else
                {
                    Ddl_IntPayout.SelectedValue = "4";
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        ProdChange();
    }


    public void ProdChange()
    {
        try
        {
            int result = 0;
            string GL = BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), "5");
            if (GL != null)
            {
                string[] GLT = GL.Split('_');
                ViewState["GL"] = GLT[1].ToString();
                ViewState["CIR"] = GLT[2].ToString();
                TxtPrdName.Text =
                Ddl_Prdname.SelectedValue = TxtProcode.Text;
                string GlS1;
                if (int.TryParse(TxtProcode.Text, out result))
                {
                    TxtPrdName.Text = SC.GetProductName(result.ToString(), Session["BRCD"].ToString());
                    SetIntPayout();
                    Set_PeriodType();

                }

                TxtDepositDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Product code.....!", this.Page);
                TxtProcode.Text = "";
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPrdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = TxtPrdName.Text;

            string[] GLS = GL.Split('_');
            if (GLS.Length > 1)
            {
                TxtPrdName.Text = GLS[0].ToString();
                TxtProcode.Text = GLS[1].ToString();
                string GL2 = BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), "5");
                string[] GLT = GL2.Split('_');
                ViewState["GL"] = GLT[1].ToString();
                ViewState["CIR"] = GLT[2].ToString();


                ViewState["CRGL"] = GLS[2].ToString();
                SetIntPayout();
                Set_PeriodType();
                TxtDepositDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Ddl_Prdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtProcode.Text = Ddl_Prdname.SelectedValue;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CheckDepoistdate()
    {
        try
        {
            string RDay = RN.GetMinRenewDay();
            if (RDay != null)
            {
                if (ViewState["MS"].ToString() == "N")
                {
                    string DD;
                    DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString(), ViewState["RecSrno"].ToString());
                    if (DD != null)
                    {
                        // string Daydiff = conn.GetDayDiff(TxtDepositDate.Text, DD.ToString());
                        string Daydiff = conn.GetDayDiff(DD.ToString(), Session["EntryDate"].ToString());
                        if (Convert.ToInt32(Daydiff) > Convert.ToInt32(RDay))
                        {
                            //WebMsgBox.Show("Deposit Date invalid, Only accept " + RDay + " day Diff less than " + DD + ".....!", this.Page);
                            WebMsgBox.Show("Deposit Date invalid, Only accept " + RDay + " day(s) difference from " + DD + ", A/C Will get renew As On Date.....!", this.Page);
                            TxtDepositDate.Text = Session["EntryDate"].ToString();
                            TxtProcode.Focus();
                            return;
                        }
                        else
                        {
                            TxtProcode.Focus();
                        }

                        //if (Convert.ToDateTime(conn.ConvertDate(TxtDepositDate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                        //{
                        //    WebMsgBox.Show("Deposit Date invalid.....!", this.Page);
                        //    TxtDepositDate.Text = "";
                        //    TxtDepositDate.Focus();
                        //    return;
                        //}
                        //else
                        //{
                        //    Ddl_IntPayout.Focus();
                        //}



                    }

                }
                else
                {
                    Ddl_IntPayout.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Add MINDAYRENEW parameter to HO in Parameter Creation....!", this.Page);
                TxtDepositDate.Text = "";
                TxtDepositDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDepositDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CheckDepoistdate();
            string PT = Ddl_PerioDtype.SelectedItem.Text;
            if (PT == "Days" || PT == "DAYS")
                PT = "D";
            else if (PT == "Months" || PT == "MONTHS")
                PT = "M";

            if (TxtPeriod.Text != "" && TxtDepositDate.Text != "")
            {
                TxtDueDate.Text = conn.AddMonthDay(TxtDepositDate.Text, TxtPeriod.Text, PT).Replace("12:00:00", "");

            }
            Ddl_IntPayout.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string RT = "";

            string ParaWhichRate = CMN.GetUniversalPara("RENEWRATE_CHANGE");
            string RR = "";

            if (ParaWhichRate == "Y")// added for MAMCO Requirement on 18-04-2018
            {
                string ParaChDays = CMN.GetUniversalPara("RENEWRATE_DAYS");
                string Daydiff = conn.GetDayDiff(TxtDepositDate.Text, Session["EntryDate"].ToString());

                if (Convert.ToInt32(Daydiff) <= Convert.ToInt32(ParaChDays))
                {
                    RR = CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, "", "", ViewState["ACCTYPE"].ToString(), Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text, TxtDepositDate.Text, Session["EntryDate"].ToString(), true, "NOACC");
                }
                else
                {
                    RR = CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, "", "", ViewState["ACCTYPE"].ToString(), Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), true, "NOACC");
                }
            }

            else
            {
                RR = CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, "", "", ViewState["ACCTYPE"].ToString(), Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text, TxtDepositDate.Text, Session["EntryDate"].ToString(), true, "NOACC");
            }


            if (RR != null)
            {
                TxtRate.Text = RR;
                RT = TxtRate.Text;
                string PT = Ddl_PerioDtype.SelectedItem.Text;
                if (PT == "Days" || PT == "DAYS")
                    PT = "D";
                else if (PT == "Months" || PT == "MONTHS")
                    PT = "M";
                if (RT != "")
                {
                    // TxtDepoAmt.Text = TxtTotalAmount.Text;
                    double RATE = Convert.ToDouble(RT);
                    TxtDueDate.Text = conn.AddMonthDay(TxtDepositDate.Text, TxtPeriod.Text, PT).Replace("12:00:00", "");
                    CalculatedepositINT(float.Parse(TxtDepoAmt.Text), TxtProcode.Text, float.Parse(RATE.ToString()), Convert.ToInt32(TxtPeriod.Text), Ddl_IntPayout.SelectedItem.Text, Ddl_PerioDtype.SelectedValue);
                }
                BtnSubmit.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Period, Add from Interest Master....!", this.Page);
                TxtPeriod.Text = "";
                TxtPeriod.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtRate_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTrfProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string PRD = BD.GetAccTypeGL(TxtTrfProcode.Text, Session["BRCD"].ToString());
            if (PRD != null)
            {
                string[] PR = PRD.Split('_');
                TxtTrfProName.Text = PR[0].ToString();
                ViewState["TRFGL"] = PR[1].ToString();
                TxtTrfAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Product code....!", this.Page);
                TxtTrfProcode.Text = "";
                TxtTrfProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTrfAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = CMN.GetCustNoNameGL1(TxtTrfProcode.Text.ToString(), TxtTrfAccno.Text.ToString(), Session["BRCD"].ToString());

            if (dt.Rows.Count > 0)
            {
                TxtTrfAccName.Text = dt.Rows[0][3].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number not found....!", this.Page);
                TxtTrfAccno.Text = "";
                TxtTrfAccName.Text = "";
                TxtTrfAccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(TxtDepoAmt.Text) > Convert.ToDouble(TxtDiff.Text))
            {
                WebMsgBox.Show("Inavlid Amount........!", this.Page);
                TxtDepoAmt.Text = "";
                TxtDepoAmt.Focus();
            }
            Ddl_PerioDtype.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Buttons click
    protected void Btn_SubmitWr_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = RN.DeleteData("DELETE", "ALL", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());

            string ChqNo = "";
            string ACTINT = "", PMTMODEINT = "";

            if (TxtWrPrdcode.Text == "")
            {
                WebMsgBox.Show("Enter Transfer Product code....!", this.Page);
                TxtWrPrdcode.Text = "";
                TxtWrPrdName.Text = "";
                TxtWrPrdcode.Focus();
                return;
            }
            if (TxtWrAccno.Text == "")
            {
                WebMsgBox.Show("Enter Transfer Account number....!", this.Page);
                TxtWrAccno.Text = "";
                TxtWrAccName.Text = "";
                TxtWrAccno.Focus();
                return;
            }
            if (Convert.ToDouble(TxtWrAmount.Text) <= 0)
            {
                WebMsgBox.Show("Invalid amount for transfer....!", this.Page);
                TxtWrAmount.Text = "";
                TxtWrAmount.Focus();
                return;
            }
            string ACT = "", PMTMD = "";
            if (TxtWrPrdcode.Text != "99" && ViewState["GLGroup"].ToString() != "CBB")
            {
                ACT = "7";
                PMTMD = "TR";
                ViewState["IntTrf_Type"] = "TR";
                string TRNFA = CMN.GetUniversalPara("TRNFA_YN");
                if (TRNFA != null && TRNFA == "Y")
                {
                    ACTINT = "10";
                    PMTMODEINT = "TR-NFA";
                }
                else
                {
                    ACTINT = "10";
                    PMTMODEINT = "TR-INT";
                }

                ChqNo = "0";
            }
            else if (TxtWrPrdcode.Text == "99" && Rdb_WithReceipt.Checked == false)
            {
                ACT = "4";
                PMTMD = "CP";
                ACTINT = "4";
                PMTMODEINT = "CP";
                ChqNo = "0";
                ViewState["IntTrf_Type"] = "CP";
            }
            else if (DdlPayType.SelectedValue == "4" || ViewState["GLGroup"].ToString() == "CBB")
            {
                ACT = "6";
                PMTMD = "TR";
                ACTINT = "6";
                PMTMODEINT = "TR";
                ChqNo = TxtInstNumber.Text;
                ViewState["IntTrf_Type"] = "TR";
            }
            else
            {
                ACT = "3";
                PMTMD = "CR";
                ACTINT = "3";
                PMTMODEINT = "CR";
                ChqNo = "0";
                ViewState["IntTrf_Type"] = "CR";
            }
            #region Single Closure
            if (ViewState["MS"].ToString() == "N") // For Single Clsoure 
            {

                if (Rdb_WithReceipt.Checked != true)
                {

                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), Convert.ToDouble(ViewState["INTPAY"].ToString()) != Convert.ToDouble(TxtWrAmount.Text) ? ViewState["INTPAY"].ToString() : TxtWrAmount.Text, "0", "2", ACTINT, PMTMODEINT, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "INTYES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString(), ChqNo);
                    if (Result > 0)
                    {

                        if (Convert.ToDouble(ViewState["INTPAY"].ToString()) != Convert.ToDouble(TxtWrAmount.Text))
                        {
                            double IntExcAmt = Convert.ToDouble(TxtWrAmount.Text) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                            ViewState["IntExtAmt"] = IntExcAmt.ToString();

                            Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(),
                                 IntExcAmt.ToString(),
                                "0", "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(),
                                Session["EntryDate"].ToString(), "INTYES", ViewState["REFID"].ToString(),
                                ViewState["RecSrno"].ToString(), ChqNo);
                        }
                        else
                        {
                            ViewState["IntExtAmt"] = "0";
                        }


                        Result = RN.InsertEntry("ADD", ViewState["CRGL"].ToString(), TxtWrPrdcode.Text, TxtWrPrdcode.Text == "99" ? "0" : TxtWrAccno.Text, TxtWrAmount.Text, "0", "1", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "INTYES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString(), ChqNo);
                        if (Result > 0)
                        {
                            ViewState["InterestCredited"] = TxtWrAmount.Text;

                            WebMsgBox.Show("Submitted Successfully......!", this.Page);

                            ValidCash = true;
                            ClearDataWR();
                        }
                    }



                    double DiffAmount = 0;
                    DiffAmount = Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString());

                    TxtDiff.Text = DiffAmount.ToString();
                    TxtTotalAmount.Text = DiffAmount.ToString();
                }
                else
                {

                    Result = RN.InsertEntry("ADD", ViewState["CRGL"].ToString(), TxtWrPrdcode.Text, TxtWrPrdcode.Text == "99" ? "0" : TxtWrAccno.Text, TxtWrAmount.Text, "0", "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "WITHREC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                    if (Result > 0)
                    {

                        ViewState["InterestCredited"] = TxtWrAmount.Text;

                        WebMsgBox.Show("Submitted Successfully......!", this.Page);

                        ValidCash = true;
                        ClearDataWR();
                    }


                    double DiffAmount = 0;
                    DiffAmount = Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString());

                    TxtDiff.Text = DiffAmount.ToString();
                    TxtTotalAmount.Text = DiffAmount.ToString();
                }

            }// Single Closure Ends
            #endregion

            #region Multiple Closure
            //For Multiple Closure
            else
            {
                if (Rdb_WithReceipt.Checked != true)
                {
                    Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", TxtWrAmount.Text, ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "INTYES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                    if (Result > 0)
                    {

                        Result = RN.InsertEntry("ADD", ViewState["CRGL"].ToString(), TxtWrPrdcode.Text, TxtWrAccno.Text, TxtWrAmount.Text, "0", "1", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "INTYES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                        if (Result > 0)
                        {
                            ViewState["InterestCredited"] = TxtWrAmount.Text;

                            WebMsgBox.Show("Submitted Successfully......!", this.Page);

                            ValidCash = true;
                            ClearDataWR();
                        }
                    }
                    double DiffAmount = 0;
                    DiffAmount = Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString());

                    TxtDiff.Text = DiffAmount.ToString();
                    TxtTotalAmount.Text = DiffAmount.ToString();
                }
                else
                {

                    Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", TxtWrAmount.Text, ViewState["CT"].ToString(), "1", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "WITHREC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                    if (Result > 0)
                    {
                        Result = RN.InsertEntry("ADD", ViewState["CRGL"].ToString(), TxtWrPrdcode.Text, TxtWrAccno.Text, TxtWrAmount.Text, "0", "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "WITHREC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                        ViewState["InterestCredited"] = TxtWrAmount.Text;

                        WebMsgBox.Show("Submitted Successfully......!", this.Page);

                        ValidCash = true;
                        ClearDataWR();
                    }

                    double DiffAmount = 0;
                    DiffAmount = Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString());

                    TxtDiff.Text = DiffAmount.ToString();
                    TxtTotalAmount.Text = DiffAmount.ToString();
                }

            }
            //For Multiple Closure Ends
            #endregion

            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = RN.DeleteData("DELETE", "INTYES", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            string ACTINT, PMTMODEINT;

            string TRNFA = CMN.GetUniversalPara("TRNFA_YN");
            if (TRNFA != null && TRNFA == "Y")
            {
                ACTINT = "10";
                PMTMODEINT = "TR-NFA";
            }
            else
            {
                ACTINT = "10";
                PMTMODEINT = "TR-INT";
            }

            if (Rdb_PrinWithInt.Checked == false && Rdb_OnlyPrin.Checked == false && Rdb_WithReceipt.Checked == false)
            {
                WebMsgBox.Show("Select Renewal Type.....!", this.Page);
                return;
            }
            if (Rdb_Multiple.Checked == false && Rdb_Single.Checked == false)
            {

                WebMsgBox.Show("Select Renewal Mode.....!", this.Page);
                return;
            }


            string ACT = "", PMTMD = "";
            ACT = "7";
            PMTMD = "TR";
            string STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
            string IntPaidPara = FDR.GetIntPaidtoPara("1");
            string Int10 = "", Int5 = "";
            if (ViewState["IPostType"].ToString() == "REV")
            {
                Int10 = "1";
                Int5 = "2";
            }
            else
            {
                Int10 = "1";
                Int5 = "2";
            }

            if (IntPaidPara == "N")
            {   //Changed on 29-11-2017 not to credit interest in 5 -Abhishek
                #region For Singular Closure
                if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == false && Rdb_OnlyPrin.Checked == false)// For Single Closure
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == (Rdb_Multiple.Checked == false ? Convert.ToDouble(TxtDiff.Text) : Convert.ToDouble(TxtAcPayment.Text)))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                    //  Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "1", "7", "TR-INT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString());

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        Btn_PostEntry.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }// For Single Closure End
                #endregion

                #region For Singular Closure only Principle
                if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == false && Rdb_PrinWithInt.Checked == false)// For Single Closure
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }
                                string ExistingFDAmt = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["IntExtAmt"].ToString())).ToString();

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ExistingFDAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                                if (Result > 0)
                                {

                                    // Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR-INT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString());

                                    //  Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "1", "7", "TR-INT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString());

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        Btn_PostEntry.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }// For Single Closure End
                #endregion
                //Above Changed on 29-11-2017 not to credit interest in 5 -Abhishek


               //Not changed on 29-11-2017 not to credit interest in 5 -Abhishek
                #region For Multiple Closure Principal with Int
                else if (ViewState["MS"].ToString() == "Y" && Rdb_OnlyPrin.Checked == false && Rdb_WithReceipt.Checked == false) // For Multiple Closure Principal with interest
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtAcPayment.Text))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Principal with Interest
                #endregion

                #region For Multiple Closure Only Principal

                // For Multiple Closure Only Principal
                else if (ViewState["MS"].ToString() == "Y" && Rdb_PrinWithInt.Checked == false && Rdb_WithReceipt.Checked == false)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", TxtDepoAmt.Text, ViewState["CT"].ToString(), "2", "7", "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion
                //Not changed on 29-11-2017 not to credit interest in 5 -Abhishek


                #region For Single Closure With Receipt Single Renew

                else if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == true && Rdb_Single.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        Result = RN.InsertEntry("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["InterestCredited"].ToString(), ViewState["CT"].ToString(), "1", "3", "CR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "WRCONTRA", ViewState["REFID"].ToString(), "0");

                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), (Convert.ToDouble(TxtDepoAmt.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString(), "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true || Rdb_WithReceipt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());


                                    BtnSubmit.Visible = false;
                                    Btn_PostEntry.Visible = true;
                                    WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                    Btn_PostEntry.Focus();
                                }
                                else
                                {
                                    WebMsgBox.Show("Opeartion failed....!", this.Page);
                                }


                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion

                #region For Single Closure With Receipt Multiple Renew

                else if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == true && Rdb_Multiple.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }



                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text) || (Rdb_WithReceipt.Checked == true && Convert.ToDouble(TxtDiff.Text) == 0))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true || Rdb_WithReceipt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());


                                    BtnSubmit.Visible = false;
                                    Btn_PostEntry.Visible = true;
                                    WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                    Btn_PostEntry.Focus();
                                }
                                else
                                {
                                    WebMsgBox.Show("Opeartion failed....!", this.Page);
                                }


                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion

                #region For Multiple Closure With Receipt

                // For MFor Multiple Closure With Receipt
                else if (ViewState["MS"].ToString() == "Y" && Rdb_WithReceipt.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        double Amt = Convert.ToDouble(TxtDepoAmt.Text);

                        Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", Amt.ToString(), ViewState["CT"].ToString(), "2", "7", "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text) || (Rdb_WithReceipt.Checked == true && Convert.ToDouble(TxtDiff.Text) == 0))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {
                                if (Rdb_WithReceipt.Checked == false)
                                {

                                    WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);
                                }
                                else
                                {
                                    WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text)).ToString() + "....!", this.Page);
                                }

                            }
                        }
                    }
                }
                // 
                #endregion

                BindGrid();

            }

            else
            {
                #region For Singular Closure
                if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == false && Rdb_OnlyPrin.Checked == false)// For Single Closure
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == (Rdb_Multiple.Checked == false ? Convert.ToDouble(TxtDiff.Text) : Convert.ToDouble(TxtAcPayment.Text)))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACTINT, PMTMODEINT, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                    Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "1", ACTINT, PMTMODEINT, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        Btn_PostEntry.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }// For Single Closure End
                #endregion

                #region For Singular Closure only Principle
                if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == false && Rdb_PrinWithInt.Checked == false)// For Single Closure
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }
                                //  string ExistingFDAmt = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                                string ExistingFDAmt = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["IntExtAmt"].ToString())).ToString();

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ExistingFDAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                                if (Result > 0)
                                {

                                    // Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR-INT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString());
                                    //Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["InterestCredited"].ToString(), ViewState["CT"].ToString(), "1", "10", "TR-INT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
                                    //Above commented on 30-07-2018 

                                    Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "1", ACTINT, PMTMODEINT, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());


                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        Btn_PostEntry.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }// For Single Closure End
                #endregion

                #region For Multiple Closure Principal with Int
                else if (ViewState["MS"].ToString() == "Y" && Rdb_OnlyPrin.Checked == false && Rdb_WithReceipt.Checked == false) // For Multiple Closure Principal with interest
                {

                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtAcPayment.Text))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + TxtDiff.Text + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Principal with Interest
                #endregion

                #region For Multiple Closure Only Principal

                // For Multiple Closure Only Principal
                else if (ViewState["MS"].ToString() == "Y" && Rdb_PrinWithInt.Checked == false && Rdb_WithReceipt.Checked == false)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", TxtDepoAmt.Text, ViewState["CT"].ToString(), "2", "7", "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion

                #region For Single Closure With Receipt Single Renew

                else if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == true && Rdb_Single.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        Result = RN.InsertEntry("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["InterestCredited"].ToString(), ViewState["CT"].ToString(), "1", ViewState["IntTrf_Type"].ToString() == "TR" ? "7" : "3", ViewState["IntTrf_Type"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "WRCONTRA", ViewState["REFID"].ToString(), "0");


                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), (Convert.ToDouble(TxtDepoAmt.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString(), "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true || Rdb_WithReceipt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());


                                    BtnSubmit.Visible = false;
                                    Btn_PostEntry.Visible = true;
                                    WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                    Btn_PostEntry.Focus();
                                }
                                else
                                {
                                    WebMsgBox.Show("Opeartion failed....!", this.Page);
                                }


                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion

                #region For Single Closure With Receipt Multiple Renew

                else if (ViewState["MS"].ToString() == "N" && Rdb_WithReceipt.Checked == true && Rdb_Multiple.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }



                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }
                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text) || (Rdb_WithReceipt.Checked == true && Convert.ToDouble(TxtDiff.Text) == 0))
                            {
                                double DrAmt = 0;
                                if (Rdb_PrinWithInt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString());
                                }
                                else if (Rdb_OnlyPrin.Checked == true || Rdb_WithReceipt.Checked == true)
                                {
                                    DrAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                                }

                                Result = RN.InsertEntry("ADD", ViewState["GLC"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), DrAmt.ToString(), ViewState["CT"].ToString(), "2", ACT, PMTMD, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "EXISTACC", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                                if (Result > 0)
                                {

                                    Result = RN.InsertEntry("ADD", "10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), ViewState["CT"].ToString(), "2", ACT, "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());


                                    BtnSubmit.Visible = false;
                                    Btn_PostEntry.Visible = true;
                                    WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                    Btn_PostEntry.Focus();
                                }
                                else
                                {
                                    WebMsgBox.Show("Opeartion failed....!", this.Page);
                                }


                            }
                            else
                            {

                                WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text)).ToString() + "....!", this.Page);

                            }
                        }
                    }
                }
                // For Multiple Closure Only Principal Ends
                #endregion

                #region For Multiple Closure With Receipt

                // For MFor Multiple Closure With Receipt
                else if (ViewState["MS"].ToString() == "Y" && Rdb_WithReceipt.Checked == true)
                {
                    if (STR == "MIS" && (TxtTrfProcode.Text == "" || TxtTrfAccno.Text == ""))
                    {
                        WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                        TxtTrfProcode.Text = "";
                        TxtTrfProcode.Focus();
                        return;
                    }
                    else
                    {
                        string PARTI = "";
                        if (Rdb_Single.Checked != true)
                        {
                            PARTI = "MNEWACC";
                        }
                        else
                        {
                            PARTI = "NEWACC";
                        }

                        double Amt = Convert.ToDouble(TxtDepoAmt.Text);

                        Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", Amt.ToString(), ViewState["CT"].ToString(), "2", "7", "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());

                        Result = RN.InsertFD("ADD", ViewState["GL"].ToString(), TxtProcode.Text, "9999", ViewState["CT"].ToString(), TxtDepoAmt.Text, "1",
                                           TxtDepositDate.Text, TxtDepoAmt.Text, TxtRate.Text, Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text,
                                           Convert.ToDouble(TxtIntrest.Text).ToString(), TxtDueDate.Text, Convert.ToDouble(TxtMaturity.Text).ToString(), "0", TxtTrfProcode.Text,
                                           TxtTrfAccno.Text, "1003", Ddl_IntPayout.SelectedValue.ToString(), TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(),
                                           ViewState["ACCTYPE"].ToString(), ACT, PMTMD, PARTI.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["REFID"].ToString());
                        if (Result > 0)
                        {
                            if (Rdb_Multiple.Checked == true)
                            {
                                CheckMultiple();
                            }

                            if (Convert.ToDouble(TxtAmtColl.Text) == Convert.ToDouble(TxtDiff.Text) || (Rdb_WithReceipt.Checked == true && Convert.ToDouble(TxtDiff.Text) == 0))
                            {

                                if (Result > 0)
                                {

                                    if (Result > 0)
                                    {
                                        BtnSubmit.Visible = false;
                                        BtnPostMultiple.Visible = true;
                                        WebMsgBox.Show("Submitted Succesfully....!", this.Page);
                                        Btn_PostEntry.Focus();
                                    }
                                    else
                                    {
                                        WebMsgBox.Show("Opeartion failed....!", this.Page);
                                    }
                                }

                            }
                            else
                            {
                                if (Rdb_WithReceipt.Checked == false)
                                {

                                    WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString() + "....!", this.Page);
                                }
                                else
                                {
                                    WebMsgBox.Show("Submitted Succesfully, Amount remaining " + (Convert.ToDouble(TxtDiff.Text)).ToString() + "....!", this.Page);
                                }

                            }
                        }
                    }
                }
                // 
                #endregion

                BindGrid();
            }

            if (Btn_PostEntry.Visible == true)
            {
                Result = RN.InsertNOMINEE("NOMI", "NOMINEE", ViewState["GL"].ToString(), ViewState["PRD"].ToString(), ViewState["ACC"].ToString(), ViewState["CT"].ToString(), "1003", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), TxtNominee_1.Text == "" ? "0" : TxtNominee_1.Text, TxtNominee_2.Text == "" ? "0" : TxtNominee_2.Text, ddlRelation_1.SelectedValue.ToString(), ddlRelation_2.SelectedValue.ToString(), RdbGender_1.SelectedValue.ToString(), RdbGender_2.SelectedValue.ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_PostEntry_Click(object sender, EventArgs e)
    {
        try
        {
            string RType = "";
            if (Rdb_Single.Checked == true)
            {
                RType = "S";
            }
            else if (Rdb_Multiple.Checked == true)
            {
                RType = "M";
            }
            string RES = RN.PostSingle("POST", RType, Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["RecSrno"].ToString());
            if (RES != null)
            {

                WebMsgBox.Show("Response : " + RES, this.Page);

                ValidCash = false;
                ValidTrf = false;

                ClearData();

                String x = "<script type='text/javascript'>self.close();</script>";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", x, false);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "RefreshAutho();", true);
                return;

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPostMultiple_Click(object sender, EventArgs e)
    {
        try
        {
            string PR = Ddl_PerioDtype.SelectedValue.ToString();


            string TGLC = "0", TSUBGL = "0", TACNO = "0";
            if (TxtTrfProcode.Text != "" || TxtTrfAccno.Text != "")
            {

                TSUBGL = TxtTrfProcode.Text;
                TACNO = TxtTrfAccno.Text;
                string[] T = BD.GetAccTypeGL(TSUBGL, Session["BRCD"].ToString()).Split('_');
                TGLC = T[1].ToString();
            }


            double IntAmt = 0;
            double MatAmt = 0;
            string RES = FDR.PostMultipleClosure("POSTMULTIPLE", "S", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["MSETNO"].ToString(), "X_" + Session["MID"].ToString(), Session["MID"].ToString(),
                                                TxtProcode.Text, TxtDepoAmt.Text, ViewState["CT"].ToString(), ViewState["ACCTYPE"].ToString(), TxtReceiptNo.Text, TxtRate.Text, PR, TxtPeriod.Text, TxtDueDate.Text, IntAmt.ToString(),
                                                MatAmt.ToString(), Ddl_IntPayout.SelectedValue.ToString(), TGLC, TSUBGL, TACNO, TxtDepositDate.Text);
            if (RES != null)
            {
                WebMsgBox.Show(RES, this.Page);
                ClearData();
                BtnSubmit.Visible = true;
                BtnPostMultiple.Visible = false;

            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region User Functions
    public void Show_Details()
    {
        try
        {
            string RR = CMN.GetUniversalPara("RENEW_MODE");
            if (RR != null)
            {
                if (RR == "1")
                {
                    Rdb_Single.Visible = true;
                    Rdb_Multiple.Visible = false;
                }
                else
                {
                    Rdb_Single.Visible = true;
                    Rdb_Multiple.Visible = true;
                }
            }
            else
            {
                Rdb_Single.Visible = true;
                Rdb_Multiple.Visible = true;
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
            RN.BindGridData("BINDGRID", GrdFDLedger, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;
            float QUATERS = 0;
            float tmp1 = 0;
            float tmp2 = 0;
            //string sql = "select GLGROUP from glmast where glcode=5 AND SUBGLCODE='"+subgl+"'";
            string sql = "SELECT CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + subgl + "'";
            string category = conn.sExecuteScalar(sql);

            if (category == "")
            {
                return;
            }
            switch (category)
            {
                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "MIS":
                    string ParaMis = CMN.GetUniversalPara("MIS_REG");
                    if (Ddl_IntPayout.SelectedValue == "1") // Monthly
                    {
                        if (ParaMis == "N") // For Danda
                        {
                            interest = Convert.ToInt32(amt * intrate / (1200));
                        }
                        else
                        {
                            interest = Convert.ToInt32(amt * intrate / (1200 + intrate));
                        }
                        maturityamt = amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (Ddl_IntPayout.SelectedValue == "2") //Quaterly
                    {
                        interest = Convert.ToInt32(amt * intrate * 3 / (1200));
                        // maturityamt = interest + amt;
                        maturityamt = amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (Ddl_IntPayout.SelectedValue == "3") // Half Yearly
                    {
                        interest = Convert.ToInt32(amt * intrate * 6 / (1200));
                        // maturityamt = interest + amt;
                        maturityamt = amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (Ddl_IntPayout.SelectedValue == "4")
                    {
                        WebMsgBox.Show("Invalid interest payout...!", this.Page);
                        Ddl_IntPayout.SelectedValue = "0";
                        Ddl_IntPayout.Focus();
                    }
                    else if (Ddl_IntPayout.SelectedValue == "5")
                    {
                        interest = Convert.ToInt32(amt * intrate * 12 / (1200));
                        // maturityamt = interest + amt;
                        maturityamt = amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }

                    break;

                case "QIS":
                    interest = amt * intrate / 100 / 4;
                    maturityamt = amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "CUM":

                    float PrmAmt = amt;
                    string CALCTYPE = CurrentCls.GetCUMCal(Session["BRCD"].ToString(), TxtProcode.Text, "CUM");

                    if (CALCTYPE == "Y" || CALCTYPE == "y")
                    {
                        QUATERS = (Period / 12);
                        for (int i = 1; i <= QUATERS; i++)
                        {
                            interest = amt * intrate / 100;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }

                    }
                    else if (CALCTYPE == "H" || CALCTYPE == "h")
                    {
                        QUATERS = (Period / 6);
                        for (int i = 1; i <= QUATERS; i++)
                        {
                            interest = amt * intrate / 100 / 2;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }


                    }
                    else if (CALCTYPE == "Q" || CALCTYPE == "q")
                    {
                        QUATERS = (Period / 3);
                        for (int i = 1; i <= QUATERS; i++)
                        {
                            interest = amt * intrate / 100 / 4;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }
                    else if (CALCTYPE == "M" || CALCTYPE == "m")
                    {
                        QUATERS = (Period / 1);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 12;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }
                    //else if (CALCTYPE == "D")
                    //{
                    //    QUATERS = (Period / 3);
                    //}
                    else
                    {
                        QUATERS = (Period / 3);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 4;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }
                    interest = 0;
                    //Added On 20-08-2018 by Abhishek as per DANDA Requirement.
                    //Added On 20-08-2018 by Abhishek as per DANDA Requirement.
                    int Rem = 0;
                    double TotIntRem = 0;
                    Rem = (int)Period % (int)QUATERS;

                    for (int i = 1; i <= Rem; i++)
                    {
                        interest = maturityamt * intrate / 100 / 12;
                        TotIntRem = TotIntRem + interest;
                    }

                    if (interest > 0)
                    {
                        maturityamt = maturityamt + (float)TotIntRem;
                    }

                    //maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt);
                    interest = maturityamt - PrmAmt;
                    TxtIntrest.Text = Math.Round(interest, 0).ToString("N");
                    TxtMaturity.Text = Math.Round(maturityamt, 0).ToString("N");

                    TxtReceiptNo.Focus();
                    break;


                case "FDS":
                    if (intpay == "ON MATURITY")
                    {
                        if (PTYPE == "D" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                        else if (PTYPE == "M" || PTYPE == "maihnao")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = amt; //Val(interest) + Val(amt);                    
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Monthly" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / (1200 + intrate));
                        maturityamt = amt; //Val(interest) + Val(amt);
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    break;

                case "FDSS":
                    if (intpay == "ON MATURITY")
                    {
                        if (PTYPE == "D" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                        else if (PTYPE == "M" || PTYPE == "maihnao")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = amt; //Val(interest) + Val(amt);                    
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Monthly" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / (1200 + intrate));
                        maturityamt = amt; //Val(interest) + Val(amt);
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    break;

                case "DP":
                    if (PTYPE == "Days" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (PTYPE == "Months" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    break;
                case "RD":
                    DataTable DT1 = CurrentCls.GetRDCalc("RD", amt.ToString(), Ddl_IntPayout.SelectedValue.ToString(), Ddl_PerioDtype.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT1 != null)
                    {
                        string intamt = DT1.Rows[0]["Interest"].ToString();
                        string MatAmt = DT1.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    //float deamt = amt;
                    //float tempamt = amt;
                    //string SQL = "";
                    //float temprate = intrate;
                    //conn.sExecuteQuery("delete from RRD");

                    //string RDPARA = conn.sExecuteScalar("SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='RDINT'");
                    ////float RDPARA = Convert.ToInt32(RDPARAstring);

                    //if (RDPARA == "")
                    //{
                    //    WebMsgBox.Show("Create parameter RDINT AND VALUE Monthly(M) or Quaterly(Q)", this.Page);
                    //    return;
                    //}
                    //else
                    //{
                    //    if (RDPARA == "M")
                    //    {
                    //        int i = 1;
                    //        int srno = 1;
                    //        float deamt1 = deamt;
                    //        for (i = 1; i <= (Period / 1); i++)
                    //        {
                    //            float int1 = (deamt1 * temprate) / 1200;
                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values(' " + srno + " ' , ' " + deamt + " ' , ' " + deamt1 + " ' , ' " + int1 + " ' )";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int2 = (deamt1 * temprate) / 1200;
                    //        }

                    //        SQL = "select sum(interest) from RRD";
                    //        string interest1 = conn.sExecuteScalar(SQL);
                    //        double MAMT = Convert.ToDouble(deamt * Period) + Convert.ToDouble(interest1);
                    //        //maturityamt = (deamt * Period) +interest1F;
                    //        //TxtIntrest.Text = interest1.ToString();
                    //        tmp1 = (float)Convert.ToDouble(interest1);
                    //        TxtIntrest.Text = tmp1.ToString("N");
                    //        tmp2 = (float)Convert.ToDouble(MAMT);
                    //        TxtMaturity.Text = tmp2.ToString("N");
                    //    }
                    //    else if (RDPARA == "Q")
                    //    {
                    //        int i = 1;
                    //        int srno = 1;
                    //        float deamt1 = deamt;

                    //        for (i = 1; i <= (Period / 3); i++)
                    //        {
                    //            float int1 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int1 + "')";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int2 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int2 + "' )";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;

                    //            deamt1 = deamt1 + deamt;
                    //            float int3 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int3 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;

                    //            float TOTINT = int1 + int2 + int3;
                    //            deamt1 = deamt1 + deamt + TOTINT;
                    //            float int4 = (deamt1 * temprate) / 1200;
                    //        }
                    //        SQL = "select sum(interest) from RRD";
                    //        interest = Convert.ToInt32(conn.sExecuteScalar(SQL));
                    //        maturityamt = (deamt * Period) + interest;
                    //        TxtIntrest.Text = interest.ToString("N");
                    //        TxtMaturity.Text = maturityamt.ToString("N");
                    //    }
                    //}
                    break;

                default:
                    WebMsgBox.Show("Record not found", this.Page);
                    break;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        TxtWrPrdcode.Text = "";
        TxtWrPrdName.Text = "";
        TxtWrAccno.Text = "";
        TxtWrAccName.Text = "";
        TxtWrAmount.Text = "0";
        TxtDepoAmt.Text = "";
        TxtMaturity.Text = "";
        TxtTotalAmount.Text = "";
        DdlPayType.SelectedValue = "0";
        TxtPrdName.Text = "";
        Ddl_PerioDtype.SelectedValue = "0";
        Ddl_IntPayout.SelectedValue = "0";
        TxtPeriod.Text = "";
        TxtProcode.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtReceiptNo.Text = "";
        //TxtProcode.Focus();

    }
    public void ClearFD()
    {
        TxtProcode.Text = "";
        TxtPrdName.Text = "";
        TxtPrdName.Text = "";
        Ddl_Prdname.SelectedValue = "0";
        TxtDueDate.Text = "";
        Ddl_IntPayout.SelectedValue = "0";
        Ddl_PerioDtype.SelectedValue = "0";
        TxtPeriod.Text = "";
        TxtMaturity.Text = "";
        TxtIntrest.Text = "";
    }
    public void ClearDataWR()
    {
        TxtWrPrdcode.Text = "";
        TxtWrPrdName.Text = "";
        TxtWrAccno.Text = "";
        TxtWrAccName.Text = "";
        TxtWrAmount.Text = "0";
    }
    public void CheckMultiple()
    {
        try
        {
            if (ViewState["MS"].ToString() == "N")
            {
                if (Rdb_OnlyPrin.Checked == true)
                {

                    //  TxtAmtColl.Text = (Convert.ToDouble(RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString()))-Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();
                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();

                }
                else if (Rdb_WithReceipt.Checked == true)
                {
                    string TotalRec = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();

                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TotalRec.ToString()) - Convert.ToDouble(TxtAmtColl.Text)).ToString();

                }
                else
                {

                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString();

                }
                TxtDepoAmt.Enabled = true;
                ClearFD();
                string DD;
                DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString(), ViewState["RecSrno"].ToString());
                if (DD != null)
                {
                    TxtDepositDate.Text = DD.ToString();
                }
                TxtDepoAmt.Text = "0";
                ViewState["CTYPE"] = "M";
            }
            else
            {
                if (Rdb_OnlyPrin.Checked == true)
                {

                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();

                }
                else if (Rdb_WithReceipt.Checked == true)
                {
                    string TotalRec = (Convert.ToDouble(TxtAcPayment.Text) + Convert.ToDouble(ViewState["InterestCredited"].ToString())).ToString();

                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TotalRec.ToString()) - Convert.ToDouble(TxtAmtColl.Text)).ToString();

                }
                else
                {

                    TxtAmtColl.Text = RN.GetBalance("BM", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    TxtDiff.Text = (Convert.ToDouble(TxtAcPayment.Text) - Convert.ToDouble(TxtAmtColl.Text)).ToString();

                }
                TxtDepoAmt.Enabled = true;
                ClearFD();
                string DD;
                TxtDepoAmt.Text = "0";
                ViewState["CTYPE"] = "M";
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void InsertNetPaid(string Amount)
    {
        try
        {
            Result = RN.InsertEntry("ADD", ViewState["GLNetPaid"].ToString(), ViewState["NetPaid"].ToString(), "0", Amount.ToString(), ViewState["CT"].ToString(), "1", "7", "TR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "YES", ViewState["REFID"].ToString(), ViewState["RecSrno"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    protected void TxtInstNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int ChqLen = 0;
            string Chqno_Text = "";
            Chqno_Text = TxtInstNumber.Text;
            ChqLen = Chqno_Text.Length;
            if (ChqLen > 6 || ChqLen < 6)
            {
                WebMsgBox.Show("Enter 6 digit Instrument number ....!!", this.Page);
                TxtInstNumber.Text = "";
                TxtInstNumber.Focus();
            }
            else
            {
                Btn_SubmitWr.Focus();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void Chk_Modify_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            NomineeOperate();
            TxtNominee_1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void NomineeOperate()
    {
        if (Chk_Modify.Checked == true)
        {
            TxtNominee_1.Enabled = true;
            TxtNominee_2.Enabled = true;
            ddlRelation_1.Enabled = true;
            ddlRelation_2.Enabled = true;
            RdbGender_1.Enabled = true;
            RdbGender_2.Enabled = true;

        }
        else
        {

            TxtNominee_1.Enabled = false;
            TxtNominee_2.Enabled = false;
            ddlRelation_1.Enabled = false;
            ddlRelation_2.Enabled = false;
            RdbGender_1.Enabled = false;
            RdbGender_2.Enabled = false;
        }
    }

    public void Set_PeriodType()
    {

        try
        {
            string RR = CurrentCls.Get_PeriodType(TxtProcode.Text);
            if (RR != null)
            {
                if (RR != "0")
                {
                    Ddl_PerioDtype.SelectedValue = RR;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}