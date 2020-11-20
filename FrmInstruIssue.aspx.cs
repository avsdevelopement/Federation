using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInstruIssue : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized Trans = new ClsAuthorized();
    ClsInstruIssue II = new ClsInstruIssue();
    ClsAccountSTS AST = new ClsAccountSTS();
    DbConnection Conn = new DbConnection();
    ClsOpenClose OC = new ClsOpenClose();
    ClsCommon CMN = new ClsCommon();
    DataTable GST = new DataTable();
    DataTable DT = new DataTable();
    string sResult = "", Restr = "";
    int ResInt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                //  Added by amol on 03/10/2018 for log details
                LogDetails();

                TxtBrcd.Text = Session["BRCD"].ToString();
                TxtBrName.Text = AST.GetBranchName(TxtBrcd.Text.ToString());
                BD.BindInsType(DdlInstrType, "BindInsType");

                Div_1.Visible = true;
                Div_PA.Visible = true;
                Div_View.Visible = false;

                DdlInstrType.Focus();
                ViewState["FL"] = "AD";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DdlInstrType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtAplhaCd.Text = DdlInstrType.SelectedValue;
            TxtNoOfBooks.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtNoOfBooks_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBookSize.SelectedValue.ToString() != "0")
                InoCalc();

            ddlBookSize.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBookSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            InoCalc();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtFDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTDate.Text != "")
            {
                if (Convert.ToDateTime(Conn.ConvertDate(TxtFDate.Text.ToString())) > Convert.ToDateTime(Conn.ConvertDate(TxtTDate.Text.ToString())))
                {
                    WebMsgBox.Show("From Date cannot be greater than To Date....!", this.Page);
                    TxtFDate.Text = "";
                    TxtFDate.Focus();
                }
                else
                {
                    TxtTDate.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFDate.Text != "")
            {
                if (Convert.ToDateTime(Conn.ConvertDate(TxtTDate.Text.ToString())) < Convert.ToDateTime(Conn.ConvertDate(TxtFDate.Text.ToString())))
                {
                    WebMsgBox.Show("To Date cannot be greater than From Date....!", this.Page);
                    TxtTDate.Text = "";
                    TxtTDate.Focus();
                }
                else
                {
                    btnSubmit.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtIssueDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(Conn.ConvertDate(TxtIssueDt.Text.ToString())) > Convert.ToDateTime(Conn.ConvertDate(Session["EntryDate"].ToString())))
            {
                WebMsgBox.Show("Issue date cannot be greater than working date....!", this.Page);
                TxtIssueDt.Text = "";
                TxtIssueDt.Focus();
            }
            else
            {
                TxtProcode.Focus();
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
            if (TxtBrcd.Text.ToString() != "")
            {
                TxtProName.Text = AST.GetAccType(TxtProcode.Text.ToString(), TxtBrcd.Text.ToString());

                string[] GL = BD.GetAccTypeGL(TxtProcode.Text, TxtBrcd.Text).Split('_');
                TxtProName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                AutoAccname.ContextKey = TxtBrcd.Text.ToString() + "_" + TxtProcode.Text.ToString() + "_" + ViewState["GL"].ToString();

                if (Convert.ToDouble(ViewState["GL"].ToString()) == Convert.ToDouble(TxtAplhaCd.Text.ToString()))
                    TxtAccno.Focus();
                else
                {
                    //TxtProcode.Text = "";
                    TxtProName.Text = "";
                    TxtProcode.Focus();
                    WebMsgBox.Show("Enter correct product code forst ...!!", this.Page);
                    return;
                }
            }
            else
            {
                //TxtProcode.Text = "";
                TxtBrcd.Focus();
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                return;
            }

        }
        catch (Exception Ex)
        {
            TxtAccno.Focus();
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrcd.Text != "")
            {
                string[] CT = TxtProName.Text.ToString().Split('_');
                if (CT.Length > 0)
                {
                    TxtProName.Text = CT[0].ToString();
                    TxtProcode.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtProcode.Text.ToString(), TxtBrcd.Text.ToString()).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = TxtBrcd.Text.ToString() + "_" + TxtProcode.Text.ToString() + "_" + ViewState["GL"].ToString();

                    if (Convert.ToDouble(ViewState["GL"].ToString()) == Convert.ToDouble(TxtAplhaCd.Text.ToString()))
                        TxtAccno.Focus();
                    else
                    {
                        //TxtProcode.Text = "";
                        TxtProName.Text = "";
                        TxtProcode.Focus();
                        WebMsgBox.Show("Enter correct product code forst ...!!", this.Page);
                        return;
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtProName.Text = "";
                TxtBrcd.Focus();
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
            if (ViewState["FL"].ToString() == "AD")
            {
                Accno();
            }
            else if ((ViewState["FL"].ToString() == "AT") || (ViewState["FL"].ToString() == "DL"))
            {
                BindGridData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtAccName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                TxtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                if (ViewState["FL"].ToString() == "AD")
                {
                    Accno();
                }
                else if ((ViewState["FL"].ToString() == "AT") || (ViewState["FL"].ToString() == "DL"))
                {
                    BindGridData();
                }
            }
            else
            {
                WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void InoCalc()
    {
        try
        {
            if (ddlBookSize.SelectedValue == "0")
            {
                ddlBookSize.SelectedValue = "0";
                ddlBookSize.Focus();
                WebMsgBox.Show("Invalid Book size...!", this.Page);
                return;
            }
            if (TxtNoOfBooks.Text == "")
            {
                TxtNoOfBooks.Text = "";
                TxtNoOfBooks.Focus();
                WebMsgBox.Show("Invalid No. of books size...!", this.Page);
                return;
            }
            else
            {
                string SIno = II.GetMaxIno("MaxInstruNo", Session["BRCD"].ToString());
                if (SIno != null)
                {
                    if (Convert.ToDouble(SIno.ToString()) > 0)
                    {
                        TxtStartInstrNo.Text = SIno.ToString();

                        if ((Convert.ToDouble(TxtNoOfBooks.Text.ToString()) > 0) && (Convert.ToDouble(ddlBookSize.SelectedValue) > 0))
                            TxtEndInstrNo.Text = (Convert.ToInt32(Convert.ToInt32(TxtStartInstrNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString()))) - 1).ToString();
                        else
                            TxtEndInstrNo.Text = TxtStartInstrNo.Text.ToString();

                        if ((II.CheckStockExists(Session["BRCD"].ToString(), TxtStartInstrNo.Text.ToString()) == "0") || (II.CheckStockExists(Session["BRCD"].ToString(), TxtEndInstrNo.Text.ToString()) == "0"))
                        {
                            if (II.CheckStockExists(Session["BRCD"].ToString(), TxtStartInstrNo.Text.ToString()) == "0")
                                TxtStartInstrNo.Text = "";

                            if (II.CheckStockExists(Session["BRCD"].ToString(), TxtEndInstrNo.Text.ToString()) == "0")
                                TxtEndInstrNo.Text = "";

                            TxtEndInstrNo.Focus();
                            WebMsgBox.Show("Stock already issued ...!!", this.Page);
                            return;
                        }

                        ChargesDetails();
                        TxtSpecialSr.Focus();
                    }
                    else
                    {
                        TxtStartInstrNo.Text = "0";
                        TxtEndInstrNo.Text = "0";
                        WebMsgBox.Show("Stock Not available ...!!", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Accno()
    {
        DT = new DataTable();
        try
        {
            if (TxtBrcd.Text.ToString() != "")
            {
                DT = II.GetAccDetails(TxtBrcd.Text.ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString());
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() != "1003")
                    {
                        TxtAccno.Text = "";
                        TxtAccName.Text = "";
                        TxtAccno.Focus();
                        WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        if ((DT.Rows[0]["Acc_Status"].ToString() == "1") || (DT.Rows[0]["Acc_Status"].ToString() == "2"))
                        {
                            TxtAccName.Text = DT.Rows[0]["CustName"].ToString();
                            //  If account type is staff then charges not apply
                            if (DT.Rows[0]["Acc_Type"].ToString() != "2")
                                ChargesDetails();

                            TxtRemark.Focus();
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "3")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is closed ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "4")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is Loan lien Marked ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "5")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is credit freezed ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "6")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is Debit freezed ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "7")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is total freezed ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "8")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is Dormant / In Operative...!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "9")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is suit filed ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "10")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is call back ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "11")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account is NPA ...!!", this.Page);
                            return;
                        }
                        else if (DT.Rows[0]["Acc_Status"].ToString() == "12")
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account have interest suspended ...!!", this.Page);
                            return;
                        }
                        else
                        {
                            TxtAccno.Text = "";
                            TxtAccName.Text = "";
                            TxtAccno.Focus();
                            WebMsgBox.Show("Account has invalid account status ...!!", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    TxtAccno.Text = "";
                    TxtAccName.Text = "";
                    TxtAccno.Focus();
                    WebMsgBox.Show("Account details not found ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtAccno.Text = "";
                TxtAccName.Text = "";
                TxtBrcd.Focus();
                WebMsgBox.Show("Enter Branch Code First ...!!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ChargesDetails()
    {
        try
        {
            double PendingLeave = 0, BookSize = 0;

            DT = II.LeaveDetails(TxtProcode.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                ViewState["ChrgGlCode"] = DT.Rows[0]["GlCode"].ToString();
                ViewState["ChrgSubGlCode"] = DT.Rows[0]["SubGlCode"].ToString();
                txtFreeLeave.Text = DT.Rows[0]["FreeLeave"].ToString();
                txtPerCharge.Text = DT.Rows[0]["Charges"].ToString();
            }

            txtUsedLeave.Text = II.LeaveCount(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString()).ToString();
            txtUsedLeave.Text = txtUsedLeave.Text.ToString() == "" ? "0" : txtUsedLeave.Text.ToString();
            txtFreeLeave.Text = txtFreeLeave.Text.ToString() == "" ? "0" : txtFreeLeave.Text.ToString();
            txtPerCharge.Text = txtPerCharge.Text.ToString() == "" ? "0" : txtPerCharge.Text.ToString();

            if ((Convert.ToDouble(ddlBookSize.SelectedValue) > 0) && (Convert.ToDouble(txtPerCharge.Text.ToString()) > 0))
            {
                if (Convert.ToDouble(Convert.ToDouble(txtFreeLeave.Text.ToString()) - Convert.ToDouble(txtUsedLeave.Text.ToString())) > 0)
                    PendingLeave = Convert.ToDouble(Convert.ToDouble(txtFreeLeave.Text.ToString()) - Convert.ToDouble(txtUsedLeave.Text.ToString()));

                if (Convert.ToDouble((Convert.ToDouble(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString())) - PendingLeave) > 0)
                    BookSize = Convert.ToDouble((Convert.ToDouble(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString())) - PendingLeave);

                txtCharges.Text = Math.Round(Convert.ToDouble(BookSize * Convert.ToDouble(txtPerCharge.Text.ToString())), 2).ToString();
                txtCGSTChrg.Text = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtCharges.Text.ToString()) * 18) / 118) / 2), 2).ToString();
                txtSGSTChrg.Text = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtCharges.Text.ToString()) * 18) / 118) / 2), 2).ToString();
                txtTotalChrg.Text = Convert.ToDouble((Convert.ToDouble(txtCharges.Text.ToString()) - Convert.ToDouble(txtCGSTChrg.Text.ToString())) - Convert.ToDouble(txtSGSTChrg.Text.ToString())).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGridData()
    {
        try
        {
            DT = II.GetGridData(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                Div_1.Visible = true;
                GridShow.DataSource = DT;
                GridShow.DataBind();

                DdlInstrType.SelectedValue = DT.Rows[0]["InsType"].ToString();
                TxtProcode.Text = DT.Rows[0]["SubGlCode"].ToString();
                TxtProName.Text = DT.Rows[0]["GlName"].ToString();
                TxtAccno.Text = DT.Rows[0]["AccNo"].ToString();
                TxtAccName.Text = DT.Rows[0]["CustName"].ToString();
                TxtAplhaCd.Text = DT.Rows[0]["InsType"].ToString();
                TxtNoOfBooks.Text = DT.Rows[0]["NoOfBook"].ToString();
                ddlBookSize.SelectedValue = DT.Rows[0]["BookSize"].ToString();
                TxtStartInstrNo.Text = DT.Rows[0]["SInstNo"].ToString();
                TxtEndInstrNo.Text = DT.Rows[0]["EInstNo"].ToString();
                TxtIssueDt.Text = DT.Rows[0]["IssueDate"].ToString();
                TxtSpecialSr.Text = DT.Rows[0]["InsType"].ToString();

                btnSubmit.Focus();
            }
            else
            {
                Div_1.Visible = false;
                GridShow.DataSource = null;
                GridShow.DataBind();

                DdlInstrType.SelectedValue = "0";
                TxtAplhaCd.Text = "";
                TxtNoOfBooks.Text = "";
                ddlBookSize.SelectedValue = "0";
                TxtStartInstrNo.Text = "";
                TxtEndInstrNo.Text = "";
                TxtSpecialSr.Text = "";
                TxtIssueDt.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ENDN(bool TF)
    {
        try
        {
            DdlInstrType.Enabled = TF;
            TxtNoOfBooks.Enabled = TF;
            ddlBookSize.Enabled = TF;
            //TxtStartInstrNo.Enabled = TF;
            //TxtEndInstrNo.Enabled = TF;

            TxtSpecialSr.Enabled = TF;
            TxtIssueDt.Enabled = TF;
            TxtRemark.Enabled = TF;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Clear()
    {
        GridShow.DataSource = null;
        GridShow.DataBind();

        DdlInstrType.SelectedValue = "0";
        TxtAplhaCd.Text = "";
        TxtNoOfBooks.Text = "";
        ddlBookSize.SelectedValue = "0";
        TxtStartInstrNo.Text = "";
        TxtEndInstrNo.Text = "";
        TxtSpecialSr.Text = "";
        TxtIssueDt.Text = "";
        //TxtProcode.Text = "";
        TxtProName.Text = "";
        TxtAccno.Text = "";
        TxtAccName.Text = "";
        TxtRemark.Text = "";

        //added by amol on 03/10/2018
        txtFreeLeave.Text = "";
        txtUsedLeave.Text = "";
        txtPerCharge.Text = "";
        txtCharges.Text = "";
        txtCGSTChrg.Text = "";
        txtSGSTChrg.Text = "";
        txtTotalChrg.Text = "";

        DdlInstrType.Focus();
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            ViewState["FL"] = "AD";
            btnSubmit.Text = "Submit";
            Div_1.Visible = true;
            Div_PA.Visible = true;
            Div_View.Visible = false;
            GridShow.Columns[1].Visible = false;
            GridShow.Columns[0].Visible = false;
            ENDN(true);
            Chequedetails.Visible = false;
            GridCheque.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkAutho_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            ViewState["FL"] = "AT";
            btnSubmit.Text = "Authorize";
            Div_1.Visible = false;
            Div_PA.Visible = true;
            Div_View.Visible = false;
            btnSubmit.Visible = false;
            GridShow.Columns[1].Visible = false;
            GridShow.Columns[0].Visible = true;
            ENDN(false);
            TxtProcode.Focus();
            Chequedetails.Visible = false;
            GridCheque.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            ViewState["FL"] = "DL";
            btnSubmit.Text = "Delete";
            Div_View.Visible = false;
            Div_1.Visible = false;
            Div_PA.Visible = true;
            btnSubmit.Visible = false;
            GridShow.Columns[1].Visible = true;
            GridShow.Columns[0].Visible = false;
            ENDN(false);
            TxtProcode.Focus();
            Chequedetails.Visible = false;
            GridCheque.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            ViewState["FL"] = "VW";
            btnSubmit.Text = "View";
            Div_1.Visible = false;
            Div_PA.Visible = false;
            Div_View.Visible = true;
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();
            GridShow.Columns[1].Visible = false;
            GridShow.Columns[0].Visible = false;
            ENDN(false);
            TxtTDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SetNo = "", RefId = "";
            string Balance = "0", GlCode = "";

            if (ViewState["FL"].ToString() == "AD")
            {
                string[] TD = Session["EntryDate"].ToString().Split('/');
                if (ViewState["GL"].ToString() == "3")
                    Balance = OC.GetOpenClose("LOANCLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), "OPT", "0").ToString();
                else
                    Balance = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), "OPT", "0").ToString();

                txtCharges.Text = txtCharges.Text.ToString() == "" ? "0" : txtCharges.Text.ToString();
                if (Convert.ToDouble(Balance) >= Convert.ToDouble(txtCharges.Text.ToString()))
                {
                    GST = II.GstDetails(Session["BRCD"].ToString());
                    if (GST.Rows.Count > 0)
                    {
                        ResInt = II.Add("Add", TxtBrcd.Text.Trim(), DdlInstrType.SelectedValue.ToString(), TxtStartInstrNo.Text.ToString(), ddlBookSize.SelectedValue, TxtNoOfBooks.Text.ToString(), TxtEndInstrNo.Text.ToString(), TxtIssueDt.Text.ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString(), TxtAplhaCd.Text.ToString(), TxtRemark.Text.ToString(), Session["MID"].ToString(), Session["Entrydate"].ToString());

                        if (ResInt != null && ResInt > 0)
                        {
                            if (Convert.ToDouble(txtCharges.Text.ToString()) > 0)
                            {
                                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                                RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                                ViewState["RID"] = (Convert.ToInt32(RefId) + 1).ToString();

                                //  For Total Debit Amount Voucher
                                ResInt = Trans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text.ToString(),
                                    TxtAccno.Text.ToString(), TxtRemark.Text.ToString(), "", (txtCharges.Text.ToString() == "" ? "0" : txtCharges.Text.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001", "",
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "", "0", "0", ViewState["RID"].ToString(), "0", "0");

                                if (ResInt > 0)
                                {
                                    //  For CGST charges
                                    if ((ResInt > 0) && (Convert.ToDouble((txtCGSTChrg.Text.ToString() == "" ? "0" : txtCGSTChrg.Text.ToString())) > 0))
                                    {
                                        GlCode = II.GetGlCode(Session["BRCD"].ToString(), GST.Rows[0]["CGSTPrdCd"].ToString());
                                        ResInt = Trans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), GST.Rows[0]["CGSTPrdCd"].ToString(),
                                             "0", TxtRemark.Text.ToString(), "", txtCGSTChrg.Text.ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(),
                                             Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "", "", "", ViewState["RID"].ToString(), "0", "0");
                                    }

                                    //  For SGST charges
                                    if ((ResInt > 0) && (Convert.ToDouble((txtSGSTChrg.Text.ToString() == "" ? "0" : txtSGSTChrg.Text.ToString())) > 0))
                                    {
                                        GlCode = II.GetGlCode(Session["BRCD"].ToString(), GST.Rows[0]["SGSTPrdCd"].ToString());
                                        ResInt = Trans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), GST.Rows[0]["SGSTPrdCd"].ToString(),
                                             "0", TxtRemark.Text.ToString(), "", txtSGSTChrg.Text.ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(),
                                             Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "", "", "", ViewState["RID"].ToString(), "0", "0");
                                    }

                                    //  For charges credit
                                    if ((ResInt > 0) && (Convert.ToDouble((txtTotalChrg.Text.ToString() == "" ? "0" : txtTotalChrg.Text.ToString())) > 0))
                                    {
                                        ResInt = Trans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["ChrgGlCode"].ToString(), ViewState["ChrgSubGlCode"].ToString(),
                                             "0", TxtRemark.Text.ToString(), "", txtTotalChrg.Text.ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(),
                                             Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "", "", "", ViewState["RID"].ToString(), "0", "0");
                                    }
                                }
                            }

                            if (Convert.ToDouble(SetNo == "" ? "0" : SetNo) > 0)
                            {
                                Clear();
                                WebMsgBox.Show("Sucessfully issued with setno : " + SetNo + " ...!", this.Page);
                            }
                            else
                            {
                                Clear();
                                WebMsgBox.Show("Sucessfully issued ...!!", this.Page);
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Operation failed ...!!", this.Page);
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Check GST master table details ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    TxtAccno.Focus();
                    WebMsgBox.Show("Insufficient account balance ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["Mid"] = Info[0].ToString();

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                ResInt = II.AuthoCHQIssued(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString(), Session["MID"].ToString());
                if (ResInt > 0)
                {
                    Clear();
                    WebMsgBox.Show("Successfully authorized ...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Same user is restricted to authorize ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["Mid"] = Info[0].ToString();

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                ResInt = II.CancelCHQIssued(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString(), Session["MID"].ToString());
                if (ResInt > 0)
                {
                    Clear();
                    WebMsgBox.Show("Successfully canceled ...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Same user is restricted to cancel ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Chequedetails.Visible = true;
            Div_1.Visible = false;
            Div_PA.Visible = false;
            Div_View.Visible = false;
            divbutton.Visible = false;
            divCharges.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPrdC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrcd.Text.ToString() != "")
            {
                TxtPNameC.Text = AST.GetAccType(TxtPrdC.Text.ToString(), TxtBrcd.Text.ToString());

                string[] GL = BD.GetAccTypeGL(TxtPrdC.Text, TxtBrcd.Text).Split('_');
                TxtPNameC.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                autopnamec.ContextKey = TxtBrcd.Text.ToString() + "_" + TxtPrdC.Text.ToString() + "_" + ViewState["GL"].ToString();


            }
            else
            {
                TxtPrdC.Text = "";
                TxtBrcd.Focus();
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPNameC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrcd.Text != "")
            {
                string[] CT = TxtPNameC.Text.ToString().Split('_');
                if (CT.Length > 0)
                {
                    TxtPNameC.Text = CT[0].ToString();
                    TxtPrdC.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtPrdC.Text.ToString(), TxtBrcd.Text.ToString()).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    autopnamec.ContextKey = TxtBrcd.Text.ToString() + "_" + TxtPrdC.Text.ToString() + "_" + ViewState["GL"].ToString();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtPNameC.Text = "";
                TxtBrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAcC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AccnoCP();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAcNameC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtAcNameC.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                TxtAcC.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                AccnoCP();
            }
            else
            {
                WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnSubC_Click(object sender, EventArgs e)
    {
        try
        {
            GridCheque.Visible = true;
            DT = II.GetCheque(Session["BRCD"].ToString(), TxtPrdC.Text.ToString(), TxtAcC.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                GrdChequeP.DataSource = DT;
                GrdChequeP.DataBind();
                DT = II.GetChequeSS(Session["BRCD"].ToString(), TxtPrdC.Text.ToString(), TxtAcC.Text.ToString());
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnExitC_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmInstruIssue.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkCheqPrint_Click(object sender, EventArgs e)
    {
        string Dex1 = "";

        if (CHK_Cover_STD.Checked == true)
        {
            Dex1 = "Y";
        }
        else if (CHK_Cover_STD.Checked == false)
        {
            Dex1 = "N";
        }

        try
        {

            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Brcd"] = ARR[0].ToString();
            ViewState["subglcode"] = ARR[1].ToString();
            ViewState["accno"] = ARR[2].ToString();

            if (Dex1 == "N")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + ViewState["Brcd"].ToString() + "&Prd=" + ViewState["subglcode"].ToString() + "&ACCNO=" + ViewState["accno"].ToString() + "&rptname=RptChequeS.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                //II.Chequeprintstatus(ViewState["Brcd"].ToString(), ViewState["subglcode"].ToString(), ViewState["accno"].ToString());
            }
            if (Dex1 == "Y")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + ViewState["Brcd"].ToString() + "&Prd=" + ViewState["subglcode"].ToString() + "&ACCNO=" + ViewState["accno"].ToString() + "&rptname=RptChequeCP.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void BtnCover_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrcd.Text.Trim().ToString() + "&Prd=" + TxtProcode.Text + "&ACCNO=" + TxtAccno.Text + "&rptname=RptChequeCP.rdlc";
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    public void AccnoCP()
    {
        try
        {
            string AT = "";
            if (TxtBrcd.Text != "")
            {
                AT = BD.Getstage1(TxtAcC.Text.ToString(), TxtBrcd.Text.ToString(), TxtPrdC.Text.ToString());
                if (AT != "1003")
                {
                    WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                    TxtAcC.Text = "";
                }
                else
                {
                    string AC_Status;
                    AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtPrdC.Text.ToString(), TxtAcC.Text.ToString());

                    if (AC_Status == "1" || AC_Status == "2")
                    {
                        DataTable DT = new DataTable();
                        DT = AST.GetCustName(TxtPrdC.Text.ToString(), TxtAcC.Text.ToString(), TxtBrcd.Text.ToString());
                        if (DT.Rows.Count > 0)
                        {
                            TxtAcNameC.Text = DT.Rows[0]["CustName"].ToString();
                        }

                    }
                    else if (AC_Status == "3")
                    {
                        WebMsgBox.Show("Account is closed...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "4")
                    {
                        WebMsgBox.Show("Account is Loan lien Marked...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "5")
                    {
                        WebMsgBox.Show("Account is Credit freezed...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "6")
                    {
                        WebMsgBox.Show("Account is Debit freezed...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "7")
                    {
                        WebMsgBox.Show("Account is Total freezed...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "8")
                    {
                        WebMsgBox.Show("Account is Dormant / In Operative...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "9")
                    {
                        WebMsgBox.Show("Account is  Suit Filed...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "10")
                    {
                        WebMsgBox.Show("Account is  Call back...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "11")
                    {
                        WebMsgBox.Show("Account is  NPA...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                    else if (AC_Status == "12")
                    {
                        WebMsgBox.Show("Account  have Interest Suspended...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();

                    }
                    else
                    {
                        WebMsgBox.Show("Account number invalid...!", this.Page);
                        TxtAcC.Text = "";
                        TxtAcNameC.Text = "";
                        TxtAcC.Focus();
                        return;
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAcC.Text = "";
                TxtBrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //  Added by amol on 03/10/2018 for log details
    public void LogDetails()
    {
        try
        {
            CMN.LogDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "Instrument Issue", "", "", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtStartInstrNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string SIno = TxtStartInstrNo.Text.ToString();
            if (Convert.ToDouble(SIno.ToString()) > 0)
            {
                TxtStartInstrNo.Text = SIno.ToString();

                if ((Convert.ToDouble(TxtNoOfBooks.Text.ToString()) > 0) && (Convert.ToDouble(ddlBookSize.SelectedValue) > 0))
                    TxtEndInstrNo.Text = (Convert.ToInt32(Convert.ToInt32(TxtStartInstrNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString()))) - 1).ToString();
                else
                    TxtEndInstrNo.Text = TxtStartInstrNo.Text.ToString();

                if ((II.CheckStockExists(Session["BRCD"].ToString(), TxtStartInstrNo.Text.ToString()) == "0") || (II.CheckStockExists(Session["BRCD"].ToString(), TxtEndInstrNo.Text.ToString()) == "0"))
                {
                    TxtStartInstrNo.Text = "";
                    TxtEndInstrNo.Text = "";
                    TxtEndInstrNo.Focus();
                    WebMsgBox.Show("Stock already issued ...!!", this.Page);
                    return;
                }
                else if (II.CheckStockExists(Session["BRCD"].ToString(), TxtStartInstrNo.Text.ToString(), TxtEndInstrNo.Text.ToString()) != "0")
                {
                    TxtStartInstrNo.Text = "";
                    TxtEndInstrNo.Text = "";
                    TxtEndInstrNo.Focus();
                    WebMsgBox.Show("Stock already issued ...!!", this.Page);
                    return;
                }
                TxtSpecialSr.Focus();
            }
            else
            {
                TxtStartInstrNo.Text = "0";
                TxtEndInstrNo.Text = "0";
                WebMsgBox.Show("Stock Not available ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCharges_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ChargesDetails();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFreeLeave_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double PendingLeave = 0, BookSize = 0;
            if (Convert.ToDouble(txtFreeLeave.Text.ToString()) >= 0)
            {
                DT = II.LeaveDetails(TxtProcode.Text.ToString());
                if (DT.Rows.Count > 0)
                {
                    ViewState["ChrgGlCode"] = DT.Rows[0]["GlCode"].ToString();
                    ViewState["ChrgSubGlCode"] = DT.Rows[0]["SubGlCode"].ToString();
                    txtPerCharge.Text = DT.Rows[0]["Charges"].ToString();
                }

                txtFreeLeave.Text = txtFreeLeave.Text.ToString();
                txtUsedLeave.Text = II.LeaveCount(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), TxtAccno.Text.ToString()).ToString();
                txtUsedLeave.Text = txtUsedLeave.Text.ToString() == "" ? "0" : txtUsedLeave.Text.ToString();
                txtFreeLeave.Text = txtFreeLeave.Text.ToString() == "" ? "0" : txtFreeLeave.Text.ToString();
                txtPerCharge.Text = txtPerCharge.Text.ToString() == "" ? "0" : txtPerCharge.Text.ToString();

                if ((Convert.ToDouble(ddlBookSize.SelectedValue) > 0) && (Convert.ToDouble(txtPerCharge.Text.ToString()) > 0))
                {
                    if (Convert.ToDouble(Convert.ToDouble(txtFreeLeave.Text.ToString()) - Convert.ToDouble(txtUsedLeave.Text.ToString())) > 0)
                        PendingLeave = Convert.ToDouble(Convert.ToDouble(txtFreeLeave.Text.ToString()) - Convert.ToDouble(txtUsedLeave.Text.ToString()));

                    if (Convert.ToDouble((Convert.ToDouble(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString())) - PendingLeave) > 0)
                        BookSize = Convert.ToDouble((Convert.ToDouble(ddlBookSize.SelectedValue) * Convert.ToDouble(TxtNoOfBooks.Text.ToString())) - PendingLeave);

                    txtCharges.Text = Math.Round(Convert.ToDouble(BookSize * Convert.ToDouble(txtPerCharge.Text.ToString())), 2).ToString();
                    txtCGSTChrg.Text = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtCharges.Text.ToString()) * 18) / 118) / 2), 2).ToString();
                    txtSGSTChrg.Text = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtCharges.Text.ToString()) * 18) / 118) / 2), 2).ToString();
                    txtTotalChrg.Text = Convert.ToDouble((Convert.ToDouble(txtCharges.Text.ToString()) - Convert.ToDouble(txtCGSTChrg.Text.ToString())) - Convert.ToDouble(txtSGSTChrg.Text.ToString())).ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}