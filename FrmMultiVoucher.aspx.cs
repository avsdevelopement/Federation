using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class FrmMultiVoucher : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccopen AO = new ClsAccopen();
    ClsAuthorized AT = new ClsAuthorized();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsOpenClose OC = new ClsOpenClose();
    ClsCommon CC = new ClsCommon();
    DbConnection conn = new DbConnection();
    ClsCashReciept CR = new ClsCashReciept();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string RefNumber, FL = "";
    int resultout = 0;
    string sResult = "", ST = "", SetNo = "";
    double DebitAmt, PrincAmt, TotalDrAmt, TotalAmt = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LnkVerify.Visible = false;
            autoglname.ContextKey = Session["BRCD"].ToString();

            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                TxtChequeDate.Text = Session["EntryDate"].ToString();
                DdlCRDR.SelectedValue = "2";
                //MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                Getinfo();
                BindGrid();
                string FL = Request.QueryString["FL"].ToString();
                ViewState["FL"] = FL;
                if (FL == "ACO")
                {
                    string P = Request.QueryString["P"].ToString();
                    string A = Request.QueryString["A"].ToString();
                    ViewState["PCode"] = string.IsNullOrEmpty(P.ToString()) ? "0" : P.ToString();
                    ViewState["ACode"] = string.IsNullOrEmpty(A.ToString()) ? "0" : A.ToString();
                    SetFDAmt(ViewState["PCode"].ToString(), ViewState["ACode"].ToString());
                }

                
            }
            BindGridRec();
            rbtnTransferType.Focus();
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void SetFDAmt(string Prdcode,string Acode)
    {
        try
        {
            string AMTVAL = CC.GetAMTVAL(Session["BRCD"].ToString(), Prdcode);
            if (AMTVAL != null && AMTVAL == "Y")
            {
                ViewState["AMTVAL"] = "Y";
                string FDAMT = CC.GetFDAMT(Session["BRCD"].ToString(), Prdcode, Acode,"0");
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

    #endregion

    #region Index Changed Event

    protected void DdlCRDR_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCRDR.SelectedValue == "0")
                DdlCRDR.Focus();
            else
                TxtPtype.Focus();
            if (DdlCRDR.SelectedValue == "1")
                TxtNarration.Text = "By TRF";
            else if (DdlCRDR.SelectedValue == "2")
                TxtNarration.Text = "To TRF";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdvoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdvoucher.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdvoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    #endregion

    #region Text Change Event

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = MV.Getaccno(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GlCode"] = AC[0].ToString();
                    TxtPname.Text = AC[1].ToString();
                    txtCustNo.Text = "0";
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();

                    string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtPtype.Text);
                    if (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y") //--abhishek as per GL LEVEL Requirment
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";

                        TxtAccNo.Text = TxtPtype.Text.ToString();
                        TxtCustName.Text = TxtPname.Text.ToString();
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();
                        txtCustNo.Text = "0";

                        TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0" , "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                        TxtAmount.Focus();
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                        TxtAccNo.Focus();
                    }

                    
                }
                else
                {
                    lblMessage.Text = "Enter Valid Product code...!!";
                    ModalPopup.Show(this.Page);
                    ClearText();
                    TxtPtype.Focus();
                }
            }
            else
            {
                TxtPtype.Text = "";
                TxtPname.Text = "";
                lblMessage.Text = "Product is not operating...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = TxtPname.Text.Split('_');
            if (TD.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), TD[1].ToString()).ToString() != "3")
                {
                    TxtPname.Text = TD[0].ToString();
                    TxtPtype.Text = TD[1].ToString();
                    txtCustNo.Text = "0";

                    string[] AC = MV.Getaccno(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString()).Split('_');
                    ViewState["GlCode"] = AC[0].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text;

                    string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtPtype.Text);
                    if (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y") //--abhishek as per GL LEVEL Requirment
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";

                        TxtAccNo.Text = TxtPtype.Text.ToString();
                        TxtCustName.Text = TxtPname.Text.ToString();
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();
                        txtCustNo.Text = "0";

                        TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                        TxtAmount.Focus();
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    TxtPtype.Text = "";
                    TxtPname.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Product code...!!";
                ModalPopup.Show(this.Page);
                ClearText();
                TxtPtype.Focus();
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
            string AT = "";

            if (ViewState["GlCode"].ToString() != "9")
            {
                sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
                if (sResult != "3")
                {
                    AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
                    if (AT != null)
                    {
                        if (AT != "1003" && ViewState["GlCode"].ToString() != "5")
                        {

                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                            TxtCustName.Text = CustName[0].ToString();
                            txtCustNo.Text = CustName[1].ToString();

                            txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);
                            if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)

                                lblMessage.Text = "Sorry Customer not Authorise...!!";
                            ModalPopup.Show(this.Page);
                            TxtAccNo.Text = "";
                            TxtCustName.Text = "";
                            txtCustNo.Text = "";
                            TxtBalance.Text = "";
                            TxtTotalBal.Text = "";
                            TxtAccNo.Focus();
                        }
                        else
                        {
                            DataTable DT = new DataTable();
                            DT = MV.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                            if (DT.Rows.Count > 0)
                            {
                                string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                                TxtCustName.Text = CustName[0].ToString();
                                txtCustNo.Text = CustName[1].ToString();
                                txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);
                                if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
                                {
                                    TxtAccNo.Text = "";
                                    TxtCustName.Text = "";
                                    txtCustNo.Text = "";
                                    TxtBalance.Text = "";
                                    TxtTotalBal.Text = "";
                                    TxtAccNo.Focus();
                                    WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                                    return;
                                }

                                TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                                TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();
                                TxtAmount.Enabled = true;

                                DebitAmt = MV.DebitAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                                if (DebitAmt.ToString() != "")
                                {
                                    TxtBalance.Text = (Convert.ToDouble(TxtBalance.Text) - Convert.ToDouble(DebitAmt)).ToString();
                                }

                                if (DdlCRDR.SelectedValue == "1")
                                {
                                    LnkVerify.Visible = true;
                                }

                                if (DdlCRDR.SelectedValue == "1")
                                    TxtNarration.Text = "By TRF";
                                else if (DdlCRDR.SelectedValue == "2")
                                    TxtNarration.Text = "To TRF";

                                if (ViewState["GlCode"].ToString() == "5")    // Added by Abhishek for FD amount Check 19-02-2018
                                {
                                    SetFDAmt(TxtPtype.Text, TxtAccNo.Text);
                                }
                                
                            }
                            if (TxtAccNo.Text != "" && TxtPtype.Text != "")
                            {
                                ////Displayed modal popup of voucher info by ankita 20/05/2017
                                DataTable dtmodal = new DataTable();
                                dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                if (dtmodal.Rows.Count > 0)
                                {
                                    resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
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
                                if (txtCustNo.Text.ToString() != "0")
                                {
                                    Photo_Sign();
                                    DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                                    if (DT.Rows.Count > 0)
                                        WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                                }
                            }
                            TxtAmount.Focus();
                            getMobile();
                        }
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        lblMessage.Text = "Enter Valid Account Number...!!";
                        ModalPopup.Show(this.Page);
                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    lblMessage.Text = "Acc number " + TxtAccNo.Text.ToString() + " is Closed...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Focus();
                }
            }
            else if (ViewState["GlCode"].ToString() == "9")
            {
                DT = new DataTable();
                DT = MV.GetCustName("4", TxtAccNo.Text, Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                    TxtCustName.Text = CustName[0].ToString();
                    getMobile();
                    TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(),"0", Session["EntryDate"].ToString(), "ClBal").ToString();
                    TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = TxtCustName.Text.Split('_');
            if (TD.Length > 1)
            {
                TxtCustName.Text = TD[0].ToString();
                TxtAccNo.Text = TD[1].ToString();
                txtCustNo.Text = TD[2].ToString();

                sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
                if (sResult != "3")
                {
                    txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);
                    if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        TxtAccNo.Focus();
                        WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                        return;
                    }
                    if (DdlCRDR.SelectedValue == "1")
                        TxtNarration.Text = "By TRF";
                    else if (DdlCRDR.SelectedValue == "2")
                        TxtNarration.Text = "To TRF";
                    TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                    TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();
                    TxtAmount.Enabled = true;
                    
                    
                    if (ViewState["GlCode"].ToString() == "5")  // Added by Abhishek for FD amount Check 19-02-2018
                    {
                        SetFDAmt(TxtPtype.Text, TxtAccNo.Text);
                    }


                    if (DdlCRDR.SelectedValue == "1")
                    {
                        LnkVerify.Visible = true;
                    }
                    if (TxtAccNo.Text != "" && TxtPtype.Text != "")
                    {
                        ////Displayed modal popup of voucher info by ankita 20/05/2017
                        DataTable dtmodal = new DataTable();
                        dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                        if (dtmodal.Rows.Count > 0)
                        {
                            resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
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
                        if (txtCustNo.Text.ToString() != "0")
                        {
                            Photo_Sign();
                            getMobile();
                            DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                            if (DT.Rows.Count > 0)
                                WebMsgBox.Show("Operation in defaulter account...!!", this.Page);

                        }
                    }
                    TxtAmount.Focus();
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    lblMessage.Text = "Acc number " + TxtAccNo.Text.ToString() + " is Closed...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Account Number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                txtCustNo.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCRDR.SelectedValue == "2")
            {
                //double BAL = Convert.ToDouble(TxtTotalBal.Text.Trim().ToString() == "" ? "0" : TxtTotalBal.Text.Trim().ToString()); // Commented by abhishek for checking balance on clear balance on 18-10-2017
                double BAL = Convert.ToDouble(TxtBalance.Text.Trim().ToString() == "" ? "0" : TxtBalance.Text.Trim().ToString());
                double curbal = Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString());

                if (MV.CheckMultiValidation(TxtPtype.Text, Session["BRCD"].ToString()).ToString() == "N")
                {
                    if ((Convert.ToInt32(ViewState["GlCode"].ToString()) < 100) && Convert.ToInt32(ViewState["GlCode"].ToString()) != 3)
                    {
                        if (curbal > BAL)
                        {
                            lblMessage.Text = "Insufficient Account Balance...!!";
                            TxtAmount.Text = "";
                            TxtAmount.Focus();
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                }
            }

            if (rbtnTransferType.SelectedValue == "T")
                TxtNarration.Focus();
            else if (rbtnTransferType.SelectedValue == "C")
                TxtChequeNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnTransferType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnTransferType.SelectedValue.ToString() == "T")
            {
                divCheque.Visible = false;
            }
            else if (rbtnTransferType.SelectedValue.ToString() == "C")
            {
                divCheque.Visible = true;
            }
            DdlCRDR.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event Here

    protected void LnkVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtAccNo.Text != "")
            {
                string custno = ViewState["CRCUSTNO"].ToString();
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

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {


            string AC, CRAC, CN, CD;
            AC = CRAC = CN = CD = "";

            //If Cheque Number is blank Then Set it to Zero
            if (TxtChequeNo.Text.Trim().ToString() == "")
            {
                CN = "0";
            }
            else
            {
                CN = TxtChequeNo.Text.Trim().ToString();
            }

            //If Cheque Date is blank Then Set it to Default Value 01/01/1900
            if (TxtChequeDate.Text.Trim().ToString() == "")
            {
                CD = "1900-01-01";
            }
            else
            {
                CD = TxtChequeDate.Text.Trim().ToString();
                string[] cd1 = CD.Split('/');
                CD = cd1[2].ToString() + '-' + cd1[1].ToString() + '-' + cd1[0].ToString();
            }

            //If Account Number is blank Then Set Acc No and Cust No to Zero
            if (TxtAccNo.Text.Trim().ToString() == "")
            {
                AC = "0";
            }
            else
            {
                AC = TxtAccNo.Text.Trim().ToString();
            }

            //Check Amount is grater than zero or not
            if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) > 0.00)
            {
                if (ViewState["GlCode"].ToString() == "3" && DdlCRDR.SelectedValue == "1")
                {
                    //Added for loan case
                    DataTable dt = new DataTable();
                    dt = MV.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                    TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));

                    if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) < TotalAmt)
                    {
                        TotalDrAmt = Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString());

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Acc_Status"].ToString() == "3")
                            {
                                lblMessage.Text = "Account is already closed...!!";
                                ModalPopup.Show(this.Page);
                                return;
                            }
                            else if (dt.Rows[0]["Acc_Status"].ToString() == "9")
                            {
                                lblMessage.Text = "Close account in loan Installment...!!";
                                ModalPopup.Show(this.Page);
                                return;
                            }
                            else if (dt.Rows[0]["Acc_Status"].ToString() == "1")
                            {
                                if (dt.Rows[0]["IntCalType"].ToString() == "1" || dt.Rows[0]["IntCalType"].ToString() == "2")
                                {
                                    resultout = 1;
                                    //  For Insurance Charge
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["InsChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                {
                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                {
                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Bank Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["BankChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                {
                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                {
                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Other Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["OtherChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                                {
                                                    // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", dt.Rows[0]["OtherChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                                {
                                                    // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Sur Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["SurChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                                {
                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", dt.Rows[0]["SurChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                                {
                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    // For Court Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["CourtChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                {
                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                {
                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Service Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["ServiceChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                {
                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                {
                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Notice Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()))
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["NoticeChrg"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                {
                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                {
                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Interest Receivable
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()))
                                        {
                                            if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                            {
                                                // Interest Received credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                    {
                                                        // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                            }
                                            else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                            {
                                                // Interest Received credit to GL 3
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                            {
                                                // Interest Received credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                    {
                                                        // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                            }
                                            else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                            {
                                                // Interest Received credit to GL 3
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Penal Charge
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())))
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Charge Credit To GL 12
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                {
                                                    //Penal Interest Debit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //Penal Charge Contra
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal chrg Applied Debit To GL 12
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "0", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //Penal chrg Applied Credit to GL 100
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "12", "TR_INT", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                        {
                                            //Penal Charge Credit To GL 12
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                {
                                                    //Penal Interest Debit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //Penal Charge Contra
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal chrg Applied Debit To GL 12
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "12", "TR_INT", "0", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //Penal chrg Applied Credit to GL 100
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "12", "TR_INT", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Interest
                                    if (resultout > 0)
                                    {
                                        if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Credit to GL 11
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Credit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Applied Debit To GL 11
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //interest Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                            {
                                                //interest Credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //Current Interest Credit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Applied Debit To GL 11
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //interest Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = 0;
                                            }
                                        }
                                        else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Received Credit to GL 3
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }

                                                //  Added As Per ambika mam Instruction 22-06-2017
                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Applied Debit To GL 3
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //interest Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                            {
                                                //interest Received Credit to GL 3
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                //  Added As Per ambika mam Instruction 22-06-2017
                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Applied Debit To GL 3
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //interest Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = 0;
                                            }
                                        }
                                    }

                                    //Principle O/S Credit To Specific GL (e.g 3)
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()))
                                    {
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, dt.Rows[0]["Principle"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                            {
                                                //Current Principle Debit To 1 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                            {
                                                //Current Principle Credit To 1 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                            {
                                                //Current Principle Debit To 1 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                            {
                                                //Current Principle Credit To 1 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        TotalDrAmt = 0;
                                    }
                                }
                                else if (dt.Rows[0]["IntCalType"].ToString() == "3")
                                {
                                    //Principle O/S Credit To Specific GL (e.g 3)
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "7", "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Debit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }

                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Credit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }

                                        //TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                                        PrincAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) +
                                                   Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) +
                                                   Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) +
                                                   Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) +
                                                   Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))) < 0 ? 0 :
                                                   Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) +
                                                   Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) +
                                                   Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) +
                                                   Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) +
                                                   Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()))));
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - PrincAmt);
                                    }

                                    //SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                                    //ViewState["SetNo1"] = SetNo1.ToString();

                                    //Principle O/S Debit To Specific GL (e.g 3) And Credit to interest GL (e.g 11)
                                    if (resultout > 0)
                                    {
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "7", "TR", "1", "PAYDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        //  For Insurance Charge
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["InsChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                    {
                                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                    {
                                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Bank Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["BankChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                    {
                                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                    {
                                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Other Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["OtherChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                                    {
                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", dt.Rows[0]["OtherChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                                    {
                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Sur Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["SurChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                                    {
                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", dt.Rows[0]["SurChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                                    {
                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        // For Court Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["CourtChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                    {
                                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                    {
                                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Service Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["ServiceChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                    {
                                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                    {
                                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Notice Charges
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()))
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["NoticeChrg"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                    {
                                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                    {
                                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Interest Receivable
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()))
                                            {
                                                // Interest Received credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                    {
                                                        // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                            }
                                            else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                // Interest Received credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                    {
                                                        // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Penal Charge
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())))
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Charge Credit To GL 12
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                    {
                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    //Penal Charge Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal chrg Applied Debit To GL 12
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "1", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //Penal chrg Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "12", "TR_INT", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                            {
                                                //Penal Charge Credit To GL 12
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                    {
                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    //Penal Charge Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal chrg Applied Debit To GL 12
                                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "12", "TR_INT", "1", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                        if (resultout > 0)
                                                        {
                                                            //Penal chrg Applied Credit to GL 100
                                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "12", "TR_INT", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Interest
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Credit to GL 11
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Credit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                            {
                                                //interest Credit to GL 11
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                if (resultout > 0)
                                                {
                                                    if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                                    {
                                                        //Current Interest Credit To 2 In AVS_LnTrx
                                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                    }
                                                }

                                                TotalDrAmt = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Excess amount of receipt than total payable amount " + Convert.ToDouble(TotalAmt.ToString()).ToString() + "...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    //end added for loan
                }
                else
                {
                    //Insert Data into Temporary Table (Avs_TempMultiTransfer) in Database here
                    if (rbtnTransferType.SelectedValue.ToString() == "T")
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "By Transfer", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                    else if (rbtnTransferType.SelectedValue.ToString() == "C")
                    {
                        ST = MV.CheckGlGroup(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());
                        if (String.IsNullOrEmpty(ST))
                        {
                            if (DdlCRDR.SelectedValue == "1")
                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "5", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            if (DdlCRDR.SelectedValue == "2")
                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "6", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                        else if (ST.Trim().ToString() == "CBB")
                        {
                            if (DdlCRDR.SelectedValue == "1")
                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "6", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            else if (DdlCRDR.SelectedValue == "2")
                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "5", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }

                if (resultout > 0)
                {
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "Enter Amount First...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (resultout > 0)
            {
                DdlCRDR.SelectedValue = "1";
                Getinfo();
                BindGrid();
                ClearText();
                return;
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
            ViewState["AMTVAL"] = ViewState["AMTVAL"] == null ? "0" : ViewState["AMTVAL"].ToString();

            if (ViewState["AMTVAL"].ToString() == "Y")//Added by Abhihsek 09-02-2018
            {
                double DR = Getinfo_DP();
                if (Convert.ToDouble(ViewState["FDAMT"].ToString()) != Convert.ToDouble(DR))
                {
                    WebMsgBox.Show("Deposit Amount not matched....!", this.Page);
                    TxtAmount.Text = "";
                    TxtAmount.Focus();
                    return;
                }

            }


            string PAYMAST = "MULTITRANSFER";
            RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
            ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();

            if (Convert.ToDouble(TxtDiff.Text.Trim().ToString() == "" ? "0" : TxtDiff.Text.Trim().ToString()) == 0.00)
            {
                //Get All Transaction From Temporary Table For First Set Number
                DT = new DataTable();
                DT = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DT.Rows.Count > 0)
                {
                    //Generate First Set Number Here
                    ST = "";
                    ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), DT.Rows[i]["TRXTYPE"].ToString(), DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    DT = new DataTable();
                    DT = MV.GetLnTrxTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), ST, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                        if (resultout > 0 && DT.Rows[i]["HeadDesc"].ToString() == "2" && DT.Rows[i]["TrxType"].ToString() == "2")
                        {
                            string IntApp = LI.GetIntApp(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString());
                            if (Convert.ToDouble(IntApp.ToString()) == 1)
                            {
                                resultout = LI.UpdateLastIntDate(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                            }
                        }
                    }
                }

                //Get All Transaction From Temporary Table For Second Set Number
                DT = new DataTable();
                DT = MV.GetTransDetails1(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DT.Rows.Count > 0)
                {
                    //Generate Second Set Number Here
                    SetNo = "";
                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), DT.Rows[i]["TRXTYPE"].ToString(), DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), SetNo.ToString(), DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    DT = new DataTable();
                    DT = MV.GetLnTrxTransDetails1(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                    }
                }

                ClearAll();
                //Delete All Data From Temporary Table Here
                MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                grdvoucher.Visible = false;

                if (resultout > 0)
                {
                    lblMessage.Text = "Transfer Seccessfully With Set No : '" + ST.ToString() + "'...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _post_" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    btnPost.Enabled = false;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Amount Difference in Credit and Debit Transaction...!!";
                ModalPopup.Show(this.Page);
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
        try
        {
            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            //Delete All Data From Temporary Table Here
            MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
            return;
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
            string id = objlink.CommandArgument;

            resultout = MV.DeleteSingleRecTable(id, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (resultout > 0)
            {
                lblMessage.Text = "Record Deleted Successfully...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _Del_" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }

            BindGrid();
            Getinfo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function


    public double Getinfo_DP() // Added by Abhishek on 09-02-2018
    {
        double CR = 0, DR = 0;
        try
        {
            DataTable DT = new DataTable();
            DT = MV.GetCRDR(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtCRBAL.Text = DT.Rows[0]["CREDIT"].ToString();
                TxtDRBAL.Text = DT.Rows[0]["DEBIT"].ToString();
                CR = DR = 0;
                CR = Convert.ToDouble(TxtCRBAL.Text);
                DR = Convert.ToDouble(TxtDRBAL.Text);

            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(DR);
    }

    public void Getinfo()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = MV.GetCRDR(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtCRBAL.Text = DT.Rows[0]["CREDIT"].ToString();
                TxtDRBAL.Text = DT.Rows[0]["DEBIT"].ToString();
                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(TxtCRBAL.Text);
                DR = Convert.ToDouble(TxtDRBAL.Text);

                TxtDiff.Text = (CR - DR).ToString();

                if (CR == DR)
                {
                    btnPost.Enabled = true;
                    btnPost.Focus();
                }
                else
                {
                    btnPost.Enabled = false;
                    TxtPtype.Focus();
                }
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
            int RC = MV.Getinfotable(grdvoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void NewWindowsVerify(string url)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearText()
    {
        try
        {
            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAccNo.Text = "";
            TxtCustName.Text = "";
            txtCustNo.Text = "";

            TxtBalance.Text = "";
            TxtTotalBal.Text = "";

            TxtChequeNo.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();

            TxtNarration.Text = "";
            TxtAmount.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAll()
    {
        try
        {
            DdlCRDR.SelectedIndex = 0;
            TxtCRBAL.Text = "";
            TxtDRBAL.Text = "";
            TxtDiff.Text = "";

            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAccNo.Text = "";
            TxtCustName.Text = "";

            TxtBalance.Text = "";
            TxtTotalBal.Text = "";

            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";

            TxtNarration.Text = "";
            TxtAmount.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = "";
        try
        {
            PanNo = CR.GetPanNo(BRCD, CustNo);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PanNo;
    }

    public void Photo_Sign()
    {
        try
        {
            string FileName = "";
            DataTable dt = CC.ShowIMAGE(txtCustNo.Text, Session["BRCD"].ToString(), TxtAccNo.Text);
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

    #endregion
    //ADDED BY ANKITA 08/02/2018 FOR UPDATE MOBILE NUMBER WHILE TRANSACTION
    protected void BtnMobUpld_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = CC.getContct(txtCustNo.Text);
            if (dt.Rows.Count > 0)
            {
                TxtCustno1.Text = txtCustNo.Text;
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
            resultout = CC.insertContct(TxtCustno1.Text, TxtBrcd1.Text, TxtMob1.Text == "" ? "0" : TxtMob1.Text, TxtMob2.Text == "" ? "0" : TxtMob2.Text, Session["MID"].ToString());
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

    public void getMobile()
    {
        try
        {
            DT = CC.getMobiles(txtCustNo.Text);
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
    protected void grdMultiRct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdMultiRct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
             grdMultiRct.PageIndex = e.NewPageIndex;
            BindGridRec();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDens_Click(object sender, EventArgs e)
    {

    }
   
    public void BindGridRec()
    {
        try
        {
            MV.Getinfotable(grdMultiRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "MULTITRANSFER");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
         LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            string dens = id.ToString();
            ViewState["SETNO"] = dens.ToString();
            string redirectURL = "FrmRView.aspx?BranchID=" + Session["BRCD"].ToString() + "&FDate=" + Session["ENTRYDATE"].ToString() + "&SetNo=" + ViewState["SETNO"] + "&rptname=RptVoucherPrintingCRDR.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
       
    }
}